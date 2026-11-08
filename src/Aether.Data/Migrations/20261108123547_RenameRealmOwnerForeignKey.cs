using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aether.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenameRealmOwnerForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Realms_AspNetUsers_ApplicationUserId",
                table: "Realms");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "Realms",
                newName: "OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Realms_ApplicationUserId",
                table: "Realms",
                newName: "IX_Realms_OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Realms_AspNetUsers_OwnerId",
                table: "Realms",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Realms_AspNetUsers_OwnerId",
                table: "Realms");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "Realms",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Realms_OwnerId",
                table: "Realms",
                newName: "IX_Realms_ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Realms_AspNetUsers_ApplicationUserId",
                table: "Realms",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
