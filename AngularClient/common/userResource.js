(function () {
    "use strict";
    angular
        .module("common.services")
        .factory("userResource", ["$resource", "appSettings","$cookies", userResource])

    function userResource($resource, appSettings, $cookies) {
            return {
                get: $resource(appSettings.serverPath + "/api/user", null,
                          {
                              'getUsers': {
                                  method: 'GET',
                                  isArray: true,
                                  headers: { 'Content-Type': 'application/x-www-form-urlencoded', 'token': $cookies.get('token') },
                                  transformRequest: function (data, headersGetter) {
                                      var str = [];
                                      for (var d in data) {
                                          str.push(encodeURIComponent(d) + "=" + encodeURIComponent(data[d]));
                                      }
                                      return str.join("&");
                                  }
                              }
                          }),

                add: $resource(appSettings.serverPath + "/api/user", null,
                   {
                       'addUser': {
                           method: 'POST',
                           headers: { 'Content-Type': 'application/x-www-form-urlencoded', 'token': $cookies.get('token') },
                           transformRequest: function (data, headersGetter) {
                               var str = [];
                               for (var d in data) {
                                   str.push(encodeURIComponent(d) + "=" + encodeURIComponent(data[d]));
                               }
                               return str.join("&");
                           }
                       }
                   }),
                delete: $resource(appSettings.serverPath + "/api/user/:cat_id", { user_id: '@id' },
                   {
                       'deleteUser': {
                           method: 'DELETE',
                           headers: { 'Content-Type': 'application/x-www-form-urlencoded', 'token': $cookies.get('token') },
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