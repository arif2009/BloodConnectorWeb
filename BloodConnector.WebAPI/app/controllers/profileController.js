'use strict';
app.controller('profileController', ['$scope', 'usersService', 'dataService', 'authService', function ($scope, usersService, dataService, authService) {
    var vm = this;
    vm.savedSuccessfully = false;
    vm.editMode = false;
    vm.messages = [];
    vm.bloodGroups = [];
    vm.countries = [];
    vm.profile = {
        name: "",
        userName: "",
        firstName: "",
        lastName: "",
        nikeName: "",
        fullName: "",
        bloodGroupId: "",
        bloodGroup: "",
        bloodGiven: "",
        email: "",
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
        lastUpdatedDate: "",
        attachments:""
    };

    vm.update = function() {
        usersService.updateUser(vm.profile).then(function(result) {
            
        });
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

    $scope.$watch("vm.editMode", function (value) {
        if (value) {

            if (!vm.bloodGroups.length) {
                dataService.getBloodGroup().then(function (result) {
                    vm.bloodGroups = result.data;
                });
            }

            if (!vm.countries.length) {
                dataService.getCountry().then(function (result) {
                    vm.countries = result.data;

                    if (!vm.profile.countryId) {
                        vm.profile.countryId = 11;
                    }
                });
            }
        }
    });

}]);