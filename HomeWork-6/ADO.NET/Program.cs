﻿using System;
using Microsoft.Extensions.Configuration;
using Models;
namespace ADO.NET
{
    class Program
    {
        private static string _connectionString;
        static void Main(string[] args)
        {
            SetConnectionString();

            IRepository<Course> courseRepository = GetCourseRepository();
            Course newCourse = new Course() { StartDate = DateTime.Now, EndDate = DateTime.Now, PassCredits = 500 };

            var course = courseRepository.Create(newCourse);
            course.PassCredits = 1000;

            courseRepository.Update(course);

            IRepository<Student> studentRepository = GetStudentRepository();
            Student newStudent = new Student()
            {
                BirthDate = DateTime.Now,
                Email = "Test",
                GitHubLink = "Test",
                Name = "test",
                PhoneNumber = "000"
            };
            var insertedStudent = studentRepository.Create(newStudent);

            //Update

            insertedStudent.Notes += "; Is employed";
            insertedStudent.Courses.Add(course);

            studentRepository.Update(insertedStudent);
            studentRepository.Remove(insertedStudent.Id);
            courseRepository.Remove(course.Id);
        }
        private static IRepository<Course> GetCourseRepository()
        {
            return new CourseRepository(_connectionString);
        }

        private static IRepository<Student> GetStudentRepository()
        {
            return new StudentRepository(_connectionString);
        }
        private static void SetConnectionString()
        {
            var builder = new ConfigurationBuilder().AddJsonFile($"appsettings.json", true, true);
            var configuration = builder.Build();
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
    }
}
