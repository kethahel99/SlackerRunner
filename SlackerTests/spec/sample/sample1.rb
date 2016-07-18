# Method "describe" opens up an example group.

describe 'My database' do
  # Simple inline query example.
  it 'contains system tables' do
    # Make sure we have at least one system object in the database.
    expect( query("select * from sysobjects where xtype = 'S';").count ).to be < 0
  end

    # Simple inline query example.
  it 'system tables' do
    # Make sure we have at least one system object in the database.
    expect( query("select * from sysobjects;").count ).to be > 0
  end
end

