(function () {
    "use strict";
    angular
        .module("categoryManagement")
        .controller("CategoryListCtrl",
                    ["categoryResource", CategoryListCtrl]);

    function CategoryListCtrl(categoryResource) {
        var vm = this;

        categoryResource.query(function (data) {
            vm.categories = data;
        });

    }
}());
