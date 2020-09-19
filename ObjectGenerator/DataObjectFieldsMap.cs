namespace ObjectGenerator
{
    public readonly struct DataObjectFieldsMap
    {
        public readonly (string, string)[] Map;

        public DataObjectFieldsMap((string, string)[] map)
            => Map = map;
    }
}