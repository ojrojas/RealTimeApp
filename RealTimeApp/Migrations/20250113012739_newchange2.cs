using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealTimeApp.Migrations
{
    /// <inheritdoc />
    public partial class newchange2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NameAnnouncer",
                table: "Chats",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NameReceiver",
                table: "Chats",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameAnnouncer",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "NameReceiver",
                table: "Chats");
        }
    }
}
