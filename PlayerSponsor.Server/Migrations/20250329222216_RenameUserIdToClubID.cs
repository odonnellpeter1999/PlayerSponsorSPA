using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlayerSponsor.Server.Migrations
{
    /// <inheritdoc />
    public partial class RenameUserIdToClubID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admins_AspNetUsers_UserId",
                table: "Admins");

            migrationBuilder.DropIndex(
                name: "IX_Admins_UserId",
                table: "Admins");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Admins");

            migrationBuilder.AddColumn<string>(
                name: "ClubId",
                table: "Admins",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Admins_ClubId",
                table: "Admins",
                column: "ClubId");

            migrationBuilder.AddForeignKey(
                name: "FK_Admins_AspNetUsers_ClubId",
                table: "Admins",
                column: "ClubId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admins_AspNetUsers_ClubId",
                table: "Admins");

            migrationBuilder.DropIndex(
                name: "IX_Admins_ClubId",
                table: "Admins");

            migrationBuilder.DropColumn(
                name: "ClubId",
                table: "Admins");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Admins",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Admins_UserId",
                table: "Admins",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Admins_AspNetUsers_UserId",
                table: "Admins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
