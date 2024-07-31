param([string]$setup)

Copy-Item -Path "docker-compose-public.yml" -Destination "docker-compose.yml"
Copy-Item -Path "appsettings-public.json" -Destination "appsettings.json"

foreach ($file in "profiles-data.json", "secrets.json") {
    $data = Get-Content -Path $file | ConvertFrom-Json
    foreach ($profile in $data.shared, $data.$setup)
    {
        foreach ($config in $profile)
        {
            $target = $config.target
            $variables = $config.variables | ConvertTo-Json | ConvertFrom-Json -AsHashtable
            $content = Get-Content -Path $target
            foreach ($variable in $variables.Keys)
            {
                $content = $content -replace "\%$variable\%", $variables[$variable]
            }
            $content | Set-Content -Path $target
        }
    }
}
Write-Output "Done"
