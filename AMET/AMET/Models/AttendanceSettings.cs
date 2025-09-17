using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMET.Models
{
    [Table("AttendanceSettings")]
    public class AttendanceSettings
    {
        [Key]
        public int AttendanceSettingID { get; set; }
        [ForeignKey(nameof(SemesterInformation))]        
        public int SemesterInfoID { get; set; }
        public int? MaxMark { get; set; }
        public int? MinPercentToWriteExam { get; set; }
        public int? MinPercentToWriteNextYear { get; set; }
        public int IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        // Navigation
        public SemesterInformation SemesterInformation { get; set; }
        public List<AttendanceMarkCriteria> AttendanceMarkCriteria { get; set; }
    }
}
