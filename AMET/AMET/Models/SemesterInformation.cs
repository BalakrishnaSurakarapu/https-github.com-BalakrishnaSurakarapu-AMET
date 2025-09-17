using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMET.Models
{
    [Table("SemesterInformation")]
    public class SemesterInformation
    {
        [Key]
        public int SemesterInfoID { get; set; }
        public DateTime SemesterStartDate { get; set; }
        public DateTime SemesterEndDate { get; set; }
        public string? Holiday { get; set; }
        public string? ScheduleOrder { get; set; }
        public string? StartingDayOrder { get; set; }
        public int? NoOfDaysPerWeek { get; set; }
        public int? NoOfWorkingDays { get; set; }
        public int? NoOfWorkingHours { get; set; }

        public int? FullDayTotalHours { get; set; }
        public int? FullDayMinHours { get; set; }
        public int? FirstHalfDayTotalHours { get; set; }
        public int? FirstHalfDayMinHours { get; set; }
        public int? SecondHalfDayTotalHours { get; set; }
        public int? SecondHalfDayMinHours { get; set; }

        public int IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public int CollegeID { get; set; }
        public int BatchID { get; set; }
        public int DegreeID { get; set; }
        public int BranchID { get; set; }
        public int SemesterID { get; set; }

        // Navigation
        public College College { get; set; }
        public Batch Batch { get; set; }
        public Degree Degree { get; set; }
        public Branch Branch { get; set; }
        public Semester Semester { get; set; }

        public AttendanceSettings AttendanceSettings { get; set; }
        public List<GradeSettings>? GradeSettings { get; set; }
    }
















}
