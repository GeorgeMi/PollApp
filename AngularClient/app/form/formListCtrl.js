(function () {
    "use strict";
    angular
        .module("formManagement")
        .controller("FormListCtrl",
                    ["formResource", FormListCtrl]);

    function FormListCtrl(formResource) {
        var vm = this;

        formResource.query(function (data) {
            vm.forms = data;
        });

    }
}());
