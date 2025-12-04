namespace MultiMerge.Model
{
    public interface IDiffObjectBuilder
    {
        IDiffObject BuildDiffObjectFromTexts(ITextObject originalText, ITextObject versionText);
    }
}
