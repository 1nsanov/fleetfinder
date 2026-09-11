ifneq (,$(wildcard .env))
include .env
export
endif

COMPOSE := docker compose
API_PROJECT := src/FleetFinder.Api/FleetFinder.Api.csproj
WEB_DIR := src/FleetFinder.Web
TEST_PROJECT := tests/FleetFinder.Application.Tests/FleetFinder.Application.Tests.csproj

.PHONY: help setup up down logs ps clean infra api web run-api run-web test build

help:
	@echo "FleetFinder"
	@echo "  make setup     Copy .env.example to .env if missing"
	@echo "  make up        First run: build and start the full stack"
	@echo "  make down      Stop containers"
	@echo "  make logs      Follow container logs"
	@echo "  make ps        List containers"
	@echo "  make clean     Stop containers and remove volumes"
	@echo "  make infra     PostgreSQL + MinIO only"
	@echo "  make api       API + dependencies (no Web)"
	@echo "  make web       Web + API + dependencies"
	@echo "  make run-api   dotnet run API against local infra"
	@echo "  make run-web   ng serve"
	@echo "  make test      Run backend handler tests"
	@echo "  make build     Build API and Angular client"

setup:
	@test -f .env || cp .env.example .env

up: setup
	$(COMPOSE) up --build -d

down:
	$(COMPOSE) down

logs:
	$(COMPOSE) logs -f

ps:
	$(COMPOSE) ps

clean:
	$(COMPOSE) down -v

infra: setup
	$(COMPOSE) up -d fleetfinder.db fleetfinder.minio
	$(COMPOSE) up fleetfinder.minio-init

api: setup
	$(COMPOSE) up --build -d fleetfinder.api

web: setup
	$(COMPOSE) up --build -d fleetfinder.web

run-api: setup
	ConnectionStrings__DefaultConnection="Host=localhost;Port=5437;Database=$(POSTGRES_DB);Username=$(POSTGRES_USER);Password=$(POSTGRES_PASSWORD)" \
	Jwt__Key="$(JWT_KEY)" \
	Jwt__Issuer="$(JWT_ISSUER)" \
	Jwt__Audience="$(JWT_AUDIENCE)" \
	S3Storage__ServiceUrl="http://localhost:9010" \
	S3Storage__PublicBaseUrl="$(S3_PUBLIC_BASE_URL)" \
	S3Storage__AccessKey="$(MINIO_ROOT_USER)" \
	S3Storage__SecretKey="$(MINIO_ROOT_PASSWORD)" \
	S3Storage__Bucket="$(S3_BUCKET)" \
	Seed__Enabled="$(SEED_DEMO_DATA)" \
	Seed__DemoPassword="$(SEED_DEMO_PASSWORD)" \
	dotnet run --project $(API_PROJECT) --urls http://localhost:8100

run-web:
	cd $(WEB_DIR) && (test -d node_modules || npm install) && npm start

test:
	dotnet test $(TEST_PROJECT)

build:
	dotnet build $(API_PROJECT)
	cd $(WEB_DIR) && (test -d node_modules || npm install) && npm run build
