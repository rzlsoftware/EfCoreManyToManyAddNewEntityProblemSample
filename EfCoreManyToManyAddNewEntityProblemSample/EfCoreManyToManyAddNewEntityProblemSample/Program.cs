using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using static System.Console;

namespace EfCoreManyToManyAddNewEntityProblemSample
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new SampleDbContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var teacher = context.CreateAddedEntity<Teacher>();
                teacher.Name = "Sepp";
                var student = context.CreateAddedEntity<Student>();
                student.Name = "Luis";

                var ts = context.CreateAddedEntity<Teacher_Student>();
                ts.Teacher = teacher;
                ts.Student = student;

                context.SaveChanges();
            }

            Problem();
            WriteLine("Ready");
            ReadKey();
        }

        private static void Problem()
        {
            using (var context = new SampleDbContext(useLogging: true))
            {
                var teacher = context.Teachers.FirstOrDefault();

                // 1.
                var connections = context.Teacher_Students.ToList();
                // 2.
                var student = context.CreateAddedEntity<Student>();

                student.Name = "Alex";
                var ts = context.CreateAddedEntity<Teacher_Student>();
                ts.Teacher = teacher;
                // 3.
                ts.TeacherId = teacher.Id;

                ts.Student = student;
                // 4.
                ts.StudentId = student.Id;

                WriteLine();
                context.SaveChanges();
            }
        }
    }

    public static partial class Extensions
    {
        public static T CreateAddedEntity<T>(this DbContext context)
            where T : class
        {
            var entity = Activator.CreateInstance<T>();
            context.Add(entity);
            return entity;
        }
    }
}
