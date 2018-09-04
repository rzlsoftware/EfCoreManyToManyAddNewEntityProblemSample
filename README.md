# EfCoreManyToManyAddNewEntityProblemSample

Sample Project for [EF Core Issue 0000]().
When adding a new `entity` to a M/N connection in a special order [EF Core](https://github.com/aspnet/EntityFrameworkCore) throws `DbUpdateException` with message `The INSERT statement conflicted with the FOREIGN KEY constraint...`

```csharp
public static T CreateAddedEntity<T>(this DbContext context)
    where T : class
{
    var entity = Activator.CreateInstance<T>();
    context.Add(entity);
    return entity;
}
```

```csharp
using (var context = new SampleDbContext(useLogging: false))
{
    var teacher = context.Teachers.FirstOrDefault();

    // Step 1.
    var connections = context.Teacher_Students.ToList();
    // Step 2.
    var student = context.CreateAddedEntity<Student>();

    student.Name = "Alex";
    var ts = context.CreateAddedEntity<Teacher_Student>();
    ts.Teacher = teacher;
    // Step 3.
    ts.TeacherId = teacher.Id;

    ts.Student = student;
    // Step 4.
    ts.StudentId = student.Id;

    WriteLine();
    context.SaveChanges();
}
```

The order of the four steps are important.
For example if you first call step 2 `var student = context.CreateAddedEntity<Student>();` and then step 1 `var connections = context.Teacher_Students.ToList();` everthing works great.  
Of if you just comment out step 1, 3 or 4 everything works great.
