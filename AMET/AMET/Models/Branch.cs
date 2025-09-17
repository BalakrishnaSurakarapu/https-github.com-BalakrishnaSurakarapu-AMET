using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMET.Models
{
    [Table("Branch")]
    public class Branch
    {
        [Key]
        public int BranchID { get; set; }
        public string BranchName { get; set; }
        public int IsActive { get; set; }
    }
}
