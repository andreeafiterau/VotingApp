using Microsoft.EntityFrameworkCore.Migrations;

namespace VotingApp.Migrations
{
    public partial class Migration10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ElectoralRoomName",
                table: "Electoral_Rooms");

            migrationBuilder.RenameColumn(
                name: "IdElectoralRoom",
                table: "Elections_Users",
                newName: "IdElectionType");

            migrationBuilder.AddColumn<int>(
                name: "IdElectionType",
                table: "Electoral_Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Election_Types",
                columns: table => new
                {
                    IdElectionType = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ElectionName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Election_Types", x => x.IdElectionType);
                });

            migrationBuilder.CreateTable(
                name: "ElectionRules",
                columns: table => new
                {
                    IdElectionType = table.Column<int>(type: "int", nullable: false),
                    IdRole = table.Column<int>(type: "int", nullable: false),
                    IdCollege = table.Column<int>(type: "int", nullable: false),
                    IdDepartment = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Election_Types");

            migrationBuilder.DropTable(
                name: "ElectionRules");

            migrationBuilder.DropColumn(
                name: "IdElectionType",
                table: "Electoral_Rooms");

            migrationBuilder.RenameColumn(
                name: "IdElectionType",
                table: "Elections_Users",
                newName: "IdElectoralRoom");

            migrationBuilder.AddColumn<string>(
                name: "ElectoralRoomName",
                table: "Electoral_Rooms",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: false,
                defaultValue: "");
        }
    }
}
