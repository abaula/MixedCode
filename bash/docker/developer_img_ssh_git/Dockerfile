#
# podman build --build-arg SSH_PUBLIC_KEY="$(cat ~/.ssh/id_rsa.pub)" --build-arg SSH_PRIVATE_KEY="$(cat ~/.ssh/id_rsa)" --build-arg USER=dev --build-arg PASS=dev -t dev-host:1.0.0 .
#

FROM mcr.microsoft.com/dotnet/sdk:6.0.407-jammy-amd64

ARG USER=dev
ARG PASS=dev
ARG SSH_PUBLIC_KEY
ARG SSH_PRIVATE_KEY

RUN apt update && apt install openssh-server sudo -y

RUN useradd -rm -d "/home/${USER}" -s "/bin/bash" -G sudo -u 1000 -U "${USER}" \
    && echo "${USER}:${PASS}" | chpasswd

RUN mkdir -p "/home/${USER}/.ssh"; \
    touch "/home/${USER}/.ssh/authorized_keys"; \
    [ -n "${SSH_PRIVATE_KEY}" ] && echo "${SSH_PRIVATE_KEY}" >> "/home/${USER}/.ssh/id_rsa"; \
    echo "${SSH_PUBLIC_KEY}" >> "/home/${USER}/.ssh/authorized_keys"; \
    chown -R "${USER}:${USER}" "/home/${USER}/.ssh"; \
    chmod 700 "/home/${USER}/.ssh"; \
    find "/home/${USER}/.ssh" -type f -exec chmod 600 '{}' \;

RUN service ssh start

EXPOSE 22

CMD ["/usr/sbin/sshd","-D"]