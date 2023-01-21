del *.nupkg
nuget pack MannFramework.Application.csproj
nuget push -Source "Garcia" -ApiKey VSTS *.nupkg