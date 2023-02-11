# О проекте
Визуальная часть проекта с примером распределённой архитектуры:
- сервис регистрации сообщений связан с web-api через RabbitMq.
- клиент использует для связи с web-api библиотеку SignalR.

_для удобства разработки, web-api поддерживает CORS._

# Использование
### Для работы с web-api в режиме CORS, нужно использовать webpack-dev-server:
- npm run start-dev

не забываем настроить url, см. Примечания.

### Сборка проекта в папку dist
- npm run build:dev - без Uglify
- npm run build:prod - с Uglify

# Примечания
1. Настройка Url для web-api производится в файле /src/ts/config/webapiconfig.ts
При запуске скрипта start-dev будет установлена глобальная переменная: `__CORS_DEV__`.
2. Настройки порта webpack-dev-server находятся в файле /webpack/devserver.js