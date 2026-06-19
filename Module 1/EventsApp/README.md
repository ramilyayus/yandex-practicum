# EventsApp

## Запуск

```bash
dotnet run
```

Swagger UI: https://localhost:PORT/swagger

PORT смотри в `Properties/launchSettings.json`.

## Эндпоинты

| Метод | Путь | Описание |
|-------|------|----------|
| GET | `/api/events` | Все события |
| GET | `/api/events/{id}` | Событие по id |
| POST | `/api/events` | Создать событие |
| PUT | `/api/events/{id}` | Обновить событие |
| DELETE | `/api/events/{id}` | Удалить событие |

## Формат ошибок

```json
{
  "code": 400,
  "messages": ["Title must not be empty"]
}
```
