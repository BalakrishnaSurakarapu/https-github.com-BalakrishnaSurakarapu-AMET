using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMET.Models
{
    [Table("College")]
    public class College
    {
        [Key]
        public int CollegeID { get; set; }
        public string CollegeName { get; set; }
        public int IsActive { get; set; }
    }
}
