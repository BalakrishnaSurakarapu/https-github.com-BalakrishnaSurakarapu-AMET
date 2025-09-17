using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMET.Models
{
    [Table("AttendanceMarkCriteria")]
    public class AttendanceMarkCriteria
    {
        [Key]
        public int CriteriaID { get; set; }
        [ForeignKey(nameof(AttendanceSettings))]
        public int AttendanceSettingID { get; set; }
        public int FromPercent { get; set; }
        public int ToPercent { get; set; }
        public int Mark { get; set; }
        public int IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        // Navigation
        public AttendanceSettings AttendanceSettings { get; set; }
    }
}
