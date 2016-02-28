(function () {
    "use strict";
    angular
        .module("userManagement")
        .controller("UserController", ["userResource", UserController]);

    function UserController(userResource) {
        var vm = this;
       
        userResource.get.getUsers(function (data) {
            vm.users = data;
        });

    }
}());
