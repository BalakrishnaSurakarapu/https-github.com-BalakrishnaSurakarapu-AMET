using AMET.Data;
using AMET.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AMET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        public ListController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("college")]
        public async Task<ActionResult<IEnumerable<College>>> GetColleges() =>
                    await _context.Colleges.ToListAsync();

        [HttpGet("batch")]
        public async Task<ActionResult<IEnumerable<Batch>>> GetBatches() =>
                   await _context.Batches.ToListAsync();

        [HttpGet("degree")]
        public async Task<ActionResult<IEnumerable<Degree>>> GetDegrees() =>
                   await _context.Degrees.ToListAsync();

        [HttpGet("branch")]
        public async Task<ActionResult<IEnumerable<Branch>>> GetBranchs() =>
                   await _context.Branches.ToListAsync();

        [HttpGet("semester")]
        public async Task<ActionResult<IEnumerable<Semester>>> GetSemesters() =>
                  await _context.Semesters.ToListAsync();

    }
}
