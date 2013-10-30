/// <reference path="../../../Scripts/_references.js" />
/// <reference path="../_references.js" />

(function () {
    'use strict';

    // Define the directive on the module.
    // Inject the dependencies. 
    // Point to the directive definition function.
    angular.module(WWEnvConfigsAppAppName).directive('ngModelOnblur', [ ngModelOnblur]);
    
    function ngModelOnblur() {
        // Usage:
        // 
        // Creates:
        // 

        function link(scope, elm, attr, ngModelCtrl) {
            if (attr.type === 'radio' || attr.type === 'checkbox') return;

            elm.unbind('input').unbind('keydown').unbind('change');
            elm.bind('blur', function () {
                scope.$apply(function () {
                    ngModelCtrl.$setViewValue(elm.val());
                });
            });
        }

        var directive = {
            link: link,
            require: 'ngModel',
            restrict: 'A'
        };
        return directive;

    }

})();

//WWEnvConfigsApp.directive('ngModelOnblur', function () {
//    return {
//        restrict: 'A',
//        require: 'ngModel',
//        link: function (scope, elm, attr, ngModelCtrl) {
//            if (attr.type === 'radio' || attr.type === 'checkbox') return;

//            elm.unbind('input').unbind('keydown').unbind('change');
//            elm.bind('blur', function () {
//                scope.$apply(function () {
//                    ngModelCtrl.$setViewValue(elm.val());
//                });
//            });
//        }
//    };
//});