using System.Collections.Generic;

namespace EfCoreManyToManyAddNewEntityProblemSample
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public HashSet<Teacher_Student> Teachers { get; } = new HashSet<Teacher_Student>();
    }
}
