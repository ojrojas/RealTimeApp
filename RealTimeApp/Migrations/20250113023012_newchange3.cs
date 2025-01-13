using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealTimeApp.Migrations
{
    /// <inheritdoc />
    public partial class newchange3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ComunicateType",
                table: "Chats",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ComunicateType",
                table: "Chats");
        }
    }
}
