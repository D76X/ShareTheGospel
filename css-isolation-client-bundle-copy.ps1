<#
.Synopsis
   copy the Blazor CSS isolation assembly bundle to the wwwroot\css folder of teh site
.DESCRIPTION
   # ASP.NET Core Blazor CSS isolation
   https://learn.microsoft.com/en-us/aspnet/core/blazor/components/css-isolation?view=aspnetcore-9.0#css-isolation-bundling
.EXAMPLE   
   .\css-isolation-client-bundle-copy.ps1 -site PWS
   .\css-isolation-client-bundle-copy.ps1 -site STG -$configuration Release
.INPUTS
   Inputs to this cmdlet (if any)
.OUTPUTS
   Output from this cmdlet (if any)
.NOTES
   General notes
.COMPONENT
   The component this cmdlet belongs to
.ROLE
   The role this cmdlet belongs to
.FUNCTIONALITY
   The functionality that best describes this cmdlet
#>
param (   

    [Parameter(Mandatory=$true)]
    [string]$site='PWS',
    # [string]$location='C:\VSProjects\MyProjetcs\Websites\Sites\PWS'

    [Parameter(Mandatory=$false)]
    [string]$configuration='Debug',    

    [Parameter(Mandatory=$false)]
    [string]$targetFramework='net8.0',   

    [Parameter(Mandatory=$false)]
    [string]$assemblyName='Client'    
)

# ASP.NET Core Blazor CSS isolation
# https://learn.microsoft.com/en-us/aspnet/core/blazor/components/css-isolation?view=aspnetcore-9.0#css-isolation-bundling
$path="C:\VSProjects\MyProjetcs\Websites\Sites\"+$site+"\Client\obj\"+$configuration+"\"+$targetFramework+"\scopedcss\bundle\"+$assemblyName+".styles.css"
$destination="C:\VSProjects\MyProjetcs\Websites\Sites\"+$site+"\"+$assemblyName+"\wwwroot\css"

# C:\VSProjects\MyProjetcs\Websites\Sites\PWS\Client\obj\Debug\net8.0\scopedcss\projectbundle
Write-Host "copy: " $path
Write-Host "to: " $destination

if(Test-Path -Path $path -PathType Leaf)
{
    Write-Host "true"    
    Copy-Item $path -Destination $destination
}
else
{
    Write-Host "false nothing copied"
}
