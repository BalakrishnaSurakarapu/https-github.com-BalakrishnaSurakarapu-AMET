using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMET.Models
{
    [Table("Batch")]
    public class Batch
    {
        [Key]
        public int BatchID { get; set; }
        public string BatchName { get; set; }
        public int IsActive { get; set; }
    }
}
