'use strict';
app.controller('forgotController', ['localStorageService', 'authService', '$location', 'utilsFactory', function (localStorageService, authService, $location, utilsFactory) {
    var vm = this;

    vm.forgotData = {
        email: ""
    };

    vm.messages = "";

    vm.forgot = function () {
        localStorageService.set('email', vm.forgotData.email);
        authService.forgot(vm.forgotData).then(function (response) {
            $location.path('/sentmail');
        },
        function (err) {
            vm.messages = utilsFactory.processModelstateError(err.data.modelState);
        });
    };

}]);