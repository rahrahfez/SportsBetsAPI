using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SportsBetsServer.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    user = table.Column<Guid>(nullable: false),
                    username = table.Column<string>(maxLength: 30, nullable: false),
                    available_balance = table.Column<int>(nullable: false),
                    date_created = table.Column<DateTime>(nullable: false),
                    user_role = table.Column<string>(nullable: false),
                    password_hash = table.Column<byte[]>(nullable: false),
                    password_salt = table.Column<byte[]>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.user);
                });

            migrationBuilder.CreateTable(
                name: "wager",
                columns: table => new
                {
                    wager = table.Column<Guid>(nullable: false),
                    user = table.Column<Guid>(nullable: false),
                    date_created = table.Column<DateTime>(nullable: false),
                    status = table.Column<int>(nullable: false),
                    result = table.Column<int>(nullable: true),
                    amount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wager", x => x.wager);
                    table.ForeignKey(
                        name: "FK_wager_user_user",
                        column: x => x.user,
                        principalTable: "user",
                        principalColumn: "user",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_wager_user",
                table: "wager",
                column: "user");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "wager");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
