using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Customer.API.Migrations
{
    public partial class update_field_username : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Customers",
                newName: "UserName");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_Username",
                table: "Customers",
                newName: "IX_Customers_UserName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Customers",
                newName: "Username");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_UserName",
                table: "Customers",
                newName: "IX_Customers_Username");
        }
    }
}
