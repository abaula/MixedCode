
class Program
{
    private static void Main()
    {
        var listA = new SrfItem[] {
            new SrfItem { Key = "a.a", Weight = 100 },
            new SrfItem { Key = "a.b", Weight = 200 },
            new SrfItem { Key = "a.c", Weight = 800 },
        };

        var listB = new SrfItem[] {
            new SrfItem { Key = "b.a", Weight = 0.1f },
            new SrfItem { Key = "b.b", Weight = 0.12f },
            new SrfItem { Key = "a.c", Weight = 0.3f },
        };

        var mergedList = MergeSrf(listA, listB);

        foreach (var item in mergedList)
        {
            Console.WriteLine($"Key: {item.Key}, Weight: {item.Weight}");
        }
    }

    public class SrfItem
    {
        public required string Key { get; set; }
        public required float Weight { get; set; }
    }

    public static float NormalizeToUnitRange(float value, float min, float max)
    {
        if (min == max)
            throw new ArgumentException("Минимум и максимум не должны быть равны");

        return (value - min) / (max - min);
    }


    public static SrfItem[] MergeSrf(SrfItem[] listA, SrfItem[] listB)
    {
        var listAMaxWight = listA.Max(x => x.Weight);
        var listAMinWight = listA.Min(x => x.Weight);
        var listBMaxWight = listB.Max(x => x.Weight);
        var listBMinWight = listB.Min(x => x.Weight);

        var scaledListA = listA.Select(x => new { x.Key, Weight = NormalizeToUnitRange(x.Weight, listAMinWight, listAMaxWight) }).ToArray();
        var scaledListB = listB.Select(x => new { x.Key, Weight = NormalizeToUnitRange(x.Weight, listBMinWight, listBMaxWight) }).ToArray();

        var mergedList = scaledListA
            .Concat(scaledListB)
            .ToLookup(_ => _.Key, _ => _.Weight)
            .Select(_ => new { _.Key, Weight = _.Max() })
            .ToArray();

        return mergedList
            .OrderByDescending(_ => _.Weight)
            .Select(_ => new SrfItem { Key = _.Key, Weight = _.Weight })
            .ToArray();
    }
}