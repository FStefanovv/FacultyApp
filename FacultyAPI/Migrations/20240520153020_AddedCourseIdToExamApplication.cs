using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FacultyApp.Migrations
{
    public partial class AddedCourseIdToExamApplication : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_Examinations_ExaminationId",
                table: "Applications");

            migrationBuilder.DropForeignKey(
                name: "FK_Applications_Students_StudentId",
                table: "Applications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Applications",
                table: "Applications");

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: "2c606000-1d29-4c01-a560-c886160e2fd6");

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: "969680cc-9dca-461c-b9ef-a89244bfcd5d");

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: "d1c270fa-13b5-43ab-9d08-8afed239cb68");

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: "0105ec87-91f4-4b81-8867-8152e6121c99");

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: "7a3b8ea0-376b-486a-998e-d17bf138ca4e");

            migrationBuilder.RenameTable(
                name: "Applications",
                newName: "ExamApplications");

            migrationBuilder.RenameIndex(
                name: "IX_Applications_ExaminationId",
                table: "ExamApplications",
                newName: "IX_ExamApplications_ExaminationId");

            migrationBuilder.AddColumn<string>(
                name: "CourseId",
                table: "ExamApplications",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "ExamApplications",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExamApplications",
                table: "ExamApplications",
                columns: new[] { "StudentId", "ExaminationId" });

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

            migrationBuilder.AddForeignKey(
                name: "FK_ExamApplications_Examinations_ExaminationId",
                table: "ExamApplications",
                column: "ExaminationId",
                principalTable: "Examinations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExamApplications_Students_StudentId",
                table: "ExamApplications",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamApplications_Examinations_ExaminationId",
                table: "ExamApplications");

            migrationBuilder.DropForeignKey(
                name: "FK_ExamApplications_Students_StudentId",
                table: "ExamApplications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExamApplications",
                table: "ExamApplications");

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

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "ExamApplications");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "ExamApplications");

            migrationBuilder.RenameTable(
                name: "ExamApplications",
                newName: "Applications");

            migrationBuilder.RenameIndex(
                name: "IX_ExamApplications_ExaminationId",
                table: "Applications",
                newName: "IX_Applications_ExaminationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Applications",
                table: "Applications",
                columns: new[] { "StudentId", "ExaminationId" });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "Department", "EspbPoints", "Name", "TeacherId", "Year" },
                values: new object[,]
                {
                    { "2c606000-1d29-4c01-a560-c886160e2fd6", "Computer Science", 4, "Data Structures and Algorithms", "1", 1 },
                    { "969680cc-9dca-461c-b9ef-a89244bfcd5d", "Databases", 5, "Database Management Systems", "2", 3 },
                    { "d1c270fa-13b5-43ab-9d08-8afed239cb68", "Computer Science", 7, "Introduction to Programming", "1", 1 }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "CurrentYear", "DateOfBirth", "Email", "EnrolledIn", "FirstName", "GPA", "Graduated", "LastName", "Password" },
                values: new object[,]
                {
                    { "0105ec87-91f4-4b81-8867-8152e6121c99", 3, new DateTime(1995, 5, 22, 0, 0, 0, 0, DateTimeKind.Utc), "alicesmith@gmail.com", 2021L, "Alice", 8.5f, false, "Smith", "D7BEEAF7D6E5067747D6E412887DDDE01BB7A7784273C459B208A9899BE9C377584A9FA4767748A00A103E6D2942EB4BC6533BCA9804D833F2BD074BC4B0A9AD" },
                    { "7a3b8ea0-376b-486a-998e-d17bf138ca4e", 1, new DateTime(1998, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), "bobjohnson@gmail.com", 2023L, "Bob", 0f, false, "Johnson", "D7BEEAF7D6E5067747D6E412887DDDE01BB7A7784273C459B208A9899BE9C377584A9FA4767748A00A103E6D2942EB4BC6533BCA9804D833F2BD074BC4B0A9AD" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_Examinations_ExaminationId",
                table: "Applications",
                column: "ExaminationId",
                principalTable: "Examinations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_Students_StudentId",
                table: "Applications",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
