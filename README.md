# SlackerRunner

[![Build status](https://deloitte-fintech.visualstudio.com/_apis/public/build/definitions/b620f5de-3806-47e5-8cf2-fb39e823213a/17/badge)](https://deloitte-fintech.visualstudio.com/Platform/_build?definitionId=17)

Intro
============
Allows you to run [Slacker](https://github.com/vassilvk/slacker) tests from within Visual Studio and during continuous integration under TFS/VSTS.  This is not a test runner but rather a process runner for unit tests which will return [Slacker](https://github.com/vassilvk/slacker) test results back to VS/TFS/VSTS.

This project is based on the [Cucumber runner at Codeplex](https://cukesfortfs.codeplex.com/)

Slacker Requirements
------------
    Ruby 1.9.2 or 1.9.3 on Windows or Linux
    (for Windows use RubyInstaller and the Ruby Windows Development Kit).
    SQL Server 2012+


Getting Started
------------

1. Install [Slacker](https://github.com/vassilvk/slacker/wiki/Installation "Slacker Installation and Requirements")
2. If behind a proxy server, use the following line:
  * `set http_proxy=http://username:password@proxyserver:8080`
3. Open `\SlackerTests\database.yml` and put in your MS SQL database and credentials. The tests that ship with the solution are basic database system tests and should run against empty MS SQL database.
4. Run the UnitTests.  


  

To add SlackerRunner to your VS solution from the Nuget Package Manager console
------------
`PM> Install-Package SlackerRunner`  

The Nuget package feed is [located here](https://www.nuget.org/packages/SlackerRunner/).




Code samples
------------
See [Code samples on the Wiki](https://github.com/deloitte-solvas/SlackerRunner/wiki/Samples "SlackerRunner samples")  to get you started.

