namespace AMET.Models.DTOModels
{
    public class GradeSettingsDto
    {
        public int? GradeSettingID { get; set; }
        public int FromMark { get; set; }
        public int ToMark { get; set; }
        public string? MarkGrade { get; set; } = "";
        public int Point { get; set; }
    }
}
