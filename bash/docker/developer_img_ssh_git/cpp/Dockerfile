#
# See readme.md
# podman build --build-arg SSH_PUBLIC_KEY="$(cat ~/.ssh/id_rsa.pub)" --build-arg SSH_PRIVATE_KEY="$(cat ~/.ssh/id_rsa)" --build-arg USER=dev --build-arg PASS=dev -t dev-host-cpp:1.0.0 .
#

FROM ubuntu:jammy

ARG USER=dev
ARG PASS=dev
ARG SSH_PUBLIC_KEY
ARG SSH_PRIVATE_KEY

# install build dependencies to build and debug
RUN apt-get update \
    && apt-get install -y g++ build-essential make cmake gdb gdbserver \
       rsync zip openssh-server git sudo

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