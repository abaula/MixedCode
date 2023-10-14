
### Dotnet 6

path: `net6`

command:

`podman build --build-arg SSH_PUBLIC_KEY="$(cat ~/.ssh/id_rsa.pub)" --build-arg SSH_PRIVATE_KEY="$(cat ~/.ssh/id_rsa)" --build-arg USER=dev --build-arg PASS=dev -t dev-host-net6:1.0.0 .`

### Python

path: `py`

command:

`podman build --build-arg SSH_PUBLIC_KEY="$(cat ~/.ssh/id_rsa.pub)" --build-arg SSH_PRIVATE_KEY="$(cat ~/.ssh/id_rsa)" --build-arg USER=dev --build-arg PASS=dev -t dev-host-py-312:1.0.0 .`
