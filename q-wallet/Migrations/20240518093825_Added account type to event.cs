using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace q_wallet.Migrations
{
    public partial class Addedaccounttypetoevent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccountTypeId",
                table: "BankAccountEvents",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountTypeId",
                table: "BankAccountEvents");
        }
    }
}
