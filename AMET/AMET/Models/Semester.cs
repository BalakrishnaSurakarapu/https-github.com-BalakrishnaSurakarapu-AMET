using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMET.Models
{
    [Table("Semester")]
    public class Semester
    {
        [Key]
        public int SemesterID { get; set; }
        public string SemesterName { get; set; }
        public int IsActive { get; set; }
    }
}
