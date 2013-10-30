/// <reference path="../../../Scripts/_references.js" />


var EditValuesController = function ($scope, $routeParams, KeyValueFactory, DirtyFactory) {

    $scope.isBusy = false;
    $scope.data = KeyValueFactory.Values;
    $scope.sortField = ["BuildId", "Locale.ShortName"];

    if (KeyValueFactory.IsReady() == false) {
        $scope.isBusy = true;
        KeyValueFactory.GetValues($routeParams.Id)
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


    $scope.Refresh = function () {
        $scope.isBusy = true;
        KeyValueFactory.GetValues($routeParams.Id)
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

    $scope.SortByBuild = function () {
        $scope.sortField = ["BuildId", "Locale.ShortName"];
    }
    $scope.SortByLocale = function () {
        $scope.sortField = ["Locale.ShortName", "BuildId"];
    }

    $scope.Save = function (kv) {
        //alert(kv.RevertValue);

    }

    $scope.Clear = function (obj) {
        KeyValueFactory.ClearValue(obj);
    }

    $scope.Revert = function (currentObj) {
        KeyValueFactory.RevertValue(currentObj);
    }

    $scope.RevertAll = function () {
        KeyValueFactory.RevertAllValues();
    }

    $scope.ApplyValueToAllLocalesForSameEnv = function (val, envId, envName) {
        //OMessage = {};
        //OMessage.Title = 'Modal Title';
        //OMessage.Body = 'something to go here';
        //OpenOkCancelDialogModal(OMessage);
        var r = confirm("Value \"" + val + "\" will be applied to all keys for Build: " + envName + ". Confirm?");
        if (r == true) {
            KeyValueFactory.ApplyValueToAllLocalesForSameEnv(val, envId);
        }
    }

    $scope.ApplyValueToAllBuildsForSameLocale = function (val, localeId, localeName) {
        var r = confirm("Value \"" + val + "\" will be applied to all keys for Locale: " + localeName + ". Confirm?");
        if (r == true) {
            KeyValueFactory.ApplyValueToAllBuildsForSameLocale(val, localeId);
        }
    }

    $scope.ApplyValueToAll = function (val) {
        var r = confirm("Value \"" + val + "\" will be applied to all keys for All Builds and all Locales. Confirm?");
        if (r == true) {
            KeyValueFactory.ApplyValueToAll(val);
        }
    }

    $scope.UpdateValue = function (updValue) {

        if (DirtyFactory.IsObjectDirty(updValue) === true) {
            $scope.isBusy = true;
            KeyValueFactory.UpdateValue(updValue)
            .then(function () {
                //success
                DirtyFactory.MakeObjectClean(updValue);
                toastr.success("Update Successed");
            },
            function () {
                //error
                toastr.error("Error in update");
            }).then(
                function () {
                    $scope.isBusy = false;
                }
            );
        }
        else {
            toastr.info("No Changes to save");
        }

    }

    $scope.UpdateAll = function () {
        $scope.isBusy = true;

        var changedObs = $.grep($scope.data, function (ob, idx) {
            return (DirtyFactory.IsObjectDirty(ob) === true);
        });
        if (changedObs != null && changedObs.length > 0) {

            KeyValueFactory.UpdateAll(changedObs)
            .then(
            function (result) {
                toastr.success(result + " of " + changedObs.length + " Records Updated.");
                //success
            },
            function () {
                //error
                toastr.error('Error occured while saving/updating.');
            })
            .then(
                function () {
                    $scope.isBusy = false;
                }
            );
        }
        else {
            toastr.info("No Changes to save");
            $scope.isBusy = false;
        }

    }

    $scope.changed = function (ob) {
        DirtyFactory.MakeObjectDirty(ob);
    }

    

   

}

EditValuesController.$inject = ['$scope', '$routeParams', 'KeyValueFactory', 'DirtyFactory'];