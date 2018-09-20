REM @echo off

IF EXIST .\create-package.cmd cd .. 

CALL scripts\environnement.cmd

scripts\nuget.exe pack "FluentDataAccess.nuspec"
scripts\nuget.exe pack "FluentDataAccess.SQLite.nuspec"
scripts\nuget.exe pack "FluentDataAccess.SQLCE35.nuspec"

