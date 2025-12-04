namespace MultiMerge.Model
{
    public interface IUniqueTextLinesStorage
    {
        int AddLine(string text);
        string GetLine(int code);
    }
}
