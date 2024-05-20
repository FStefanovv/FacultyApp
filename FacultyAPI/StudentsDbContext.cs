namespace FacultyApp;

using Microsoft.EntityFrameworkCore;

using FacultyApp.Model;
using FacultyApp.Utils;

public class StudentsDbContext : DbContext {
    public StudentsDbContext(DbContextOptions<StudentsDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ExaminationApplication>()
            .HasKey(ea => new { ea.StudentId, ea.ExaminationId });

        modelBuilder.Entity<Teacher>().HasData(
            new Teacher
            {
                Id = "1",
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new DateTime(1978, 1, 15, 0, 0, 0, DateTimeKind.Utc),
                Email = "johndoe@gmail.com",
                Password = PasswordHasher.HashPassword("password"),
                Department = "Computer Science",
                EmployedIn = 2010
            },
            new Teacher
            {
                Id =  "2",
                FirstName = "Jane",
                LastName = "Doe",
                DateOfBirth = new DateTime(1985, 5, 20, 0, 0, 0, DateTimeKind.Utc),
                Email = "janedoe@gmail.com",
                Password = PasswordHasher.HashPassword("password"),
                Department = "Databases",
                EmployedIn = 2012
            });

        modelBuilder.Entity<Student>().HasData(
            new Student
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = "Alice",
                LastName = "Smith",
                DateOfBirth = new DateTime(1995, 5, 22, 0, 0, 0, DateTimeKind.Utc),
                Email = "alicesmith@gmail.com",
                Password = PasswordHasher.HashPassword("password"),
                CurrentYear = 3,
                EnrolledIn = 2021,
                Graduated = false,
                GPA = 8.5f
            },
            new Student
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = "Bob",
                LastName = "Johnson",
                DateOfBirth = new DateTime(1998, 9, 10, 0, 0, 0, DateTimeKind.Utc),
                Email = "bobjohnson@gmail.com",
                Password = PasswordHasher.HashPassword("password"),
                CurrentYear = 1,
                EnrolledIn = 2023,
                Graduated = false
            });

        modelBuilder.Entity<Course>().HasData(
            new Course
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Introduction to Programming",
                Year = 1,
                EspbPoints = 7,
                Department = "Computer Science",
                TeacherId = "1"
            },
            new Course
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Data Structures and Algorithms",
                Year = 1,
                EspbPoints = 4,
                Department = "Computer Science",
                TeacherId = "1"
            },
            new Course
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Database Management Systems",
                Year = 3,
                EspbPoints = 5,
                Department = "Databases",
                TeacherId = "2"
            });

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Student> Students {get; set;}
    public DbSet<Teacher> Teachers {get; set;}
    public DbSet<Course> Courses {get; set;}  
    public DbSet<Examination> Examinations {get; set;}
    public DbSet<ExaminationApplication> ExamApplications {get; set;}
}

