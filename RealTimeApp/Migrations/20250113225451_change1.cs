using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealTimeApp.Migrations
{
    /// <inheritdoc />
    public partial class change1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ComunicateType",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "IsReadMessage",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "Message",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "MessageDate",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "NameAnnouncer",
                table: "Chats");

            migrationBuilder.RenameColumn(
                name: "UserAnnouncer",
                table: "Chats",
                newName: "ChatDate");

            migrationBuilder.RenameColumn(
                name: "NameReceiver",
                table: "Chats",
                newName: "Announcer");

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    MessageDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    MessageWrited = table.Column<string>(type: "TEXT", nullable: false),
                    IsReadMessage = table.Column<bool>(type: "INTEGER", nullable: false),
                    ComunicateType = table.Column<int>(type: "INTEGER", nullable: false),
                    ChatId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_Chats_ChatId",
                        column: x => x.ChatId,
                        principalTable: "Chats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ChatId",
                table: "Messages",
                column: "ChatId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.RenameColumn(
                name: "ChatDate",
                table: "Chats",
                newName: "UserAnnouncer");

            migrationBuilder.RenameColumn(
                name: "Announcer",
                table: "Chats",
                newName: "NameReceiver");

            migrationBuilder.AddColumn<int>(
                name: "ComunicateType",
                table: "Chats",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsReadMessage",
                table: "Chats",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "Chats",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "MessageDate",
                table: "Chats",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "NameAnnouncer",
                table: "Chats",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
