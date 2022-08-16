using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelApp.Migrations
{
    public partial class PicturesField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Picture",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    contentUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    width = table.Column<int>(type: "int", nullable: false),
                    height = table.Column<int>(type: "int", nullable: false),
                    CountryId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Picture", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Picture_TravelList_CountryId",
                        column: x => x.CountryId,
                        principalTable: "TravelList",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Picture_CountryId",
                table: "Picture",
                column: "CountryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Picture");
        }
    }
}
