/// <reference path="../../../Scripts/_references.js" />

var BuildsListController = function ($scope, BuildsFactory) {
    $scope.isBusy = false;
    $scope.data = BuildsFactory.Items;
    $scope.sortField = "SiteId";

    if (BuildsFactory.IsReady() == false) {
        $scope.isBusy = true;
        BuildsFactory.GetItems(false)
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

        var r = confirm("Build Delete Confirmation? Deleting Build " + this.i.Name );
        if (r == true) {
            BuildsFactory.DelItem(this.i.Id, position)
            .then(
                function () {
                    //successes
                    $("#itemTR_" + id).fadeOut(2000);
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
        BuildsFactory.GetItems(true)
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

BuildsListController.$inject = ['$scope', 'BuildsFactory'];

var BuildsCreateController = function ($scope, $window, BuildsFactory) {
    $scope.isBusy = false;
    $scope.PageHeading = "Create New Build";
    $scope.NewBuild = {};
    $scope.Save = function () {
        $scope.isBusy = true;
        var success = false;
        BuildsFactory.AddItem ($scope.NewBuild)
        .then(
            function (newlyCreatedItem) {
                //success
                success = true;
                $scope.isBusy = false;
                $window.location = "#/Builds";
            },
            function (errorOb, status) {
                //error
                success = false;
                ToastError("Error in Save[BuildsCreateController]. Please refer to server logs.");
            }
        )
        .then(function () {
            $scope.isBusy = false;
        });
    }

    $scope.cancel = function () {
    }
}

BuildsCreateController.$inject = ['$scope', '$window', 'BuildsFactory'];


var BuildsEditController = function ($scope, $routeParams, $window, BuildsFactory) {
    $scope.PageHeading = "Edit Build";
    var originalItem = {};

    var currentId = $routeParams.Id;

    if (currentId > 0) {
        var s = BuildsFactory.GetItem(currentId);
        originalItem = PBDeskJS.Utils.Clone(s);
        //angular.copy(s, originalItem);
        $scope.NewBuild = s;

    }

    $scope.Save = function () {

        if (PBDeskJS.Utils.DeepCompare($scope.NewBuild, originalItem)) {
            ToastInfo("No Changes to Save");
        }
        else {


            BuildsFactory.UpdItem($scope.NewBuild)
            .then(
                function () {
                    //success
                    ToastSuccess("Build information updated successfully")

                },
                function () {
                    //error
                    ToastError("Error while saving Build information.[BuildsEditController.Save]")
                }
            );
        }
        $window.location = "#/Builds";
    }

    $scope.cancel = function () {
        angular.copy(originalItem, $scope.NewBuild);
    }

}

BuildsEditController.$inject = ['$scope', '$routeParams', '$window', 'BuildsFactory'];