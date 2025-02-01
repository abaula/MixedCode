using System.Linq;

namespace SetOfStates.States.Extensions
{
    public static class StatesInfoExtension
    {
        public static bool AnySiblingStates<TObject, TId>(this StatesInfo<TObject, TId> statesInfo)
        {
            return statesInfo.SetStates
                .GroupBy(s => s.Parent)
                .Any(g => g.Count() > 1);
        }

        public static bool NoStates<TObject, TId>(this StatesInfo<TObject, TId> statesInfo)
        {
            return !statesInfo.SetStates.Any();
        }
    }
}
