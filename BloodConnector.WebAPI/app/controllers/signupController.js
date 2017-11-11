'use strict';
app.controller('signupController', ['authService', 'dataService', 'utilsFactory', 'toaster', function (authService, dataService, utilsFactory, toaster) {
    var vm = this;
    vm.savedSuccessfully = false;
    vm.messages = [];
    vm.bloodGrups = [];
    vm.registration = {
        firstName: "",
        lastName: "",
        nikeName: "",
        bloodGiven: "",
        email: "",
        phoneNumber: "",
        bloodGroupId: "",
        password: "",
        confirmPassword: "",
        termsAndCondition: 0
    };

    vm.$onInit = function () {
        dataService.getBloodGroup().then(function(result) {
            vm.bloodGroups = result.data;
        });
    };

    vm.signUp = function () {

        if (!vm.registration.bloodGiven) {
            vm.registration.bloodGiven = 0;
        }

        authService.saveRegistration(vm.registration).then(function (response) {
            vm.savedSuccessfully = true;
            toaster.pop({
                type: 'success',
                closeButton: true,
                body: 'Congratulation. You are joined successfully with us !!',
                timeout: 10000,
                onShowCallback: function () {
                    var loginData = { email: vm.registration.email, password: vm.registration.password };
                    authService.login(loginData).then(function (response) {
                        utilsFactory.redirectToUsers();
                    });
                }
            });
          },
         function (error) {
             We.scroll(0);
             vm.messages = utilsFactory.processModelstateError(error.data.modelState);
         });
    };

}]);