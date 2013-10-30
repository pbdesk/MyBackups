/// <reference path="../../../Scripts/_references.js" />
/// <reference path="../_references.js" />

var ExportObjectController = function ($scope, $window, ExportFactory, LocalesFactory, BuildsFactory) {

    $scope.isBusy = false;
    $scope.locales = LocalesFactory.Items;
    $scope.builds = BuildsFactory.Items;

    $scope.eiParamsObj = {
        "process": "EXPORT",
        "file": "",
        "dir": "c:\\temp",
        "locale": "ALL",
        "build": "ALL",
        "schema": "App1",
        "track": "1",
        "db": "",
    };

   
    

    


    if (ExportFactory.IsReady() == false) {
        $scope.isBusy = true;
        LocalesFactory.GetItems()        
            .then(function () {
                BuildsFactory.GetItems()
                    .then(function () { },
                          function () { ToastError("Error  Loading Builds"); });
            
            },
            function () {
                ToastError("Error  Loading Locales");
            })
            .then(function () {
                $scope.isBusy = false;
            }
            );
    }



    $scope.RunExport = function () {
       
       
         ExportFactory.RunExport($scope.eiParamsObj)
        .then(function () {
            //success
            ToastSuccess("Env Config files exported to " + $scope.eiParamsObj.dir);
            $window.location = "#/";
        },
            function () {
                //error
                ToastError("Error in RunExport[ExportObjectController]");
            });
    };

}