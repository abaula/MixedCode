using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ObjectComparer.Abstractions;
using ObjectComparer.Abstractions.Comparers;

namespace ObjectComparer.Drafts
{
    public abstract class ObjectComparerFactory<TObject>
    {
        protected ObjectComparerFactory()
        {
            Comparers = new Dictionary<Type, IObjectEqualityComparer>();
        }

        public Dictionary<Type, IObjectEqualityComparer> Comparers { get; }

        protected void AddComparer(IObjectEqualityComparer comparer)
        {
            Comparers.Add(comparer.Type, comparer);
        }
    }
}
