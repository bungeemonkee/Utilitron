@echo off

if not defined configuration set configuration=Release

dotnet pack --configuration %configuration% --output . Utilitron
