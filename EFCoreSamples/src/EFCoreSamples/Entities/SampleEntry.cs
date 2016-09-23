
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreSamples.Entities
{
    [Table("SampleEntry")]
    public class SampleEntry
    {
        public int? Id { get; set; }
        public string Value { get; set; }
    }
}
