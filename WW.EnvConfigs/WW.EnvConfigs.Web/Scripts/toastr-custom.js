/// <reference path="toastr.js" />


function ToastLog(message) {
    if(window && window.console && window.console.log)
    {
        window.console.log(message);
    }
}
function ToastSuccess(message, title) {
    var toasterSuccessOptions = {

    };
    toastr.success(message, title, toasterSuccessOptions);
    ToastLog("Success: "  + message)
}

function ToastError(message, title) {
    var toasterErrorOptions = {
        "closeButton": true,
        "timeOut": "0"
    };
    
    toastr.error(message, title, toasterErrorOptions);
    ToastLog("Error: " + message)
}

function ToastInfo(message, title) {
    var toasterInfoOptions = {

    };
    toastr.info(message, title, toasterInfoOptions);
    ToastLog("Info: "  + message)
}

function ToastWarning(message, title) {
    var toasterWarningOptions = {

    };
    toastr.warning(message, title, toasterWarningOptions);
    ToastLog("Warning: "  + message)
}


