(function (app)
{
    "use strict";
    app.factory("publishService", ["notifyService", function(notifyService)
    {
        return {
            publishText: function(text)
            {
                notifyService.fireTextChanged(text);
            }
        }
    }]);
})(angular.module("communicateBtwServicesApp"));