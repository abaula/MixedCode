# Описание
Конфигурация для создания и использования образов Docker для разработки в контейнерах.

Контейнер созданный из образа запускает ssh сервер, что даёт возможность вести внутри контейнера разработку
через функционал [VS Code Remote Development](https://code.visualstudio.com/docs/remote/remote-overview).

Подробнее смотри [Remote Development using SSH](https://code.visualstudio.com/docs/remote/ssh).

Для удобства подключения к контейнеру через ssh, в образ копируется публичный ключ из текущего профиля пользователя запустившего сборку образа.
Также, для доступа к внешним сервисам по SSH (например github.com), в образ копируется закрытый ключ из текущего профиля пользователя запустившего сборку образа.

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

## Dotnet 8
`./net8/`

Образ для разработки на dotnet 8.

### Пользователь в конейнере:
`dev`, пароль `dev`.

### Команда для создания образа:
`podman build --build-arg SSH_PUBLIC_KEY="$(cat ~/.ssh/id_rsa.pub)" --build-arg SSH_PRIVATE_KEY="$(cat ~/.ssh/id_rsa)" --build-arg USER=dev --build-arg PASS=dev -t dev-host-net8:1.0.0 .`

### SSH config:
```
Host dev-host-net8
    HostName localhost
    User dev
    Port <порт указанный при создании docker контейнера>
```

## Python 3.14.2 bookworm
`./py/`

Образ для разработки на python 3.14.2 bookworm

### Дополнительные модули python:
Смотри файл `requirements.txt`.

### Пользователь в конейнере:
`dev`, пароль `dev`.

### Команда для создания образа:
`podman build --build-arg SSH_PUBLIC_KEY="$(cat ~/.ssh/id_rsa.pub)" --build-arg SSH_PRIVATE_KEY="$(cat ~/.ssh/id_rsa)" --build-arg USER=dev --build-arg PASS=dev -t dev-host-py-3-14-2:1.0.0 .`

### Команда для создания контейнера:

`podman create --name dev-host-py314 -v /host/path:/container/path --publish=HOSTPORT:22/tcp dev-host-py-3-14-2:1.0.0`

### SSH config:
```
Host dev-host-py314
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

## Python + Pytorch + CUDA
`./pytorch-cuda/`

Образ для разработки на Python, с предустановленными Pytorch, Cuda.

### Дополнительные модули python:
Смотри файл `requirements.txt`.

### Пользователь в конейнере:
`dev`, пароль `dev`.

### Команда для создания образа:
`podman build --build-arg SSH_PUBLIC_KEY="$(cat ~/.ssh/id_rsa.pub)" --build-arg SSH_PRIVATE_KEY="$(cat ~/.ssh/id_rsa)" --build-arg USER=dev --build-arg PASS=dev -t dev-host-pytorch-cuda-13.1.0:1.0.0 .`

### Команда для создания контейнера:

`podman create --name dev-host-pytorch-cuda13 -v /host/path:/container/path --device nvidia.com/gpu=all --publish=HOSTPORT:22/tcp dev-host-pytorch-cuda-13.1.0:1.0.0`

### SSH config:
```
Host dev-host-pytorch-cuda-13
    HostName localhost
    User dev
    Port <порт указанный при создании docker контейнера>
```

## C++
`./cpp/`

Образ для разработки на C++.

### Пользователь в конейнере:
`dev`, пароль `dev`.

### Команда для создания образа:
`podman build --build-arg SSH_PUBLIC_KEY="$(cat ~/.ssh/id_rsa.pub)" --build-arg SSH_PRIVATE_KEY="$(cat ~/.ssh/id_rsa)" --build-arg USER=dev --build-arg PASS=dev -t dev-host-cpp:1.0.0 .`

### Создание контейнера:
Для возможности отладки необходимо при создании контейнера указать флаг `--cap-add=SYS_PTRACE`.

Подробнее см. [Troubleshoot attaching to processes using GDB](https://github.com/Microsoft/MIEngine/wiki/Troubleshoot-attaching-to-processes-using-GDB)

### SSH config:
```
Host dev-host-cpp
    HostName localhost
    User dev
    Port <порт указанный при создании docker контейнера>
```

## Rust
`./rust/`

Образ для разработки на Rust.

### Пользователь в конейнере:
`dev`, пароль `dev`.

### Команда для создания образа:
`podman build --build-arg SSH_PUBLIC_KEY="$(cat ~/.ssh/id_rsa.pub)" --build-arg SSH_PRIVATE_KEY="$(cat ~/.ssh/id_rsa)" --build-arg USER=dev --build-arg PASS=dev -t dev-host-rust-1.86.0:1.0.0 .`

### Создание контейнера:
Для возможности отладки необходимо при создании контейнера указать флаг `--cap-add=SYS_PTRACE`.

Подробнее см. [Troubleshoot attaching to processes using GDB](https://github.com/Microsoft/MIEngine/wiki/Troubleshoot-attaching-to-processes-using-GDB)

### SSH config:
```
Host dev-host-rust-186
    HostName localhost
    User dev
    Port <порт указанный при создании docker контейнера>
```