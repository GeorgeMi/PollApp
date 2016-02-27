(function () {
    "use strict";
    angular
        .module("common.services")
        .factory("userAccount", ["$resource", "appSettings","$cookies", userAccount])

    function userAccount($resource, appSettings, $cookies) {
        return {
            registration: $resource(appSettings.serverPath + "/api/registration", null,
                       {
                           'registerUser': { method: 'POST' }
                       }),

            login: $resource(appSettings.serverPath + "/api/auth", null,
                {
                    'loginUser': {
                        method: 'POST',
                        headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                        transformRequest: function (data, headersGetter) {
                            var str = [];
                            for (var d in data) {
                                str.push(encodeURIComponent(d) + "=" + encodeURIComponent(data[d]));
                            }
                            return str.join("&");
                        }
                    }
                }),

            tokenRegistration: $resource(appSettings.serverPath + "/api/index" , null,
                       {
                           'registerToken': {
                               method: 'GET',
                               headers: { 'Content-Type': 'application/x-www-form-urlencoded', 'token' : $cookies.get('token')  },
                               transformRequest: function (data, headersGetter) {
                                   var str = [];
                                   for (var d in data) {
                                       str.push(encodeURIComponent(d) + "=" + encodeURIComponent(data[d]));
                                   }
                                   return str.join("&");
                               }
                           }
                       })
        }
    }
}());