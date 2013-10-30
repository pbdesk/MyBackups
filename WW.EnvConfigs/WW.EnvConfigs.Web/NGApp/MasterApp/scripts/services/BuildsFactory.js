/// <reference path="../../../Scripts/_references.js" />

(function () {
    'use strict';

    var serviceId = 'BuildsFactory';
    angular.module(MasterAppName).factory(serviceId, ["$http", "$q", 'CacheFactory', BuildsFactory ]);
    function BuildsFactory($http, $q, CacheFactory) {

        //#region Internal Methods        

        var _url = "/api/Builds/";
        var _items = [];
        var _isInit = false;
        var cacheKeyName = "BuildsCache";

        var _isReady = function () {
            return _isInit;
        }

        var _getItems = function (hardRefresh) {

            var deferred = $q.defer();
           
            if (CacheFactory.IsSessionCacheDefined(cacheKeyName) && !hardRefresh) {
                angular.copy(CacheFactory.GetSessionCache(cacheKeyName), _items);
                _isInit = true;
                deferred.resolve();
            }
            else {

                $http.get(_url)
                .then(
                    function (result) {
                        //success
                        angular.copy(result.data.$values, _items);
                        //sessionStorage.BuildsCache = angular.toJson(_items);
                        CacheFactory.SetSessionCache(cacheKeyName, _items);
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

        var _addItem = function (newEnv) {
            var deferred = $q.defer();

            $http.post(_url, newEnv)
                .success(function (result, status, headers, config) {

                    
                    _items.splice(0, 0, result);
                    CacheFactory.SetSessionCache(cacheKeyName, _items);
                    deferred.resolve(result);

                })
                .error(function (result, status, headers, config) {
                    deferred.reject(result, status);
                });
            //$http.post(_url, newEnv)
            //.then(
            //    function (result) {
            //        //success
            //        var newlyCreatedEnv = result.data;
            //        _items.splice(0, 0, newlyCreatedEnv);
            //        CacheFactory.SetSessionCache(cacheKeyName, _items);
            //        deferred.resolve(newlyCreatedEnv);

            //    },
            //    function () {
            //        //error
            //        deferred.reject();
            //    }
            //);

            return deferred.promise;
        }

        var _updItem = function (updEnv) {
            var deferred = $q.defer();
            $http.put(_url + updEnv.Id, updEnv)
            .then(
                function () {
                    //success  
                    CacheFactory.SetSessionCache(cacheKeyName, _items);
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
                    CacheFactory.SetSessionCache(cacheKeyName, _items);
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
            UpdItem: _updItem,
            DelItem: _delItem,
            IsReady: _isReady
        };

        return service;
    }
})();

