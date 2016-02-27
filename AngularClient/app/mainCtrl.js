(function () {
    "use strinct";
    angular
        .module("app")
        .controller("MainCtrl", ["userAccount", "$cookies", MainCtrl]);

    function MainCtrl(userAccount, $cookies) {
        var vm = this;
        vm.isLoggedIn = false;
        vm.message = '';
        vm.role = '';

        //data for login
        vm.userData = {
            username: $cookies.get('username'),
            password: ''
        };

        //data for registration
        vm.userDataRegistration = {
            username: '',
            password: '',
            email: ''
        };

        //app pages
        vm.pages = {
            home: true,
            my_polls: false,
            categories: false,
            new_poll: false,
            manage_users: false,
            manage_polls: false,
            manage_categories: false
        };

        vm.confirm_password = '';

        vm.tokenDataRegistration = $cookies.get('token');

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

        //data for categories
        vm.category = {
            id: '',
            name: ''
        };

        //------------------register-----------------------
        vm.registerUser = function () {

            if (vm.confirm_password == vm.userDataRegistration.password) {

                userAccount.registration.registerUser(vm.userDataRegistration,

                    function (data) {
                        //inregistrarea a avut succes
                        vm.message = data;
                        vm.userData.username = vm.userDataRegistration.username;
                        vm.userData.password = vm.userDataRegistration.password;
                        vm.login();
                    },

                    function (response) {
                        //inregistrarea nu a avut succes
                        vm.isLoggedIn = false;

                        if (response.data.error) {
                            vm.message = response.data.error;
                        }

                        //validation errors
                        if (response.data.modelState) {

                            for (var key in response.data.modelState) {
                                vm.message += response.data.modelState[key] + "\r\n";
                            }
                        }
                    });
            }
            else {

                vm.message = "Passwords don't match";
            }
        }

        //-----------------login with username and password-----------------------
        vm.login = function () {

            userAccount.login.loginUser(vm.userData,
                function (data) {
                    if (data.error) {
                        vm.isLoggedIn = false;
                        vm.password = "";
                        vm.message = data.error;

                    } else {
                        vm.isLoggedIn = true;
                        vm.password = "";
                        vm.token = data.token;
                        vm.role = data.role;

                        var expireDate = new Date();
                        expireDate.setDate(expireDate.getDate() + 1);

                        $cookies.put("token", data.token, { 'expires': expireDate });
                        $cookies.put("username", vm.userData.username, { 'expires': expireDate });
                        $cookies.put("role", vm.role, { 'expires': expireDate });

                    }

                })
        }

        //------------------login with token-----------------------
        vm.registerToken = function () {

            userAccount.tokenRegistration.registerToken(
                function (data) {

                    if (data.error) {
                        //token invalid
                        vm.isLoggedIn = false;
                        vm.message = data.error;

                    } else {
                        //token acceptat
                        vm.isLoggedIn = true;

                        var expireDate = new Date();
                        expireDate.setDate(expireDate.getDate() + 1);
                        vm.role = data.role;

                        $cookies.put("token", vm.tokenDataRegistration, { 'expires': expireDate });
                        $cookies.put("username", vm.userData.username, { 'expires': expireDate });
                        $cookies.put("role", vm.role, { 'expires': expireDate });

                    }

                })

        }

        //------------------logout-----------------------
        vm.logout = function () {
            $cookies.remove("token");
            $cookies.remove("role");

            vm.isLoggedIn = false;

            vm.pages.home = false;
            vm.pages.categories = false;
            vm.pages.my_polls = false;
            vm.pages.new_poll = false;
            vm.pages.manage_users = false;
            vm.pages.manage_polls = false;
            vm.pages.manage_categories = false;

        }

        //------------------change page-----------------------
        vm.changePage = function (mypage) {
            vm.pages.home = false;
            vm.pages.categories = false;
            vm.pages.my_polls = false;
            vm.pages.new_poll = false;
            vm.pages.manage_users = false;
            vm.pages.manage_polls = false;
            vm.pages.manage_categories = false;

            if (mypage == 'home') {
                vm.pages.home = true;
            }
            else if (mypage == 'categories') {
                vm.pages.categories = true;
            }
            else if (mypage == 'my_polls') {
                vm.pages.my_polls = true;
            }
            else if (mypage == 'new_poll') {
                vm.pages.new_poll = true;
            }
            else if (mypage == 'manage_users') {
                vm.pages.manage_users = true;
            }
            else if (mypage == 'manage_polls') {
                vm.pages.manage_polls = true;
            }
            else if (mypage == 'manage_categories') {
                vm.pages.manage_categories = true;
            }
        }

        //------------------add form-----------------------

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

        vm.getCategories = function () {
            categoryResource.get.getCategory(vm.category,

                   function (data) {

                   },

                   function (response) {

                   });
        }

    };
})();