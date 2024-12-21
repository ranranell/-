using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebImage.Migrations
{
    /// <inheritdoc />
    public partial class first : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    Id_image = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Entity_Id = table.Column<int>(type: "int", nullable: false),
                    Entity_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    File_url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Create_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.Id_image);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Image");
        }
    }
}
