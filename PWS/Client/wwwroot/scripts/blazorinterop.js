var blazorInterop = blazorInterop || {};

blazorInterop.showAlert = function (message) {
    alert(message);
};

blazorInterop.logToConsoleTable = function (obj) {
    console.table(obj);
};

blazorInterop.showPrompt = function (message, defaultValue) {
    return prompt(message, defaultValue);
};

blazorInterop.createEmployee = function (firstName, lastName) {
    return {
        firstName,
        lastName,
        email: firstName + "." + lastName + "@yourmailserverdomain.com"
    }
};

blazorInterop.focusElement = function (htmlElement) {
    htmlElement.focus();
};

blazorInterop.throwsError = function () {
    throw Error("this function has not been implemented!")
};


blazorInterop.callStaticDotNetMethod = function () {

    // Here we use the DotNet.invokeMethodAsync to invoke a .Net
    // static method from JS. The static .Net method becomes available
    // at runtime to JS only when the .Net attribute [JSInvokable] is
    // applied to it. The attrinute register the .Net method on the
    // window.DotNet object.
    //
    // The DotNet.invokeMethodAsync takes three args
    // 1- the name of the .Net assembly that
    // 2- the name of the static method
    // 3- any args that may be in the static method signature
    //
    // Moreover, the DotNet.invokeMethodAsync returns a Promise to
    // the invoking JS code. There is also a DotNet.invokeMethod
    // but can only be used in Blazor Server while the async version
    // can be used in Blazor Server, Cluient and Hosted models.
    var promise = DotNet.invokeMethodAsync(
        "Client",
        "BuildEmail",
        "User");

    promise.then(email => alert(email));

};

blazorInterop.callStaticDotNetMethodCustomIdentifier = function () {
        
    var promise = DotNet.invokeMethodAsync(
        "Client",
        "BuildEmailWithLastName",
        "FirstName",
        "LastName");

    promise.then(email => alert(email));

};

/*
In order to call a .Net instance method from JS it is necessary to
make available to the JS runtime a reference to the instance of the
.Net object on which the method to invoke is located.
 */
blazorInterop.callInstanceDotNetMethod = function (dotNetObjectReference) {

    // take a look at what is passed to JavaScript!
    console.log(dotNetObjectReference);

    // if everything is all right then you may invoke from JS
    // the instance methods that are marked with [JSInvokable]
    // on the .Net class. The name convection is that the name
    // of teh method in JS at Runtime is the camel case of the
    // corresponding .Net instance method unless there is a
    // name override [JSInvokable(Name="myMethod")]

    dotNetObjectReference.invokeMethodAsync("SetWindowSize",
        {
            width: window.innerWidth,
            height: window.innerHeight
        });

};

/*
In order to call a .Net instance method from JS event handler
 */
blazorInterop.registerResizeHandler = function (dotNetObjectReference) {

    function resizeHandler() {

        dotNetObjectReference.invokeMethodAsync("SetWindowSize",
            {
                width: window.innerWidth,
                height: window.innerHeight
            });
    }

    // set up initial values
    resizeHandler();

    // register event handler on the "resize" event of the window object
    window.addEventListener("resize", resizeHandler);
};

blazorInterop.registerOnlineHandler = function (dotNetObjectReference) {

    function onlineHandler() {

        // the navigator object of the window holds the .online bool prop
        dotNetObjectReference.invokeMethodAsync(
            "SetOnlineStatus",
            navigator.onLine);
    }

    // set up initial values
    onlineHandler();

    // register event handler on the "online" and "offline" events 
    // of the window object
    window.addEventListener("online", onlineHandler);
    window.addEventListener("offline", onlineHandler);
};

// this is available on the windows objet therefore on the global scope
// you may want to wrapt it into blazorInterop..
//localStorage.setItem
//localStorage.getItem

