
https://gentle-hill-0ba167403.1.azurestaticapps.net/

# Blazor Starter Application

This template contains an example .NET 6 [Blazor WebAssembly](https://docs.microsoft.com/aspnet/core/blazor/?view=aspnetcore-6.0#blazor-webassembly) client application, a .NET 6 C# [Azure Functions](https://docs.microsoft.com/azure/azure-functions/functions-overview), and a C# class library with shared code.

## Getting Started

1. Create a repository from the [GitHub template](https://docs.github.com/en/enterprise/2.22/user/github/creating-cloning-and-archiving-repositories/creating-a-repository-from-a-template) and then clone it locally to your machine.

1. In the **Api** folder, copy `local.settings.example.json` to `local.settings.json`

1. Continue using either Visual Studio or Visual Studio Code.

### Visual Studio 2022

Once you clone the project, open the solution in [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) and follow these steps:

1. Right-click on the solution and select **Set Startup Projects...**.

1. Select **Multiple startup projects** and set the following actions for each project:
    - *Api* - **Start**
    - *ApiIsolated* - None
    - *Client* - **Start**
    - *Shared* - None

1. Press **F5** to launch both the client application and the Functions API app.


---

## Azure Functions Core Tools and Static Web Apps CLI (SWA)

[Node Version Manager](https://github.com/nvm-sh/nvm)  

[Static Web Apps CLI](https://azure.github.io/static-web-apps-cli/)
[Develop Azure Functions locally using Core Tools](https://learn.microsoft.com/en-us/azure/azure-functions/functions-run-local?tabs=windows%2Cisolated-process%2Cnode-v4%2Cpython-v2%2Chttp-trigger%2Ccontainer-apps&pivots=programming-language-csharp#v2)  

[Azure Functions Core Tools](https://www.npmjs.com/package/azure-functions-core-tools)  
[Azure Functions Core Tools](https://www.npmjs.com/package/azure-functions-core-tools/v/2.7.1149)  


---

#### Uninstall Azure Functions Core Tools v1 from npm v12

The following illustrates how to uninstall a package from a version of Node that has been
registered with NVM.

```
node -v
npm list -g --depth=0
func --version
cd 'C:\Users\pb00270\AppData\Roaming\nvm\v12.13.0'
npm uninstall -g azure-functions-core-tools
```

---

#### Install Azure Functions Core Tools on npm v > 12

The following script illustrates the steps that can be used with **nvm** to setup the npm packages
that are required to work with Azure Static Web Apps and Azure Functions on the developer machine.

In order to execute some of the commands such as `nvm use` the Terminal must be executed in Admin mode.

The following links lists all the NodeJS LTS versions: 
[NodeJS Previous Releases](https://nodejs.org/en/about/previous-releases)  

---

```
nvm ls
nvm install 16.20.2
nvm use 16.20.2

18.12.0
* 16.20.2 (Currently using 64-bit executable)
16.16.0
12.13.0

npm list -g --depth=0
+-- corepack@0.17.0
`-- npm@8.19.4

npm install -g @azure/static-web-apps-cli
npm install -g azure-functions-core-tools

npm list -g --depth=0
C:\Program Files\nodejs -> .\
+-- @azure/static-web-apps-cli@1.1.7
+-- azure-functions-core-tools@4.0.5571
+-- corepack@0.17.0
`-- npm@8.19.4

```

---

### Visual Studio Code with Azure Static Web Apps CLI local development

The following has been tested on the following versions of Node:

```
18.12.0
16.20.2 
```

After **SWA** and **azure-functions-core-tools@4xx** have been installed on your 
local machine it is possible to start the client app and the related Azure Functions Api
through **SWA** either from a Terminal or from the integrated VSCode Terminal.

Details about this command can be found at the following link:
[swa start](https://azure.github.io/static-web-apps-cli/docs/cli/swa-start/)  

The following is an example of the command that is used to start the PWS app and its AF Api.
 
```
cd 'C:\VSProjects\MyProjetcs\Websites\Sites\PWS'
swa start http://localhost:5000 --run "dotnet watch run --launch-profile http --project Client/Client.csproj" --api-location Api --api-port 7174
```

There are some important details related to this command to notice in order to understand 
how it works and be able to modify it should the need be.

1. cd 'C:\VSProjects\MyProjetcs\Websites\Sites\PWS'

The command must be executed from the parent folder that contains the subfolders `Client`, `Api`, `Shared`, etc.

2. http://localhost:5000

This is the address on which the Client app is published locally by **SWA**, the port `5000` **must** agree 
with what is specified in the configuration file `Client\Properties\launchSettings.json`. 
The port number `5000` is a special number as this is the default port of the **Blazor Web Dev Server** on
the localhost. Any other port number can be chosen, for example `5015`, but in order to be able to use the
Hot Reload only the EP `http://localhost:5000` of the **Blazor Web Dev Server** on the localhost can be used.
Any other port number such as for example `http://localhost:5015` will not provide the Hot Reload whether or 
not the watch switch is used in: [dotnet watch run].
Furthermore if the [dotnet run] is used instead of [dotnet watch run] then also the the EP `http://localhost:5000` 
of the **Blazor Web Dev Server** will not provide Hot Reload.
 
The following is the excerpt from the the configuration file `Client\Properties\launchSettings.json` where the 
`http` launch profile is defined.

```
"http": {
      "commandName": "Project",
      "dotnetRunMessages": true,  
      "launchBrowser": true,          
      "inspectUri": "{wsProtocol}://{url.hostname}:{url.port}/_framework/debug/ws-proxy?browser={browserInspectUri}",      
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    },
```

3. `--api-location Api --api-port 7174`

The final part of the **SWA** command causes the Azure Function Api consumed by the client app
to be launched locally from the subfolder `Api` and specifies the publishing port `7174`.
This port **must** agree with the port on which the Client App expects to find its AF Api.

**Important:** if the `--api-port` is not specified then `7071` is used by **SWA** by default.

This is specified in the client app by means of the following files:

> Program.cs

```
builder.Services
	.AddScoped(sp => new HttpClient
	{
		BaseAddress = new Uri(
      builder.Configuration[Constants.ApiPrefix] ??
		  builder.HostEnvironment.BaseAddress)
	});  
```

> Costants.cs

```
namespace Client.Abstractions;

public static class Constants
{
	public const string ApiPrefix = "API_Prefix";
}
```

> the configuration file `Client\Properties\launchSettings.json`

```
{
  ...
  "AllowedHosts": "*",
  "API_Prefix": "http://localhost:7174",
  ...
}
```

4. It may happen that when the **SWA** command above is run the Azure Function Api fails to build.
`SWA emulator stopped because SWA emulator exited with code 1.`

[Can't determine Project to build. Expected 1 .csproj or .fsproj but found 3 when running "func start" #3594](https://github.com/Azure/azure-functions-core-tools/issues/3594)  

The following comment is the fix and consists of deleting the **bin & obj** folder in the Api subfolder.  

```
ryanzwe commented on Feb 16
Delete the /bin & obj folder
```

This means for example that you do the following:

```
cd C:\VSProjects\MyProjetcs\Websites\Sites\PWS\Api
Remove-Item obj -Recurse
Remove-Item bin -Recurse
```

---

> Let's now put everything together:

The following shows how to:

1. remove the **bin & obj** folder in the Api subfolder
2. use `swa start` to start the client application on the localhost on port 5000 over http
3. use `swa start` to start the supporting Azure Function API on port 7174

```
cd C:\VSProjects\MyProjetcs\Websites\Sites\PWS\Api
Remove-Item obj -Recurse
Remove-Item bin -Recurse
cd C:\VSProjects\MyProjetcs\Websites\Sites\PWS
swa start http://localhost:5000 --run "dotnet watch run --launch-profile http --project Client/Client.csproj" --api-location Api --api-port 7174
```

---

## [SWA Endpoint for Hot Reload Emulated Services](https://azure.github.io/static-web-apps-cli/docs/cli/swa-start/) 

`swa start http://localhost:5000`

and any of its variants starts the **Blazor WebAssembly Dev Server** as this is by default at the EP `http://localhost:5000`.

However, SWA provides a **Emulated Service** also at the EP: `http://localhost:4280` and this endpoint is always available
for local testing even when you start use `swa start http://localhost:5000`.

The EP `http://localhost:4280` behaves as the the **Blazor WebAssembly Dev Server** and therefore provides Hot Reload when 
the [dotnet watch run] is used as in the example below. However, the `http://localhost:4280` is **special** as it offers 
out-of-the-box integration of authentication emulation for the default authentication providers - more information on this 
is provided later in this document.

```
swa start http://localhost:5000 --run "dotnet watch run --launch-profile http --project Client/Client.csproj" --api-location Api --api-port 7174
```

---

## HTTPS with the SWA Emulator: 


The **SWA Emulator** provides an endpoint to test out HTTPS locally.
This **endpoint is fixed** at `https://localhost:7249`.

In order to run the WASM client application on HTTPS at this EP the SWA command 
`swa start`must be provided with the correct configuration and options as 
illustrated below.

This is separate from the `swa start` to run the site locally over HTTP that was 
discussed earlier.

It is possible to run the client app in HTTP or HTTPS at any one time.

When local testing over HTTPS is required then this `swa start` must be used.

> the file `swa.ps1` included in this solution offers a convenient way to start 
the SWA emulator with HTTPS with the right commands and to perform some necessary
upkeeping operations that are not mentioned in the official documentation of the
SWA CLI.

All that is required is illustrated in the following examples:

```
cd 'C:\VSProjects\MyProjetcs\Websites\Sites'

.\swa.ps1       # start the SWA emulator with the http profile
.\swa.ps1       -site `PWS` # start the SWA emulator with the http profile
.\swa.ps1 -ssl  -site `STG` # start the SWA emulator with the https profile (must be on port: 7249)
```

```
# the port to serve HTTPS traffic: 7249 seems to work with SWA 
# this value must agree with the applicationUrl of the https profile in launchSettings.json 

[Parameter(Mandatory=$false)]
[int]$sslport=7249,

[Parameter(Mandatory=$false)]
[int]$apiport=7174

swa start --ssl https://localhost:$sslport --run "dotnet watch run --launch-profile https --project Client/Client.csproj" --api-location Api --api-port $apiport
```

The configuration file `Client\Properties\launchSettings.json` for each project 
provides the http and https profiles that are used by the SWA CLI emulator.
The following is an example of https profile which has the only restriction 
that port `7249` must be used for local https as this is where the SWA CLI
HTTPS enable web server runs by default and cannot be changed.

```
"https": {
      "commandName": "Project",
      "dotnetRunMessages": true,  
      "launchBrowser": true,   
      "applicationUrl": "https://localhost:7249",       
      "inspectUri": "{wsProtocol}://{url.hostname}:{url.port}/_framework/debug/ws-proxy?browser={browserInspectUri}",      
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    }
```

---

# SWA CLI Error: Could not find openssl on your system on this path:  

It may happen that the following command that attempts to start the SWA CLI HTTPS
emulator may result in an error like the one below:

```
cd 'C:\VSProjects\MyProjetcs\Websites\Sites\PWS'
.\swa.ps1 -ssl  # start the SWA emulator with the https profile (must be on port: 7249)

SWA CLI Error: Could not find openssl on your system on this path: .....
cannot find openssl in folder ...  
C:\Users\pb00270\AppData\Roaming\nvm\v18.12.0\node_modules\@azure\static-web-apps-cli\node_modules\pem
```

This problem is fixed by installing the **OpenSSL** even when [Node Version Manager](https://github.com/nvm-sh/nvm) 
is used to support multiple NodeJs versions on the same OS.

[How to install OpenSSL in windows 10?](https://stackoverflow.com/questions/50625283/how-to-install-openssl-in-windows-10)  

Installing **OpenSSL** on Windows may be complicated as it can be seen by reading the 
Stack Overflow article given above. However, there is a easy and painless option that works 
and that is one of the solutions offered in the same article.

From an Admin PowerShell console install [Chocolatey](https://chocolatey.org/install) or 
upgrade it [Chocolatey: Upgrade](https://docs.chocolatey.org/en-us/choco/commands/upgrade/).
Then use Chocolatey to install **OpenSLL** for you without having to know the details of 
a complete setup.

```
choco upgrade chocolatey
choco install openssl
```

Once this is done the and **OpenSSL** is installed the SWA CLI emulatro should work also 
with the built-in HTTPS server without the error mentioned above.

---

## Template Structure

- **Client**: The Blazor WebAssembly sample application
- **Api**: A C# Azure Functions API, which the Blazor application will call
- **Shared**: A C# class library with a shared data model between the Blazor and Functions application

---

## Deploy to Azure Static Web Apps

This application can be deployed to [Azure Static Web Apps](https://docs.microsoft.com/azure/static-web-apps), to learn how, check out [our quickstart guide](https://aka.ms/blazor-swa/quickstart).

[Build configuration for Azure Static Web Apps](https://learn.microsoft.com/en-gb/azure/static-web-apps/build-configuration?tabs=github-actions#build-and-deploy)

---

## Azure Static Web Apps with Managed Azure Functions Feature

[Deploy .NET 8 Blazor apps and serverless APIs with Azure Static Web Apps](https://www.youtube.com/watch?v=crycB22_58s)   
[Build and deploy .NET 8 Blazor WASM apps with serverless APIs using Azure Static Web Apps](https://techcommunity.microsoft.com/t5/apps-on-azure-blog/build-and-deploy-net-8-blazor-wasm-apps-with-serverless-apis/ba-p/3988412)  

#### Steps

    - Step 1: add the API Azure Functions project to the solution
    
    - Step 2: edit the file `local.settings.json` of the API project to enable CORS
    
    ```
    ...
    "Host": {
        "CORS": "*",
        "CORSCredentials": false
    }
    ...
    ```
        
    - Step 3: Add the file `appsettings.Development.json` with the ref to the API Azure Functions AE
    
    ```
    {
    "API_Prefix": "http://localhost:<YOUR_PORT_HERE>"
    }
    ```

    The <YOUR_PORT_HERE> is the port number of the EP that can be found in the file 
    `launchSettings.json` under Api > Properties.

    ```
    ...
    "Api": {
      "commandName": "Project",
      "commandLineArgs": "--port <YOUR_PORT>"
    },
    ...
    ```
---

## Authentication in Azure Static Web Apps & Blazor

[SWA CLI Local Authentication](https://azure.github.io/static-web-apps-cli/docs/cli/local-auth/)

Local Authentication is available locally through the SWA emulator exclusively on the fixed endpoints below:

http://localhost:4280/.auth/login/<PROVIDER_NAME>
http://localhost:4280/.auth/login/aad
http://localhost:4280/.auth/login/github

Once you authenticate at one of these endpoints its is possible to retrieve the details for the
locally authenticated users at the endpoint below:

http://localhost:4280/.auth/me

> from http://localhost:4280/.auth/login/aad

```
{
  "clientPrincipal": {
    "userId": "097872e1923dc16289bd93c6728576ad",
    "userRoles": [
          "anonymous",
          "authenticated"
      ],
      "claims": [],
      "identityProvider": "aad",
      "userDetails": "user1"
  }
}
```

> Important: 

It **must** be on the port **4280** which is where the **SWA emulator** runs as a default.
It **will not work**, for example on port **5000** which is the default port for the 
**Blazor Dev Server** - here you get the message: 

`Sorry, there's nothing at this address.`

http://localhost:5000/.auth/login/<PROVIDER_NAME>
http://localhost:5000/.auth/login/aad
http://localhost:5000/.auth/login/github

> Other References:

[How to setup Built-in Authentication for Azure Static Web Apps with Azure Active Directory](https://techcommunity.microsoft.com/t5/apps-on-azure-blog/how-to-setup-built-in-authentication-for-azure-static-web-apps/ba-p/3734709)  
[Authenticating in Azure Static Web Apps](https://www.youtube.com/watch?v=KjSY9vmGz24&t=928s)  

[.NET 8 Blazor Authentication & Authorization with Identity](https://www.youtube.com/watch?v=tNzSuwV62Lw)  

# [ASP.NET Core Blazor authentication and authorization](https://learn.microsoft.com/en-us/aspnet/core/blazor/security/?view=aspnetcore-8.0&tabs=visual-studio#client-side-blazor-authentication)  

[Server-side Blazor authentication](https://learn.microsoft.com/en-us/aspnet/core/blazor/security/?view=aspnetcore-8.0&tabs=visual-studio#server-side-blazor-authentication)  
[Client-side Blazor authentication](https://learn.microsoft.com/en-us/aspnet/core/blazor/security/?view=aspnetcore-8.0&tabs=visual-studio#client-side-blazor-authentication)

[Securing a Blazor WASM app on Azure Static Web App - Part 1](https://www.hackmum.in/blog/azureswa_cli_customauthentication)  
[Securing a Blazor WASM app on Azure Static Web App - Part 2](https://www.hackmum.in/blog/azureswa_customauthentication_part2)  

# Custom Authentication

[Custom authentication in Azure Static Web Apps](https://learn.microsoft.com/en-us/azure/static-web-apps/authentication-custom?tabs=aad%2Cinvitations)  
[Blazor Web Assembly Static Web App Custom Authentication](https://www.reddit.com/r/dotnet/comments/1cyjghv/need_help_setting_up_authentication_for_azure/)

# Custom Authentication with Microsoft Account on Microsoft Entra ID

[Build a website using Azure Static Web Apps and Authenticate with AAD-Doug Does Tech](https://www.youtube.com/watch?v=jnwRpEM6GR8)  
[Azure function app secured by AAD: You do not have permission to view this directory or page](https://learn.microsoft.com/en-us/answers/questions/93243/azure-function-app-secured-by-aad-you-do-not-have)

---

# Login

[Bootstrap: Modal](https://getbootstrap.com/docs/4.4/components/modal/)  
[Bootstrap: Open an Modal window from Navigation bar](https://stackoverflow.com/questions/23685837/open-an-modal-window-from-navigation-bar)  
[Bootstrap 5 Login Modal](https://bootstrapexamples.com/@anonymous/bootstrap-5-login-modal)  

[Sign in with Microsoft Button Design Guidelines](https://learn.microsoft.com/en-us/entra/identity-platform/howto-add-branding-in-apps)  

[Bootstrap: Login form with social login buttons](https://bootstrapexamples.com/@juanmz/login-form-with-social-login-buttons)  
[CSS hover not working, inside a bootstrap modal overlay](https://stackoverflow.com/questions/33233609/css-hover-not-working-inside-a-bootstrap-modal-overlay) 

---

# Blazor CSS Isolation

[ASP.NET Core Blazor CSS isolation](https://learn.microsoft.com/en-us/aspnet/core/blazor/components/css-isolation?view=aspnetcore-9.0)
[Blazor Web Assembly CSS isolation doesn't work](https://stackoverflow.com/questions/78171904/blazor-web-assembly-css-isolation-doesnt-work)  
[Blazor CSS Isolation not working and not adding scope identifiers after migrating to .NET 5.0 and using SASS](https://stackoverflow.com/questions/64833632/blazor-css-isolation-not-working-and-not-adding-scope-identifiers-after-migratin)  

In this project the identifiers that are created by the .Net Compiler for the CSS associated to 
Blazor components are customized so that the generation of random identifiers that is used as 
default by the the .net compiler is replaced by identifiers that are controlled at a component
level. 

This is done as it results to have a much simpler test-deploy workflow, with fewer errors as 
the default mechanism has proven to be not very reliable, difficult to understand and control
and prone to breaking with new releases of .Net.

[Blazor CSS Isolation: Customize scope identifier format](https://learn.microsoft.com/en-us/aspnet/core/blazor/components/css-isolation?view=aspnetcore-9.0#customize-scope-identifier-format)  

This is achieved by editing the `csproj` file as shown by the example below:

`C:\VSProjects\MyProjetcs\Websites\Sites\PWS\Client\Client.csproj`

```
<ItemGroup>
    <None Update="Components/MainNavBar.razor.css" CssScope="mnb-20241217-1" />
</ItemGroup>
```

The `CssScope="mnb-20241217-1"` applied to `Components/MainNavBar.razor.css` results 
in the identifier `mnb-20241217-1` being applied to the HTML element of the corresponding
component `Components/MainNavBar.razor`.

## Deploy the CSS Isolation Bundle

The PowerShell file below contains teh logic that ...

`css-isolation-client-bundle-copy.ps1`

---

# Blazor Bootstrap Libraries

[Getting started - Blazor WebAssembly (.NET 8)](https://docs.blazorbootstrap.com/getting-started/blazor-webassembly-net-8)  

---

## Use Multiple Accounts with VS Code

[Git Config User Profiles](https://marketplace.visualstudio.com/items?itemName=onlyutkarsh.git-config-user-profiles)  
[How to configure multiple git accounts in Visual Studio Code workspace](https://stackoverflow.com/questions/55141142/how-to-configure-multiple-git-accounts-in-visual-studio-code-workspace)

---

## [Basic component layout inheritance in Blazor](https://stackoverflow.com/questions/59990832/basic-component-layout-inheritance-blazor) 

#### @sw1337 answer

As of this writing, derived razor components implement all methods of their base classes automatically, 
including **BuildRenderTree** (which renders the HTML markup that you type within the razor file). 
When you type nothing, that method will make no attempt at calling the base BuildRenderTree method on 
its own. 

So you need to do it manually like so:

```

@inherits BaseComponent;

@{
    base.BuildRenderTree(__builder);
}

@code {
  protected override void OnInitialized()
    {
         base.header = "Setting header for the parent"
    }
}

```

---

[How to prevent default navigation for Blazor NavLink component](https://stackoverflow.com/questions/60653318/how-to-prevent-default-navigation-for-blazor-navlink-component)  

[ASP.NET Core updates in .NET Core 3.1 Preview 2](https://devblogs.microsoft.com/dotnet/asp-net-core-updates-in-net-core-3-1-preview-2/)  

- New component tag helper
- Prevent default actions for events in Blazor apps
- Stop event propagation in Blazor apps
- Validation of nested models in Blazor forms
- Detailed errors during Blazor app development

[How to cancel navigation when user clicks a link (<a> element)?](https://stackoverflow.com/questions/866583/how-to-cancel-navigation-when-user-clicks-a-link-a-element)  

> 

```

This uses the NavLink directly and works also without 
providing the href to it. However, because href is missing
the drowser does not see it as a <a> element and does not 
shoe the cursor when you hover over the link.
  
@* 
<NavLink class="nav-link"
aria-current="page"
@onclick='() => LinkSelected(PageTranslations.Index)'>
@miIndex
</NavLink> 
*@

This restores the <a> pointer in the browser but the 
href value always wins over the vavigation set by the 
@onclick lambda.

@* 
<MenuItem class="nav-link"
Match="NavLinkMatch.All"
aria-current="page"
href=@Model.Index.Href
@onclick='() => LinkSelected(PageTranslations.Index)'>
@miIndex
</MenuItem> 
*@

This according to: https://stackoverflow.com/questions/60653318/how-to-prevent-default-navigation-for-blazor-navlink-component
should Stop event propagation in Blazor apps.
However, it causes the build error: 

The component parameter 'onclick' is used two or more times for this component. 
Parameters must be unique (case-insensitive)

as noted here: https://stackoverflow.com/questions/60653318/how-to-prevent-default-navigation-for-blazor-navlink-component

@* 
<MenuItem class="nav-link"
Match="NavLinkMatch.All"
aria-current="page"
@onclick:preventDefault
@onclick='() => LinkSelected(PageTranslations.Index)'>
@miIndex
</MenuItem> 
*@
        
This works and it is based on the very simple line:
href="javascript:undefined"

It has been suggested here: https://stackoverflow.com/questions/866583/how-to-cancel-navigation-when-user-clicks-a-link-a-element
by: Kyad

<MenuItem class="nav-link"
            Match="NavLinkMatch.All"
            aria-current="page"
            href="javascript:undefined"
            @onclick='() => LinkSelected(PageTranslations.Index)'>
    @miIndex
</MenuItem>
```

---


https://www.syncfusion.com/faq/blazor/general/how-do-i-intercept-routing-in-blazor-before-it-navigates
https://stackoverflow.com/questions/54297711/blazor-how-to-pass-arguments-to-onclick-function 
https://github.com/SyncfusionExamples/Blazor-FAQ-Samples/tree/master/Intercept%20Routing%20in%20Blazor%20Server%20App 

---

https://stackoverflow.com/questions/45592581/cannot-debug-in-vs-code-by-attaching-to-chrome 
https://stackoverflow.com/questions/56267303/blazor-client-side-debugging  
https://stackoverflow.com/questions/64826309/blazor-two-way-binding-text-area-inside-component  

---

# Unit Tests

[Xunit.DependencyInjection 9.6.0](https://www.nuget.org/packages/Xunit.DependencyInjection/)

[moq: The most popular and friendly mocking library for .NET](https://github.com/devlooped/moq)  
[moq: Quickstart](https://github.com/devlooped/moq/wiki/Quickstart)
[Unit Testing: Moq Framework - Visual Studio ToolBox](https://learn.microsoft.com/en-us/shows/visual-studio-toolbox/unit-testing-moq-framework)  

[Is it possible to use Dependency Injection with xUnit?](https://stackoverflow.com/questions/39131219/is-it-possible-to-use-dependency-injection-with-xunit)  
[Implementation of Unit Test using xUnit and Moq in .NET Core 6 Web API](https://medium.com/@jaydeepvpatil225/implementation-of-unit-test-using-xunit-and-moq-in-net-core-6-web-api-539205f1d38f)  

[How to use Moq to mock xUnit tests for a .NET project](https://www.youtube.com/watch?v=NEtEmHgJBDQ)  
[How to mock the dependency injection object in unit test](https://stackoverflow.com/questions/68275461/how-to-mock-the-dependency-injection-object-in-unit-test)   

---



