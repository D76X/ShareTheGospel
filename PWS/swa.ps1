param (
    
    # if you want to run SSL locally
    [switch]$opentab,

    # if you want to run SSL locally
    [switch]$ssl,

    # the port to serve HTTP traffic: 5000 is the default port of the Blazor Dev Server
    # this value must agree with the applicationUrl of the http profile in launchSettings.json 
    [Parameter(Mandatory=$false)]
    [int]$port=5000,   
    
    # the port to serve HTTPS traffic: 7249 seems to work with SWA 
    # this value must agree with the applicationUrl of the https profile in launchSettings.json 
    [Parameter(Mandatory=$false)]
    [int]$sslport=7249,

    # the port where the Azure Function API is deployed locally to the Azure Function Runtime 
    # this works only when CORS is enabled for the AF Api - refer to teh READ.me for details  
    [Parameter(Mandatory=$false)]
    [int]$apiport=7174
)

# the following three lines perfrom some cleanup in the Api folder 
# the obj and bin folder in the Api folder must be clean otherwise
# the Azure Function runtime fails to run the API for the static web app
# more details are in the READ.me
Set-Location Api
if(Test-Path obj) {Remove-Item obj -Recurse}
if(Test-Path bin) {Remove-Item bin -Recurse}

# This part goes back to the folder that contains the three folders for
# client app, the api app and the sharedd folder and there then it stops 
# any running jobs that may have been started by previous runs of this 
# script
Set-Location ..
Get-Job | Where-Object state -ne running | Group-Object -Property Name
Get-Job | Where-Object state -ne running | Remove-Job
Get-Job | Where-Object state -ne running | Group-Object -Property Name

if ($opentab) { 

  # this can be used to force the opening of an additional tab in chrome
  # however the same can be achieved by the "launchBrowser": true
  # in the http(s) profile in launchSettings.json 

  # we leave it in only as a debug strategy to test things out when 
  # the launchSettings.json is changed for example by adding a new 
  # configuration or testing out different ports
  
  $cmd = {
    Start-Sleep -Seconds 20
    Start-Process chrome.exe '--new-window http://localhost:5015'
  }

  # an async Job  is used to invoke chrome and open the tab in a new windows
  # at the give address.
  Start-Job -ScriptBlock $cmd
}

# References:
# SWA start:
# https://azure.github.io/static-web-apps-cli/docs/cli/swa-start/
# dotnet run:
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

if ($ssl) {  
  # Launch with hot reload and SSL: https://localhost:7249
  #swa start --ssl https://localhost:7249 --run "dotnet watch run --launch-profile https --project Client/Client.csproj" --api-location Api --api-port 7174
  swa start --ssl https://localhost:$sslport --run "dotnet watch run --launch-profile https --project Client/Client.csproj" --api-location Api --api-port $apiport
}
else{
  
  # Launch with hot reload: http://localhost:5000 | http://localhost:4280 
  #swa start http://localhost:5000 --run "dotnet watch run --launch-profile http --project Client/Client.csproj" --api-location Api --api-port 7174  
  swa start http://localhost:$port --run "dotnet watch run --launch-profile http --project Client/Client.csproj" --api-location Api --api-port $apiport  
}
