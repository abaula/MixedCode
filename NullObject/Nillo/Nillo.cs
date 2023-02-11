
namespace NilloLib
{
    public static class Nillo
    {
        public static T Get<T>()
        {
            var type = typeof(T);

            if (!NilloStorage.Storage.ContainsKey(type))
                NilloBuilder.BuildAndAddToStorage(type);

            return (T)NilloStorage.Storage[type];
        }
    }
}
