using System;
using System.Collections.Generic;

namespace SetOfStates.States
{
    public abstract class StateBase<TObject, TId>
    {
        protected StateBase()
        {
            States = new List<StateNode<TObject, TId>>();
        }

        public List<StateNode<TObject, TId>> States { get; }

        protected void State(TId id, Action<StateNode<TObject, TId>> stateAction)
        {
            var state = new StateNode<TObject, TId>(id, null);
            States.Add(state);
            stateAction(state);
        }

        public StatesInfo<TObject, TId> Handle(TObject obj)
        {
            var info = new StatesInfo<TObject, TId>();

            foreach (var state in States)
                state.Handle(obj, info.SetStatesList);

            return info;
        }
    }
}
