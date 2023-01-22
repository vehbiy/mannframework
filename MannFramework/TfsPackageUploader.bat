del *.nupkg
nuget pack MannFramework.csproj
nuget push -Source "MannFramework" -ApiKey VSTS *.nupkg