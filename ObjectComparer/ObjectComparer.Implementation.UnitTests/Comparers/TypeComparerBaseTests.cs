using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using ObjectComparer.Implementation.Comparers;
using ObjectComparer.Implementation.UnitTests.Dtos;
using Xunit;

namespace ObjectComparer.Implementation.UnitTests.Comparers
{
    public class TypeComparerBaseTests
    {
        [Fact]
        public void Constructor_Works()
        {
            var comparer = new SampleDtoComparer();
            Assert.NotNull(comparer.DefaultComparer);
            Assert.IsType<DefaultComparer>(comparer.DefaultComparer);
            Assert.IsAssignableFrom<IEqualityComparer>(comparer.DefaultComparer);
            Assert.NotNull(comparer.Properties);
            Assert.IsType<Dictionary<string, PropertyInfo>>(comparer.Properties);
            Assert.Equal(1, comparer.Properties.Count);
            Assert.Equal(nameof(SampleDto.PublicProperty), comparer.Properties[nameof(SampleDto.PublicProperty)].Name);
        }
    }
}
