using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AzuriteSample.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Workflow",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    AssignedTo = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Reviewed = table.Column<bool>(type: "bit", nullable: false),
                    FileSize = table.Column<long>(type: "bigint", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workflow", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Workflow");
        }
    }
}
