using System.Collections.Generic;

namespace SetOfStates.States
{
    public sealed class StatesInfo<TObject, TId>
    {
        public StatesInfo()
        {
            SetStatesList = new List<StateNode<TObject, TId>>();
        }

        public IReadOnlyCollection<StateNode<TObject, TId>> SetStates => SetStatesList;
        internal List<StateNode<TObject, TId>> SetStatesList { get; }
    }
}
