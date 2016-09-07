(function (ng, app)
{
    "use strict";
    app.factory("listenerService", ["notifyService", function (notifyService)
    {
        var notifyCallbacks = [];

        var onTextChanged = function (text)
        {
            ng.forEach(notifyCallbacks, function(callback)
            {
                callback(text);
            });
        }

        var registerCallback = function(callback)
        {
            notifyCallbacks.push(callback);
        }

        notifyService.subscribeOnTextChanged(onTextChanged);

        return {
            registerCallback: registerCallback
        }

    }]);
})(angular, angular.module("communicateBtwServicesApp"));