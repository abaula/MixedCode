#
# See readme.md
# podman build --build-arg SSH_PUBLIC_KEY="$(cat ~/.ssh/id_rsa.pub)" --build-arg SSH_PRIVATE_KEY="$(cat ~/.ssh/id_rsa)" --build-arg USER=dev --build-arg PASS=dev -t dev-host-pytorch-cuda-12.6.2:1.0.0 .
#

FROM docker.io/nvidia/cuda:12.6.2-cudnn-devel-ubuntu22.04

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
    libglib2.0-0

RUN useradd -rm -d "/home/${USER}" -s "/bin/bash" -G sudo -u 1000 -U "${USER}" \
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

RUN pip3 config set global.trusted-host "pypi.org files.pythonhosted.org pypi.python.org" \
  && pip3 install --no-cache-dir --upgrade pip \
  && pip3 install torch torchvision torchaudio -f https://download.pytorch.org/whl/cu111/torch_stable.html \
  && pip3 install --no-cache-dir -r requirements.txt 

USER root

RUN service ssh start

EXPOSE 22

CMD ["/usr/sbin/sshd","-D"]