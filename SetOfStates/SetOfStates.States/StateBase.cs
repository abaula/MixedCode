using System;
using System.Collections.Generic;

namespace SetOfStates.States
{
    public abstract class StateBase<TObject, TId>
    {
        private readonly List<StateNode<TObject, TId>> _rootStates;

        protected StateBase()
        {
            _rootStates = new List<StateNode<TObject, TId>>();
        }

        protected void State(TId id, Action<StateNode<TObject, TId>> stateAction)
        {
            var state = new StateNode<TObject, TId>(id);
            _rootStates.Add(state);
            stateAction(state);
        }

        public StatesInfo<TObject, TId> Handle(TObject obj)
        {
            var info = new StatesInfo<TObject, TId>();

            foreach (var state in _rootStates)
                state.Handle(obj, info.SetStatesList);

            return info;
        }
    }
}
