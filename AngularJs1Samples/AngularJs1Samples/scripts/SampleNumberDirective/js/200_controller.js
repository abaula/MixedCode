(function (app) {
    "use strict";

    app.controller("ctrl", function ($scope)
    {
        $scope.myNumber = undefined;
        $scope.myNumberIsNumber = function()
        {
            var n = $scope.myNumber;
            return !isNaN(parseFloat(n)) && isFinite(n);
        }
    });

})(angular.module("sampleNumberDirectiveApp"));
