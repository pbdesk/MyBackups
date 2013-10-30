/// <reference path="../../../Scripts/_references.js" />
/// <reference path="../_references.js" />


(function () {
    'use strict';

    // Factory name is handy for logging
    var serviceId = 'KeysFactory';

    // Define the factory on the module.
    // Inject the dependencies. 
    // Point to the factory definition function.
    angular.module(WWEnvConfigsAppAppName).factory(serviceId, ["$http", "$q", KeysFactory]);

    function KeysFactory($http, $q) {
       
        //#region Internal Methods        
        var _url = "/api/Keys/";
        var _keys = [];
        var _isInit = false;

        var _isReady = function () {
            return _isInit;
        }

        var _getKeys = function () {

            var deferred = $q.defer();

            $http.get(_url)
            .then(
                function (result) {
                    //success
                    angular.copy(result.data.$values, _keys);
                    _isInit = true;
                    deferred.resolve(_keys);
                },
                function () {
                    //error
                    deferred.reject();
                }
            );

            return deferred.promise;
        }

        var _getKey = function (id) {
            if (_isReady() == true) {
                var result = $.grep(_keys, function (e) { return e.Id == id; });
                if (result.length == 0) {
                    return null;
                } else if (result.length == 1) {
                    return result[0];
                } else {
                    return null;
                }
            }
            else
                return null;
        }

        var _createKey = function(newKey){
            var deferred = $q.defer();
            $http.post(_url, newKey)
            .then(
                function (result) {
                    //success
                    var newlyCreatedKey = result.data;
                    _keys.splice(0, 0, newlyCreatedKey);
                    deferred.resolve(newlyCreatedKey);

                },
                function () {
                    //error
                    deferred.reject();
                }
            );

            return deferred.promise;

}
        
        var _updateKey = function (updKey) {
            var deferred = $q.defer();
            $http.put(_url + updKey.Id, updKey)
            .then(
                function (result) {
                    //success    
                    var updatedKey = result.data;

                    var res = $.grep(_keys, function (e) { return e.Id == updatedKey.Id; });
                    angular.copy(updatedKey, res[0])

                    deferred.resolve(updatedKey);
                },
                function () {
                    //error
                    deferred.reject();
                }
            );

            return deferred.promise;
        }

        //#endregion


        var service = {
            Keys: _keys,
            GetKeys: _getKeys,
            GetKey: _getKey,
            IsReady: _isReady,
            CreateKey: _createKey,
            UpdateKey: _updateKey
        };

        return service;

       

       
    }
})();


