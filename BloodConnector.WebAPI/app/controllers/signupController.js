'use strict';
app.controller('signupController', ['authService', 'dataService', 'utilsFactory', function (authService, dataService, utilsFactory) {
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
            utilsFactory.redirectToLogin();
          },
         function (response) {
             We.scroll(0);
             vm.messages = utilsFactory.processModelstateError(response.data.modelState);
         });
    };

}]);