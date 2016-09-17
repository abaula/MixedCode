(function(app)
{
    "use strict";
    app.controller("page1Controller", ["$scope", "$location",
        function ($scope, $location)
        {
            $scope.dogName = "Шарик";
            $scope.goToMainPage = function(path)
            {
                $location.path(path);
            }
        }]);
})(angular.module("routedApp"));