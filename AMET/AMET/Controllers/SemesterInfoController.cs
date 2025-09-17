using AMET.Data;
using AMET.Models;
using AMET.Models.DTOModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AMET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SemesterInfoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public SemesterInfoController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSemesterInfo(SemesterInformationDto dto)
        {
            var semester = new SemesterInformation
            {
                SemesterStartDate = dto.SemesterStartDate,
                SemesterEndDate = dto.SemesterEndDate,
                Holiday = dto.Holiday,
                ScheduleOrder = dto.ScheduleOrder,
                StartingDayOrder = dto.StartingDayOrder,
                NoOfDaysPerWeek = dto.NoOfDaysPerWeek,
                NoOfWorkingDays = dto.NoOfWorkingDays,
                NoOfWorkingHours = dto.NoOfWorkingHours,
                FullDayTotalHours = dto.FullDayTotalHours,
                FullDayMinHours = dto.FullDayMinHours,
                FirstHalfDayTotalHours = dto.FirstHalfDayTotalHours,
                FirstHalfDayMinHours = dto.FirstHalfDayMinHours,
                SecondHalfDayTotalHours = dto.SecondHalfDayTotalHours,
                SecondHalfDayMinHours = dto.SecondHalfDayMinHours,
                CollegeID = dto.CollegeID,
                BatchID = dto.BatchID,
                DegreeID = dto.DegreeID,
                BranchID = dto.BranchID,
                SemesterID = dto.SemesterID,
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow,
                IsDeleted = 0
            };

            if (dto.AttendanceSettings != null)
            {
                var attendance = new AttendanceSettings
                {
                    MaxMark = dto.AttendanceSettings.MaxMark,
                    MinPercentToWriteExam = dto.AttendanceSettings.MinPercentToWriteExam,
                    MinPercentToWriteNextYear = dto.AttendanceSettings.MinPercentToWriteNextYear,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow,
                    IsDeleted = 0,
                    AttendanceMarkCriteria = dto.AttendanceSettings.AttendanceMarkCriteria?
                        .Select(c => new AttendanceMarkCriteria
                        {
                            FromPercent = c.FromPercent,
                            ToPercent = c.ToPercent,
                            Mark = c.Mark,
                            CreatedDate = DateTime.UtcNow,
                            ModifiedDate = DateTime.UtcNow,
                            IsDeleted = 0
                        }).ToList() ?? new List<AttendanceMarkCriteria>()
                };
                semester.AttendanceSettings = attendance;
            }

            semester.GradeSettings = dto.GradeSettings?
                .Select(g => new GradeSettings
                {
                    FromMark = g.FromMark,
                    ToMark = g.ToMark,
                    MarkGrade = g.MarkGrade,
                    Point = g.Point,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow,
                    IsDeleted = 0
                }).ToList() ?? new List<GradeSettings>();

            _context.SemesterInformations.Add(semester);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSemesterInfo), new { SemesterInfoID = semester.SemesterInfoID }, MapToDto(semester));
        }


        [HttpPut("{SemesterInfoID}")]
        public async Task<IActionResult> UpdateSemesterInfo(SemesterInformationDto dto)
        {
            if (dto.SemesterInfoID <= 0)
                return BadRequest("Invalid SemesterInfoID.");

            var semester = await _context.SemesterInformations
                .Include(s => s.AttendanceSettings)
                    .ThenInclude(a => a.AttendanceMarkCriteria)
                .Include(s => s.GradeSettings)
                .FirstOrDefaultAsync(s => s.SemesterInfoID == dto.SemesterInfoID && s.IsDeleted == 0);

            if (semester == null)
                return NotFound($"Semester with SemesterInfoID {dto.SemesterInfoID} not found.");

            // Update main fields
            semester.SemesterStartDate = dto.SemesterStartDate;
            semester.SemesterEndDate = dto.SemesterEndDate;
            semester.Holiday = dto.Holiday;
            semester.ScheduleOrder = dto.ScheduleOrder;
            semester.StartingDayOrder = dto.StartingDayOrder;
            semester.NoOfDaysPerWeek = dto.NoOfDaysPerWeek;
            semester.NoOfWorkingDays = dto.NoOfWorkingDays;
            semester.NoOfWorkingHours = dto.NoOfWorkingHours;
            semester.FullDayTotalHours = dto.FullDayTotalHours;
            semester.FullDayMinHours = dto.FullDayMinHours;
            semester.FirstHalfDayTotalHours = dto.FirstHalfDayTotalHours;
            semester.FirstHalfDayMinHours = dto.FirstHalfDayMinHours;
            semester.SecondHalfDayTotalHours = dto.SecondHalfDayTotalHours;
            semester.SecondHalfDayMinHours = dto.SecondHalfDayMinHours;
            semester.CollegeID = dto.CollegeID;
            semester.BatchID = dto.BatchID;
            semester.DegreeID = dto.DegreeID;
            semester.BranchID = dto.BranchID;
            semester.SemesterID = dto.SemesterID;
            semester.ModifiedDate = DateTime.UtcNow;

            // Attendance Settings
            if (dto.AttendanceSettings != null)
            {
                semester.AttendanceSettings ??= new AttendanceSettings
                {
                    CreatedDate = DateTime.UtcNow,
                    IsDeleted = 0
                };

                var attendance = semester.AttendanceSettings;
                attendance.MaxMark = dto.AttendanceSettings.MaxMark;
                attendance.MinPercentToWriteExam = dto.AttendanceSettings.MinPercentToWriteExam;
                attendance.MinPercentToWriteNextYear = dto.AttendanceSettings.MinPercentToWriteNextYear;
                attendance.ModifiedDate = DateTime.UtcNow;

                // Remove old criteria
                _context.AttendanceMarkCriterias.RemoveRange(attendance.AttendanceMarkCriteria);
                attendance.AttendanceMarkCriteria = new List<AttendanceMarkCriteria>();

                // Insert fresh list
                foreach (var cDto in dto.AttendanceSettings.AttendanceMarkCriteria ?? new List<AttendanceMarkCriteriaDto>())
                {
                    attendance.AttendanceMarkCriteria.Add(new AttendanceMarkCriteria
                    {
                        FromPercent = cDto.FromPercent,
                        ToPercent = cDto.ToPercent,
                        Mark = cDto.Mark,
                        CreatedDate = DateTime.UtcNow,
                        ModifiedDate = DateTime.UtcNow,
                        IsDeleted = 0
                    });
                }
            }

            // Grade Settings
            _context.GradeSettings.RemoveRange(semester.GradeSettings);
            semester.GradeSettings = new List<GradeSettings>();

            foreach (var gDto in dto.GradeSettings ?? new List<GradeSettingsDto>())
            {
                semester.GradeSettings.Add(new GradeSettings
                {
                    FromMark = gDto.FromMark,
                    ToMark = gDto.ToMark,
                    MarkGrade = gDto.MarkGrade,
                    Point = gDto.Point,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow,
                    IsDeleted = 0
                });
            }

            await _context.SaveChangesAsync();

            return Ok(new { Message = $"Semester {dto.SemesterInfoID} updated successfully." });
        }


        [HttpGet("{SemesterInfoID}")]
        public async Task<IActionResult> GetSemesterInfo(int SemesterInfoID)
        {
            var semester = await _context.SemesterInformations
                .Include(s => s.AttendanceSettings)
                    .ThenInclude(a => a.AttendanceMarkCriteria)
                .Include(s => s.GradeSettings)
                .FirstOrDefaultAsync(s => s.SemesterInfoID == SemesterInfoID && s.IsDeleted == 0);

            if (semester == null)
                return NotFound($"Semester with ID {SemesterInfoID} not found.");

            var dto = MapToDto(semester);
            return Ok(dto);
        }


        [HttpDelete("{SemesterInfoID}")]
        public async Task<IActionResult> DeleteSemesterInfo(int SemesterInfoID)
        {
            var semester = await _context.SemesterInformations
                .Include(s => s.AttendanceSettings)
                    .ThenInclude(a => a.AttendanceMarkCriteria)
                .Include(s => s.GradeSettings)
                .FirstOrDefaultAsync(s => s.SemesterInfoID == SemesterInfoID && s.IsDeleted == 0);

            if (semester == null)
                return NotFound($"Semester with ID {SemesterInfoID} not found.");

            semester.IsDeleted = 1;
            semester.ModifiedDate = DateTime.UtcNow;

            if (semester.AttendanceSettings != null)
            {
                semester.AttendanceSettings.IsDeleted = 1;
                semester.AttendanceSettings.ModifiedDate = DateTime.UtcNow;

                foreach (var c in semester.AttendanceSettings.AttendanceMarkCriteria ?? new List<AttendanceMarkCriteria>())
                {
                    c.IsDeleted = 1;
                    c.ModifiedDate = DateTime.UtcNow;
                }
            }

            foreach (var g in semester.GradeSettings ?? new List<GradeSettings>())
            {
                g.IsDeleted = 1;
                g.ModifiedDate = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();
            return Ok(new { Message = $"Semester {SemesterInfoID} deleted successfully." });
        }


        //Helper Mapping Method
        private SemesterInformationDto MapToDto(SemesterInformation semester)
        {
            return new SemesterInformationDto
            {
                SemesterInfoID = semester.SemesterInfoID,
                SemesterStartDate = semester.SemesterStartDate,
                SemesterEndDate = semester.SemesterEndDate,
                Holiday = semester.Holiday,
                ScheduleOrder = semester.ScheduleOrder,
                StartingDayOrder = semester.StartingDayOrder,
                NoOfDaysPerWeek = semester.NoOfDaysPerWeek,
                NoOfWorkingDays = semester.NoOfWorkingDays,
                NoOfWorkingHours = semester.NoOfWorkingHours,
                FullDayTotalHours = semester.FullDayTotalHours,
                FullDayMinHours = semester.FullDayMinHours,
                FirstHalfDayTotalHours = semester.FirstHalfDayTotalHours,
                FirstHalfDayMinHours = semester.FirstHalfDayMinHours,
                SecondHalfDayTotalHours = semester.SecondHalfDayTotalHours,
                SecondHalfDayMinHours = semester.SecondHalfDayMinHours,
                CollegeID = semester.CollegeID,
                BatchID = semester.BatchID,
                DegreeID = semester.DegreeID,
                BranchID = semester.BranchID,
                SemesterID = semester.SemesterID,
                AttendanceSettings = semester.AttendanceSettings != null ? new AttendanceSettingsDto
                {
                    AttendanceSettingID = semester.AttendanceSettings.AttendanceSettingID,
                    MaxMark = semester.AttendanceSettings.MaxMark,
                    MinPercentToWriteExam = semester.AttendanceSettings.MinPercentToWriteExam,
                    MinPercentToWriteNextYear = semester.AttendanceSettings.MinPercentToWriteNextYear,
                    AttendanceMarkCriteria = semester.AttendanceSettings.AttendanceMarkCriteria?
                        .Where(c => c.IsDeleted == 0)
                        .Select(c => new AttendanceMarkCriteriaDto
                        {
                            CriteriaID = c.CriteriaID,
                            FromPercent = c.FromPercent,
                            ToPercent = c.ToPercent,
                            Mark = c.Mark
                        }).ToList()
                } : null,
                GradeSettings = semester.GradeSettings?
                    .Where(g => g.IsDeleted == 0)
                    .Select(g => new GradeSettingsDto
                    {
                        GradeSettingID = g.GradeSettingID,
                        FromMark = g.FromMark,
                        ToMark = g.ToMark,
                        MarkGrade = g.MarkGrade,
                        Point = g.Point
                    }).ToList()
            };
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<SemesterInformation>>> GetSemesterInfo()
        {

            var semesterInfoList = await _context.SemesterInformations
                .Where(s => s.IsDeleted == 0)
                 .Include(s => s.College)
                 .Include(s => s.Batch)
                 .Include(s => s.Degree)
                 .Include(s => s.Branch)
                 .Include(s => s.Semester)
                 .Select(s => new SelectSemesterInformation
                 {
                     SemesterInfoID = s.SemesterInfoID,

                     CollegeID = s.CollegeID,
                     CollegeName = s.College.CollegeName,

                     BatchID = s.BatchID,
                     BatchName = s.Batch.BatchName,

                     DegreeID = s.DegreeID,
                     DegreeName = s.Degree.DegreeName,

                     BranchID = s.BranchID,
                     BranchName = s.Branch.BranchName,

                     SemesterID = s.SemesterID,
                     SemesterName = s.Semester.SemesterName,

                     SemesterStartDate = s.SemesterStartDate,
                     SemesterEndDate = s.SemesterEndDate,
                     ScheduleOrder = s.ScheduleOrder,
                     NoOfDaysPerWeek = s.NoOfDaysPerWeek,
                     NoOfWorkingDays = s.NoOfWorkingDays

                 }).ToListAsync();

            return Ok(semesterInfoList);

        }


    }
}
