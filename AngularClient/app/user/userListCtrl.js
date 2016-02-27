(function () {
    "use strict";
    angular
        .module("userManagement")
        .controller("UserListCtrl", ["userResource", UserListCtrl]);

    function UserListCtrl(userResource) {
        var vm = this;
       
        userResource.query(function (data) {
            vm.users = data;
        });

    }
}());
