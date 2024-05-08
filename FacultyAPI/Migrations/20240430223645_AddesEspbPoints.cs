using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FacultyApp.Migrations
{
    public partial class AddesEspbPoints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: "1bae64b7-877a-4777-afe3-ed2e184ef953");

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: "65c0660d-245f-4f62-8400-202ee49c5404");

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: "d9707da0-47c5-44c9-ba9e-b97ce70afe0a");

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: "7a00bff3-3094-4e33-b423-af676849ed0c");

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: "a425224f-4cba-4ca7-9f07-b878d3fc8cbe");

            migrationBuilder.AddColumn<int>(
                name: "EspbPoints",
                table: "Courses",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "Department", "EspbPoints", "Name", "TeacherId", "Year" },
                values: new object[,]
                {
                    { "5c66b176-93eb-414d-a35c-8daeb27c299e", "Computer Science", 7, "Introduction to Programming", "1", 1 },
                    { "a1b77f04-9706-4c16-9966-1afa07ad06c6", "Computer Science", 4, "Data Structures and Algorithms", "1", 1 },
                    { "c3536a3b-f736-4dca-9b95-8350214b400b", "Databases", 5, "Database Management Systems", "2", 3 }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "CurrentYear", "DateOfBirth", "Email", "EnrolledIn", "FirstName", "GPA", "Graduated", "LastName", "Password" },
                values: new object[,]
                {
                    { "37beda5c-7c0a-4c92-9687-e77ec523e674", 1, new DateTime(1998, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), "bobjohnson@gmail.com", 2023L, "Bob", 0f, false, "Johnson", "D7BEEAF7D6E5067747D6E412887DDDE01BB7A7784273C459B208A9899BE9C377584A9FA4767748A00A103E6D2942EB4BC6533BCA9804D833F2BD074BC4B0A9AD" },
                    { "dda62be6-c0e7-4084-91a3-41351d8db4b9", 3, new DateTime(1995, 5, 22, 0, 0, 0, 0, DateTimeKind.Utc), "alicesmith@gmail.com", 2021L, "Alice", 8.5f, false, "Smith", "D7BEEAF7D6E5067747D6E412887DDDE01BB7A7784273C459B208A9899BE9C377584A9FA4767748A00A103E6D2942EB4BC6533BCA9804D833F2BD074BC4B0A9AD" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: "5c66b176-93eb-414d-a35c-8daeb27c299e");

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: "a1b77f04-9706-4c16-9966-1afa07ad06c6");

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: "c3536a3b-f736-4dca-9b95-8350214b400b");

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: "37beda5c-7c0a-4c92-9687-e77ec523e674");

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: "dda62be6-c0e7-4084-91a3-41351d8db4b9");

            migrationBuilder.DropColumn(
                name: "EspbPoints",
                table: "Courses");

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "Department", "Name", "TeacherId", "Year" },
                values: new object[,]
                {
                    { "1bae64b7-877a-4777-afe3-ed2e184ef953", "Computer Science", "Introduction to Programming", "1", 1 },
                    { "65c0660d-245f-4f62-8400-202ee49c5404", "Computer Science", "Data Structures and Algorithms", "1", 2 },
                    { "d9707da0-47c5-44c9-ba9e-b97ce70afe0a", "Databases", "Database Management Systems", "2", 3 }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "CurrentYear", "DateOfBirth", "Email", "EnrolledIn", "FirstName", "GPA", "Graduated", "LastName", "Password" },
                values: new object[,]
                {
                    { "7a00bff3-3094-4e33-b423-af676849ed0c", 3, new DateTime(1995, 5, 22, 0, 0, 0, 0, DateTimeKind.Utc), "alicesmith@gmail.com", 2021L, "Alice", 8.5f, false, "Smith", "D7BEEAF7D6E5067747D6E412887DDDE01BB7A7784273C459B208A9899BE9C377584A9FA4767748A00A103E6D2942EB4BC6533BCA9804D833F2BD074BC4B0A9AD" },
                    { "a425224f-4cba-4ca7-9f07-b878d3fc8cbe", 4, new DateTime(1998, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), "bobjohnson@gmail.com", 2020L, "Bob", 9.2f, false, "Johnson", "D7BEEAF7D6E5067747D6E412887DDDE01BB7A7784273C459B208A9899BE9C377584A9FA4767748A00A103E6D2942EB4BC6533BCA9804D833F2BD074BC4B0A9AD" }
                });
        }
    }
}
