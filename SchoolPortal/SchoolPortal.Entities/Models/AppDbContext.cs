using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Entities.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<StudentMaster> Students { get; set; }
        public DbSet<StudentAttendanceDetails> StudentAttendance { get; set; }
        public DbSet<TeacherMaster> Teachers { get; set; }
        public DbSet<TeacherClassDetails> TeacherClassDetails { get; set; }
        public DbSet<UserDetails> UserDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Configure the AuditLog entity
            modelBuilder.Entity<AuditLog>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Action).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).HasMaxLength(1000);
                entity.Property(e => e.UserId).IsRequired();
                entity.Property(e => e.IpAddress).HasMaxLength(100);
                entity.Property(e => e.Timestamp).IsRequired();
            });
        }
    }
}
