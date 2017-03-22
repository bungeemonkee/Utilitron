@echo off
set scripts=%~dp0
set scripts=%scripts:~0,-1%

echo .NET Core CLI Version:
dotnet --version
