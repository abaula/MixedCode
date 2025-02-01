
namespace Streamlets
{
    public interface IStreamlet<TIn, TOut>
    {
        Task Run(TIn payload);
        TOut Result();
    }
}