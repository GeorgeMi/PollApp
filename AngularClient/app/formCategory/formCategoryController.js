(function () {
    "use strict";
    angular
        .module("formManagement")
        .controller("FormCategoryController", ["formResource", "$cookies", FormCategoryController]);

    function FormCategoryController(formResource, $cookies) {
        var vm = this;


        var param = { category_id: $cookies.get('category_id') };

        formResource.getCategoryForms.getCategoryForms(param,function (data) {
            vm.forms = data;

        });

        vm.viewResults = function (id) {
            //alert("cookie");
            $cookies.put('my_poll_result', id);
            return 'my_poll_result';
        }

    }
}());
