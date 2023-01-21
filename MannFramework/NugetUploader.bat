del *.nupkg
nuget pack MannFramework.csproj
nuget push -f MannFramework.1.0.0.0.nupkg 68f98d6f-fde1-4db5-a9fb-b06358cd6111 -Source https://www.nuget.org/api/v2/package