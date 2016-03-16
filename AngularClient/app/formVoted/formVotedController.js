(function () {
    "use strict";
    angular
        .module("formManagement")
        .controller("FormVotedController", ["formResource", "$cookies", FormVotedController]);

    function FormVotedController(formResource, $cookies, RadarCtrl) {
        var vm = this;

       
        formResource.getVotedForms.getVotedForms(function (data) {
            vm.forms = data;

        });

        vm.viewResults = function (id) {
            //alert("cookie");
            $cookies.put('my_poll_result', id);
            return 'my_poll_result';
        }
        

    }

}());
