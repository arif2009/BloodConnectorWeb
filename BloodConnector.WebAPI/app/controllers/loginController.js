'use strict';
app.controller('loginController', ['$location', 'authService', 'ngAuthSettings', function ($location, authService) {
    var vm = this;

    vm.loginData = {
        email: "",
        password: ""
    };

    vm.message = "";

    vm.login = function () {
        authService.login(vm.loginData).then(function (response) {
            $location.path('/users');
        },
        function (err) {
            vm.message = err.data.error_description;
        });
    };

}]);
