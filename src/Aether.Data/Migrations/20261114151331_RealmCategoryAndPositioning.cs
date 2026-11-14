using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aether.Data.Migrations
{
    /// <inheritdoc />
    public partial class RealmCategoryAndPositioning : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TextChannels_Realms_RealmId",
                table: "TextChannels");

            migrationBuilder.DropForeignKey(
                name: "FK_VoiceChannels_Realms_RealmId",
                table: "VoiceChannels");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "VoiceChannels");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "TextChannels");

            migrationBuilder.RenameColumn(
                name: "RealmId",
                table: "VoiceChannels",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_VoiceChannels_RealmId",
                table: "VoiceChannels",
                newName: "IX_VoiceChannels_CategoryId");

            migrationBuilder.RenameColumn(
                name: "RealmId",
                table: "TextChannels",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_TextChannels_RealmId",
                table: "TextChannels",
                newName: "IX_TextChannels_CategoryId");

            migrationBuilder.AddColumn<int>(
                name: "Position",
                table: "VoiceChannels",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Position",
                table: "TextChannels",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RealmId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Category_Realms_RealmId",
                        column: x => x.RealmId,
                        principalTable: "Realms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Category_RealmId",
                table: "Category",
                column: "RealmId");

            migrationBuilder.AddForeignKey(
                name: "FK_TextChannels_Category_CategoryId",
                table: "TextChannels",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VoiceChannels_Category_CategoryId",
                table: "VoiceChannels",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TextChannels_Category_CategoryId",
                table: "TextChannels");

            migrationBuilder.DropForeignKey(
                name: "FK_VoiceChannels_Category_CategoryId",
                table: "VoiceChannels");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "VoiceChannels");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "TextChannels");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "VoiceChannels",
                newName: "RealmId");

            migrationBuilder.RenameIndex(
                name: "IX_VoiceChannels_CategoryId",
                table: "VoiceChannels",
                newName: "IX_VoiceChannels_RealmId");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "TextChannels",
                newName: "RealmId");

            migrationBuilder.RenameIndex(
                name: "IX_TextChannels_CategoryId",
                table: "TextChannels",
                newName: "IX_TextChannels_RealmId");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "VoiceChannels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "TextChannels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TextChannels_Realms_RealmId",
                table: "TextChannels",
                column: "RealmId",
                principalTable: "Realms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VoiceChannels_Realms_RealmId",
                table: "VoiceChannels",
                column: "RealmId",
                principalTable: "Realms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
