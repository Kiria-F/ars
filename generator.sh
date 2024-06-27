#!/usr/bin/bash

for file_i in $( seq 0 $(( $(jq -r ".[\"$1\"] | length" secrets.json ) - 1 )) ); do
    filename=$( jq -r ".[\"$1\"][$file_i][\"filename\"]" secrets.json )
    target=$( jq -r ".[\"$1\"][$file_i][\"target\"]" secrets.json )
    keys_count=$( jq -r ".[\"$1\"][$file_i][\"keys\"] | length" secrets.json )
    cp "$filename" "$target"
    for key_i in $( seq 0 $(( $keys_count - 1 )) ); do
        key=$( jq -r ".[\"$1\"][$file_i][\"keys\"] | keys | .[$key_i]" secrets.json )
        value=$( jq -r ".[\"$1\"][$file_i][\"keys\"][\"$key\"]" secrets.json )
        value=${value//\//\\\/}
        sed -i "s/%$key%/$value/" "$target"
    done
done
