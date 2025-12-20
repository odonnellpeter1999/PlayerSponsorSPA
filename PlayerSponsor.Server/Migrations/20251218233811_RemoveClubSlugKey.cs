using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlayerSponsor.Server.Migrations
{
    /// <inheritdoc />
    public partial class RemoveClubSlugKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "Clubs",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Slug",
                table: "Clubs");
        }
    }
}
