/// <reference path="../../../Scripts/_references.js" />
/// <reference path="../_references.js" />

(function () {
    'use strict';

    // Factory name is handy for logging
    var serviceId = 'DirtyFactory';

    // Define the factory on the module.
    // Inject the dependencies. 
    // Point to the factory definition function.
    angular.module(WWEnvConfigsAppAppName).factory(serviceId, [DirtyFactory]);

    function DirtyFactory() {
        
        //#region Internal Methods        

        var _isObjectDirty = function (ob) {
            var result = false;
            if (ob != null && isObject(ob)) {
                if (typeof ob.IsDirty === 'undefined') {

                }
                else
                    if (ob.IsDirty === true) {
                        result = true;
                    }

            }
            return result;
        }

        var _isArrayDirty = function (arr) {
            if (arr != null && isArray(arr)) {
                $.each(arr, function (index, value) {
                    if (_isObjectDirty(value) === true) {
                        return true;
                    }
                });
            }
            return false;

        }

        var _makeObjectDirty = function (ob) {
            if (ob != null && isObject(ob)) {
                ob.IsDirty = true;
            }
        }

        var _makeObjectClean = function (ob) {
            if (ob != null && isObject(ob) && typeof ob.IsDirty !== 'undefined') {
                delete ob.IsDirty;
            }
        }

        var _makeArrayDirty = function (arr) {
            if (arr != null && isArray(arr)) {
                $.each(arr, function (index, value) {
                    _makeObjectDirty(value);
                });
            }
        }

        var _makeArrayClean = function (arr) {
            if (arr != null && isArray(arr)) {
                $.each(arr, function (index, value) {
                    _makeObjectClean(value);
                });
            }
        }

        var isArray = function (obj) {
            return $.isArray(obj);
        }

        var isObject = function (obj) {
            return $.isPlainObject(obj);
        }

        //#endregion


        var service = {
            IsObjectDirty: _isObjectDirty,
            IsArrayDirty: _isArrayDirty,
            MakeObjectDirty: _makeObjectDirty,
            MakeObjectClean: _makeObjectClean,
            MakeArrayDirty: _makeArrayDirty,
            MakeArrayClean: _makeArrayClean
        };

        return service;


        
    }
})();

