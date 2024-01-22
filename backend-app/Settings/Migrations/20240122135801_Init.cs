using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend_app.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Avatar",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AdminAccounts",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$KtxAo2uUioWxsR777crPiuoHtl57VN6hO9qhtJ3Xn19/4lnCsve8O");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "Students");

            migrationBuilder.UpdateData(
                table: "AdminAccounts",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$7Xm9iusN14zgbeSj2Spzuu0/vcBnat1eHnxg/9bzLcx62f9kJH1nK");
        }
    }
}
