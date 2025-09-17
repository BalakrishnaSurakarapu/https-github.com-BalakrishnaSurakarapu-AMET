using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMET.Models
{
    [Table("Degree")]
    public class Degree
    {
        [Key]
        public int DegreeID { get; set; }
        public string DegreeName { get; set; }
        public int IsActive { get; set; }
    }
}
