@echo off

if not defined configuration set configuration=Release

set project=Utilitron.Tests.Unit
set bin=%project%\bin\%configuration%\net46
set target=%bin%\win7-x64\dotnet-test-mstest.exe
set libs=%bin%\%project%.dll

tools\OpenCover\OpenCover.Console.exe -register:path64 -filter:"+[Utilitron]* -[Utilitron]System.*" -target:"%target%" -targetargs:"%libs%" -output:coverage.xml

tools\coveralls.io\coveralls.net.exe --opencover coverage.xml
