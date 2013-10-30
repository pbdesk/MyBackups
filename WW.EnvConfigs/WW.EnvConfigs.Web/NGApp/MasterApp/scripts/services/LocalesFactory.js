/// <reference path="../../../Scripts/_references.js" />

(function () {
    'use strict';

    var serviceId = 'LocalesFactory';
    angular.module(MasterAppName).factory(serviceId, ['$http', '$q', 'CacheFactory', LocalesFactory]);
    function LocalesFactory($http, $q, CacheFactory) {
        

        //#region Internal Methods        


        var _url = "/api/locales/";
        var _items = [];
        var _isInit = false;
        var cacheKeyName = "LocalesCache";
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
                .success(function (result, status, headers, config) {
                    var res = PBDeskJS.Utils.ResolveReferences(result);

                    $.each(res, function (idx, val) {
                        val.EnvValues = null;

                    });

                    angular.copy(res, _items);
                    //sessionStorage.LocalesCache = angular.toJson(_items);
                    CacheFactory.SetSessionCache(cacheKeyName, _items);
                    _isInit = true;
                    deferred.resolve();
                })
                .error(function (result, status, headers, config) {
                    deferred.reject(result, status);
                });
                //.then(
                //    function (result) {
                //        //success

                //        var res = PBDeskJS.Utils.ResolveReferences(result.data);

                //        $.each(res, function (idx, val) {
                //            val.EnvValues = null;

                //        });

                //        angular.copy(res, _items);
                //        //sessionStorage.LocalesCache = angular.toJson(_items);
                //        CacheFactory.SetSessionCache(cacheKeyName, _items);
                //        _isInit = true;
                //        deferred.resolve();
                //    },
                //    function () {
                //        //error
                //        deferred.reject();
                //    }
                //);
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
            .success(function (result, status, headers, config) {
                var newlyCreatedItem = result.data;
                _items.splice(0, 0, newlyCreatedItem);
                CacheFactory.SetSessionCache(cacheKeyName, _items);
                deferred.resolve(newlyCreatedItem);
            })
            .error(function (result, status, headers, config) {
                deferred.reject(result, status);
            });

            return deferred.promise;

            //.then(
            //    function (result) {
            //        //success
            //        var newlyCreatedItem = result.data;
            //        _items.splice(0, 0, newlyCreatedItem);
            //        deferred.resolve(newlyCreatedItem);

            //    },
            //    function () {
            //        //error
            //        deferred.reject();
            //    }
            //);

            
        }

        var _updItem = function (updItem) {
            var deferred = $q.defer();
            $http.put(_url + updItem.Id, updItem)
            .success(function (result, status, headers, config) {
                CacheFactory.SetSessionCache(cacheKeyName, _items);
                deferred.resolve();
            })
            .error(function (result, status, headers, config) {
                deferred.reject(result, status);
            });
            //.then(
            //    function () {
            //        //success  
            //        CacheFactory.SetSessionCache(cacheKeyName, _items);
            //        deferred.resolve();
            //    },
            //    function () {
            //        //error
            //        deferred.reject();
            //    }
            //);

            return deferred.promise;
        }

        var _delItem = function (id, position) {
            var deferred = $q.defer();
            $http.delete(_url + id)
            .success(function (result, status, headers, config) {
                _items.splice(position, 1);
                CacheFactory.SetSessionCache(cacheKeyName, _items);
                deferred.resolve();
            })
            .error(function (result, status, headers, config) {
                deferred.reject(result, status);
            });
            return deferred.promise;
            //.then(
            //    function () {
            //        //success
            //        _items.splice(position, 1);
            //        CacheFactory.SetSessionCache(cacheKeyName, _items);
            //        deferred.resolve();
            //    },
            //    function () {
            //        //error
            //        deferred.reject();
            //    }
            //);

            

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

