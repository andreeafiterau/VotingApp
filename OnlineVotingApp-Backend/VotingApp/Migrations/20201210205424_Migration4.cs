using Microsoft.EntityFrameworkCore.Migrations;

namespace VotingApp.Migrations
{
    public partial class Migration4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Users",
                newName: "IdUser");

            migrationBuilder.RenameColumn(
                name: "ElectoralRoomId",
                table: "Electoral_Rooms",
                newName: "IdElectoralRoom");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Candidates",
                newName: "IdUser");

            migrationBuilder.RenameColumn(
                name: "CandidateId",
                table: "Candidates",
                newName: "IdCandidate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdUser",
                table: "Users",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "IdElectoralRoom",
                table: "Electoral_Rooms",
                newName: "ElectoralRoomId");

            migrationBuilder.RenameColumn(
                name: "IdUser",
                table: "Candidates",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "IdCandidate",
                table: "Candidates",
                newName: "CandidateId");
        }
    }
}
