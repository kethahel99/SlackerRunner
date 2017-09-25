<# 
  Replaces version information in assembly 
  Pass the version as parameter to this script, otherwize it will default to 0.0.0.7
#>
param (  [string]$version = "0.0.0.7"  )
$x = 'Version("{0}")' -f $version
$tmp = "Updating assembly Version: " 
$tmp += $version
write-output $tmp
$content = Get-Content .\SlackerRunner\Properties\AssemblyInfo.cs
$content -replace 'Version\(".*"\)',$x > ..\SlackerRunner\Properties\AssemblyInfo.cs