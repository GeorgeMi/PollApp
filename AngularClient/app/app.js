(function () {
    "use strict";
    
    var userManagementModule = angular.module("userManagement", ["common.services", "ngCookies"]);
    var formManagementModule = angular.module("formManagement", ["common.services", "ngCookies"]);
    var categoryManagementModule = angular.module("categoryManagement", ["common.services", "ngCookies"]);

    var app = angular.module("app", ["userManagement", "formManagement", "categoryManagement"]);
   
      
       

}());
