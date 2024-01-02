using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingApp.Migrations
{
    /// <inheritdoc />
    public partial class RemovedFacilityprops : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SpeakerMicrophone",
                table: "Facilities");

            migrationBuilder.DropColumn(
                name: "VCSoundSystem",
                table: "Facilities");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "SpeakerMicrophone",
                table: "Facilities",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "VCSoundSystem",
                table: "Facilities",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
