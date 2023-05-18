using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MaliLb.Migrations
{
    /// <inheritdoc />
    public partial class ThirdMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Book_Reader_ReaderID",
                table: "Book");

            migrationBuilder.DropTable(
                name: "Reader");

            migrationBuilder.DropIndex(
                name: "IX_Book_ReaderID",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "ReaderID",
                table: "Book");

            migrationBuilder.AddColumn<int>(
                name: "VisitorID",
                table: "Book",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Visitor",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    СardNumber = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visitor", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Book_VisitorID",
                table: "Book",
                column: "VisitorID");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Visitor_VisitorID",
                table: "Book",
                column: "VisitorID",
                principalTable: "Visitor",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Book_Visitor_VisitorID",
                table: "Book");

            migrationBuilder.DropTable(
                name: "Visitor");

            migrationBuilder.DropIndex(
                name: "IX_Book_VisitorID",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "VisitorID",
                table: "Book");

            migrationBuilder.AddColumn<int>(
                name: "ReaderID",
                table: "Book",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Reader",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    СardNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reader", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Book_ReaderID",
                table: "Book",
                column: "ReaderID");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Reader_ReaderID",
                table: "Book",
                column: "ReaderID",
                principalTable: "Reader",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
