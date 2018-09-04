using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EfCoreManyToManyAddNewEntityProblemSample
{
    public class SampleDbContext : DbContext
    {
        private readonly bool useLogging;

        public SampleDbContext(bool useLogging = false)
            => this.useLogging = useLogging;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=Repro13205_EfCoreManyToManyAddNewEntityProblemSample;Integrated Security=True");

            if (useLogging)
                optionsBuilder.UseConsoleLogging();
        }

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

    public static partial class Extensions
    {
        public static DbContextOptionsBuilder UseConsoleLogging(this DbContextOptionsBuilder @this)
        {
            var loggerFactory = new LoggerFactory();
            loggerFactory.AddProvider(new LoggerProvider());
            @this.UseLoggerFactory(loggerFactory);

            return @this;
        }
    }
}
