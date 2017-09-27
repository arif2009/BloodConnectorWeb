'use strict';
app.controller('indexController', ['$scope', '$location', 'authService', 'ngAuthSettings', function ($scope, $location, authService, ngAuthSettings) {

    var vm = this;
    vm.baseURI = ngAuthSettings.apiServiceBaseUri;
    vm.$onInit = function () {
        vm.active = {
            "home": true,
            "welcome": false,
            "user": false,
            "logout": false,
            "login": false,
            "signup": false,
            "about":false
        };
    };

    vm.changeSelectedClass = function (menu) {
        Object.keys(vm.active).forEach(function (key) {
            vm.active[key] = key === menu;
        });
    };

    vm.logOut = function () {
        authService.logOut();
        $location.path('/home');
    }

    vm.authentication = authService.authentication;

}]);