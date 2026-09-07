# Lab9

ASP.NET Core Web API с конфигурацией для сред Development, Production и Testing.

## Запуск профиля из отчёта

```bash
dotnet run --launch-profile http
```

Swagger: `http://localhost:5176/swagger`.

Профиль `http` запускает среду Production и переопределяет `StudentApiSettings:Code` значением `12345`.
