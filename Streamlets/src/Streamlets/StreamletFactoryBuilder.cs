
namespace Streamlets
{
    public class StreamletFactoryBuilder
    {
        private static StreamletFactoryBuilder Instance;

        static StreamletFactoryBuilder()
        {
            Instance = new StreamletFactoryBuilder();
        }

        public IStreamletFactory Build()
        {
            throw new NotImplementedException();
        }

        public static StreamletFactoryBuilder CreateStreamlet<TIn, TOut>(string key, Action<IStreamletBuilder<TIn, TOut>> streamletBuilder)
        {
            return Instance;
        }
    }
}