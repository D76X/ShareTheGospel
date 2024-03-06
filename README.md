
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

### Visual Studio Code with Azure Static Web Apps CLI

1. Install the [Azure Static Web Apps CLI](https://www.npmjs.com/package/@azure/static-web-apps-cli) and [Azure Functions Core Tools CLI](https://www.npmjs.com/package/azure-functions-core-tools).

1. Open the folder in Visual Studio Code.

1. In the VS Code terminal, run the following command to start the Static Web Apps CLI, along with the Blazor WebAssembly client application and the Functions API app:

    ```bash
    swa start http://localhost:5000 --run "dotnet run --project Client/Client.csproj" --api-location Api
    ```

    The Static Web Apps CLI (`swa`) first starts the Blazor WebAssembly client application and connects to it at port 5000, and then starts the Functions API app.

1. Open a browser and navigate to the Static Web Apps CLI's address at `http://localhost:4280`. You'll be able to access both the client application and the Functions API app in this single address. When you navigate to the "Fetch Data" page, you'll see the data returned by the Functions API app.

1. Enter Ctrl-C to stop the Static Web Apps CLI.

## Template Structure

- **Client**: The Blazor WebAssembly sample application
- **Api**: A C# Azure Functions API, which the Blazor application will call
- **Shared**: A C# class library with a shared data model between the Blazor and Functions application
- **ApiIsolated**: A C# Azure Functions API using the .NET isolated execution model, which the Blazor application will call. This version can be used instead of the in-process function app in `Api`.

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

## Azure Functions Core Tools

[Develop Azure Functions locally using Core Tools](https://learn.microsoft.com/en-us/azure/azure-functions/functions-run-local?tabs=windows%2Cisolated-process%2Cnode-v4%2Cpython-v2%2Chttp-trigger%2Ccontainer-apps&pivots=programming-language-csharp#v2)  
[Azure Functions Core Tools](https://www.npmjs.com/package/azure-functions-core-tools)  
[Azure Functions Core Tools](https://www.npmjs.com/package/azure-functions-core-tools/v/2.7.1149)  
[Node Version Manager](https://github.com/nvm-sh/nvm)  

#### Uninstall Azure Functions Core Tools v1 from npm v12

```
node -v
npm list -g --depth=0
func --version
cd 'C:\Users\pb00270\AppData\Roaming\nvm\v12.13.0'
npm uninstall -g azure-functions-core-tools
```

#### Install Azure Functions Core Tools on npm v > 12

```
```

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

