namespace AMET.Models.DTOModels
{
    public class AttendanceMarkCriteriaDto
    {
        public int? CriteriaID { get; set; }
        public int FromPercent { get; set; }
        public int ToPercent { get; set; }
        public int Mark { get; set; }
    }
}
