param([string]$setup)
Write-Output "Generating secrets for $setup..."
$secrets = Get-Content -Path "secrets.json" | ConvertFrom-Json
foreach ($config in $secrets.$setup) {
    $filename = $config.filename
    $target = $config.target
    $keys = $config.keys | ConvertTo-Json | ConvertFrom-Json -AsHashtable
    $content = Get-Content -Path $filename
    foreach ($key in $keys.Keys) {
        $content = $content -replace "\%$key\%", $keys[$key]
    }
    $content | Set-Content -Path $target
}
foreach ($config in $secrets.shared) {
    $filename = $config.target
    $target = $config.target
    $keys = $config.keys | ConvertTo-Json | ConvertFrom-Json -AsHashtable
    $content = Get-Content -Path $filename
    foreach ($key in $keys.Keys) {
        $content = $content -replace "\%$key\%", $keys[$key]
    }
    $content | Set-Content -Path $target
}
Write-Output "Done"
