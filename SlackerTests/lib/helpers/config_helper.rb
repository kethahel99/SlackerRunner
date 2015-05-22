RSpec.configure do |config|
  config.before(:each) do
    user_name = 'slacker_user'
    user_sid = 'S1ACK3R'
    sql.common.clear_context_info
    sql.common.create_operation_result
    sproc 'bc.User_put', :user_name => user_name, :user_sid => user_sid, :locale_code => 'en-us'
    sproc 'bc.Set_Context_Info', :user_sid => user_sid
  end
end

module ConfigHelper
end