PowerShell -Command "Set-ExecutionPolicy Unrestricted" >> "%TEMP%\StartupLog.txt" 2>&1
PowerShell C:\Git\Project\Nexthink\scripts\Main.ps1 >> "%TEMP%\StartupLog.txt" 2>&1