using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SportsBetsServer.Migrations
{
    public partial class UpdatedCredentials : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.CreateTable(
            //     name: "credential",
            //     columns: table => new
            //     {
            //         user = table.Column<Guid>(nullable: false),
            //         password_hash = table.Column<byte[]>(nullable: false),
            //         password_salt = table.Column<byte[]>(nullable: false)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_credential", x => x.user);
            //         table.ForeignKey(
            //             name: "FK_credential_user_Id",
            //             column: x => x.user,
            //             principalTable: "user",
            //             principalColumn: "user",
            //             onDelete: ReferentialAction.Cascade);
            //     });
            // migrationBuilder.AddColumn<Guid>(
            //     name: "UserId",
            //     table: "credential",
            //     nullable: true);

            // migrationBuilder.CreateIndex(
            //     name: "IX_credential_UserId",
            //     table: "credential",
            //     column: "UserId");

            // migrationBuilder.AddForeignKey(
            //     name: "FK_credential_user_UserId",
            //     table: "credential",
            //     column: "UserId",
            //     principalTable: "user",
            //     principalColumn: "user",
            //     onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_credential_user_UserId",
                table: "credential");

            migrationBuilder.DropIndex(
                name: "IX_credential_UserId",
                table: "credential");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "credential");

            migrationBuilder.RenameColumn(
                name: "user",
                table: "credential",
                newName: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_credential_user_Id",
                table: "credential",
                column: "Id",
                principalTable: "user",
                principalColumn: "user",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
