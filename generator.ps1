param([string]$setup)
$secrets = Get-Content -Path "secrets.json" | ConvertFrom-Json
foreach ($config in $secrets.$setup)
{
    $target = $config.target
    if (Test-Path -Path $target) {
        Remove-Item -Path $target
    }
}
foreach ($config in $secrets.shared)
{
    $target = $config.target
    if (Test-Path -Path $target) {
        Remove-Item -Path $target
    }
}
foreach ($config in $secrets.$setup) {
    $filename = $config.filename
    $target = $config.target
    $keys = $config.keys | ConvertTo-Json | ConvertFrom-Json -AsHashtable
    $content = Get-Content -Path $filename
    foreach ($key in $keys.Keys) {
        $content = $content -replace "\%$key\%", $keys[$key]
    }
    New-Item -Path $target
    $content | Set-Content -Path $target
}
foreach ($config in $secrets.shared) {
    $filename = $config.filename
    $target = $config.target
    $keys = $config.keys | ConvertTo-Json | ConvertFrom-Json -AsHashtable
    if (!(Test-Path -Path $target)) {
        Copy-Item -Path $filename -Destination $target
    }
    $content = Get-Content -Path $target
    foreach ($key in $keys.Keys) {
        $content = $content -replace "\%$key\%", $keys[$key]
    }
    $content | Set-Content -Path $target
}
Write-Output "Done"
