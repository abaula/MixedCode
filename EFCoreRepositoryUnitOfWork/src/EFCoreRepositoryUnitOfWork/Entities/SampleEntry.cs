
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreRepositoryUnitOfWork.Entities
{
    [Table("SampleEntry", Schema = "dbo")]
    public class SampleEntry
    {
        public int? Id { get; set; }
        public string Value { get; set; }
    }
}
