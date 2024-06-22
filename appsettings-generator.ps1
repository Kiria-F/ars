$secrets = Get-Content -Path "secrets.json" | ConvertFrom-Json -AsHashtable
foreach ($setup in $secrets.Values) {
    $public = Get-Content -Path "appsettings-public.json"
    $keys = $setup.keys
    foreach ($key in $keys.Keys) {
        $public = $public -replace "\%$key\%", $keys[$key]
    }
    $public | Set-Content -Path $setup.filename
}