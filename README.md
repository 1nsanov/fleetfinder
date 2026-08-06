# fleetfinder

Площадка объявлений транспорта (cargo / passenger / special): ASP.NET Core API, Angular SPA, PostgreSQL, MinIO.

## Demo-данные

При пустой БД и `Seed:Enabled=true` API заполняет демо-контент:

| Логин | Пароль |
|---|---|
| `demo` | `Demo123!` |
| `carrier1` | `Demo123!` |
| `carrier2` | `Demo123!` |

3 объявления каждого типа (cargo, passenger, special), 1–4 фото на объявление.

Переменные: `SEED_DEMO_DATA`, `SEED_DEMO_PASSWORD` (см. `.env.example`).
