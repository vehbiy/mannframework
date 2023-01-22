del *.nupkg
nuget pack MannFramework.Application.csproj
nuget push -Source "MannFramework" -ApiKey VSTS *.nupkg