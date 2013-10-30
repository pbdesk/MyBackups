/// <reference path="../../../Scripts/_references.js" />

(function () {
    'use strict';


    var serviceId = 'WWAppsFactory';

    angular.module(MasterAppName).factory(serviceId, ["$http", "$q", WWAppsFactory]);

    function WWAppsFactory($http, $q) {


        //#region Internal Methods        


        var _url = "/api/Apps/";
        var _items = [];
        var _isInit = false;

        var _isReady = function () {
            return _isInit;
        }

        var _getItems = function (hardRefresh) {

            var deferred = $q.defer();

            if (sessionStorage.WWAppsCache && !hardRefresh) {
                angular.copy(angular.fromJson(sessionStorage.WWAppsCache), _items);
                _isInit = true;
                deferred.resolve();
            }
            else {

                $http.get(_url)
                .then(
                    function (result) {
                        //success

                        var res = PBDeskJS.Utils.ResolveReferences(result.data);

                        $.each(res, function (idx, val) {
                            val.EnvValues = null;

                        });

                        angular.copy(res, _items);
                        sessionStorage.WWAppsCache = angular.toJson(_items);
                        _isInit = true;
                        deferred.resolve();
                    },
                    function () {
                        //error
                        deferred.reject();
                    }
                );

            }

            return deferred.promise;
        }

        var _getItem = function (id) {
            if (_isReady() == true) {
                var result = $.grep(_items, function (e) { return e.Id == id; });
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

        var _addItem = function (newItem) {
            var deferred = $q.defer();

            $http.post(_url, newItem)
            .then(
                function (result) {
                    //success
                    var newlyCreatedItem = result.data;
                    _items.splice(0, 0, newlyCreatedItem);
                    deferred.resolve(newlyCreatedItem);

                },
                function () {
                    //error
                    deferred.reject();
                }
            );

            return deferred.promise;
        }

        var _updItem = function (updItem) {
            var deferred = $q.defer();
            $http.put(_url + updItem.Id, updItem)
            .then(
                function () {
                    //success                
                    deferred.resolve();
                },
                function () {
                    //error
                    deferred.reject();
                }
            );

            return deferred.promise;
        }

        var _delItem = function (id, position) {
            var deferred = $q.defer();
            $http.delete(_url + id)
            .then(
                function () {
                    //success
                    _items.splice(position, 1);
                    deferred.resolve();
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
            Items: _items,
            GetItems: _getItems,
            GetItem: _getItem,
            AddItem: _addItem,
            UpdateItem: _updItem,
            DeleteItem: _delItem,
            IsReady: _isReady
        };

        return service;
    }
})();

