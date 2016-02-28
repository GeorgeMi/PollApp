(function () {
    "use strict";
    angular
        .module("formManagement")
        .controller("FormController", ["formResource", FormController]);

    function FormController(formResource) {
        var vm = this;

        //data form to send
        vm.sendForm = {
            username: '',
            title: '',
            category: '',
            createdDate: '',
            deadline: '',
            state: '',
            questions: [{ id: 1, question: '', answers: [{ id: 1, answer: '' }] }]
        };

        vm.sendFormMessage = '';
        //max val 
        vm.maxQuestionCount = 20;
        vm.maxAnswerCount = 6;

        //------------------add form-----------------------

        //add question
        vm.addNewQuestion = function () {
            alert("asdsad");
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

        formResource.query(function (data) {
            vm.forms = data;
        });

    }
}());
