using System;
using System.Collections.Generic;
using System.Linq;

namespace SetOfStates.States
{
    public sealed class StateNode<TObject, TId>
    {
        private readonly List<StateNode<TObject, TId>> _childrenStates;
        private readonly List<Func<TObject, bool>> _conditions;

        public StateNode(TId id)
        {
            Id = id;
            _childrenStates = new List<StateNode<TObject, TId>>();
            _conditions = new List<Func<TObject, bool>>();
        }

        public TId Id { get; }

        public StateNode<TObject, TId> State(TId id, Action<StateNode<TObject, TId>> stateAction)
        {
            var state = new StateNode<TObject, TId>(id);
            _childrenStates.Add(state);
            stateAction(state);
            return this;
        }

        public StateNode<TObject, TId> Condition(Func<TObject, bool> expression)
        {
            _conditions.Add(expression);
            return this;
        }

        internal void Handle(TObject obj, List<StateNode<TObject, TId>> setStatesList)
        {
            if (_conditions.Any(condition => !condition(obj)))
                return;

            setStatesList.Add(this);

            foreach (var state in _childrenStates)
                state.Handle(obj, setStatesList);
        }
    }
}
