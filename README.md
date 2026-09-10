# FleetFinder

Marketplace for cargo, passenger, and special-machinery listings.

Stack: ASP.NET Core API (.NET 10), Angular 22 SPA, PostgreSQL, MinIO (S3-compatible storage).

## Architecture

Clean Architecture + CQRS (MediatR). Feature folders are vertical slices (create / update / delete / get / list per transport type).

```
Api            → Application, Infrastructure
Infrastructure → Application, Domain
Application    → Domain
Domain         → (POCOs only)
```

| Project | Role |
|---|---|
| `src/FleetFinder.Api` | HTTP host, controllers, middleware |
| `src/FleetFinder.Application` | handlers, validation, abstractions |
| `src/FleetFinder.Domain` | entities and enums |
| `src/FleetFinder.Infrastructure` | EF Core, JWT, MinIO/S3, migrations |
| `src/FleetFinder.Web` | Angular client |

Command and query use separate DbContexts. Problem Details for API errors.

## Prerequisites

- [Docker](https://docs.docker.com/get-docker/)
- Optional for local (non-Docker) runs: .NET 10 SDK, Node.js 24, [GNU Make](https://www.gnu.org/software/make/) (Git Bash / WSL on Windows)

## Quick start

```bash
cp .env.example .env
make up
```

Without Make:

```bash
cp .env.example .env
docker compose up --build -d
```

| Service | URL |
|---|---|
| Web UI | http://localhost:4200 |
| API | http://localhost:8100 |
| Swagger | http://localhost:8100/swagger |
| MinIO console | http://localhost:9011 |

Swagger UI is served only when `ASPNETCORE_ENVIRONMENT=Development` (the Compose default).

Stop: `make down` or `docker compose down`. Remove volumes: `make clean`.

## Demo accounts

Seeded when the database is empty and `Seed:Enabled=true` (`SEED_DEMO_DATA` in `.env`):

| Login | Password |
|---|---|
| `demo` | `Demo123!` |
| `carrier1` | `Demo123!` |
| `carrier2` | `Demo123!` |

Three listings of each type (cargo, passenger, special), with photos.

## Makefile

| Target | What it does |
|---|---|
| `make setup` | Copy `.env.example` → `.env` if missing |
| `make up` | Full stack (first run) |
| `make down` / `make logs` / `make ps` | Stop / logs / status |
| `make clean` | Stop and delete volumes |
| `make infra` | PostgreSQL + MinIO only |
| `make api` | API container + dependencies, no Web |
| `make web` | Web + API + dependencies |
| `make run-api` | `dotnet run` against local infra (`localhost:5437` / `:9010`) |
| `make run-web` | `ng serve` (API at `http://localhost:8100/api/`) |
| `make test` | Backend handler tests |
| `make build` | Build API + Angular |

PowerShell without Make: the same `docker compose`, `dotnet`, and `npm` commands as in the Makefile.

Local API/Web (not in Docker):

```bash
make infra
make run-api
make run-web
```

## Tests and CI

```bash
dotnet test tests/FleetFinder.Application.Tests/FleetFinder.Application.Tests.csproj
```

GitHub Actions runs `dotnet test` and `npm run build` on push and pull request.
