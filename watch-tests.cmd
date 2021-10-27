dotnet watch --project .\tests\ test /p:CollectCoverage=true /p:CoverletOutputFormat=lcov /p:CoverletOutput=./lcov.info
@REM reportgenerator -reports:"./tests/lcov.info" -targetdir:"testgenerator" -reporttypes:Html -- this generates a stats file
@REM dotnet tool install -g dotnet-reportgenerator-globaltool