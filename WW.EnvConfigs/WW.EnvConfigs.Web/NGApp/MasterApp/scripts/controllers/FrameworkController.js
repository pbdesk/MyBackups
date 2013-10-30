/// <reference path="../../../Scripts/_references.js" />

var FrameworksListController = function ($scope, FrameworkFactory) {
    $scope.isBusy = false;
    $scope.data = FrameworkFactory.Items;
    $scope.sortField = "SiteId";
    

    if (FrameworkFactory.IsReady() == false) {
        $scope.isBusy = true;
        FrameworkFactory.GetItems(false)
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

        var r = confirm("Delete Confirmation? " + this.i.Name + "(Site Id = " + this.i.SiteId + ")");
        if (r == true) {
            FrameworkFactory.DeleteItem(this.i.Id, position)
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
        FrameworkFactory.GetItems(true)
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

FrameworksListController.$inject = ['$scope', 'FrameworkFactory'];