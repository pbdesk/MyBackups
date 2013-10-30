/// <reference path="../../../Scripts/_references.js" />

var KeyValuesAppName = 'KeyValuesApp';
var KeyValuesApp = angular.module('KeyValuesApp', ['ngRoute', 'ngAnimate', 'MyNGHelperApp']);
var keyValuesAppViewsPath = "/NGApp/KeyValuesApp/views/";
KeyValuesApp.config(['$routeProvider', function ($routeProvider) {
    $routeProvider
        .when('/', { templateUrl: keyValuesAppViewsPath + 'Index.html' })

        //.when('/Locales', { controller: LocalesListController, templateUrl: '/NGApp/Views/LocalesList.html' })
        //.when('/CreateLocale', { controller: LocalesCreateController, templateUrl: '/NGApp/Views/LocalesCreate.html' })
        //.when('/EditLocale/:Id', { controller: LocalesEditController, templateUrl: '/NGApp/Views/LocalesCreate.html' })

        //.when('/Envs', { controller: EnvsListController, templateUrl: '/SPA/Views/EnvList.html' })
        //.when('/EnvCreate', { controller: EnvCreateController, templateUrl: '/SPA/Views/EnvCreate.html' })
        //.when('/EnvEdit/:Id', { controller: EnvEditCtrl, templateUrl: '/SPA/Views/EnvCreate.html' })

        .when('/KeyValues/:Id', { controller: EditValuesController, templateUrl: keyValuesAppViewsPath + 'EditValues.html' })
        .when('/EditKey/:Id', { controller: KeyEditController, templateUrl: keyValuesAppViewsPath + 'KeyCreate.html' })
        .when('/CreateKey', { controller: KeyCreateController, templateUrl: keyValuesAppViewsPath + 'KeyCreate.html' })

        //.when('/Export', { controller: ExportObjectController, templateUrl: '/NGApp/Views/Export.html' })
        
        .otherwise({ redirectTo: '/' });
    

}]);

