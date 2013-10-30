/// <reference path="../../../Scripts/_references.js" />

var MasterAppName = 'MasterApp';
var MasterApp = angular.module('MasterApp', ['ngRoute', 'ngAnimate', 'MyNGHelperApp']);
var masterAppViewsPath = "/NGApp/MasterApp/views/";
MasterApp.config(['$routeProvider', function ($routeProvider) {
    $routeProvider
        .when('/', { controller: FrameworksListController, templateUrl: masterAppViewsPath + 'Index.html' })

        .when('/Locales', { controller: LocalesListController, templateUrl: masterAppViewsPath + 'LocalesList.html' })
        .when('/CreateLocale', { controller: LocalesCreateController, templateUrl: masterAppViewsPath + 'LocalesCreate.html' })
        .when('/EditLocale/:Id', { controller: LocalesEditController, templateUrl: masterAppViewsPath + 'LocalesCreate.html' })

        .when('/Builds', { controller: BuildsListController, templateUrl: masterAppViewsPath + 'BuildsList.html' })
        .when('/CreateBuild', { controller: BuildsCreateController, templateUrl: masterAppViewsPath + 'BuildsCreate.html' })
        .when('/EditBuild/:Id', { controller: BuildsEditController, templateUrl: masterAppViewsPath + 'BuildsCreate.html' })


        //.when('/KeyValues/:Id', { controller: EditValuesController, templateUrl: '/NGApp/Views/EditValues.html' })
        //.when('/EditKey/:Id', { controller: KeyEditController, templateUrl: '/NGApp/Views/KeyCreate.html' })
        //.when('/CreateKey', { controller: KeyCreateController, templateUrl: '/NGApp/Views/KeyCreate.html' })

        .when('/Export/:Id', { controller: ExportController, templateUrl: masterAppViewsPath + 'Export.html' })

        .otherwise({ redirectTo: '/' });


}]);

