#
# podman build --no-cache --build-arg SSH_PUBLIC_KEY="$(cat ~/.ssh/id_rsa.pub)" --build-arg USER=dev --build-arg PASS=dev -t dev-host:1.0.0 .
#

FROM mcr.microsoft.com/dotnet/sdk:6.0.407-jammy-amd64

ARG USER=test
ARG PASS=test
ARG SSH_PUBLIC_KEY

RUN apt update && apt install openssh-server sudo -y

RUN useradd -rm -d "/home/${USER}" -s "/bin/bash" -G sudo -u 1000 -U "${USER}"

RUN echo "${USER}:${PASS}" | chpasswd

RUN mkdir -p "/home/${USER}/.ssh" \
    && touch "/home/${USER}/.ssh/authorized_keys" \
    && echo "${SSH_PUBLIC_KEY}" >> "/home/${USER}/.ssh/authorized_keys" \
    && chown -R "${USER}:${USER}" "/home/${USER}/.ssh" \
    && chmod 700 "/home/${USER}/.ssh" \
    && chmod 600 "/home/${USER}/.ssh/authorized_keys"

RUN service ssh start

EXPOSE 22

CMD ["/usr/sbin/sshd","-D"]