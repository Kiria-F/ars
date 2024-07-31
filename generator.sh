#!/usr/bin/bash

if [ -z "$1" ]; then
    echo "Warning: profile is not provided."
fi

cp docker-compose-public.yml docker-compose.yml
cp appsettings-public.json appsettings.json

for file in profile-data.json secrets.json; do
    for profile in shared $1; do
        for config_index in $( seq 0 $(( $(jq -r ".[\"$profile\"] | length" $file ) - 1 )) ); do
            target=$( jq -r ".[\"$profile\"][$config_index][\"target\"]" $file )
            variables_count=$( jq -r ".[\"$profile\"][$config_index][\"variables\"] | length" $file )
            for variable_i in $( seq 0 $(( variables_count - 1 )) ); do
                variable=$( jq -r ".[\"$profile\"][$config_index][\"variables\"] | keys | .[$variable_i]" $file )
                value=$( jq -r ".[\"$profile\"][$config_index][\"variables\"][\"$variable\"]" $file )
                value=${value//\//\\\/}
                sed -i "s/%$variable%/$value/" "$target"
            done
        done
    done
done
echo "Done"
