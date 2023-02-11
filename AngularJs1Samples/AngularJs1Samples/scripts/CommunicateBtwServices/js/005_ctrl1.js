(function(app)
{
    "use strict";
    app.controller("ctrl1", ["$scope", "publishService", function ($scope, publishService)
    {
        $scope.text = "";
        $scope.$watch("text", function(newText)
        {
            publishService.publishText(newText);
        });
    }]);
})(angular.module("communicateBtwServicesApp"));