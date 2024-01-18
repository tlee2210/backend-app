using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace backend_app.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdminAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminAccounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublishDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactUs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    YouTubeLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactUs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Feedbacks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    responses = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedbacks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ArticleCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArticleId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArticleCategories_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticleCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Faculties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Skill_learn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Opportunities = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EntryScore = table.Column<int>(type: "int", nullable: false),
                    Course_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faculties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Faculties_Courses_Course_id",
                        column: x => x.Course_id,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AdminAccounts",
                columns: new[] { "Id", "Email", "Name", "Password", "Role" },
                values: new object[] { 1, "Tlee2210@gmail.com", "Tlee", "$2a$11$HqUXqf6rzrEmvC2LpX7yfe03wpMuTUElCNIdBno85rkdHxfokemKq", "Admin" });

            migrationBuilder.InsertData(
                table: "ContactUs",
                columns: new[] { "Id", "Address", "Description", "Email", "Phone", "YouTubeLink" },
                values: new object[] { 1, "391 Nam Ky Khoi Nghia,Quan 3", "Welcome to [TQVC University] - a place that fosters personal development and academic success. We take pride in providing a diverse and positive learning environment where students not only gain knowledge but also develop essential life skills.\r\n\r\nMission:\r\nWe are committed to educating and developing individuals with deep knowledge, flexible skills, and compassion. Our mission is to create a motivated, innovative, and globally-minded community of students.\r\n\r\nEducation:\r\nAt [TQVC University], we offer a diverse range of educational programs from undergraduate to postgraduate levels, training students to become visionaries ready to face the challenges of the contemporary world.\r\n\r\nFacilities:\r\nWith modern and comprehensive facilities, we have created an international-standard environment for learning and research. Our library, laboratories, and sports facilities are well-equipped to support students in their academic and research endeavors.\r\n\r\nCore Values:\r\n\r\nQuality: We are dedicated to ensuring high quality in education and research.\r\nInnovation: We encourage creativity and innovative thinking in all aspects.\r\nDiversity: We respect and promote diversity within our academic and social community.\r\nSocial Interaction: We foster collaboration and social interaction between students and faculty.\r\n\r\nWe hope that [TQVC University] becomes your choice for personal development and preparation for a successful future. Let's work together to build new dreams and achievements!", "univercity@gmail.com", "0905028073", "https://www.youtube.com/watch?v=LlCwHnp3kL4" });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "Description", "Name", "Slug" },
                values: new object[,]
                {
                    { 1, "2022-2026", "K22", null },
                    { 2, "2023-2027", "K23", null }
                });

            migrationBuilder.InsertData(
                table: "Faculties",
                columns: new[] { "Id", "Code", "Course_id", "Description", "EntryScore", "Opportunities", "Skill_learn", "Slug", "Title" },
                values: new object[,]
                {
                    { 1, "C01", 1, "123", 100, "DEF", "ABC", "IT", "IT" },
                    { 2, "C02", 1, "123", 100, "123", "XYZ", "Philosophy", "Philosophy" },
                    { 3, "C03", 2, "123", 100, "456", "Math", "Advanced-Math", "Advanced Math" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArticleCategories_ArticleId",
                table: "ArticleCategories",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleCategories_CategoryId",
                table: "ArticleCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_Title",
                table: "Articles",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name",
                table: "Categories",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Faculties_Course_id",
                table: "Faculties",
                column: "Course_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminAccounts");

            migrationBuilder.DropTable(
                name: "ArticleCategories");

            migrationBuilder.DropTable(
                name: "ContactUs");

            migrationBuilder.DropTable(
                name: "Faculties");

            migrationBuilder.DropTable(
                name: "Feedbacks");

            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Courses");
        }
    }
}
