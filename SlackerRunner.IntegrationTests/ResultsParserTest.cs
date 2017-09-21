//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//
using Xunit;


namespace SlackerRunner.IntegrationTests
{
  public class ResultsParserTest
  {
    private static string ResultSomeFailed()
    {
      var sb = new StringBuilder();
      sb.AppendLine("Beachcomber ((local))");
      sb.AppendLine("............FFF............");
      sb.AppendLine(" ");
      sb.AppendLine("Finished in 0.11845 seconds");
      sb.AppendLine("27 examples, 3 failure");
      sb.AppendLine("");
      return sb.ToString();
    }


    private static string ResultOnePassed()
    {
      var sb = new StringBuilder();
      sb.AppendLine("Beachcomber ((local))");
      sb.AppendLine("...");
      sb.AppendLine(" ");
      sb.AppendLine("Finished in 0.11845 seconds");
      sb.AppendLine("1 example, 0 failures");
      sb.AppendLine("");
      return sb.ToString();
    }

    private static string ResultAllPassed()
    {
      var sb = new StringBuilder();
      sb.AppendLine("Beachcomber ((local))");
      sb.AppendLine("...");
      sb.AppendLine(" ");
      sb.AppendLine("Finished in 0.11845 seconds");
      sb.AppendLine("3 examples, 0 failures");
      sb.AppendLine("");
      return sb.ToString();
    }

    private static string UnforseenError()
    {
      var sb = new StringBuilder();
      sb.AppendLine("Beachcomber ((local))");
      sb.AppendLine("...");
      sb.AppendLine(" ");
      sb.AppendLine("ODBC::Error: 28000 (18456) [Microsoft][ODBC SQL Server Driver][SQL Server]Login failed for user 'something'.");
      sb.AppendLine("");
      return sb.ToString();
    }

    private static string NoTestNoFailure()
    {
      var sb = new StringBuilder();
      sb.AppendLine("Beachcomber ((local))");
      sb.AppendLine("");
      sb.AppendLine(" ");
      sb.AppendLine("Finished in 0.00001 seconds");
      sb.AppendLine("0 examples, 0 failures");
      sb.AppendLine("");
      return sb.ToString();
    }

    private static string ResultIncludesJson()
    {
      var sb = new StringBuilder();
      sb.AppendLine("Beachcomber ((local))");
      sb.AppendLine("...");
      sb.AppendLine(" ");
      sb.AppendLine("Finished in 0.11845 seconds");
      sb.AppendLine("3 examples, 0 failures");
      sb.AppendLine("");
      // Embedded json ( just like using the -fj option )
      sb.AppendLine("{\"version\":\"3.6.0\",\"examples\":[{\"id\":\"./spec/sam ple/sample1.rb[1:1]\",\"description\":\"contains system tables\",\"full_description\":\"My database contains system tables\",\"status\":\"passed\",\"file_path\":\"./spec/sam ple/sample1.rb\",\"line_number\":5,\"run_time\":0.034996,\"pending_message\":null},{\"id\":\"./spec/sample/sample2.rb[1:3]\",\"description\":\"contains system tables (take two)\",\"full_description\":\"My database contains system tables (take two)\",\"status\":\"failed\",\"file_path\":\"./spec/sample / sample2.rb\",\"line_number\":17,\"run_time\":0.037508,\"pending_message\":null,\"exception\":{\"class\":\"RuntimeError\",\"message\":\"No SQL file found corresponding to method 'sample_1' in folder C:/work/_solv/src/SlackerRunner/SlackerTests/sql\",\"backtrace\":[\"C:/Ruby23-x64/lib/ruby/gems/2.3.0/gems/slacker-1.0.19/lib/slacker/sql.rb:25:in `method_missing'\",\"C:/Ruby23-x64/lib/ruby/gems/2.3.0/gems/slacker-1.0.19/bin/slacker:32:in `<top (required)>'\",\"C:/Ruby23-x64/bin/slacker:22:in `load'\",\"C:/Ruby23-x64/bin/slacker:22:in `<main>'\"]}},{\"id\":\"./spec / sample / sample2.rb[1:4]\",\"description\":\"contains system tables (take three)\",\"full_description\":\"My database contains system tables (take three)\",\"status\":\"failed\",\"file_path\":\"./spec/sample/sample2.rb\",\"line_number\":24,\"run_time\":0.028012,\"pending_message\":null,\"exception\":{\"class\":\"RuntimeError\",\"message\":\"No SQL file found corresponding to method 'sample_1' in folder C:/work/_solv/src/SlackerRunner/SlackerTests/sql\",\"backtrace\":[\"C:/work/_solv/src/SlackerRunner/SlackerTests/spec/sample/sample2.rb:26:in `block (2 levels) in <top (required)>'\",\"C:/Ruby23-x64/lib/ruby/gems/2.3.0/gems/slacker-1.0.19/lib/slacker/application.rb:50:in `run'\",\"C:/Ruby23-x64/lib/ruby/gems/2.3.0/gems/slacker-1.0.19/bin/slacker:32:in `<top (required)>'\",\"C:/Ruby23-x64/bin/slacker:22:in `load'\",\"C:/Ruby23-x64/bin/slacker:22:in `<main>'\"]}}],\"summary\":{\"duration\":0.736109,\"example_count\":14,\"failure_count\":6,\"pending_count\":0},\"summary_line\":\"14 examples, 6 failures\"}");
      return sb.ToString();
    }

