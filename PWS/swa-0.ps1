Set-Location Api
if(Test-Path obj) {Remove-Item obj -Recurse}
if(Test-Path bin) {Remove-Item bin -Recurse}
Set-Location ..
Get-Job | Where-Object state -ne running | Group-Object -Property Name
Get-Job | Where-Object state -ne running | Remove-Job
Get-Job | Where-Object state -ne running | Group-Object -Property Name
$cmd = {
    Start-Sleep -Seconds 20
    Start-Process chrome.exe '--new-window http://localhost:5015'
  }
Start-Job -ScriptBlock $cmd

# https://azure.github.io/static-web-apps-cli/docs/cli/swa-start/
# https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-run

# swa start allows you to start the blazor application with or without Hot Reload.
# This is controlled solely by the command that is passed to the --run option.
# of the swa start command. Hot Reload is only possible if the [dotnet watch run]
# that is the watch switch is used.

# The Blazor Web Dev Server is the default Dev Server for Blazor.
# It runs by default at the fixed EP http://localhost:5000 and this cannot be changed.
# Only at the EP exposed by the Blazor Web Dev Server Hot Reload is possible.
# If you choose to use lauch your app on a port different from 5000 
# for example: http://localhost:5015 
# then Hot Reload will not be available on this EP (http://localhost:5015) whether the 
# the [dotnet watch run] or the [dotnet run] is used bacause this EP is not the EP of 
# the Blazor Web Dev Server
# then there will be no hot reload there as that EOP is not the EP of the 

# Launch with hot reload:
# No Hot Reload: http://localhost:5015
# No Hot Reload: http://localhost:5000
# Hot Reload OK: http://localhost:4280 
swa start http://localhost:5000 --run "dotnet watch run --launch-profile http --project Client/Client.csproj" --api-location Api --api-port 7174

# Launch without hot reload:
# swa start http://localhost:5000 --run "dotnet run --project Client/Client.csproj" --api-location Api --api-port 7174