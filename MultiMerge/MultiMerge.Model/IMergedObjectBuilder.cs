using System.Collections.Generic;

namespace MultiMerge.Model
{
    public interface IMergedObjectBuilder
    {
        IMergedObject BuildMergedObjectFromDiffs(List<IDiffObject> diffObjects);
    }
}
