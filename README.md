# POC para uso de configurações dinâmicas em .NET (em andamento)

## Criação da estrutura dos projetos

```shell
dotnet new sln

dotnet new classlib -n Core.DynamicConfig
dotnet new classlib -n Core.LocalEnvConfig

dotnet sln add ./Core.DynamicConfig/Core.DynamicConfig.csproj
dotnet sln add ./Core.LocalEnvConfig/Core.LocalEnvConfig.csproj

dotnet add Core.LocalEnvConfig/Core.LocalEnvConfig.csproj reference ./Core.DynamicConfig/Core.DynamicConfig.csproj

dotnet new console -n TestApplication
dotnet add TestApplication/TestApplication.csproj reference ./Core.LocalEnvConfig/Core.LocalEnvConfig.csproj

dotnet run --project TestApplication
```

### Limpeza de cache local

```shell
rm -rf bin obj
dotnet nuget locals all --clear
```

## Chamadas de teste

Adicionar uma nova chave:

```shell
curl -X POST http://localhost:5000/add-entry -H "Content-Type: application/json" -d '{"key": "AppSettings:ApplicationName", "value": "MyAppNameFromHazelcast"}'
```

Obter chaves atuais:

```shell
curl http://localhost:5000/list-entries
```

Chamada do `WeatherApi`:

```shell
curl -X 'GET' 'http://localhost:5062/WeatherForecast' -H 'accept: text/plain'
```

### Publicar Local

```shell
dotnet pack --configuration Release
mv bin/Release/*.nupkg ../nuget-local/
```