using System;
using Xunit;
using NilloLib;

namespace NilloTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var nillo = Nillo.Get<TestClass>();
            var b = nillo.GetBool();
        }
    }
}
