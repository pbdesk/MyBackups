/// <reference path="../../../Scripts/_references.js" />

(function () {
    'use strict';

    // Factory name is handy for logging
    var serviceId = 'KeyValueFactory';

    // Define the factory on the module.
    // Inject the dependencies. 
    // Point to the factory definition function.
    angular.module(KeyValuesAppName).factory(serviceId, ["$http", "$q", KeyValueFactory]);

    function KeyValueFactory($http, $q) {
        
        //#region Internal Methods       


        var _url = "/api/Values/";
        var _initial = [];
        var _values = [];
        var _isInit = false;

        var _isReady = function () {
            return _isInit;
        }

        var _setInitState = function (v) {
            _isInit = v;
        }

        var _getValues = function (id) {

            var deferred = $q.defer();

            $http.get(_url + 'ByKeyId/' + id)
            .then(
                function (result) {
                    //success
                    var res = PBDeskJS.Utils.ResolveReferences(result.data);
                    
                    $.each(res, function (idx, val) {
                        val.EnvKey.EnvValues = null;
                        val.EnvKey.WWFramework = null;
                        val.Build.EnvValues = null;
                        val.Locale.EnvValues = null;
                    });

                    angular.copy(res, _initial);
                    angular.copy(res, _values);
                    _isInit = true;
                    deferred.resolve();
                },
                function () {
                    //error
                    deferred.reject();
                }
            );

            return deferred.promise;

        }

        var _revertValue = function (currentObj) {
            var originalObj = $.grep(_initial, function (ob, idx) {
                return (ob.Id == currentObj.Id);
            });

            if (originalObj != null) {
                angular.copy(originalObj[0], currentObj);
            }

        }

        var _clearValue = function (valueObj) {

            var item = ($.grep(_initial, function (o, i) {
                return o.Id === valueObj.Id;
            }))[0];
            if (item.KeyValue === '') {                
                valueObj.IsDirty = false;
            }
            else {
                valueObj.IsDirty = true;
            }
            valueObj.KeyValue = '';

        }


        var _revertAllValues = function () {
            angular.copy(_initial, _values);
        }

        var _ApplyValueToAllLocalesForSameEnv = function (val, envId) {
            $.each(_values, function (index, value) {
                if (value.Build.Id == envId) {
                    if (value.KeyValue != val) {
                        value.KeyValue = val;
                        value.IsDirty = true;
                    }

                }
            });
        }

        var _ApplyValueToAllBuildsForSameLocale = function (val, localeId) {
            $.each(_values, function (index, value) {
                if (value.Locale.Id == localeId) {
                    if (value.KeyValue != val) {
                        value.KeyValue = val;
                        value.IsDirty = true;
                    }

                }
            });
        }

        var _ApplyValueToAll = function (val) {

            $.each(_values, function (index, value) {
                if (value.KeyValue != val) {
                    value.KeyValue = val;
                    value.IsDirty = true;
                }
            });
        }

        var _ApplyValueToAllFilteredItems = function (valueOfKey, filteredItems) {
          
            $.each(filteredItems, function (index, value) {

                var matchItems = $.grep(_values, function (item, i) {
                    return (item.Id === value.Id);
                });


                $.each(matchItems, function (matchIdx, matchVal) {
                    matchVal.KeyValue = valueOfKey;
                    matchVal.IsDirty = true;
                });
            });


        }

        var _updateAll = function (list) {
            var deferred = $q.defer();
            $http.put(_url + 'UpdateAll/', list)
            .then(
                function (result) {
                    //success      
                    $.each(_values, function (index, value) {

                        value.IsDirty = false;

                    });
                    angular.copy(_values, _initial);
                    deferred.resolve(result.data);
                },
                function () {
                    //error
                    deferred.reject();
                }
            );

            return deferred.promise;
        }

        var _updValue = function (updValue) {
            var deferred = $q.defer();
            $http.put(_url + 'Update/' + updValue.Id, updValue)
            .then(
                function (result) {
                    //success   
                    result.data.IsDirty = false;
                    _updateInitialValue(result.data);
                    deferred.resolve();
                },
                function () {
                    //error
                    deferred.reject();
                }
            );

            return deferred.promise;
        }

        var _updateInitialValue = function (updValue) {
            var foundAtIndex = -1;
            $.each(_initial, function (index, value) {
                if (value.Id == updValue.Id) {
                    foundAtIndex = index;
                }
            });

            if (foundAtIndex >= 0) {
                _initial[foundAtIndex] = updValue;
            }
        }

        //#endregion
        

        

        var service = {
            Values: _values,
            GetValues: _getValues,
            IsReady: _isReady,
            SetInitState: _setInitState,
            RevertValue: _revertValue,
            RevertAllValues: _revertAllValues,
            ApplyValueToAllLocalesForSameEnv: _ApplyValueToAllLocalesForSameEnv,
            ApplyValueToAllBuildsForSameLocale: _ApplyValueToAllBuildsForSameLocale,
            ApplyValueToAllFilteredItems: _ApplyValueToAllFilteredItems,
            ApplyValueToAll: _ApplyValueToAll,
            UpdateValue: _updValue,
            UpdateAll: _updateAll,
            ClearValue : _clearValue
        };

        return service;
    }
})();