    private static string ResultJsonError()
    {
      var sb = new StringBuilder();
      sb.AppendLine("Beachcomber ((local))");
      sb.AppendLine("...");
      sb.AppendLine(" ");
      sb.AppendLine("Finished in 0.11845 seconds");
      sb.AppendLine("3 examples, 0 failures");
      sb.AppendLine("");
      // Embedded json ( just like using the -fj option )
      sb.AppendLine("{\"version\":\"3.6.0\",\"messages\":[\"\nAn error occurred while loading./ spec / fake directory/**/*.\nFailure / Error: load file\n\nLoadError:\n cannot load such file-- C:/ work / _solv / src / SlackerRunner / SlackerTests / spec / fake directory/**/*\n# C:/Ruby23-x64/lib/ruby/gems/2.3.0/gems/rspec-core-3.6.0/lib/rspec/core/configuration.rb:1922:in `load'\n# C:/Ruby23-x64/lib/ruby/gems/2.3.0/gems/rspec-core-3.6.0/lib/rspec/core/configuration.rb:1922:in `load_spec_file_handling_errors'\n# C:/Ruby23-x64/lib/ruby/gems/2.3.0/gems/rspec-core-3.6.0/lib/rspec/core/configuration.rb:1494:in `block in load_spec_files'\n# C:/Ruby23-x64/lib/ruby/gems/2.3.0/gems/rspec-core-3.6.0/lib/rspec/core/configuration.rb:1492:in `each'\n# C:/Ruby23-x64/lib/ruby/gems/2.3.0/gems/rspec-core-3.6.0/lib/rspec/core/configuration.rb:1492:in `load_spec_files'\n# C:/Ruby23-x64/lib/ruby/gems/2.3.0/gems/rspec-core-3.6.0/lib/rspec/core/runner.rb:100:in `setup'\n# C:/Ruby23-x64/lib/ruby/gems/2.3.0/gems/rspec-core-3.6.0/lib/rspec/core/runner.rb:86:in `run'\n# C:/Ruby23-x64/lib/ruby/gems/2.3.0/gems/rspec-core-3.6.0/lib/rspec/core/runner.rb:71:in `run'\n# C:/Ruby23-x64/lib/ruby/gems/2.3.0/gems/slacker-1.0.19/lib/slacker/application.rb:76:in `run_rspec'\n# C:/Ruby23-x64/lib/ruby/gems/2.3.0/gems/slacker-1.0.19/lib/slacker/application.rb:56:in `block in run'\n# C:/Ruby23-x64/lib/ruby/gems/2.3.0/gems/slacker-1.0.19/lib/slacker/application.rb:50:in `catch'\n# C:/Ruby23-x64/lib/ruby/gems/2.3.0/gems/slacker-1.0.19/lib/slacker/application.rb:50:in `run'\n# C:/Ruby23-x64/lib/ruby/gems/2.3.0/gems/slacker-1.0.19/bin/slacker:32:in `<top (required)>'\n# C:/Ruby23-x64/bin/slacker:22:in `load'\n# C:/Ruby23-x64/bin/slacker:22:in `<main>'\n# \n#   Showing full backtrace because every line was filtered out.\n#   See docs for RSpec::Configuration#backtrace_exclusion_patterns and\n#   RSpec::Configuration#backtrace_inclusion_patterns for more information.\n\",\"No examples found.\"],\"examples\":[],\"summary\":{\"duration\":0.001,\"example_count\":0,\"failure_count\":0,\"pending_count\":0},\"summary_line\":\"0 examples, 0 failures, 1 error occurred outside of examples\"}");
      return sb.ToString();
    }


