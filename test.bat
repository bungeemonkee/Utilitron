@echo off

if not defined configuration set configuration=Release

set project=Utilitron.Tests.Unit

tools\OpenCover\OpenCover.Console.exe -returntargetcode -register:user -target:"dotnet.exe" -targetargs:"test %project%" -output:coverage.xml
if %errorlevel% neq 0 exit /b %errorlevel%

tools\coveralls.io\coveralls.net.exe --opencover coverage.xml
if %errorlevel% neq 0 exit /b %errorlevel%
