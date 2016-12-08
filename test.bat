@echo off

if not defined configuration set configuration=Release

set project=Utilitron.Tests.Unit
set bin=%project%\bin\%configuration%\net46
set target=%bin%\win7-x64\dotnet-test-mstest.exe
set libs=%bin%\%project%.dll

tools\OpenCover\OpenCover.Console.exe -returntargetcode -register:user -filter:"+[Utilitron]* -[Utilitron]System.*" -target:"%target%" -targetargs:"%libs%" -output:coverage.xml
if %errorlevel% neq 0 exit /b %errorlevel%

tools\coveralls.io\coveralls.net.exe --opencover coverage.xml
if %errorlevel% neq 0 exit /b %errorlevel%
