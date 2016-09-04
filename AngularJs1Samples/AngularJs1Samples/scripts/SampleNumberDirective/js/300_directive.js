(function() {
    "use strict";

    /*
      Директива для использования в input.
      Допускает ввод только целых и натуральных чисел и пустого значения.
      Передаёт в model только 2 типа значений: number и undefined.
     */
    angular.module("sampleNumberDirectiveApp")
    .directive("floatNumber", function ()
    {
        return {
            require: "ngModel",
            link: function ($scope, element, attr, ngModelCtrl)
            {
                              
                ngModelCtrl.$parsers.push(function (text)
                {
                    // Функция нормализует сроку, приводит её к виду числа.
                    // Реализовано методом конечного автомата.
                    function normalize(str)
                    {
                        var states =
                            {
                                initial: 0,
                                skipLeadingZeros: 1,
                                intPart: 2,
                                fractPart: 3
                            };

                        var clear = str.replace(/[^0-9.\-]/g, "");
                        var currentState = states.initial;
                        var resultSign = "";
                        var resultIntPart = "";
                        var resultDelimiter = "";
                        var resultFractPart = "";

                        for (var i = 0, len = clear.length; i < len; i++)
                        {
                            var ch = clear[i];

                            switch(currentState)
                            {
                                case states.skipLeadingZeros:
                                    if (ch === ".")
                                    {
                                        resultDelimiter = ".";
                                        resultIntPart = "0";                                        
                                        currentState = states.fractPart;
                                    }
                                    else if (ch === "0")
                                    {
                                        resultIntPart = "0";
                                    }
                                    else if (ch !== "0" && ch !== "-")
                                    {
                                        resultIntPart = ch;
                                        currentState = states.intPart;
                                        break;
                                    }
                                    break;
                                case states.intPart:
                                    if (ch === ".")
                                    {
                                        resultDelimiter = ".";
                                        currentState = states.fractPart;
                                    }
                                    else if(ch !== "-")
                                    {
                                        resultIntPart += ch;
                                    }
                                    break;
                                case states.fractPart:
                                    if (ch !== "-" && ch !== ".")
                                    {
                                        resultFractPart += ch;
                                    }
                                    break;
                                default : // states.initial
                                    if (ch === ".")
                                    {
                                        resultIntPart = "0";
                                        resultDelimiter = ".";
                                        currentState = states.fractPart;
                                    }
                                    else if (ch === "-")
                                    {
                                        resultSign = "-";
                                        currentState = states.skipLeadingZeros;
                                    }
                                    else if (ch === "0")
                                    {
                                        resultIntPart = "0";
                                        currentState = states.skipLeadingZeros;
                                    }
                                    else
                                    {
                                        resultIntPart = ch;
                                        currentState = states.intPart;
                                    }
                            }
                        }

                        return resultSign + resultIntPart + resultDelimiter + resultFractPart;
                    }

                    if (text)
                    {
                        var transformedInput = normalize(text.toString().trim());
                        ngModelCtrl.$setViewValue(transformedInput);
                        ngModelCtrl.$render();

                        if (transformedInput.length > 0 && transformedInput !== "-")
                            return parseFloat(transformedInput);
                    }

                    return undefined;
                });
            }
        };
    });
})();