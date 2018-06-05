'use strict';
app.controller('indexController', ['$location', 'authService', 'ngAuthSettings', function ($location, authService, ngAuthSettings) {

    var vm = this;
    vm.baseURI = ngAuthSettings.apiServiceBaseUri;
    vm.$onInit = function () {
        vm.active = {
            "home": true,
            "welcome": false,
            "update": false,
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