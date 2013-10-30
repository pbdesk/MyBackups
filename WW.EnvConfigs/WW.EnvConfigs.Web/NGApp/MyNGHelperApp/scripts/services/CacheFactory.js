/// <reference path="../../../Scripts/_references.js" />

(function () {
    'use strict';

    // Factory name is handy for logging
    var serviceId = 'CacheFactory';

    // Define the factory on the module.
    // Inject the dependencies. 
    // Point to the factory definition function.
    angular.module(MyNGHelperAppName).factory(serviceId, [CacheFactory]);

    function CacheFactory() {

        //#region Internal Methods        


        var _IsSessionCacheDefined = function (cacheKey) {
            if (window.sessionStorage && window.sessionStorage.getItem(cacheKey)) {
                return true;
            }
            else {
                return false;
            }
        }

        var _GetSessionCache = function (cacheKey) {
            return angular.fromJson(sessionStorage.getItem(cacheKey));
        }

        var _SetSessionCache = function (cacheKey, cacheObj) {
            sessionStorage.setItem(cacheKey, angular.toJson(cacheObj));
        }


        //#endregion


        var service = {
            GetSessionCache: _GetSessionCache,
            SetSessionCache: _SetSessionCache,
            IsSessionCacheDefined: _IsSessionCacheDefined
        };

        return service;



    }
})();

