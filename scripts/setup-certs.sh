#!/usr/bin/env bash

set -e

source ./scripts/functions.sh

[[ "$EUID" -ne 0 ]] && IS_SUDO=false || IS_SUDO=true

[[ $IS_SUDO == true ]] && echo 'avoid running this script as sudo!'

add_hosts() {
    echo "127.0.0.1 innoclinic.local sts.innoclinic.local admin.innoclinic.local admin-api.innoclinic.local" | sudorun tee /etc/hosts
}


[[ "$(cat /etc/hosts)" != *'innoclinic.local'* ]] && add_hosts || echo "hosts already present!"

mkcert -h 2> /dev/null || ( echo "mkcert not found!"; exit 1 )

safecd shared/nginx/certs
mkcert --install
ln -s "$HOME/.local/share/mkcert/rootCA.pem" ./cacerts.pem
ln -s "$HOME/.local/share/mkcert/rootCA.pem" ./cacerts.crt

mkcert -cert-file innoclinic.local.crt -key-file innoclinic.local.key innoclinic.local *.innoclinic.local
mkcert -pkcs12 innoclinic.local.pfx innoclinic.local *.innoclinic.local


