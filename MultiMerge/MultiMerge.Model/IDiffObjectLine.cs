namespace MultiMerge.Model
{
    public interface IDiffObjectLine
    {
        DiffState LineState { get; set; }
        int LineCode { get; set; }
        ITextObject SourceText { get; set; }
        int SourceTextLineNumber { get; set; }
    }
}
