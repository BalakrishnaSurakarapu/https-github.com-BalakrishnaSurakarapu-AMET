using AMET.Models.DTOModels;

namespace AMET.Models.DTOModels
{
    public class AttendanceSettingsDto
    {
        public int? AttendanceSettingID { get; set; }
        public int? MaxMark { get; set; }
        public int? MinPercentToWriteExam { get; set; }
        public int? MinPercentToWriteNextYear { get; set; }
        public List<AttendanceMarkCriteriaDto>? AttendanceMarkCriteria { get; set; }
    }
}
