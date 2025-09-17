namespace AMET.Models.DTOModels
{
    public class SelectSemesterInformation
    {

        public int SemesterInfoID { get; set; }

        public int CollegeID { get; set; }
        public string CollegeName { get; set; }

        public int BatchID { get; set; }
        public string BatchName { get; set; }

        public int DegreeID { get; set; }
        public string DegreeName { get; set; }

        public int BranchID { get; set; }
        public string BranchName { get; set; }

        public int SemesterID { get; set; }
        public string SemesterName { get; set; }
        public DateTime SemesterStartDate { get; set; }
        public DateTime SemesterEndDate { get; set; }
        public int? NoOfDaysPerWeek { get; set; }
        public string? ScheduleOrder { get; set; }
        public int? NoOfWorkingDays { get; set; }

    }
}
