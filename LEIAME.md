# Clean Architecture Essencial

Projeto de estudo e referência que demonstra a implementação de **Clean Architecture** com **.NET 10**, combinando padrões modernos como **CQRS**, **Repository Pattern** e **MediatR** em uma aplicação real de gerenciamento de produtos e categorias.

## Arquitetura

O projeto segue os princípios da **Arquitetura Limpa (Clean Architecture)**, onde as dependências apontam sempre de fora para dentro, mantendo o domínio isolado de frameworks, banco de dados e interfaces externas.

```
┌──────────────────────────────────────────────────────┐
│                  Presentation Layer                  │
│              WebUI (MVC)  |  WebAPI (REST)           │
├──────────────────────────────────────────────────────┤
│               Infrastructure Layer                   │
│         Infra.Data (EF Core)  |  Infra.IoC           │
├──────────────────────────────────────────────────────┤
│               Application Layer                      │
│     Services | DTOs | Commands | Queries | Handlers  │
├──────────────────────────────────────────────────────┤
│                  Domain Layer                        │
│          Entities | Interfaces | Validations         │
└──────────────────────────────────────────────────────┘
```

## 🗂️ Estrutura de Projetos

| Projeto | Responsabilidade |
|---|---|
| `Domain` | Entidades, interfaces de repositório, validações de domínio e contratos de autenticação |
| `Application` | Casos de uso via Services e CQRS (Commands, Queries, Handlers), DTOs e perfis AutoMapper |
| `Infra.Data` | Implementação do EF Core com PostgreSQL, repositórios, Identity e Migrations |
| `Infra.IoC` | Registro de dependências para WebUI e WebAPI |
| `WebUI` | Interface web com ASP.NET Core MVC e Razor Views |
| `WebAPI` | API REST com ASP.NET Core Web API e OpenAPI |
| `Tests` | Testes unitários das entidades de domínio |

## Tecnologias

- [.NET 10](https://dotnet.microsoft.com/)
- [ASP.NET Core MVC](https://learn.microsoft.com/aspnet/core/mvc/) — interface web
- [ASP.NET Core Web API](https://learn.microsoft.com/aspnet/core/web-api/) — API REST
- [Entity Framework Core](https://learn.microsoft.com/ef/core/) com [Npgsql](https://www.npgsql.org/) — ORM para PostgreSQL
- [MediatR](https://github.com/jbogard/MediatR) — implementação do padrão CQRS
- [AutoMapper](https://automapper.org/) — mapeamento entre entidades, DTOs e Commands
- [ASP.NET Core Identity](https://learn.microsoft.com/aspnet/core/security/authentication/identity) — autenticação e autorização com Roles
- [xUnit](https://xunit.net/) — testes unitários

## Padrões de Projeto

- **Clean Architecture** — separação em camadas com dependências invertidas
- **CQRS** (Command Query Responsibility Segregation) — separação de leitura e escrita via MediatR
- **Repository Pattern** — abstração do acesso a dados no domínio
- **Dependency Injection** — inversão de controle nativa do ASP.NET Core

## Domínio

### Entidades

**`Product`**
- `Name`, `Description`, `Price`, `Stock`, `Image`
- Relacionamento com `Category`
- Validações de domínio encapsuladas na própria entidade

**`Category`**
- `Name`
- Coleção de `Products` associados
- Validações de domínio encapsuladas na própria entidade

## Configuração e Execução

### Pré-requisitos

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [PostgreSQL](https://www.postgresql.org/)

### Variáveis de Ambiente

A string de conexão com o banco de dados é lida a partir de uma variável de ambiente:

```bash
# Windows (PowerShell)
$env:DEV_DB = "Host=localhost;Database=CleanArchDb;Username=postgres;Password=sua_senha"

# Linux/macOS
export DEV_DB="Host=localhost;Database=CleanArchDb;Username=postgres;Password=sua_senha"
```

### Migrations

Aplique as migrations para criar o banco de dados:

```bash
dotnet ef database update --project Infra.Data --startup-project WebUI
```

### Executando a aplicação

**Interface Web (WebUI):**
```bash
dotnet run --project WebUI
```

**API REST (WebAPI):**
```bash
dotnet run --project WebAPI
```

A documentação OpenAPI da API estará disponível em `https://localhost:{porta}/openapi` no ambiente de desenvolvimento.

### Executando os Testes

```bash
dotnet test
```

## 🔐 Autenticação

A aplicação utiliza **ASP.NET Core Identity** com seed inicial de usuários e roles.

- Ao iniciar o `WebUI`, os roles e usuários padrão são criados automaticamente via `SeedUserRoleInitial`.
- O acesso negado redireciona para `/Account/Login`.