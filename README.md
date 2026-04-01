# Encurtador de URLs Inteligente

Api REST feita para Modelagem de dados, (CRUD) e persistência com banco de dados.

## Instalação

Certifique-se de ter o SDK do .NET 9 instalado. Para restaurar as dependências e compilar o projeto, execute:

```bash
# Restaurar pacotes NuGet
dotnet restore

# Compilar o projeto
dotnet build
```

## Usage
Para rodar a aplicação pelo terminal:
```
# Garanta que o banco de dados exista (SQLite), rode:
dotnet ef migrations add Initial -p UrlShortener.Infrastructure -s UrlShortener.API -o DataBase/Migrations

# E depois:
dotnet ef database update -p UrlShortener.Infrastructure -s UrlShortener.API 

dotnet run
```
