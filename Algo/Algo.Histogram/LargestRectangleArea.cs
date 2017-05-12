using System;
using System.Collections.Generic;

namespace Algo.Histogram
{
    public static class LargestRectangleArea
    {
        private class BarInfo
        {
            public int Position { get; set; }
            public int Height { get; set; }
        }

        public static int Calculate(IEnumerable<int> barHeights)
        {
            var stack = new Stack<BarInfo>();
            var pos = 0;
            var area = 0;
            var prevBarHeight = 0;
            var reducedPos = 0;

            foreach (var barHeight in barHeights)
            {
                if (pos == 0 || barHeight > prevBarHeight)
                {
                    stack.Push(new BarInfo {Position = pos, Height = barHeight});
                }
                else if (barHeight < prevBarHeight)
                {
                    while (stack.Count > 0 && stack.Peek().Height > barHeight)
                        area = Math.Max(area, ReduceStack(pos, stack, out reducedPos));

                    stack.Push(new BarInfo {Position = reducedPos, Height = barHeight});
                }

                prevBarHeight = barHeight;
                pos++;
            }

            while (stack.Count > 0)
                area = Math.Max(area, ReduceStack(pos, stack, out reducedPos));

            return area;
        }

        private static int ReduceStack(int barPos, Stack<BarInfo> stack, out int reducedPos)
        {
            var barInfo = stack.Pop();
            reducedPos = barInfo.Position;
            return (barPos - barInfo.Position) * barInfo.Height;
        }
    }
}
