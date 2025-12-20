using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlayerSponsor.Server.Migrations
{
    /// <inheritdoc />
    public partial class RemoveClubAttributes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_Teams_TeamId",
                table: "Players");

            migrationBuilder.DropForeignKey(
                name: "FK_Sponsors_Clubs_ClubId",
                table: "Sponsors");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Clubs_ClubId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_ClubId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Sponsors_ClubId",
                table: "Sponsors");

            migrationBuilder.DropColumn(
                name: "ClubId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "ClubId",
                table: "Sponsors");

            migrationBuilder.DropColumn(
                name: "Bio",
                table: "Clubs");

            migrationBuilder.RenameColumn(
                name: "PlayerKey",
                table: "Clubs",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "Logo",
                table: "Clubs",
                newName: "Description");

            migrationBuilder.AddColumn<int>(
                name: "ClubId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_ClubId",
                table: "Products",
                column: "ClubId");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Teams_TeamId",
                table: "Players",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Clubs_ClubId",
                table: "Products",
                column: "ClubId",
                principalTable: "Clubs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_Teams_TeamId",
                table: "Players");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Clubs_ClubId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ClubId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ClubId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Clubs",
                newName: "PlayerKey");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Clubs",
                newName: "Logo");

            migrationBuilder.AddColumn<int>(
                name: "ClubId",
                table: "Teams",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClubId",
                table: "Sponsors",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Bio",
                table: "Clubs",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_ClubId",
                table: "Teams",
                column: "ClubId");

            migrationBuilder.CreateIndex(
                name: "IX_Sponsors_ClubId",
                table: "Sponsors",
                column: "ClubId");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Teams_TeamId",
                table: "Players",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sponsors_Clubs_ClubId",
                table: "Sponsors",
                column: "ClubId",
                principalTable: "Clubs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Clubs_ClubId",
                table: "Teams",
                column: "ClubId",
                principalTable: "Clubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
