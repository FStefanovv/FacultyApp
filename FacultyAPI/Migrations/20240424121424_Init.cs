using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FacultyApp.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    CurrentYear = table.Column<int>(type: "integer", nullable: false),
                    EnrolledIn = table.Column<long>(type: "bigint", nullable: false),
                    Graduated = table.Column<bool>(type: "boolean", nullable: false),
                    GPA = table.Column<float>(type: "real", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Department = table.Column<string>(type: "text", nullable: false),
                    EmployedIn = table.Column<long>(type: "bigint", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    Department = table.Column<string>(type: "text", nullable: false),
                    TeacherId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Courses_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Examinations",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    CourseId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Examinations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Examinations_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "CurrentYear", "DateOfBirth", "Email", "EnrolledIn", "FirstName", "GPA", "Graduated", "LastName", "Password" },
                values: new object[,]
                {
                    { "7648ce32-e260-4614-b301-b51d3e09cfa6", 3, new DateTime(1995, 5, 22, 0, 0, 0, 0, DateTimeKind.Utc), "alicesmith@gmail.com", 2021L, "Alice", 8.5f, false, "Smith", "D7BEEAF7D6E5067747D6E412887DDDE01BB7A7784273C459B208A9899BE9C377584A9FA4767748A00A103E6D2942EB4BC6533BCA9804D833F2BD074BC4B0A9AD" },
                    { "ccb15ded-b5ee-4f0c-a7dc-fee6aeb878eb", 4, new DateTime(1998, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), "bobjohnson@gmail.com", 2020L, "Bob", 9.2f, false, "Johnson", "D7BEEAF7D6E5067747D6E412887DDDE01BB7A7784273C459B208A9899BE9C377584A9FA4767748A00A103E6D2942EB4BC6533BCA9804D833F2BD074BC4B0A9AD" }
                });

            migrationBuilder.InsertData(
                table: "Teachers",
                columns: new[] { "Id", "DateOfBirth", "Department", "Email", "EmployedIn", "FirstName", "LastName", "Password" },
                values: new object[,]
                {
                    { "1", new DateTime(1978, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Computer Science", "johndoe@gmail.com", 2010L, "John", "Doe", "D7BEEAF7D6E5067747D6E412887DDDE01BB7A7784273C459B208A9899BE9C377584A9FA4767748A00A103E6D2942EB4BC6533BCA9804D833F2BD074BC4B0A9AD" },
                    { "2", new DateTime(1985, 5, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Databases", "janedoe@gmail.com", 2012L, "Jane", "Doe", "D7BEEAF7D6E5067747D6E412887DDDE01BB7A7784273C459B208A9899BE9C377584A9FA4767748A00A103E6D2942EB4BC6533BCA9804D833F2BD074BC4B0A9AD" }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "Department", "Name", "TeacherId", "Year" },
                values: new object[,]
                {
                    { "8e120241-2e9b-43f4-b315-50418fb52c46", "Computer Science", "Introduction to Programming", "1", 1 },
                    { "b023ea15-4558-40c0-890c-e459d1d5f95f", "Databases", "Database Management Systems", "2", 3 },
                    { "cf10cc98-e2fe-4f91-8a59-5a7e97bd142e", "Computer Science", "Data Structures and Algorithms", "1", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_TeacherId",
                table: "Courses",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Examinations_CourseId",
                table: "Examinations",
                column: "CourseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Examinations");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Teachers");
        }
    }
}
