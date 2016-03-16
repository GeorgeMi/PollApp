(function () {
    "use strict";
    angular
        .module("formManagement")
        .controller("FormController", ["formResource", "$cookies", FormController]);

    function FormController(formResource, $cookies) {
        var vm = this;

        vm.created = false;
        //data form to send
        vm.sendForm = {
            username: $cookies.get('username'),
            title: '',
            category: '',
            createdDate: '',
            deadline: '',
            id: 0,
            state: 'ok',
            //  answer: {id:1,answer:"a"}
            questions: [{ id: 1, question: '', answers: [{ id: 1, answer: '' }] }]
        };

        vm.sendFormMessage = '';
        //max vals
        vm.maxQuestionCount = 20;
        vm.maxAnswerCount = 6;

        //add question
        vm.addNewQuestion = function () {

            var questionID = vm.sendForm.questions.length + 1;

            if (questionID <= vm.maxQuestionCount) {
                vm.sendForm.questions.push({ 'id': questionID, 'question': '', 'answers': [{ 'id': 1, 'answer': '' }] });
            }
            else {
                alert("Maximum Number of Questions Allowed is " + vm.maxQuestionCount);
            }
        };

        //add answer
        vm.addNewAnswer = function (questionID) {

            var answerID = vm.sendForm.questions[questionID - 1].answers.length + 1;

            if (answerID <= vm.maxAnswerCount) {
                vm.sendForm.questions[questionID - 1].answers.push({ id: answerID, 'answer': '' });
            }
            else {
                alert("Maximum Number of Answers Allowed is " + vm.maxAnswerCount);
            }
        };

        //delete question
        vm.deleteQuestion = function (questionID) {

            if (vm.sendForm.questions.length > 1) {
                vm.sendForm.questions.splice(questionID - 1, 1);
                var count = 1;
                var i;

                //rewrite id foreach element in array
                for (i = 0; i <= vm.sendForm.questions.length; i++) {
                    if (vm.sendForm.questions[i] !== undefined) {
                        vm.sendForm.questions[i].id = count;
                        count++;
                    }
                }
            } else {
                alert("Minimum Number of Questions Allowed is 1");
            }
        }

        //delete answer
        vm.deleteAnswer = function (questionID, answerID) {
            // alert('q: ' + questionID + ', a: ' + answerID);
            if (vm.sendForm.questions[questionID - 1].answers.length > 1) {
                vm.sendForm.questions[questionID - 1].answers.splice(answerID - 1, 1);
                var count = 1;
                var i;

                //rewrite id foreach element in array
                for (i = 0; i <= vm.sendForm.questions[questionID - 1].answers.length; i++) {
                    if (vm.sendForm.questions[questionID - 1].answers[i] !== undefined) {
                        vm.sendForm.questions[questionID - 1].answers[i].id = count;
                        count++;
                    }

                }
            } else {
                alert("Minimum Number of Answers Allowed is 1");
            }
        }

        formResource.get.getForms(function (data) {

            vm.forms = data;
        });



        vm.addForm = function () {
            // alert(vm.sendForm);
            var x = JSON.stringify(vm.sendForm)

            formResource.add.addForm(x,
                //s-a creat cu succes
                function (data) {
                    vm.sendForm.title = '';
                    vm.sendForm.category = '';
                    vm.sendForm.createdDate = '';
                    vm.sendForm.deadline = '';
                    vm.sendForm.questions = [{ id: 1, question: '', answers: [{ id: 1, answer: '' }] }];
                    vm.messageForm = 'Poll created successfully';

                    vm.created = true;
                },

               //nu s-a creat
                function (response) {
                    if (response.data.error) {
                        vm.messageForm = response.data.error;
                    }
                    else {

                    }
                });
        }

        vm.deleteForm = function (formID) {
            var r = confirm("Are you sure that you want to permanently delete this form?");
            if (r == true) {

                var param = { form_id: formID };
                var i;
                // alert(formID);

                formResource.delete.deleteForm(param,
                    function (data) {

                        for (i = 0; i < vm.forms.length ; i++) {

                            if (vm.forms[i].Id === formID) {
                                vm.forms.splice(i, 1);
                            }
                        }
                    });
            }
        }

        vm.viewResults = function (id) {
            //alert("cookie");
            $cookies.put('my_poll_result', id);
            return 'my_poll_result';
        }

    }
}());
