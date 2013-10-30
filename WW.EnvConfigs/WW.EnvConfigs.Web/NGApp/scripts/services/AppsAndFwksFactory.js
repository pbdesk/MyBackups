/// <reference path="../../../Scripts/_references.js" />
/// <reference path="../_references.js" />

(function () {
    'use strict';

    // Factory name is handy for logging
    var serviceId = 'AppsAndFwksFactory';

    // Define the factory on the module.
    // Inject the dependencies. 
    // Point to the factory definition function.
    angular.module(WWEnvConfigsAppAppName).factory(serviceId, ["$http", "$q", AppsAndFwksFactory]);

    function AppsAndFwksFactory($http, $q) {
        // Define the functions and properties to reveal.
       

        function getData() {

        }

        //#region Internal Methods        

        //#endregion

        var service = {
            getData: getData
        };

        return service;
    }
})();