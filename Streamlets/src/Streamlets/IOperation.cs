
namespace Streamlets
{
    public interface IOperation<TOut>
    {
        void OnResult(Action<TOut> onResult);
        void OnResultAsync(Action<Task<TOut>> onResult);
    }
}