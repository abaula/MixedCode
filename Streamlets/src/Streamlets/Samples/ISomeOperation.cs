
namespace Streamlets.Samples
{
    public interface ISomeOperation
    {
        Task<OutputDto> GetAsync(InputDto input);
        OutputDto Get(InputDto input);
    }
}