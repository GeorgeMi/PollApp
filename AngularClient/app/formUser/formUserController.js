(function () {
    "use strict";
    angular
        .module("formManagement")
        .controller("FormUserController", ["formResource", "$cookies", FormUserController]);

    function FormUserController(formResource, $cookies) {
        var vm = this;
       
      //  var param = { username: $cookies.get('username') };
       
        formResource.getForms.getForms(function (data) {

            vm.userForms = data;
           

        });


        vm.deleteForm = function (formID) {
            var param = { form_id: formID };
            var i;
            // alert(formID);

            formResource.delete.deleteForm(param,
                function (data) {

                    for (i = 0; i < vm.userForms.length ; i++) {

                        if (vm.userForms[i].Id === formID) {
                            vm.userForms.splice(i, 1);
                        }
                    }
                });
        }

    }
}());
