'use strict';
app.controller('indexController', ['$scope', '$location', 'authService', function ($scope, $location, authService) {

    var vm = this;
    vm.$onInit = function () {
        vm.active = {
            "home": true,
            "welcome": false,
            "user": false,
            "logout": false,
            "login": false,
            "signup":false
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