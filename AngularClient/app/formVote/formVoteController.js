(function () {
    "use strict";
    angular
        .module("formManagement")
        .controller("FormVoteController", ["formResource", "$cookies", FormVoteController]);

    function FormVoteController(formResource, $cookies) {
        var vm = this;

        vm.formID = $cookies.get('last_poll');

        vm.changeID= function (id)
        {
           // alert("cookie");
            $cookies.put('last_poll', id);
            return 'vote_poll';
        }
      
        var param = { form_id: vm.formID };
      
        if (vm.formID)
        {
          //  alert("asdasd");
            $cookies.remove('last_poll');
            formResource.getForm.getForm(param,
                function (data) {

                    vm.detailedForm = data;
                  
                });
        }
    }

}());
