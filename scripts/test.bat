@echo off

if not defined configuration set configuration=Release

set project=Utilitron.Tests.Unit
set assembly=%project%\bin\%configuration%\netstandard4.1\%project%.dll

tools\OpenCover\OpenCover.Console.exe -returntargetcode -register:user -target:"mstest.exe" -targetargs:"/nologo /usestderr /testcontainer:%assembly%" -output:coverage.xml
if %errorlevel% neq 0 exit /b %errorlevel%
exit /b 0
tools\coveralls.io\coveralls.net.exe --opencover coverage.xml
if %errorlevel% neq 0 exit /b %errorlevel%
