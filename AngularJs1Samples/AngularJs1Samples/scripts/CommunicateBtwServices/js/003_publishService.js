(function (app)
{
    "use strict";
    app.service("publishService", ["notifyService", function(notifyService)
    {
        return {
            publishText: function(text)
            {
                notifyService.fireTextChanged(text);
            }
        }
    }]);
})(angular.module("communicateBtwServicesApp"));