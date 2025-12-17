#
# See readme.md
# podman build --build-arg SSH_PUBLIC_KEY="$(cat ~/.ssh/id_rsa.pub)" --build-arg SSH_PRIVATE_KEY="$(cat ~/.ssh/id_rsa)" --build-arg USER=dev --build-arg PASS=dev -t dev-host-pytorch-cuda-13.1.0:1.0.0 .
#

FROM docker.io/nvidia/cuda:13.1.0-devel-ubuntu24.04

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
    python3-pip \
    python3-dev \
    python3-opencv \
    python3.12-venv \
    libglib2.0-0

RUN useradd -rm -d "/home/${USER}" -s "/bin/bash" -G sudo -u 1001 -U "${USER}" \
    && echo "${USER}:${PASS}" | chpasswd

RUN mkdir -p "/home/${USER}/.ssh"; \
    touch "/home/${USER}/.ssh/authorized_keys"; \
    [ -n "${SSH_PRIVATE_KEY}" ] && echo "${SSH_PRIVATE_KEY}" >> "/home/${USER}/.ssh/id_rsa"; \
    echo "${SSH_PUBLIC_KEY}" >> "/home/${USER}/.ssh/authorized_keys"; \
    chown -R "${USER}:${USER}" "/home/${USER}/.ssh"; \
    chmod 700 "/home/${USER}/.ssh"; \
    find "/home/${USER}/.ssh" -type f -exec chmod 600 '{}' \;

COPY requirements.txt ./
RUN chown ${USER}:${USER} requirements.txt

USER ${USER}

# Создаём venv и ставим туда torch
RUN cd /home/${USER} \
  && python3 -m venv venv \
  && . venv/bin/activate \
  && pip3 install torch torchvision --index-url https://download.pytorch.org/whl/cu130

# Ставим пакеты из файла /requirements.txt в venv
RUN cd /home/${USER} \
  && . venv/bin/activate \
  && pip3 install --no-cache-dir -r /requirements.txt

USER root

RUN service ssh start

EXPOSE 22

CMD ["/usr/sbin/sshd","-D"]