    [Fact]
    public void ParseUnforseenError()
    {
      var resultsParser = new ResultsParser();
      SlackerResults res = resultsParser.Parse(UnforseenError(), null);
      // advertise what what the score is 
      Console.WriteLine(res.getString());
      // Proof it 
      Assert.True(res.FailedSpecs > 0);
      Assert.False(res.Passed);
    }

    [Fact]
    public void ParseReturnsFailedSpecsWhenAllPassed()
    {
      var resultsParser = new ResultsParser();
      SlackerResults res = resultsParser.Parse(ResultAllPassed(), null);
      // advertise what what the score is 
      Console.WriteLine(res.getString());
      // Proof it 
      Assert.Equal(res.FailedSpecs, 0);
      Assert.Equal(res.PassedSpecs, 3);
    }

    [Fact]
    public void ParseReturnsFailedSpecsWhenOnePassed()
    {
      var resultsParser = new ResultsParser();
      SlackerResults res = resultsParser.Parse(ResultOnePassed(), null);
      // advertise what what the score is 
      Console.WriteLine(res.getString());
      // Proof it 
      Assert.Equal(res.FailedSpecs, 0);
      Assert.Equal(res.PassedSpecs, 1);
    }


    // This is the main parser test 
    [Fact]
    public void ParseReturnsFailedSpecsWhenSomeFailed()
    {
      var resultsParser = new ResultsParser();
      SlackerResults res = resultsParser.Parse(ResultSomeFailed(), null);
      // advertise what what the score is 
      Console.WriteLine(res.getString());
      // Proof it 
      Assert.Equal(res.FailedSpecs, 3);
      Assert.Equal(res.PassedSpecs, 24);
    }

    [Fact]
    public void ParseNoPassNoFailure()
    {
      var resultsParser = new ResultsParser();
      SlackerResults res = resultsParser.Parse(NoTestNoFailure(), null);
      // advertise what what the score is 
      Console.WriteLine(res.getString());
      // Proof it 
      Assert.True(res.FailedSpecs == 0);
      Assert.True(res.PassedSpecs == 0);
      // ***** NOTE *****
      // If there is no test in the rb file, it has not failed
      // This will guarantee empty tests files not to be failed as they should not
      Assert.True(res.Passed);
    }


    // This is the main parser test 
    [Fact]
    public void ParseJson()
    {
      var resultsParser = new ResultsParser();
      IEnumerable<SlackerResults> res = resultsParser.ParseJson(ResultIncludesJson(), null);
      // Proof it 
      Assert.True(res.Count() == 3 );
    }

    [Fact]
    public void ParseJsonError()
    {
      var resultsParser = new ResultsParser();
      IEnumerable<SlackerResults> res = resultsParser.ParseJson(ResultJsonError(), null);
      // Proof it 
      Assert.True(res.Count() == 1);
      Assert.False(res.First().Passed);
    }

    [Fact]
    public void ParseReturnsFalseWhenStandardErrorNotNull()
    {
      var resultsParser = new ResultsParser();
      SlackerResults SlackerResults = resultsParser.Parse(ResultAllPassed(), "StandardError");
      Assert.False(SlackerResults.Passed);
    }

    [Fact]
    public void ValidateReturnsFalseWhenOutResultsHaveFailedScenario()
    {
      var resultsParser = new ResultsParser();
      SlackerResults SlackerResults = resultsParser.Parse(ResultSomeFailed(), "StandardError");
      Assert.False(SlackerResults.Passed);
    }

    [Fact]
    public void ValidateReturnsTrueWhenOutputResultsPassedAndErrorIsEmpty()
    {
      var resultsParser = new ResultsParser();
      SlackerResults SlackerResults = resultsParser.Parse(ResultAllPassed(), "");
      Assert.True(SlackerResults.Passed);
    }

    [Fact]
    public void ValidateReturnsTrueWhenOutputResultsPassedAndErrorIsNull()
    {
      var resultsParser = new ResultsParser();
      SlackerResults SlackerResults = resultsParser.Parse(ResultAllPassed(), null);
      Assert.True(SlackerResults.Passed);
    }
  }
}