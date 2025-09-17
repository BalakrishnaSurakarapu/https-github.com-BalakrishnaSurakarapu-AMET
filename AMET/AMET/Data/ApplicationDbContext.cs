using AMET.Models;
using Microsoft.EntityFrameworkCore;

namespace AMET.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<College> Colleges { get; set; }
        public DbSet<Batch> Batches { get; set; }
        public DbSet<Degree> Degrees { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<SemesterInformation> SemesterInformations { get; set; }
        public DbSet<AttendanceSettings> AttendanceSettings { get; set; }
        public DbSet<AttendanceMarkCriteria> AttendanceMarkCriterias { get; set; }
        public DbSet<GradeSettings> GradeSettings { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Table mappings
            modelBuilder.Entity<College>().ToTable("College");
            modelBuilder.Entity<Batch>().ToTable("Batch");
            modelBuilder.Entity<Degree>().ToTable("Degree");
            modelBuilder.Entity<Branch>().ToTable("Branch");
            modelBuilder.Entity<Semester>().ToTable("Semester");
            modelBuilder.Entity<SemesterInformation>()
                .ToTable("SemesterInformation")
                .HasKey(s => s.SemesterInfoID);


            // SemesterInformation ↔ AttendanceSettings (1:1)
            modelBuilder.Entity<SemesterInformation>()
                .HasOne(s => s.AttendanceSettings)
                .WithOne(a => a.SemesterInformation)
                .HasForeignKey<AttendanceSettings>(a => a.SemesterInfoID);

            // SemesterInformation ↔ GradeSettings (1:M)
            modelBuilder.Entity<SemesterInformation>()
                .HasMany(s => s.GradeSettings)
                .WithOne(g => g.SemesterInformation)
                .HasForeignKey(g => g.SemesterInfoID);

            // AttendanceSettings ↔ AttendanceMarkCriteria (1:M)
            modelBuilder.Entity<AttendanceSettings>()
                .HasMany(a => a.AttendanceMarkCriteria)
                .WithOne(c => c.AttendanceSettings)
                .HasForeignKey(c => c.AttendanceSettingID);

        }
    

    }
}


