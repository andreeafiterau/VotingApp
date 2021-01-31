using Microsoft.EntityFrameworkCore.Migrations;

namespace VotingApp.Migrations
{
    public partial class Migration7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ElectoralRoomId",
                table: "Candidates",
                newName: "IdElectoralRoom");

            migrationBuilder.AddColumn<int>(
                name: "IdCollege",
                table: "Departments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Activation_Codes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdCollege",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Activation_Codes");

            migrationBuilder.RenameColumn(
                name: "IdElectoralRoom",
                table: "Candidates",
                newName: "ElectoralRoomId");
        }
    }
}
