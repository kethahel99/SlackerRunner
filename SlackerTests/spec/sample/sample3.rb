# Method "describe" opens up an example group.
describe 'My database' do
  # Simple inline query example.
  it 'can not insert garbage' do
    # Make sure we have at least one system object in the database.
    query("insert fails into sysobjects where xtype = 'S';").count.should > 0
  end

    # Simple inline query example.
  it 'can not use gargabe syntax' do
    # Make sure we have at least one system object in the database.
    query("not good syntax from sysobjects;").count.should > 0
  end
end