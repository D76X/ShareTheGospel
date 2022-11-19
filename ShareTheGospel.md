# Share The Gospel

The site is published on Azure to an Azure Web App
[ShareTheGospel](https://gentle-hill-0ba167403.1.azurestaticapps.net/)

To run the application locally cd into the Client folder in which the Client.csproj
is located and execute the following

```
cd C:\VSProjects\ShareTheGospel\Client 
C:\VSProjects\ShareTheGospel\Client> dotnet clean
C:\VSProjects\ShareTheGospel\Client> dotnet RUN
```

### Debugging and Hot Reload

It is possible to use 

https://dev.to/sacantrell/vs-code-and-blazor-wasm-debug-with-hot-reload-5317
https://www.youtube.com/watch?v=4S3vPzawnoQ

```
dotnet watch -p C:\VSProjects\ShareTheGospel\Client\ run
```

[Hot Reload](https://stackoverflow.com/questions/65340426/how-to-activate-hot-reload-on-save-with-blazor-wasm)  
[Debug ASP.NET Core Blazor WebAssembly](https://learn.microsoft.com/en-us/aspnet/core/blazor/debug?view=aspnetcore-6.0&abs=visual-studio-code#debug-a-standalone-blazor-webassembly-app)  

[Troubleshooting .NET Blazor WASM Debugging](https://khalidabuhakmeh.com/troubleshooting-dotnet-blazor-wasm-debugging)  

???
Detected multiple function projects in the same workspace folder. 
You must either set the default or use a multi-root workspace.

[Cannot debug Blazor wasm](https://stackoverflow.com/questions/72037423/cannot-debug-blazor-wasm)  
Unable to launch browser: the URL's protocol must be one of ws wss

[VS Code and Blazor WASM: Debug with Hot Reload](https://dev.to/sacantrell/vs-code-and-blazor-wasm-debug-with-hot-reload-5317)  

---

## Technical Details

### [Node Version Manager for Windows](https://github.com/coreybutler/nvm-windows)

On Windows if there is the need to manage different version of NodeJS on the same machine
and switch between them according to the project at hand then *before installing any version of NodeJS** 
on the Windows OS the **NVMW** should be installed. 

This tools make it possible to manage this scenario and easily switch between the versions
of NodeJS installed on the system from a terminal. In order to install and use this tool one MUST use an **admin** terminal
session.

For example, at the time of writing, the setup in use is the following.

```
nvm --version
Running version 1.1.9.

 nvm list
  * 16.16.0 (Currently using 64-bit executable)
    12.13.0
```

The **NVM** manages the isolation between version of NodeJS on the same OS through folders
as in the example below.

```
The location of the nvm.exe
C:\Users\pb00270\AppData\Roaming\nvm  

The locations of the version of NodeJS installed on the system and managed by NVM
C:\Users\pb00270\AppData\Roaming\nvm\v12.13.0
C:\Users\pb00270\AppData\Roaming\nvm\v16.16.0

The location of the GLOBALLY INSTALLED NPM modules for each of the the NodeJS installed version.
These are those modules that are installed via **npm -i** with the **-g** switch.
C:\Users\pb00270\AppData\Roaming\nvm\v12.13.0\node_modules
C:\Users\pb00270\AppData\Roaming\nvm\v16.16.0\node_modules
```

This particular project makes use of two tools that requires NodeJS to run.

---

### [Azure Functions Core Tools](https://www.npmjs.com/package/azure-functions-core-tools)

The Azure Functions Core Tools module has been installed as local npm package instead of locally.
This is in order to use differen versions of the AFCTs on the same machine independently on 
projects that have different requiremnts in terms of the AFs Runtime they target.

For example, In this project the location of the locally installed NPM package and executable 
for the AFCTs is as below.

```
C:\VSProjects\ShareTheGospel\node_modules\azure-functions-core-tools\bin
```

The instructions to install the AFCTs as NPM module are found at [Azure Functions Core Tools](https://www.npmjs.com/package/azure-functions-core-tools) amd are repeated below for convenience. 

Firstly, if your system has different version of NodeJS installed managed my Node Version Manager for Windows then switch to the version of NodeJS that is most suitable or recommended to install the tool as a Node package.
In order to be able to use **nvm use** the terminal window in which the command is run must run as an 
admin account on the system. If the terminal used is the embedded terminal in VS Code the only way to
run it as admin is that the VS Code instance is itself run with elevated admin account, otherwise the
**nvm use** will fail.

```
nvm use 16.16.0
```

Then the commands to install the tool as a Node Package are illustrated.

```
npm i -g azure-functions-core-tools@4 --unsafe-perm true
```

However, in order to install the AFCTs as LOCAL NPM module the `-g` switch must be omitted.
In general the version of the tool to install is controlled by the @X value at the end of 
azure-functions-core-tools@X in this specific case it is X=4 as this is the latest version 
of the tool vailable at the time of writing. 

```
cd C:\VSProjects\ShareTheGospel
npm i azure-functions-core-tools@4 --unsafe-perm true
```

Once this command has been executed then NPM will have the module installed LOCALLY into

```
C:\VSProjects\ShareTheGospel\node_modules
```

#### Run the [Azure Functions Core Tools](https://www.npmjs.com/package/azure-functions-core-tools)

The following is what happens if you try to run the `func` command from the project folder.
The same happens in any other folder or terminal i.e. Powershell as the `func` executable 
is not automatically added to the PATH. 

In the following example we switch to the installed NodeJS 16.16.0 as we know that for this 
NodeJS installed there is no globally installed Azure Functions installed package then we 
run the **func** command on the path of the application and show that it fails as this command
in not on the PATH when the version NodeJS 16.16.0 is selected via NVM.

```
PS C:\VSProjects\ShareTheGospel> nvm list
    16.16.0
  * 12.13.0 (Currently using 64-bit executable)
  
PS C:\VSProjects\ShareTheGospel> nvm use 16.16.0
Now using node v16.16.0 (64-bit)

PS C:\VSProjects\ShareTheGospel> 

PS C:\VSProjects\ShareTheGospel> func --help

func : The term 'func' is not recognized as the name of a cmdlet, function, script file, or operable program. Check the spelling of the name, or if a path was included, verify that the path is correct and try again.
At line:1 char:1
+ func --help
+ ~~~~
    + CategoryInfo          : ObjectNotFound: (func:String) [], CommandNotFoundException
    + FullyQualifiedErrorId : CommandNotFoundException
```

However, it is still possible to run the **Azure Functions local node package** from the `.bin` folder
as illustrated by the following example as `.\func`. It is important to notice that what is found in this
case is **Azure Functions Core Tools 4** which indeed is teh version of the locally installed node package.

```

PS C:\VSProjects\ShareTheGospel\node_modules\.bin> func

func : The term 'func' is not recognized as the name of a cmdlet, function, script file, or operable program. Check the spelling of the name, or if a path was included, verify that 
the path is correct and try again.

At line:1 char:1
+ func
+ ~~~~
    + CategoryInfo          : ObjectNotFound: (func:String) [], CommandNotFoundException
    + FullyQualifiedErrorId : CommandNotFoundException
 

Suggestion [3,General]: 

The command func was not found, but does exist in the current location. Windows PowerShell does not load commands from the current location by default. If you trust this command, instead type: ".\func". See "get-help about_Command_Precedence" for more details.

PS C:\VSProjects\ShareTheGospel\node_modules\.bin> .\func

Azure Functions Core Tools
Core Tools Version:       4.0.4736 Commit hash: N/A  (64-bit)
Function Runtime Version: 4.8.1.18957

...

```

However, if any Azure Functions Runtime is installed as **global** node package the
**func** command is registered and found on the system PATH as the following example 
illustrates. In this case the  **Azure Functions Core Tools 1** has been installed 
as global node package with the **NodeJS 12.13.0** thus when the **func** command is 
run, although the command path is located in the **.bin** folder the 
**Azure Functions Core Tools 1** is found instead of the **Azure Functions Core Tools 4**.
However, if in the same condition with the **NodeJS 12.13.0** selected the **.\func**
command is run instead the version found is **Azure Functions Core Tools 4**.

#### Warning!

As the examples show whether the **NodeJS 12.13.0** or the **NodeJS 16.16.0**  is currently 
selected as the NodeJS system instance via NVM it is always possible to run the AFCT @4 
**Azure Functions Core Tools 4** from `C:\VSProjects\ShareTheGospel\node_modules\.bin`
by means of the `.\func` command in place of the `func` command. 

However, I am not sure that the **Azure Functions Core Tools 4** would run correctly on
the **NodeJS 12.13.0**, although the [documentation](https://www.npmjs.com/package/azure-functions-core-tools)
does not explicetly required any minimum version of NodeJS to run on, thus in theory AFCT @4
should work on **NodeJS 12.13.0** as well as **NodeJS 16.16.0**.


```
PS C:\VSProjects\ShareTheGospel\node_modules\.bin> nvm list       
  * 16.16.0 (Currently using 64-bit executable)
    12.13.0
PS C:\VSProjects\ShareTheGospel\node_modules\.bin> nvm use 12.13.0
Now using node v12.13.0 (64-bit)

PS C:\VSProjects\ShareTheGospel\node_modules\.bin> func

Azure Functions Core Tools (1.0.28)
Function Runtime Version: 1.0.13154.0
Usage: func [context] [context] <action> [-/--options]

PS C:\VSProjects\ShareTheGospel\node_modules\.bin> .\func

Azure Functions Core Tools       
Core Tools Version:       4.0.4736 Commit hash: N/A  (64-bit)
Function Runtime Version: 4.8.1.18957

```

---
