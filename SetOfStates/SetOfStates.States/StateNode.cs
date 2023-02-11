using System;
using System.Collections.Generic;
using System.Linq;

namespace SetOfStates.States
{
    public sealed class StateNode<TObject, TId>
    {
        private readonly List<StateNode<TObject, TId>> _children;
        private readonly List<Func<TObject, bool>> _conditions;
        
        public StateNode(TId id, StateNode<TObject, TId> parent)
        {
            Id = id;
            Parent = parent;
            _children = new List<StateNode<TObject, TId>>();
            _conditions = new List<Func<TObject, bool>>();
        }

        public TId Id { get; }
        public StateNode<TObject, TId> Parent { get; }

        public StateNode<TObject, TId> AddState(TId id)
        {
            var state = new StateNode<TObject, TId>(id, this);
            _children.Add(state);
            return state;
        }

        public void AddCondition(Func<TObject, bool> condition)
        {
            _conditions.Add(condition);
        }

        internal void Handle(TObject obj, List<StateNode<TObject, TId>> setStatesList)
        {
            if (_conditions.Any(condition => !condition(obj)))
                return;

            setStatesList.Add(this);

            foreach (var state in _children)
                state.Handle(obj, setStatesList);
        }
    }
}
