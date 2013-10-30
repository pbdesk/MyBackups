/// <reference path="../../../Scripts/_references.js" />

var ExportController = function ($scope, $routeParams, $filter, ExportFactory, LocalesFactory, BuildsFactory, FrameworkFactory) {

    $scope.isBusy = false;
    $scope.isExporting = false;
    $scope.locales = LocalesFactory.Items;
    $scope.selectedLocales = [];
    $scope.builds = BuildsFactory.Items;
    $scope.selectedBuilds = [];
    $scope.CancelText = "Cancel";
    var frameworkId = $routeParams.Id;
    $scope.framework = FrameworkFactory.GetItem(frameworkId);
    if (typeof $scope.framework === 'undefined') {
        ToastError("Current Framework for Export utility cannot be identified.");
    }


    $scope.eiParamsObj = {
        "process": "EXPORT",
        "file": "",
        "dir": "c:\\temp",
        "locale": "ALL",
        "build": "ALL",
        "schema": "App1",
        "track": "1",
        "db": "",
        "frameworkId": frameworkId
    };

   
    

    


    if (ExportFactory.IsReady() == false) {
        $scope.isBusy = true;
        LocalesFactory.GetItems()        
            .then(function () {

                $.each($scope.locales, function (idx, val) {
                    val.checked = false;
                });


                BuildsFactory.GetItems()
                    .then(function () {

                        $.each($scope.builds, function (idx, val) {
                            val.checked = false;

                        });

                    },
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
        
        $scope.isExporting = true;

        $scope.eiParamsObj.build = GetSelectedBuilds();
        $scope.eiParamsObj.locale = GetSelectedLocales();
        
         ExportFactory.RunExport($scope.eiParamsObj)
        .then(function () {
            //success
            ToastSuccess("Env Config files exported to " + $scope.eiParamsObj.dir);
            $scope.CancelText = "Back/Home";
            $scope.isExporting = false;
        },
            function () {
                //error
                ToastError("Error in RunExport[ExportObjectController]");
                $scope.isExporting = false;
            });
    };

    var GetSelectedBuilds = function () {
        var retrunStr = '';
        //var selectedBuilds = $filter('filter')($scope.builds, { checked: true });
        if ($scope.selectedBuilds.length === $scope.builds.length) {
            retrunStr = 'ALL';
        }
        else {
            $.each($scope.selectedBuilds, function (idx, val) {
                retrunStr = retrunStr + val.Name + ",";

            });
        }
        return retrunStr;
    }
    
    var GetSelectedLocales = function () {
        var retrunStr = '';
        //var selectedLocales = $filter('filter')($scope.locales, { checked: true });
        if ($scope.selectedLocales.length === $scope.locales.length) {
            retrunStr = 'ALL';
        }
        else {
            $.each($scope.selectedLocales, function (idx, val) {
                retrunStr = retrunStr + val.ShortName + ",";

            });
        }
        return retrunStr;
    }

    $scope.AllLocalesCheckboxChange = function () {
        if ($scope.AllLocales == "1") {
            $.each($scope.locales, function (idx, val) {
                val.checked = true;
            });
        }
        else {
            $.each($scope.locales, function (idx, val) {
                val.checked = false;
            });
        }
        $scope.selectedLocales = $filter('filter')($scope.locales, { checked: true });
    }

    $scope.LocaleCheckboxChange = function (selected) {        
        if (!selected) {
            $scope.AllLocales = "0";
        }
        $scope.selectedLocales = $filter('filter')($scope.locales, { checked: true });
    }

           
    $scope.AllBuildsCheckboxChange = function () {
        if ($scope.AllBuilds == "1") {
            $.each($scope.builds, function (idx, val) {
                val.checked = true;
            });
        }
        else {
            $.each($scope.builds, function (idx, val) {
                val.checked = false;
            });
        }
        $scope.selectedBuilds = $filter('filter')($scope.builds, { checked: true });
    }

    $scope.BuildCheckboxChange = function (selected) {
        if (!selected) {
            $scope.AllBuilds = "0";
        }
        $scope.selectedBuilds = $filter('filter')($scope.builds, { checked: true });
    }

    $scope.IsFormValid = function (frmInValid) {
        var result = !frmInValid && $scope.selectedLocales.length > 0 && $scope.selectedBuilds.length > 0
        return result;
    }

    
}