using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SP2023_Assignment3_lrichardson.Data.Migrations
{
    public partial class updates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Actors_ActorID",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Movies_ActorID",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "ActorID",
                table: "Movies");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ActorID",
                table: "Movies",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Movies_ActorID",
                table: "Movies",
                column: "ActorID");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Actors_ActorID",
                table: "Movies",
                column: "ActorID",
                principalTable: "Actors",
                principalColumn: "Id");
        }
    }
}
