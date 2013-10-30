/// <reference path="../../../Scripts/_references.js" />
/// <reference path="../_references.js" />

var BuildsListController = function ($scope, BuildsFactory) {
    $scope.isBusy = false;
    $scope.data = BuildsFactory.Items;
    $scope.sortField = "SiteId";

    if (BuildsFactory.IsReady() == false) {
        $scope.isBusy = true;
        BuildsFactory.GetItems()
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
            BuildsFactory.DeleteItem(this.i.Id, position)
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
        BuildsFactory.GetItems()
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