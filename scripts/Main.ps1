
$xCmdString = { rundll32.exe user32.dll, LockWorkStation }
$scriptDir = Split-Path -Path $MyInvocation.MyCommand.Definition -Parent
& $scriptDir\functions.ps1

LogInfo "Running script" $MyInvocation.MyCommand.Name
$counter = 0;

function StartAsProcess($path, $params) {
  LogInfo "StartAsProcess:path" $path
  LogInfo "StartAsProcess:params" $params	
  [Diagnostics.Process]::Start($path, $params)
}

function CheckIfProcessActive() {
  $global:counter++;

  LogInfo "checking if process is running, attempt number "  $counter
  if ((get-process "TOURISTINFO" -ea SilentlyContinue) -eq $Null) {
    if ($env:ServiceCrashed -eq "true") {
      LogInfo "process still not working after reboot";
      LogInfo "Locking machine ...";
      Invoke-Command $xCmdString;
      break;
    }
            
    if ($counter -eq 2) {
      LogInfo "process still not running , rebooting machine ... "
      [System.Environment]::SetEnvironmentVariable('ServiceCrashed', 'true', [System.EnvironmentVariableTarget]::Machine)
      copy "$scriptDir\onstartup.cmd" "%USERPROFILE%\Start Menu\Programs\Startup"
      Restart-Computer;
    }

    else {
      LogInfo "process is not running, starting process ..." 
      StartService;
    }
  }

  else { 
    try {
      [System.Environment]::SetEnvironmentVariable('ServiceCrashed', 'false', [System.EnvironmentVariableTarget]::Machine)
    }
    catch {
      LogInfo $Error[0];
    }       
  }
}


function StartService() { 
  LogInfo "Starting proccess" $Main\TOURISTINFO.exe
  Start-Process $Main\TOURISTINFO.exe
}

Set-Variable -Name "Main" -Value "C:\Program Files\InfoPoint" -Scope Global
LogInfo "Running main script"
CheckIfProcessActive;
LogInfo "Proccess sleep for 2 minutes"
Start-Sleep -s 7200
CheckIfProcessActive;