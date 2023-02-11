(function (app)
{
    "use strict";
    app.controller("ctrl2", ["$scope", "listenerService", function ($scope, listenerService)
    {
        $scope.text = "";

        listenerService.registerCallback(function(changedText)
        {
            $scope.text = changedText;
        });
    }]);
})(angular.module("communicateBtwServicesApp"));