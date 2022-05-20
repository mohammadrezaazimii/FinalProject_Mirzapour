using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sajjadmirzapour.Migrations
{
    public partial class newsss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Banner",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Picture1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Picture2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Picture3 = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banner", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Banner");
        }
    }
}
