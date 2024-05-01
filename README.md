# Report Generator

Report Generator - это веб-приложение, которое предназначено для автоматической загрузки и генерации отчетов на основе данных из дампа базы данных.

## Особенности:

- **Автоматическая загрузка дампа**: При первом запуске приложение автоматически скачивает дамп базы данных. В последующие разы оно автоматически проверяет наличие нового дампа на сервере и загружает его, если он доступен.
- **Принудительная загрузка дампа**: Пользователи могут также вручную загрузить дамп базы данных, используя соответствующую кнопку в интерфейсе приложения.

## Запуск приложения:

Для запуска приложения выполните следующие шаги:

1. Откройте терминал.
2. Перейдите в корневую директорию проекта.
3. Выполните команду `dotnet run`.

## Требования:

- .NET Core