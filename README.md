
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

In order to execute some of the commands such as `nvm use` teh Terminal must be executed in Admin mode.

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
the localhost. Any other port nymber can be chosen, for example `5015`, but in order to be able to use the
Hot Reload only the EP `http://localhost:5000` of the **Blazor Web Dev Server** on the localhost can be used.
Any other port number such as for example `http://localhost:5015` will not provide the Hot Reload whether or not
the watch switch is used in: [dotnet watch run].
Furthemore if the [dotnet run] is used instead of [dotnet watch run] then also the the EP `http://localhost:5000` 
of the **Blazor Web Dev Server** will not proviode Hot Reload.
 

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

> Let's now put everythig together:

The following shows how to:

1. the **bin & obj** folder in the Api subfolder
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
However, SWA provides a **Emulated Service** at the EP: `http://localhost:4280`.
This behaves as the the **Blazor WebAssembly Dev Server** and therefore provides Hot Reload when the [dotnet watch run] is used
as in the following example: 

```
swa start http://localhost:5000 --run "dotnet watch run --launch-profile http --project Client/Client.csproj" --api-location Api --api-port 7174
```

---

## HTTPS: 


The following is an excerpt from this file that shows that relevant part of the configuration, in which 
the  ```"applicationUrl": "https://localhost:7249;http://localhost:5000",``` is where the the port `5000`
is specified.


```
"https": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": true,
      "inspectUri": "{wsProtocol}://{url.hostname}:{url.port}/_framework/debug/ws-proxy?browser={browserInspectUri}",
      "applicationUrl": "https://localhost:7249;http://localhost:5000",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
     }
```

```
"https": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": true,
      "applicationUrl": "https://localhost:7249",
      "inspectUri": "{wsProtocol}://{url.hostname}:{url.port}/_framework/debug/ws-proxy?browser={browserInspectUri}",
      "environmentVariables": {
      "ASPNETCORE_ENVIRONMENT": "Development",
      
     }, 
```

```
"https": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": true,      
      "inspectUri": "{wsProtocol}://{url.hostname}:{url.port}/_framework/debug/ws-proxy?browser={browserInspectUri}",
      "applicationUrl": "https://localhost:7249;https://localhost:5000",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    },

"http": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": true,      
      "inspectUri": "{wsProtocol}://{url.hostname}:{url.port}/_framework/debug/ws-proxy?browser={browserInspectUri}",      
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    }
```

---


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

[How to setup Built-in Authentication for Azure Static Web Apps with Azure Active Directory](https://techcommunity.microsoft.com/t5/apps-on-azure-blog/how-to-setup-built-in-authentication-for-azure-static-web-apps/ba-p/3734709)  
[Authenticating in Azure Static Web Apps](https://www.youtube.com/watch?v=KjSY9vmGz24&t=928s)  
[Build a website using Azure Static Web Apps and Authenticate with AAD](https://www.youtube.com/watch?v=jnwRpEM6GR8)  
[.NET 8 Blazor Authentication & Authorization with Identity](https://www.youtube.com/watch?v=tNzSuwV62Lw)  



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

https://www.syncfusion.com/faq/blazor/general/how-do-i-intercept-routing-in-blazor-before-it-navigates
https://stackoverflow.com/questions/54297711/blazor-how-to-pass-arguments-to-onclick-function 
https://github.com/SyncfusionExamples/Blazor-FAQ-Samples/tree/master/Intercept%20Routing%20in%20Blazor%20Server%20App 

---

https://stackoverflow.com/questions/45592581/cannot-debug-in-vs-code-by-attaching-to-chrome 
https://stackoverflow.com/questions/56267303/blazor-client-side-debugging  
https://stackoverflow.com/questions/64826309/blazor-two-way-binding-text-area-inside-component  

---



