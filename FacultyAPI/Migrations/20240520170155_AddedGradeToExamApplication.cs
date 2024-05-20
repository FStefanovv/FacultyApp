using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FacultyApp.Migrations
{
    public partial class AddedGradeToExamApplication : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: "1f6e3588-6e88-4ed1-aa3f-906dbd353eb6");

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: "f30952cc-388e-45c7-9331-ef6476e4f097");

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: "f8a04881-de7e-453f-93ef-c12f52c60175");

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: "47687f62-4247-4ea7-a098-3f1fba976f8c");

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: "b953dabc-5369-4e19-a4b7-355c4cee4489");

            migrationBuilder.AddColumn<int>(
                name: "Grade",
                table: "ExamApplications",
                type: "integer",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "Department", "EspbPoints", "Name", "TeacherId", "Year" },
                values: new object[,]
                {
                    { "429b8c62-01c4-4e00-b15d-8bf8340d19b1", "Computer Science", 4, "Data Structures and Algorithms", "1", 1 },
                    { "a6f938c3-e1db-45e7-9a20-2d23c283c768", "Databases", 5, "Database Management Systems", "2", 3 },
                    { "ef9bea75-9617-40f8-b187-c4a0f8a75290", "Computer Science", 7, "Introduction to Programming", "1", 1 }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "CurrentYear", "DateOfBirth", "Email", "EnrolledIn", "FirstName", "GPA", "Graduated", "LastName", "Password" },
                values: new object[,]
                {
                    { "8c5d9286-849b-4f24-aadc-c7cf269bf04e", 3, new DateTime(1995, 5, 22, 0, 0, 0, 0, DateTimeKind.Utc), "alicesmith@gmail.com", 2021L, "Alice", 8.5f, false, "Smith", "D7BEEAF7D6E5067747D6E412887DDDE01BB7A7784273C459B208A9899BE9C377584A9FA4767748A00A103E6D2942EB4BC6533BCA9804D833F2BD074BC4B0A9AD" },
                    { "977f193c-ab84-4b34-8525-8d1b095b4e85", 1, new DateTime(1998, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), "bobjohnson@gmail.com", 2023L, "Bob", 0f, false, "Johnson", "D7BEEAF7D6E5067747D6E412887DDDE01BB7A7784273C459B208A9899BE9C377584A9FA4767748A00A103E6D2942EB4BC6533BCA9804D833F2BD074BC4B0A9AD" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: "429b8c62-01c4-4e00-b15d-8bf8340d19b1");

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: "a6f938c3-e1db-45e7-9a20-2d23c283c768");

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: "ef9bea75-9617-40f8-b187-c4a0f8a75290");

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: "8c5d9286-849b-4f24-aadc-c7cf269bf04e");

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: "977f193c-ab84-4b34-8525-8d1b095b4e85");

            migrationBuilder.DropColumn(
                name: "Grade",
                table: "ExamApplications");

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "Department", "EspbPoints", "Name", "TeacherId", "Year" },
                values: new object[,]
                {
                    { "1f6e3588-6e88-4ed1-aa3f-906dbd353eb6", "Computer Science", 7, "Introduction to Programming", "1", 1 },
                    { "f30952cc-388e-45c7-9331-ef6476e4f097", "Computer Science", 4, "Data Structures and Algorithms", "1", 1 },
                    { "f8a04881-de7e-453f-93ef-c12f52c60175", "Databases", 5, "Database Management Systems", "2", 3 }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "CurrentYear", "DateOfBirth", "Email", "EnrolledIn", "FirstName", "GPA", "Graduated", "LastName", "Password" },
                values: new object[,]
                {
                    { "47687f62-4247-4ea7-a098-3f1fba976f8c", 1, new DateTime(1998, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), "bobjohnson@gmail.com", 2023L, "Bob", 0f, false, "Johnson", "D7BEEAF7D6E5067747D6E412887DDDE01BB7A7784273C459B208A9899BE9C377584A9FA4767748A00A103E6D2942EB4BC6533BCA9804D833F2BD074BC4B0A9AD" },
                    { "b953dabc-5369-4e19-a4b7-355c4cee4489", 3, new DateTime(1995, 5, 22, 0, 0, 0, 0, DateTimeKind.Utc), "alicesmith@gmail.com", 2021L, "Alice", 8.5f, false, "Smith", "D7BEEAF7D6E5067747D6E412887DDDE01BB7A7784273C459B208A9899BE9C377584A9FA4767748A00A103E6D2942EB4BC6533BCA9804D833F2BD074BC4B0A9AD" }
                });
        }
    }
}
