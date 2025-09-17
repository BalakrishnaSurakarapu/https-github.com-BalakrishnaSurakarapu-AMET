using AMET.Models.DTOModels;

namespace AMET.Models.DTOModels
{
    public class SemesterInformationDto
    { 
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
        public int CollegeID { get; set; }
        public int BatchID { get; set; }
        public int DegreeID { get; set; }
        public int BranchID { get; set; }
        public int SemesterID { get; set; }

        public AttendanceSettingsDto? AttendanceSettings { get; set; }
        public List<GradeSettingsDto>? GradeSettings { get; set; }
    }
}
