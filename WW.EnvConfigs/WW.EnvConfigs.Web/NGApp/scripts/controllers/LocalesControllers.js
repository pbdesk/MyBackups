/// <reference path="../../../Scripts/_references.js" />

var LocalesListController = function ($scope, LocalesFactory) {
    $scope.isBusy = false;
    $scope.data = LocalesFactory.Items;
    $scope.sortField = "SiteId";

    if (LocalesFactory.IsReady() == false) {
        $scope.isBusy = true;
        LocalesFactory.GetItems()
            .then(function () {
                //success

            },
            function () {
                //error
                alert("Error");
            })
            .then(function () {
                $scope.isBusy = false;
            }
            );
    }

    $scope.delete = function (position) {

        var id = this.i.Id;

        var r = confirm("Delete Confirmation? " + this.i.ShortName + "(Site Id = " + this.i.SiteId + ")");
        if (r == true) {
            LocalesFactory.DeleteItem(this.i.Id, position)
            .then(
                function () {
                    //successes
                    $("#siteTR_" + id).fadeOut(2000);
                },
                function () {
                    //error
                    alert("error in delete")
                }
            );
        }
        else {

        }


    }

    $scope.Refresh = function () {
        $scope.isBusy = true;
        LocalesFactory.GetItems()
            .then(function () {
                //success
                //$scope.$apply();
            },
            function () {
                //error
                alert("Error");
            })
            .then(function () {
                $scope.isBusy = false;
            }
            );
    }


}

LocalesListController.$inject = ['$scope', 'LocalesFactory'];


var LocalesCreateController = function ($scope, $window, LocalesFactory) {
    $scope.PageHeading = "Create New Locale";
    $scope.NewLocale = {};
    $scope.Save = function () {

        SitesFactory.addSite($scope.NewLocale)
        .then(
            function () {
                //success
                $window.location = "#/Locales";
            },
            function () {
                //error
                alert("Error in save[LocalesCreateController]");
            }
        );
    }

    $scope.cancel = function () {
    }
}

LocalesCreateController.$inject = ['$scope', '$window', 'LocalesFactory'];


var LocalesEditController = function ($scope, $routeParams, $window, LocalesFactory) {
    $scope.PageHeading = "Edit Locale";
    var originalItem = {};

    var currentId = $routeParams.Id;

    if (currentId > 0) {
        var s = LocalesFactory.GetItem(currentId);
        originalItem =  PBDeskJS.Utils.Clone(s);
        //angular.copy(s, originalItem);
        $scope.NewLocale = s;

    }

    $scope.Save = function () {

        if (DeepCompare($scope.NewLocale, originalItem)) {
            ToastInfo("No Changes to Save");
        }
        else {


            LocalesFactory.UpdateItem($scope.NewLocale)
            .then(
                function () {
                    //success
                    ToastSuccess("Locale information updated successfully")
                    
                },
                function () {
                    //error
                    ToastError("Error while saving Locale information.[LocalesEditController.Save]")
                }
            );
        }
        $window.location = "#/Locales";
    }

    $scope.cancel = function () {
        angular.copy(originalItem, $scope.NewLocale);
    }

}

LocalesEditController.$inject = ['$scope', '$routeParams', '$window', 'LocalesFactory'];





