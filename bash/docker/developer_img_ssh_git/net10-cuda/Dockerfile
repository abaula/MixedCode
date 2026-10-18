#
# See readme.md
# podman build --build-arg SSH_PUBLIC_KEY="$(cat ~/.ssh/id_rsa.pub)" --build-arg SSH_PRIVATE_KEY="$(cat ~/.ssh/id_rsa)" --build-arg USER=dev --build-arg PASS=dev -t dev-host-net10-cuda-13.0.2:1.0.0 .
#

FROM docker.io/nvidia/cuda:13.0.2-devel-ubuntu24.04

# Конфигурация Unubtu по умолчанию...
ENV DEBIAN_FRONTEND=noninteractive

ARG USER=dev
ARG PASS=dev
ARG SSH_PUBLIC_KEY
ARG SSH_PRIVATE_KEY

RUN apt-get update && \
  apt-get install -y \
    openssh-server \
    sudo \
    git \
    mc \
    wget \
    ca-certificates \
    libc6 \
    libgcc-s1 \
    libgssapi-krb5-2 \
    libicu74 \
    libssl3 \
    libstdc++6 \
    zlib1g \
    && rm -rf /var/lib/apt/lists/*

RUN useradd -rm -d "/home/${USER}" -s "/bin/bash" -G sudo -u 1001 -U "${USER}" \
    && echo "${USER}:${PASS}" | chpasswd

RUN mkdir -p "/home/${USER}/.ssh"; \
    touch "/home/${USER}/.ssh/authorized_keys"; \
    [ -n "${SSH_PRIVATE_KEY}" ] && echo "${SSH_PRIVATE_KEY}" >> "/home/${USER}/.ssh/id_rsa"; \
    echo "${SSH_PUBLIC_KEY}" >> "/home/${USER}/.ssh/authorized_keys"; \
    chown -R "${USER}:${USER}" "/home/${USER}/.ssh"; \
    chmod 700 "/home/${USER}/.ssh"; \
    find "/home/${USER}/.ssh" -type f -exec chmod 600 '{}' \;

# Глобальная установка .NET 10 SDK в /usr/share/dotnet
# Даем права всем пользователям на чтение и запуск .NET
RUN wget https://dot.net/v1/dotnet-install.sh -O dotnet-install.sh \
    && chmod +x dotnet-install.sh \
    && ./dotnet-install.sh --version 10.0.402 --install-dir /usr/share/dotnet \
    && rm dotnet-install.sh \
    && chmod -R o+rx /usr/share/dotnet

# 1. Добавляем пути в общесистемный bash.bashrc (для интерактивных шеллов)
RUN echo "export DOTNET_ROOT=/usr/share/dotnet" >> /etc/bash.bashrc \
    && echo "export PATH=$PATH:$DOTNET_ROOT:$DOTNET_ROOT/tools" >> /etc/bash.bashrc
# 2. Добавляем пути в /etc/profile (для login-шеллов)
RUN echo "export DOTNET_ROOT=/usr/share/dotnet" >> /etc/profile \
    && echo "export PATH=$PATH:$DOTNET_ROOT:$DOTNET_ROOT/tools" >> /etc/profile
# 3. Делаем симлинк в стандартную системную папку бинарников (самый надежный костыль)
# Если bash сбросит PATH до стандартного, он все равно найдет dotnet в /usr/bin
RUN ln -s /usr/share/dotnet/dotnet /usr/bin/dotnet

# Проверяем работу под вашим пользователем
USER ${USER}
RUN dotnet --version

USER root
RUN service ssh start

EXPOSE 22

CMD ["/usr/sbin/sshd","-D"]