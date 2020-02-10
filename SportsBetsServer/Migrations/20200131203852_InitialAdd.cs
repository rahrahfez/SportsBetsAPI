using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SportsBetsServer.Migrations
{
    public partial class InitialAdd : Migration
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
                    date_created = table.Column<DateTime>(nullable: false)
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
                    date_created = table.Column<DateTime>(nullable: false),
                    status = table.Column<string>(nullable: false),
                    win_condition = table.Column<string>(nullable: false),
                    result = table.Column<string>(nullable: true),
                    amount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wager", x => x.wager);
                });

            migrationBuilder.CreateTable(
                name: "credential",
                columns: table => new
                {
                    user = table.Column<Guid>(nullable: false),
                    password_hash = table.Column<byte[]>(nullable: false),
                    password_salt = table.Column<byte[]>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_credential", x => x.user);
                    table.ForeignKey(
                        name: "FK_credential_user_Id",
                        column: x => x.user,
                        principalTable: "user",
                        principalColumn: "user",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "bet",
                columns: table => new
                {
                    bet = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    wager = table.Column<Guid>(nullable: false),
                    user = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bet", x => x.bet);
                    table.ForeignKey(
                        name: "FK_bet_user_UserId",
                        column: x => x.user,
                        principalTable: "user",
                        principalColumn: "user",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_bet_wager_WagerId",
                        column: x => x.wager,
                        principalTable: "wager",
                        principalColumn: "wager",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_bet_UserId",
                table: "bet",
                column: "user");

            migrationBuilder.CreateIndex(
                name: "IX_bet_WagerId",
                table: "bet",
                column: "wager");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "bet");

            migrationBuilder.DropTable(
                name: "credential");

            migrationBuilder.DropTable(
                name: "wager");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
