(function () {
    "use strict";
    angular
        .module("common.services")
        .factory("formResource", ["$resource", "appSettings", "$cookies", formResource])

    function formResource($resource, appSettings, $cookies) {
        return {
            get: $resource(appSettings.serverPath + "/api/form", null,
                      {
                          'getForms': {
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

            add: $resource(appSettings.serverPath + "/api/form", null,
               {
                   'addForm': {
                       method: 'POST',
                       headers: { 'Content-Type': 'application/json', 'token': $cookies.get('token') }
                   }

               }),
            getForms: $resource(appSettings.serverPath + "/api/form/user/" + $cookies.get('username'), null,
                      {
                          'getForms': {
                              method: 'POST',
                              isArray: true,
                              headers: { 'Content-Type': 'application/x-www-form-urlencoded', 'token': $cookies.get('token') },
                              
                          }
                      }),
            delete: $resource(appSettings.serverPath + "/api/form/:form_id", { form_id: '@id' },
               {
                   'deleteForm': {
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