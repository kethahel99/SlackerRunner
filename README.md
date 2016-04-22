# SlackerRunner

This project is based on the Cucumber runner at Codeplex
============
https://cukesfortfs.codeplex.com/


Intro
============
Slacker TFS / VS test runner.  Allows you to run Slacker tests from within Visual Studio and during Continuous Integration under TFS.  This is not a TestRunner, rather a process runner that can be run by UnitTests and will bring back number of failures and passed Slacker tests back to TFS.



Slacker
============
https://github.com/vassilvk/slacker


Slacker Requirements
------------
    Ruby 1.9.2 or 1.9.3 on Windows or Linux
    (for Windows use RubyInstaller and the Ruby Windows Development Kit).
    SQL Server 2005 or 2008


Slacker Installation
------------
If behind a proxy server use the following line:
set http_proxy=http://username:password@proxyserver:8080

Open up a Ruby command prompt and type:

gem install slacker



Quick start
============
1. Open \SlackerTests\database.yml and put in your Ms Sql database and credentials.  The tests that ship with the solution are basic database system test and should run against empty Ms Sql Server database.

2. Run the UnitTests.



Add SlackerRunner to your VS solution
============
PM> Install-Package SlackerRunner  
Then copy the run.bat from the packages SlackerRunner directory to your execution directory.
