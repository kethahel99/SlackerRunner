@ECHO OFF
ECHO Running Slacker ....
REM set some=%~1
REM ECHO some=%some%
REM set some=%some:"='%
REM echo running='%some%'
REM CALL slacker '%some%'
REM echo running=%1%

CALL slacker %1 >> %2
