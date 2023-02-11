
namespace Streamlets
{
    public interface IStreamletBuilder<TIn, TOut>
    {
        void MapInputToVariable(string variableKey);
        void MapOutputToVariable(string variableKey);
        TVar Variable<TVar>(string variableKey);
        void Variable<TVar>(string variableKey, TVar value);

        IStreamletOperations Operations { get; }
    }
}