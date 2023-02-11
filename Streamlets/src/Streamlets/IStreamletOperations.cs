
namespace Streamlets
{
    public interface IStreamletOperations
    {
        void Add<TOperation>(Action<TOperation> operation);
        void AddAsync<TOperation>(Func<TOperation, Task> operation);
        IOperation<TOperationOutput> Add<TOperation, TOperationOutput>(Func<TOperation, TOperationOutput> operation);
        IOperation<TOperationOutput> AddAsync<TOperation, TOperationOutput>(Func<TOperation, Task<TOperationOutput>> operation);
    }
}