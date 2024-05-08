using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FacultyApp.Migrations
{
    public partial class AddedExamStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: "5e8f2466-c523-4a04-8a4c-9c6b8b2b0a18");

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: "6fa22881-a88e-4916-b9b3-f91f850a3f06");

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: "ed72c2d1-28b9-4c11-872e-b58abb05a20d");

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: "095bf885-2554-47e0-a91f-edc89969d6df");

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: "61ea6414-7bae-4485-819f-5a232b386f01");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Examinations",
                type: "integer",
                nullable: false,
                defaultValue: 0);

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Examinations");

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "Department", "Name", "TeacherId", "Year" },
                values: new object[,]
                {
                    { "5e8f2466-c523-4a04-8a4c-9c6b8b2b0a18", "Computer Science", "Data Structures and Algorithms", "1", 2 },
                    { "6fa22881-a88e-4916-b9b3-f91f850a3f06", "Computer Science", "Introduction to Programming", "1", 1 },
                    { "ed72c2d1-28b9-4c11-872e-b58abb05a20d", "Databases", "Database Management Systems", "2", 3 }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "CurrentYear", "DateOfBirth", "Email", "EnrolledIn", "FirstName", "GPA", "Graduated", "LastName", "Password" },
                values: new object[,]
                {
                    { "095bf885-2554-47e0-a91f-edc89969d6df", 4, new DateTime(1998, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), "bobjohnson@gmail.com", 2020L, "Bob", 9.2f, false, "Johnson", "D7BEEAF7D6E5067747D6E412887DDDE01BB7A7784273C459B208A9899BE9C377584A9FA4767748A00A103E6D2942EB4BC6533BCA9804D833F2BD074BC4B0A9AD" },
                    { "61ea6414-7bae-4485-819f-5a232b386f01", 3, new DateTime(1995, 5, 22, 0, 0, 0, 0, DateTimeKind.Utc), "alicesmith@gmail.com", 2021L, "Alice", 8.5f, false, "Smith", "D7BEEAF7D6E5067747D6E412887DDDE01BB7A7784273C459B208A9899BE9C377584A9FA4767748A00A103E6D2942EB4BC6533BCA9804D833F2BD074BC4B0A9AD" }
                });
        }
    }
}
