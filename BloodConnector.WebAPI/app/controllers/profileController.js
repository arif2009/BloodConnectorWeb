/// <reference path="profileController.js" />
'use strict';
app.controller('profileController', ['$scope', 'usersService', 'dataService', 'authService', 'toaster', 'fileService', function ($scope, usersService, dataService, authService, toaster, fileService) {
    var vm = this;
    vm.editMode = false;
    vm.messages = [];
    vm.bloodGroups = [];
    vm.countries = [];
    vm.profile = {
        id: "",
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

    vm.attachment = {
        id: "",
        userId: "",
        avater: ""
    };

    vm.update = function () {

        usersService.updateUser(vm.profile).then(function (result) {
            fileService.postMultipartForm({
                url: "/api/users",
                data: vm.attachment
            }).success(function(data) {
                vm.profile.lastUpdatedDate = result.data.lastUpdatedDate;
                vm.profile.city = result.data.city;
                vm.disablEditeMode();
                We.scroll(0);
                toaster.pop({
                    type: 'success',
                    body: 'Successfully update your information.',
                    timeout: 10000
                });
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

            vm.attachment.id = result.data.id;
            vm.attachment.avater = result.data.avater;
            vm.attachment.userId = result.data.userId;
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