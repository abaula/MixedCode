(function (app) {
    "use strict";
    app.controller("ctrl", ["$scope", function ($scope)
    {
        $scope.explain1 = "Роутинг \"локально\" работает только в Firefox.";
        $scope.explain2 = "Crome выдаёт ошибку - Cross origin requests are only supported for protocol schemes: http, data, chrome, chrome-extension, https, chrome-extension-resource.";
        $scope.explain3 = "Edge выдаёт ошибку - Не найден указанный файл.";
    }]);
})(angular.module("routedApp"));
