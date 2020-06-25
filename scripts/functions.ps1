
function global:cdg {
    Set-Location "C:\Program Files\"
}


function  global:LogInfo($n, $val) { 
    $timestamp = Get-Date -Format o | ForEach-Object { $_ -replace “:”, “.” }
    $message = "LOG: $n"
    if ($val -ne $null -and $val -ne "") {
        $message += " -> $val"
    }

    Write-Host $message -ForegroundColor Cyan;
    "$message" | Out-File $LogfilePath\$timestamp.log -Append -Force 
}


Write-Host "Global functions configured ....";
Set-Variable -Name "LogFilePath" -Value "C:\Git\Project\Nexthink\scripts\crash" -Scope Global

