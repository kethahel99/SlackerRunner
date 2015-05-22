module MyHelper

  # Construct a select statement with optinal variable declaration
  # Injects a select statement at the end which selects all variables
  def select_vars(vars, sql_script, options = {:declare_vars => true}, &block)
    sql.common.select_vars({:vars => vars, :sql_script => sql_script, :options => options}, &block)
  end
  
  def s_func(func_name, *params)
    raise 's_func called with a block' if block_given?
    sql_value_params = params.map{|param| sql_value(param) }
    query("select #{func_name}(#{sql_value_params.join(', ')}) as value;")[0][:value]
  end

  def t_func(func_name, *params, &block)
    # Extract the optional options at the end of the params list
    opts = {}
    pcount = params.count
    if pcount > 0 && params[pcount - 1].is_a?(Hash)
      opts = params[pcount - 1]
      params.delete_at(pcount - 1)
    end
    
    order_by_clause = opts[:order_by] != nil ? " order by #{opts[:order_by]}" : ""
    
    sql_value_params = params.map{|param| sql_value(param) }
    query "select * from #{func_name}(#{sql_value_params.join(', ')})#{order_by_clause};", &block
  end

  def sproc(sproc_name, params = {}, out_params = {}, &block)
    begin
      resultset = nil
      
      if out_params.count > 0
        resultset = sql.common.sproc :sproc_name => sproc_name, :params => params, :out_params => out_params
      else
       # Bypass the call to the template - speeds up execution of simple sproc calls up to x2
       query "delete from #OperationResult"
       resultset = query "exec #{sproc_name}\n#{sproc_params(params, {})};"        
      end
      
      yield(resultset, []) if block_given?
    rescue ODBC::Error => e
      if e.message == '37000 (50000) [Microsoft][ODBC SQL Server Driver][SQL Server]BEACHCOMBER OPERATION RESULT THROW'
        # Read the contents of temporary table #OperationResult
        op_res = sql.common.query_operation_result
        if block_given?
          yield(nil, op_res)
        else
          raise op_res.inspect
        end
      else
        raise
      end
    end
    resultset
  end
  
  def sql_value(p)
    if p
      case p
      when String then
        # Do not put the %{ } escapes into quotes - this will be replaced by the Slacker's SQL preprocessor
        (p =~ /^%{.*?}$/) != nil ? p :  "'#{p}'"
      else p.to_s
      end
    else
      'NULL'
    end
  end

  def sproc_params(in_params, out_params)
    expanded = []
    
    out_params.each do |key, value|
      expanded.push "@#{key.to_s} = @#{value} out"
    end
    
    in_params.each do |key, value|
      expanded.push "@#{key.to_s} = #{sql_value(value)}"
    end
    
    expanded.count == 0 ? '' : '  ' + expanded.join(",\n  ")
  end

  def create_lookup_type(lookup_type, description, inherited_lookup_type = nil)
    sproc 'bc.Lookup_Type_put',
          :lookup_type => lookup_type,
          :description => description,
          :inherited_lookup_type => inherited_lookup_type
  end
  
  def create_lookup_code(lookup_type, code, description, comments = nil)
    sproc 'bc.Lookup_Code_put',
          :lookup_type => lookup_type,
          :lookup_code => code,
          :description => description,
          :code_order => 0,
          :comments => comments
  end
  
end