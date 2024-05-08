using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FacultyApp.Migrations
{
    public partial class AddedCourseOwnershipFilter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Examinations_Courses_CourseId",
                table: "Examinations");

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: "8e120241-2e9b-43f4-b315-50418fb52c46");

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: "b023ea15-4558-40c0-890c-e459d1d5f95f");

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: "cf10cc98-e2fe-4f91-8a59-5a7e97bd142e");

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: "7648ce32-e260-4614-b301-b51d3e09cfa6");

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: "ccb15ded-b5ee-4f0c-a7dc-fee6aeb878eb");

            migrationBuilder.AlterColumn<string>(
                name: "CourseId",
                table: "Examinations",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ScheduledFor",
                table: "Examinations",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "TeacherId",
                table: "Examinations",
                type: "text",
                nullable: false,
                defaultValue: "");

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

            migrationBuilder.CreateIndex(
                name: "IX_Examinations_TeacherId",
                table: "Examinations",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Examinations_Courses_CourseId",
                table: "Examinations",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Examinations_Teachers_TeacherId",
                table: "Examinations",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Examinations_Courses_CourseId",
                table: "Examinations");

            migrationBuilder.DropForeignKey(
                name: "FK_Examinations_Teachers_TeacherId",
                table: "Examinations");

            migrationBuilder.DropIndex(
                name: "IX_Examinations_TeacherId",
                table: "Examinations");

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

            migrationBuilder.DropColumn(
                name: "ScheduledFor",
                table: "Examinations");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "Examinations");

            migrationBuilder.AlterColumn<string>(
                name: "CourseId",
                table: "Examinations",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "Department", "Name", "TeacherId", "Year" },
                values: new object[,]
                {
                    { "8e120241-2e9b-43f4-b315-50418fb52c46", "Computer Science", "Introduction to Programming", "1", 1 },
                    { "b023ea15-4558-40c0-890c-e459d1d5f95f", "Databases", "Database Management Systems", "2", 3 },
                    { "cf10cc98-e2fe-4f91-8a59-5a7e97bd142e", "Computer Science", "Data Structures and Algorithms", "1", 2 }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "CurrentYear", "DateOfBirth", "Email", "EnrolledIn", "FirstName", "GPA", "Graduated", "LastName", "Password" },
                values: new object[,]
                {
                    { "7648ce32-e260-4614-b301-b51d3e09cfa6", 3, new DateTime(1995, 5, 22, 0, 0, 0, 0, DateTimeKind.Utc), "alicesmith@gmail.com", 2021L, "Alice", 8.5f, false, "Smith", "D7BEEAF7D6E5067747D6E412887DDDE01BB7A7784273C459B208A9899BE9C377584A9FA4767748A00A103E6D2942EB4BC6533BCA9804D833F2BD074BC4B0A9AD" },
                    { "ccb15ded-b5ee-4f0c-a7dc-fee6aeb878eb", 4, new DateTime(1998, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), "bobjohnson@gmail.com", 2020L, "Bob", 9.2f, false, "Johnson", "D7BEEAF7D6E5067747D6E412887DDDE01BB7A7784273C459B208A9899BE9C377584A9FA4767748A00A103E6D2942EB4BC6533BCA9804D833F2BD074BC4B0A9AD" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Examinations_Courses_CourseId",
                table: "Examinations",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id");
        }
    }
}
