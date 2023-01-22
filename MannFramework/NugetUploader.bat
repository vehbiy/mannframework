del *.nupkg
nuget pack MannFramework.csproj
nuget push -f MannFramework.1.0.0.0.nupkg 111111-111111-111111 -Source https://www.nuget.org/api/v2/package