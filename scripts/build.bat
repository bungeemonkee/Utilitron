@echo off

if not defined configuration set configuration=Release

set solution=Utilitron.sln

msbuild /m /nologo /verbosity:minimal /p:Configruation=%configuration% %solution%
