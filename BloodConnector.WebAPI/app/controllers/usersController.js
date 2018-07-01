﻿'use strict';
app.controller('usersController', ['usersService', '$scope', '$filter', function (usersService, $scope, $filter) {

    var vm = this;

    vm.itemsPerPage = 15;
    vm.currentPage = 0;
    vm.items = [];
    vm.hasDeleteAbility = false;

    vm.$onInit = function() {
        usersService.getUsers().then(function(results) {
            vm.items = results.data.users;
            vm.hasDeleteAbility = results.data.role === 'superadmin';
            vm.pages = vm.range();
        });
    };

    vm.itemToDel = "";
    vm.deleteUserName = "";
    vm.setDeleteItem = function(user) {
        vm.itemToDel = user.id;
        vm.deleteUserName = user.fullName;
    };

    vm.deleteUser = function () {

        usersService.deleteUser(vm.itemToDel).then(function() {
            vm.items = vm.items.filter(function (item) {
                return item.id !== vm.itemToDel;
            });
        },function(err) {
            alert("Can't delete");
        });
    };

    vm.pageCount = function () {
        var pages = $filter('filter')(vm.items, vm.searchText);
        return Math.ceil(pages.length / vm.itemsPerPage);
    };

    vm.range = function () {
        var rangeSize = vm.itemsPerPage;
        var ret = [];
        var start;

        var count = vm.pageCount();

        if (count < rangeSize) {
            rangeSize = count;
        }

        start = vm.currentPage;
        if (start > count - rangeSize) {
            start = count - rangeSize;
        }

        for (var i = start; i < start + rangeSize; i++) {
            ret.push(i);
        }
        return ret;
    };
    vm.pages = vm.range();

    $scope.$watch('vm.searchText', function (v) {
        vm.currentPage = 0;
        vm.pages = vm.range();
    });

    $scope.$watch('vm.currentPage', function (v) {
        vm.pages = vm.range();
    });

    vm.prevPage = function () {
        if (vm.prevPageDisabled()) {
            return;
        }
        if (vm.currentPage > 0) {
            vm.currentPage--;
        }
    };

    vm.prevPageDisabled = function () {
        return vm.currentPage === 0 ? "disabled" : "";
    };

    vm.nextPage = function () {
        if (vm.nextPageDisabled()) {
            return;
        }
        if (vm.currentPage < vm.pageCount()) {
            vm.currentPage++;
        }
    };

    vm.nextPageDisabled = function () {
        return vm.currentPage + 1 === vm.pageCount() ? "disabled" : "";
    };

    vm.setPage = function (n) {
        vm.currentPage = n;
    };

}]);