/// <reference path="../../../Scripts/_references.js" />
/// <reference path="../_references.js" />

var KeyListController = function ($scope, KeysFactory, KeyValueFactory) {
    $scope.isBusy = false;
    $scope.data = KeysFactory.Keys;
    $scope.sortField = "KeyName";

    if (KeysFactory.IsReady() == false) {
        $scope.isBusy = true;

        KeysFactory.GetKeys()
            .then(function (result) {
                //success
                ToastSuccess(result.length + " Keys Loaded.")
                
            },
            function () {
                //error
                ToastError("Error getting list of keys. [KeyListController]");
                
            })
            .then(function () {
                $scope.isBusy = false;
            });
    }


    $scope.Refresh = function () {
        $scope.isBusy = true;
        KeysFactory.GetKeys()
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

    $scope.SetInitStateForKeyValueFactoryToFalse = function () {
        KeyValueFactory.SetInitState(false);
    }

}

KeyListController.$inject = ['$scope', 'KeysFactory', 'KeyValueFactory'];

var KeyCreateController = function ($scope, $window, KeyListFactory) {
    $scope.PageHeading = "Create New Key";
    $scope.NewKey = {};
    $scope.NewKey.CreatedBy = Global_UserName;
    $scope.NewKey.IsActive = true;
    $scope.Save = function () {

        KeyListFactory.CreateKey($scope.NewKey)
        .then(
            function () {
                //success
                $window.location = "#/";
            },
            function () {
                //error
                alert("Error in save[KeyCreateController]");
            }
        );
    }

    $scope.cancel = function () {
    } 
}

KeyCreateController.$inject = ['$scope', '$window', 'KeysFactory'];

var KeyEditController = function ($scope, $routeParams, $window, KeyListFactory) {
    $scope.PageHeading = "Edit Key";
    var Original = null;
    var Current = null;

    var currentId = $routeParams.Id;

    if (currentId > 0) {
        var s = KeyListFactory.GetKey(currentId);
        //Original = Clone(s);
        Original = $.extend(true, {}, s);
        Current = $.extend(true, {}, s);
        $scope.NewKey = Current;

    }

    $scope.Save = function (opt) {

        KeyListFactory.UpdateKey($scope.NewKey)
        .then(
            function (result) {
                //success
                if (opt === false) {
                    $window.location = "#/";
                }
                else {
                    $window.location = "#/KeyValues/" + result.Id;
                }

            },
            function () {
                //error
                alert("Error in Update");
            }
        );
    }

    $scope.cancel = function () {
        angular.copy(Original, $scope.NewKey);
    }

}

KeyEditController.$inject = ['$scope', '$routeParams', '$window', 'KeysFactory'];