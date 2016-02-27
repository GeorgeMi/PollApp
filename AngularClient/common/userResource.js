(function () {
    "use strict";
    angular
        .module("common.services")
        .factory("userResource", ["$resource", "appSettings", userResource])

    function userResource($resource, appSettings) {
        return $resource(appSettings.serverPath + "/api/user");
    }

}());