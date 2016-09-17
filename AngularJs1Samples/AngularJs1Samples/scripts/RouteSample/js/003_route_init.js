(function(app)
{
    "use strict";
    app.config(["$routeProvider", "$locationProvider", function ($routeProvider, $locationProvider)
    {
        $routeProvider
            .when("/",
                {
                    templateUrl: "defaultPage.html",
                    controller: "defaultPageController"
                })
            .when("/page1",
                {
                    templateUrl: "page1.html",
                    controller: "page1Controller"
                })
            .otherwise({ redirectTo: "/" });

        // Отключаем для работы с локальным файлом. Если включено, то выдаёт ошибку CORS.
        $locationProvider.html5Mode(false);
    }]);
})(angular.module("routedApp"));