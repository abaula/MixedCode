# Описание
Конфигурация для создания и использования образов Docker для разработки в контейнерах.

Контейнер созданный из образа запускает ssh сервер, что даёт возможность вести внутри контейнера разработку
через функционал [VS Code Remote Development](https://code.visualstudio.com/docs/remote/remote-overview).

Подробнее смотри [Remote Development using SSH](https://code.visualstudio.com/docs/remote/ssh).

Для удобства подключения к контейнеру через ssh, в образ копируется публичный ключ из текущего профиля пользователя запустивщего сборку образа.

## Dotnet 6
`./net6/`

Образ для разработки на dotnet 6.

### Пользователь в конейнере:
`dev`, пароль `dev`.

### Команда для создания образа:
`podman build --build-arg SSH_PUBLIC_KEY="$(cat ~/.ssh/id_rsa.pub)" --build-arg SSH_PRIVATE_KEY="$(cat ~/.ssh/id_rsa)" --build-arg USER=dev --build-arg PASS=dev -t dev-host-net6:1.0.0 .`

### SSH config:
```
Host dev-host-net6
    HostName localhost
    User dev
    Port <порт указанный при создании docker контейнера>
```

## Python 3.12
`./py/`

Образ для разработки на python 3.12.

### Дополнительные модули python:
Смотри файл `requirements.txt`.

### Пользователь в конейнере:
`dev`, пароль `dev`.

### Команда для создания образа:
`podman build --build-arg SSH_PUBLIC_KEY="$(cat ~/.ssh/id_rsa.pub)" --build-arg SSH_PRIVATE_KEY="$(cat ~/.ssh/id_rsa)" --build-arg USER=dev --build-arg PASS=dev -t dev-host-py-312:1.0.0 .`

### SSH config:
```
Host dev-host-py312
    HostName localhost
    User dev
    Port <порт указанный при создании docker контейнера>
```

## Python + Pytorch + Jupyter
`./pytorch/`

Образ для разработки на Python, с предустановленными Pytorch, Jupyter.

### Дополнительные модули python:
Смотри файл `requirements.txt`.

### Пользователь в конейнере:
`dev`, пароль `dev`.

### Команда для создания образа:
`podman build --build-arg SSH_PUBLIC_KEY="$(cat ~/.ssh/id_rsa.pub)" --build-arg SSH_PRIVATE_KEY="$(cat ~/.ssh/id_rsa)" --build-arg USER=dev --build-arg PASS=dev -t dev-host-pytorch-321:1.0.0 .`

### SSH config:
```
Host dev-host-pytorch-321
    HostName localhost
    User dev
    Port <порт указанный при создании docker контейнера>
```
