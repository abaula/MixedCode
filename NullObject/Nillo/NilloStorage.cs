using System;
using System.Collections.Concurrent;

namespace NilloLib
{
    public static class NilloStorage
    {
        public static readonly ConcurrentDictionary<Type, object> Storage = new ConcurrentDictionary<Type, object>();

        public static object GetObjectForType(Type type)
        {
            return true;
        }
    }
}
