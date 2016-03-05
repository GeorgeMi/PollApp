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
           var param = { user_id: userID };

            userResource.delete.deleteUser(param,
                function (data) {

                    userResource.get.getUsers(function (data) {
                        vm.users = data;
                    });
                });
        }

        vm.promote = function (userID) {
            var param = { user_id: userID };

            userResource.promote.promoteUser(param,
                function (data) {

                    userResource.get.getUsers(function (data) {
                        vm.users = data;
                    });
                });
        }

        vm.demote = function (userID) {
            var param = { user_id: userID };
            var i;

            userResource.demote.demoteUser(param,
                function (data) {

                    for (i = 0; i < vm.users.length ; i++) {

                        if (vm.users[i].UserID === userID) {
                            vm.users.splice(i, 1);
                        }
                    }
                });
        }
    }
}());
