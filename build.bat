@echo off
set msBuildFile=c:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\msbuild.exe
if exist "%msBuildFile%" goto build

set msBuildFile=c:\Program Files\Microsoft Visual Studio\2022\Professional\MSBuild\Current\Bin\msbuild.exe
if exist "%msBuildFile%" goto build

set msBuildFile=c:\Program Files\Microsoft Visual Studio\2022\Enterprise\MSBuild\Current\Bin\msbuild.exe
if exist "%msBuildFile%" goto build

echo*
echo* Please edit 'build.bat' and correct the path to 'msbuild.bat'
echo*
pause
exit

:build
call "%msBuildFile%" -t:rebuild Terminal2.sln /p:Configuration=Release
pause
