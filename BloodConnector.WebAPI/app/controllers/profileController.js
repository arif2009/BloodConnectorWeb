'use strict';
app.controller('profileController', ['$scope', 'dataService', 'authService', function ($scope, dataService, authService) {
    var vm = this;
    vm.savedSuccessfully = false;
    vm.editMode = false;
    vm.messages = [];
    vm.bloodGrups = [];
    vm.profile = {
        name: "",
        userName: "",
        firstName: "",
        lastName: "",
        nikeName: "",
        bloodGroupId: "",
        bloodGroup: "",
        phoneNumber: "",
        alternativeContactNo: "",
        dateOfBirth: "",
        address: "",
        postCode: "",
        city: "",
        country: "",
        countryId: "",
        gender: "",
        genderName: "",
        religion: "",
        religionName: "",
        personalIdentityNum: "",
        email: "",
        attachments:""
    };

    vm.enableEditMode = function () {
        vm.editMode = true;
    };

    vm.disablEditeMode = function() {
        vm.editMode = false;
    };

    vm.$onInit = function () {
        var userId = authService.authentication.userId;
        dataService.getUserById(userId).then(function (result) {
            var userData = result.data;
            Object.keys(vm.profile).forEach(function (key) {
                vm.profile[key] = userData[key];
            });
        });
    };

    vm.populateBloodGroup = function () {
        dataService.getBloodGroup.then(function (result) {
            vm.bloodGroups = result.data;
        });
    };
}]);