using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SportsBetsServer.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "accounts",
                columns: table => new
                {
                    account = table.Column<Guid>(nullable: false),
                    username = table.Column<string>(maxLength: 30, nullable: false),
                    available_balance = table.Column<int>(nullable: false),
                    date_created = table.Column<DateTime>(nullable: false),
                    role = table.Column<int>(nullable: false),
                    password_hash = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_accounts", x => x.account);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "accounts");
        }
    }
}
