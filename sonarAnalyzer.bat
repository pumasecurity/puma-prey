@echo off
REM https://sonarcloud.io/project/configuration/GitHubManual?id=MinChanSike_puma-prey
REM https://docs.sonarsource.com/sonarcloud/advanced-setup/ci-based-analysis/sonarscanner-for-net/

echo Start Scanner...
dotnet sonarscanner begin ^
 /o:"minchansike" ^
 /k:"MinChanSike_puma-prey" ^
 /d:sonar.host.url="https://sonarcloud.io"

echo Build Project...
dotnet build PumaPrey.sln

echo Commit Scanner...
dotnet sonarscanner end 