using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FacultyApp.Migrations
{
    public partial class AddedAvailablePlacesForCourse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "AvailablePlaces",
                table: "Examinations",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "Department", "EspbPoints", "Name", "TeacherId", "Year" },
                values: new object[,]
                {
                    { "0af7852d-b849-40df-92ef-f4c617f67f99", "Computer Science", 4, "Data Structures and Algorithms", "1", 1 },
                    { "c3cddfd9-9d04-4b42-9c4a-8ae262e74036", "Databases", 5, "Database Management Systems", "2", 3 },
                    { "f1c99bf0-3667-431c-81a8-8d538a2369be", "Computer Science", 7, "Introduction to Programming", "1", 1 }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "CurrentYear", "DateOfBirth", "Email", "EnrolledIn", "FirstName", "GPA", "Graduated", "LastName", "Password" },
                values: new object[,]
                {
                    { "0c9a54ee-a0ca-453e-93b0-cb70b2cb47d8", 3, new DateTime(1995, 5, 22, 0, 0, 0, 0, DateTimeKind.Utc), "alicesmith@gmail.com", 2021L, "Alice", 8.5f, false, "Smith", "D7BEEAF7D6E5067747D6E412887DDDE01BB7A7784273C459B208A9899BE9C377584A9FA4767748A00A103E6D2942EB4BC6533BCA9804D833F2BD074BC4B0A9AD" },
                    { "57865505-59b8-45bb-9f4a-d6a9691ee0f9", 1, new DateTime(1998, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), "bobjohnson@gmail.com", 2023L, "Bob", 0f, false, "Johnson", "D7BEEAF7D6E5067747D6E412887DDDE01BB7A7784273C459B208A9899BE9C377584A9FA4767748A00A103E6D2942EB4BC6533BCA9804D833F2BD074BC4B0A9AD" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: "0af7852d-b849-40df-92ef-f4c617f67f99");

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: "c3cddfd9-9d04-4b42-9c4a-8ae262e74036");

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: "f1c99bf0-3667-431c-81a8-8d538a2369be");

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: "0c9a54ee-a0ca-453e-93b0-cb70b2cb47d8");

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: "57865505-59b8-45bb-9f4a-d6a9691ee0f9");

            migrationBuilder.DropColumn(
                name: "AvailablePlaces",
                table: "Examinations");

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
    }
}
