'use strict';
app.controller('profileController', ['dataService', function (dataService) {
    var vm = this;
    vm.savedSuccessfully = false;
    vm.editMode = false;
    vm.messages = [];
    vm.bloodGrups = [];
    vm.registration = {
        firstName: "",
        lastName: "",
        nikeName: "",
        email: "",
        phoneNumber: "",
        bloodGroupId: ""
    };

    vm.toggleEditMode = function() {
        vm.editMode = !vm.editMode;
    };

    vm.disablEditeMode = function() {
        vm.editMode = false;
    };

    vm.populateBloodGroup = function () {
        dataService.getBloodGroup.then(function (result) {
            vm.bloodGroups = result.data;
        });
    };
}]);