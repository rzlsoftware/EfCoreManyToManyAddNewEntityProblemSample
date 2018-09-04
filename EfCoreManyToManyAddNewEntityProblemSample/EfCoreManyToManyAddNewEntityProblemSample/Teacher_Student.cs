namespace EfCoreManyToManyAddNewEntityProblemSample
{
    public class Teacher_Student
    {
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }
    }
}
