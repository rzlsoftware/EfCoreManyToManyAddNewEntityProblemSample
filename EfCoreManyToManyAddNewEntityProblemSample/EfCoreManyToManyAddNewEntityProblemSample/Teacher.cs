using System.Collections.Generic;

namespace EfCoreManyToManyAddNewEntityProblemSample
{
    public class Teacher
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public HashSet<Teacher_Student> Students { get; set; } = new HashSet<Teacher_Student>();
    }
}
