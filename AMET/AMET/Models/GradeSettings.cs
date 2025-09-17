using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMET.Models
{
    [Table("GradeSettings")]
    public class GradeSettings
    {
        [Key]
        public int GradeSettingID { get; set; }
        [ForeignKey(nameof(SemesterInformation))]
        public int SemesterInfoID { get; set; }
        public int FromMark { get; set; }
        public int ToMark { get; set; }
        public string? MarkGrade { get; set; }
        public int Point { get; set; }
        public int IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        // Navigation
        public SemesterInformation SemesterInformation { get; set; }
    }
}
