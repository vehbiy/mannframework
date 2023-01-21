del *.nupkg
nuget pack MannFramework.csproj
nuget push -Source "Garcia" -ApiKey VSTS *.nupkg