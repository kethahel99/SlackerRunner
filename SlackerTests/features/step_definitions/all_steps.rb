Given /^I have a test$/ do
end

When /^it passes$/ do 
end

When /^it fails$/ do 
end

Then /^I have a passing test$/ do
end

Then /^I have a failing test$/ do
    1.should == 2
end
