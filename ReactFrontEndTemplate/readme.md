# О шаблоне
Шаблон проекта поможет быстро начать front-end SPI проект TypeScript, React-Redux.
Главная особенность - возможность запускать webpack-dev-server с 3-я разными вариантами web-api.
Обычно это могут быть web-api на машине разработчика DEV, на тестовом сервере TEST, и на предпродуктовом сервере PRE.
Таким образом можно работать с любым удобным web-api.

_Не забываем, что для этого web-api должен поддерживать CORS._

# Настройка проекта
1. Отредатировать файл package.json - name, version, description, author, licence.
2. Отредактировать title в файле /src/pug/index.pug.
3. Установить или проверить, что установлены: nodejs, npm.
4. Установить пакеты, перечисленные в project.json, для этого выполнить команду npm install.
5. Собрать проект или запустить под webpack-dev-server (см. Использование) с целью убедиться, что нет ошибок.
6. По желанию обновить версии пакетов - помним, что после этого могут быть ошибки.

# Использование
### Для работы с web-api в режиме CORS, нужно использовать webpack-dev-server:
npm run start-dev
npm run start-test
npm run start-pre

не забываем настроить url, см. Примечания.

### Сборка проекта в папку dist
npm run build:dev - без Uglify
npm run build:prod - с Uglify

# Примечания
1. Настройка Url для web-api производится в файле /src/ts/config/webapiconfig.ts
В зависимости от запущенного скрипта start-dev, start-test или start-pre
будет установлена одна из глобальных переменных: `__CORS_DEV__, __CORS_TEST__ или __CORS_PRE__`.
2. Настройки порта webpack-dev-server находятся в файле /webpack/devserver.js