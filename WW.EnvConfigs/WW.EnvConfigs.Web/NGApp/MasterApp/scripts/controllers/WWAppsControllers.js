/// <reference path="../../../Scripts/_references.js" />

var WWAppsListController = function ($scope, WWAppsFactory) {
    $scope.isBusy = false;
    $scope.data = WWAppsFactory.Items;
    $scope.sortField = "SiteId";

    if (WWAppsFactory.IsReady() == false) {
        $scope.isBusy = true;
        WWAppsFactory.GetItems(false)
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
            WWAppsFactory.DeleteItem(this.i.Id, position)
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
        WWAppsFactory.GetItems(true)
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

WWAppsListController.$inject = ['$scope', 'WWAppsFactory'];