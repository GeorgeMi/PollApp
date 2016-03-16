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

        vm.deleteUser = function (userID) {
            var r = confirm("Are you sure that you want to permanently delete this user?");

            if (r == true) {

                var param = { user_id: userID };
                var i;

                userResource.delete.deleteUser(param,
                    function (data) {

                        for (i = 0; i < vm.users.length ; i++) {

                            if (vm.users[i].UserID === userID) {
                                vm.users.splice(i, 1);
                            }
                        }

                    });
            }
        }

        vm.promote = function (userID) {
            var param = { user_id: userID };
            var i;

            userResource.promote.promoteUser(param,
                function (data) {

                    for (i = 0; i < vm.users.length ; i++) {

                        if (vm.users[i].UserID === userID) {
                            vm.users[i].Role = 'admin';
                        }
                    }
                });
        }

        vm.demote = function (userID) {
            var param = { user_id: userID };
            var i;

            userResource.demote.demoteUser(param,
                function (data) {

                    for (i = 0; i < vm.users.length ; i++) {

                        if (vm.users[i].UserID === userID) {
                            vm.users[i].Role='user';
                        }
                    }
                });
        }
    }
}());
