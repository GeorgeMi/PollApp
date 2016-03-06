(function () {
    "use strict";
    angular
        .module("formManagement")
        .controller("FormVoteController", ["formResource", "$cookies", FormVoteController]);

    function FormVoteController(formResource, $cookies) {
        var vm = this;

        vm.formID = $cookies.get('last_poll');

        //array pentru raspuns
        vm.voteForm = {
            username: $cookies.get('username'),
            answers: [{ question: 0, answer: 0 }]
        };
       
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
                  
                    //creez array-ul pentru raspuns
                    if (vm.detailedForm.Questions.length > 0) {
                        var i;

                        vm.voteForm.answers[0].question = vm.detailedForm.Questions[0].QuestionID;

                        for (i = 0; i < vm.detailedForm.Questions.length; i++) {
                            //deja este un rand introdus la initializare
                            if (i >= 1) {
                                vm.voteForm.answers.push({ 'question': vm.detailedForm.Questions[i].QuestionID, 'answer': 0 });
                            }
                            vm.detailedForm.Questions[i].nrCrt = i;
                        }
                    }
                });
        }

       

      
    }

}());
