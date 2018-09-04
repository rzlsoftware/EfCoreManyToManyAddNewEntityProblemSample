using Microsoft.EntityFrameworkCore;

namespace EfCoreManyToManyAddNewEntityProblemSample
{
    public class SampleDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer("Server=.;Database=ReproEfCoreManyToManyAddNewEntityProblemSample;Integrated Security=True");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Teacher_Student>(b =>
            {
                b.HasKey(ts => new { ts.TeacherId, ts.StudentId });
                b.HasOne(ts => ts.Teacher)
                 .WithMany(t => t.Students)
                 .HasForeignKey(t => t.TeacherId);
                b.HasOne(ts => ts.Student)
                 .WithMany(s => s.Teachers)
                 .HasForeignKey(ts => ts.StudentId);
            });
        }

        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher_Student> Teacher_Students { get; set; }
    }
}
