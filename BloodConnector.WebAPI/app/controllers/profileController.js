﻿'use strict';
app.controller('profileController', ['$scope', 'usersService', 'dataService', 'authService', 'toaster', function ($scope, usersService, dataService, authService, toaster) {
    var vm = this;
    vm.editMode = false;
    vm.messages = [];
    vm.bloodGroups = [];
    vm.countries = [];
    vm.profile = {
        userId: "",
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
        attachments:[]
    };

    vm.imageSource = '../../Images/noimage.jpg';

    vm.selectFile = function (element) {
        var reader = new FileReader();
        reader.onload = function (event) {
            vm.imageSource = event.target.result;
            $scope.$apply();
        }
        reader.readAsDataURL(element.files[0]);
    }

    vm.update = function() {
        usersService.updateUser(vm.profile).then(function (result) {
            vm.profile.lastUpdatedDate = result.data.lastUpdatedDate;
            vm.disablEditeMode();
            We.scroll(0);
            toaster.pop({
                type: 'success',
                body: 'Successfully update your information.',
                timeout: 10000
            });
        }, function (error) {
            We.scroll(0);
            toaster.pop({
                type: 'warning',
                body: 'Required field missing.',
                timeout: 10000
            });
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

    $scope.$watch("vm.profile.gender", function(value) {
        vm.profile.genderName = value === 0 ? "Female" : "Male";
    });

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