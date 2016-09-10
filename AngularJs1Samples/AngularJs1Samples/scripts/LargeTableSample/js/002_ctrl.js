(function(ng, app)
{
    "use strict";
    app.controller("ctrl", function($scope)
    {
        var tableData = [];

        function getId(rowNum, colNum)
        {
            return rowNum + "." + colNum;
        }

        function getRowNumById(id)
        {
            var len = $scope.data.length;
            for (var i = 0; i < len; i++)
            {
                var row = $scope.data[i];

                if (row.id === id)
                    return i;
            }

            return undefined;
        }

        function createRowColumns(rowNumber, pushZeros)
        {
            var row = [];

            for (var j = 1; j <= 100; j++)
            {
                var id = getId(rowNumber, j);
                row.push(
                    {
                        id: id,
                        text: pushZeros === true ? 0 : id
                    });
            }

            return row;
        }

        function initData()
        {
            for (var i = 1; i <= 1000; i++)
                tableData.push(
                    {
                        id: i - 1,
                        text: i,
                        cols: createRowColumns(i, false)
                    });

            $scope.data = tableData;
        }

        //$scope.updateRow = function(row)
        //{
        //    var t0 = performance.now();
        //    var len = row.cols.length;

        //    for (var i = 0; i < len; i++)
        //        row.cols[i].text = 0;

        //    var t1 = performance.now();
        //    console.log("Время выполнения $scope.updateRow: " + (t1 - t0) + " мс.");
        //}

        $scope.removeRow = function(row)
        {
            var t0 = performance.now();
            var num = getRowNumById(row.id);

            $scope.data.splice(num, 1);
            $scope.data.splice(num, 0,
                {
                    id: num,
                    text: num,
                    cols: createRowColumns(num, true)
                });

            var t1 = performance.now();
            console.log("Время выполнения $scope.removeRow: " + (t1 - t0) + " мс.");
        }

        $scope.text = "";

        initData();
    });
})(angular, angular.module("LargeTableSampleApp"));