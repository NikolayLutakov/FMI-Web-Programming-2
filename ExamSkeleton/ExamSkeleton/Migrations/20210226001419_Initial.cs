using Microsoft.EntityFrameworkCore.Migrations;

namespace ExamSkeleton.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SomeEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ColumnOne = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ColumnTwo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ColumnThree = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ColumnFour = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SomeEntities", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SomeEntities");
        }
    }
}
