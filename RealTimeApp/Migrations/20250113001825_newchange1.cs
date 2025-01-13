using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealTimeApp.Migrations
{
    /// <inheritdoc />
    public partial class newchange1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Chats",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserAnnouncer = table.Column<Guid>(type: "TEXT", nullable: false),
                    Receiver = table.Column<Guid>(type: "TEXT", nullable: false),
                    MessageDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    Message = table.Column<string>(type: "TEXT", nullable: false),
                    IsReadMessage = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chats", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Chats");
        }
    }
}
