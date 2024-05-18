using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace q_wallet.Migrations
{
    public partial class Updatedbankaccountevent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AccountNumber",
                table: "BankAccountEvents",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountNumber",
                table: "BankAccountEvents");
        }
    }
}
