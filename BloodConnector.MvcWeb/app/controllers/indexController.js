﻿'use strict';
app.controller('indexController', ['$location', 'authService', function ($location, authService) {

    var vm = this;

    vm.logOut = function () {
        authService.logOut();
        $location.path('/home');
    }

    vm.authentication = authService.authentication;

}]);