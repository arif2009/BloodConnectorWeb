﻿
var app = angular.module('BloodConnectorApp', ['ngRoute', 'ngFileUpload', 'LocalStorageModule', 'blockUI', 'toaster']);

app.config(function ($routeProvider) {

    $routeProvider.when("/home", {
        controller: "homeController",
        templateUrl: "/app/views/home.html"
    });

    $routeProvider.when("/profile", {
        controller: "profileController",
        templateUrl: "/app/views/profile-view.html"
    });

    $routeProvider.when("/update", {
        controller: "profileController",
        templateUrl: "/app/views/profile.html"
    });

    $routeProvider.when("/login", {
        controller: "loginController",
        templateUrl: "/app/views/login.html"
    });

    $routeProvider.when("/forgot", {
        controller: "forgotController",
        templateUrl: "/app/views/forgot.html"
    });

    $routeProvider.when("/sentmail", {
        controller: "sentmailController",
        templateUrl: "/app/views/sentmail.html"
    });

    $routeProvider.when("/signup", {
        controller: "signupController",
        templateUrl: "/app/views/signup.html"
    });

    $routeProvider.when("/users", {
        controller: "usersController",
        templateUrl: "/app/views/users.html"
    });

    $routeProvider.when("/help", {
        controller: "aboutController",
        templateUrl: "/app/views/about.html"
    });

    $routeProvider.otherwise({ redirectTo: "/home" });

});

var serviceBase = '/';
//var rootURL = 'http://localhost:14290/';
var rootURL = 'http://www.bloodconnector.org/';
//var rootURL = 'http://local.bloodconnector.org';
app.constant('ngAuthSettings', {
    apiServiceBaseUri: serviceBase,
    rootURL: rootURL,
    clientId: 'ngAuthApp',
    developers: {
        'arif' : 1,
        'jahangir' : 2,
        'mafi' : 3
    }
});

/*app.config(function (blockUIConfig) {
    // Change the default delay to 100ms before the blocking is visible
    //blockUIConfig.delay = 100;
});*/

app.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
});

app.run(['authService', function (authService) {
    authService.fillAuthData();
}]);


