using System;

namespace SetOfStates.States.Extensions
{
    public static class StateNodeExtension
    {
        public static StateNode<TObject, TId> State<TObject, TId>(this StateNode<TObject, TId> state, 
            TId id,
            Action<StateNode<TObject, TId>> stateAction)
        {
            var childState = state.AddState(id);
            stateAction(childState);
            return state;
        }

        public static StateNode<TObject, TId> Condition<TObject, TId>(this StateNode<TObject, TId> state, 
            Func<TObject, bool> condition)
        {
            state.AddCondition(condition);
            return state;
        }
    }
}
