(function () {
    "use strict";
    angular
        .module("categoryManagement")
        .controller("CategoryController", ["categoryResource", "$cookies", CategoryController]);

    //data for categories


    function CategoryController(categoryResource, $cookies) {
        var vm = this;

        vm.category = {
            name: ''
        };

        categoryResource.get.getCategories(function (data) {
            vm.categories = data;
        });

        vm.addCategory = function () {

            categoryResource.add.addCategory(vm.category, function (data) {
                
                categoryResource.get.getCategories(function (data) {
                    vm.category.name = '';
                    vm.categories = data;
                });
            });
        }

        vm.deleteCategory = function (categoryID) {
            var param = { cat_id: categoryID };

            categoryResource.delete.deleteCategory(param,
                function (data) {

                    categoryResource.get.getCategories(function (data) {
                        vm.categories = data;
                    });
                });
        }
    }
}());
