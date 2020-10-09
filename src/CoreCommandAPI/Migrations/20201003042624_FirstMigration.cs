using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreCommandAPI.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Commands",
                columns: table => new
                {
                    CommandId = table.Column<Guid>(nullable: false),
                    SnippetDescription = table.Column<string>(maxLength: 250, nullable: false),
                    Platform = table.Column<string>(nullable: false),
                    Snippet = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commands", x => x.CommandId);
                });

            migrationBuilder.CreateTable(
                name: "CommandImages",
                columns: table => new
                {
                    CommandImageId = table.Column<Guid>(nullable: false),
                    Url = table.Column<string>(nullable: true),
                    OrderShown = table.Column<int>(nullable: false),
                    CommandId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommandImages", x => x.CommandImageId);
                    table.ForeignKey(
                        name: "FK_CommandImages_Commands_CommandId",
                        column: x => x.CommandId,
                        principalTable: "Commands",
                        principalColumn: "CommandId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommandImages_CommandId",
                table: "CommandImages",
                column: "CommandId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommandImages");

            migrationBuilder.DropTable(
                name: "Commands");
        }
    }
}
