﻿'use strict';
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
        fullName: "",
        bloodGroupId: "",
        bloodGroup: "",
        bloodGiven: "",
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

    $scope.$watch("vm.editMode", function (value) {
        if (value && !vm.bloodGroups) {
            dataService.getBloodGroup().then(function (group) {
                vm.bloodGroups = group.data;
            });
        }
    });

}]);