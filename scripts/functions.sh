safecd() {
    [ -d "$1" ] || mkdir "$1"
    cd "$1"
}

sudorun() {
    [[ $IS_SUDO = false ]] && sudo $@ || $@
}

gen_random_string() {
    cat /dev/urandom | base64 -w 0 | head -c$1
}