@echo off
echo Restoring .NET dependencies...
dotnet restore

echo Building the project...
dotnet build

echo Selenium project is ready to run.
echo To run tests, use:
echo dotnet test --no-restore --verbosity normal
dotnet test --no-restore --verbosity normal
pause