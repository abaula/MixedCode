using System.Reflection;
using ObjectComparer.Abstractions.Results;
using ObjectComparer.Implementation.Helpers;
using Xunit;

namespace ObjectComparer.Implementation.UnitTests.Helpers
{
    public class NullParamsTypeCompareResultBuilderTests
    {
        [Fact]
        public void Buid__MemberInfoNotNull_ReturnsTrue()
        {
            var memberInfo = typeof(string).GetProperty(nameof(string.Length));
            var result = NullParamsTypeCompareResultBuilder.Build<string>(memberInfo);

            Assert.True(result.Match); 
            Assert.Null(result.Left); 
            Assert.Null(result.Right); 
            Assert.Same(memberInfo, result.Member);
            Assert.IsType<ICompareResult[]>(result.MembersResults);
            Assert.NotNull(result.MembersResults);
            Assert.Empty(result.MembersResults);
        }

        [Fact]
        public void Buid__MemberInfoNull_ReturnsTrue()
        {
            var result = NullParamsTypeCompareResultBuilder.Build<string>();

            Assert.True(result.Match);
            Assert.Null(result.Left);
            Assert.Null(result.Right);
            Assert.Null(result.Member);
            Assert.IsType<ICompareResult[]>(result.MembersResults);
            Assert.NotNull(result.MembersResults);
            Assert.Empty(result.MembersResults);
        }
    }
}
