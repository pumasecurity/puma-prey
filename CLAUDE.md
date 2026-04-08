# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Purpose

Puma Prey is a collection of intentionally vulnerable .NET applications used as targets for the Puma Scan security analyzer rules engine, CTFs, and secure coding training. **Code in this repo is deliberately insecure by design** -- do not "fix" vulnerabilities unless explicitly asked.

## Build Commands

```bash
# Build Coyote (.NET 8 - the primary actively developed project)
dotnet build src/Coyote/Coyote.csproj

# Build Rabbit data access library (.NET Standard 2.1)
dotnet build src/Rabbit/Rabbit.csproj

# Run tests
dotnet test test/Coyote.Tests/
dotnet test test/Rabbit.Tests/

# Run a single test
dotnet test test/Coyote.Tests/ --filter "FullyQualifiedName~TestMethodName"

# Full CI test script (used by GitHub Actions)
./build/dotnet-test.sh ./test/Coyote.Tests dotnet-test.trx

# Start local MySQL + Coyote via Docker
docker-compose up
```

The legacy .NET Framework projects (Common, Raccoon, Skunk, Fox) require Windows/Visual Studio with `nuget restore` + `msbuild`. The full solution is `PumaPrey.sln`; use `PumaPrey-Coyote.sln` for Coyote-only work.

## Architecture

The solution is a layered multi-project structure organized by animal codenames:

| Project | Framework | Role |
|---------|-----------|------|
| **Coyote** (`src/Coyote/`) | .NET 8 ASP.NET Core | Modern web API with JWT auth (port 8443) |
| **Rabbit** (`src/Rabbit/`) | .NET Standard 2.1 | Data access library (EF Core, ADO.NET, LINQ) using MySQL/Pomelo |
| **Squirrel** (`src/Squirrel/`) | Angular 12 | Frontend SPA consuming Coyote API |
| **Common** (`src/Common/`) | .NET Framework 4.8 | Shared crypto, validation, and data helpers |
| **Raccoon** (`src/Raccoon/`) | .NET Framework 4.6 | ASP.NET MVC 5 with Identity (legacy) |
| **Skunk** (`src/Skunk/`) | .NET Framework 4.6 | ASP.NET Web Forms with Membership (legacy) |
| **Fox** (`src/Fox/`) | .NET Framework 4.6 | ASP.NET Web API service (legacy) |

**Active development** centers on Coyote + Rabbit + their tests. The legacy Framework projects are mostly stable reference implementations.

Coyote depends on Rabbit for data access. Rabbit's `RabbitDBContext` (EF Core) connects to MySQL via Pomelo provider.

## Puma Scan Integration

`.pumafile` at repo root configures the Puma Scan security analyzer (v1.6.0) with 60+ rules (SEC0001-SEC1005) covering SQL injection, XSS, weak crypto, deserialization, command injection, JWT issues, and more. Dataflow analysis is enabled with engine v2.0.

## CI

GitHub Actions (`.github/workflows/dotnet_test.yaml`) runs `dotnet test` on PRs to main using .NET 8.x on Ubuntu. Azure Pipelines (`azure-pipelines.yml`) runs Puma Scan analysis on Windows.

## Testing

Tests use xUnit with FluentAssertions. Test output format is TRX (`--logger "trx;LogFileName=name.trx"`).
