
namespace Streamlets
{
    public interface IStreamletFactory
    {
        IStreamlet<TIn, TOut> GetStreamlet<TIn, TOut>(string key);
    }
}