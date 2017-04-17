using ObjectComparer.Implementation.Helpers;
using Xunit;

namespace ObjectComparer.Implementation.UnitTests.Helpers
{
    public class SafeNullableEnumeratorTests
    {
        [Fact]
        public void MoveNext_CurrentIsNull_ReturnsTrue()
        {
            var values = new[] {""};
            var enumerator = new SafeNullableEnumerator<string>(values);
            enumerator.MoveNext();
            Assert.NotNull(enumerator.Current);
            enumerator.MoveNext();
            Assert.Null(enumerator.Current);
            enumerator.MoveNext();
            Assert.Null(enumerator.Current);
        }

        [Fact]
        public void Reset_CurrentIsNotNull_ReturnsTrue()
        {
            var values = new[] { "" };
            var enumerator = new SafeNullableEnumerator<string>(values);
            enumerator.MoveNext();
            Assert.NotNull(enumerator.Current);
            enumerator.MoveNext();
            Assert.Null(enumerator.Current);
            enumerator.Reset();
            enumerator.MoveNext();
            Assert.NotNull(enumerator.Current);
        }
    }
}
