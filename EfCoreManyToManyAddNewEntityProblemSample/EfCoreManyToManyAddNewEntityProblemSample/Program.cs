using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

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
            Console.WriteLine("Ready");
            Console.ReadKey();
        }

        private static void Problem()
        {
            using (var context = new SampleDbContext())
            {
                var teacher = context.Teachers.FirstOrDefault();

                var connections = context.Teacher_Students.ToList();
                var student = context.CreateAddedEntity<Student>();

                student.Name = "Alex";
                var ts = context.CreateAddedEntity<Teacher_Student>();
                ts.Teacher = teacher;
                ts.TeacherId = teacher.Id;
                ts.Student = student;
                ts.StudentId = student.Id;

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
