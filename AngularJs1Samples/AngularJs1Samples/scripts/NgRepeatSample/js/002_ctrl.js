(function(app)
{
    "use strict";
    app.controller("ctrl", function($scope)
    {
        $scope.basket = [];
        $scope.showBasketOne = function()
        {
            $scope.basket = [{ id: 1, name: "Apple" }, { id: 2, name: "Oliva" }];
        };
        $scope.showBasketTwo = function ()
        {
            $scope.basket = [{ id: 3, name: "Orange" }, { id: 4, name: "Kiwi" }];
        };
    });
})(angular.module("ngRepeatSampleApp"));