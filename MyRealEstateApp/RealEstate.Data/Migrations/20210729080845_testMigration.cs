using Microsoft.EntityFrameworkCore.Migrations;

namespace RealEstate.Data.Migrations
{
    public partial class testMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Brokers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Brokers_UserId1",
                table: "Brokers",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Brokers_AspNetUsers_UserId1",
                table: "Brokers",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Brokers_AspNetUsers_UserId1",
                table: "Brokers");

            migrationBuilder.DropIndex(
                name: "IX_Brokers_UserId1",
                table: "Brokers");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Brokers");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
