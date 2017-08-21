'use strict';
app.controller('signupController', ['$location', '$timeout', 'authService', 'dataService', 'utilsFactory', function ($location, $timeout, authService, dataService, utilsFactory) {
    var vm = this;
    vm.savedSuccessfully = false;
    vm.messages = [];
    vm.bloodGrups = [];
    vm.registration = {
        firstName: "",
        lastName: "",
        nikeName: "",
        email: "",
        phoneNumber: "",
        bloodGroupId: "",
        password: "",
        confirmPassword: ""
    };

    vm.$onInit = function () {
        dataService.getBloodGroup.then(function(result) {
            vm.bloodGroups = result.data;
        });
    };

    vm.signUp = function () {

        authService.saveRegistration(vm.registration).then(function (response) {

            vm.savedSuccessfully = true;
            vm.messages = ["User has been registered successfully, you will be redicted to login page in 2 seconds."];
            startTimer();

        },
         function (response) {
             vm.messages = utilsFactory.processModelstateError(response.data.modelState);
         });
    };

    var startTimer = function () {
        var timer = $timeout(function () {
            $timeout.cancel(timer);
            $location.path('/login');
        }, 2000);
    }

}]);