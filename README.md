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