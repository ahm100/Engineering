using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddMessengerTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "Messengers",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    MessengerType = table.Column<int>(type: "int", nullable: false),
                    MessengerTargetType = table.Column<int>(type: "int", nullable: false),
                    TargetId = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CompanyId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messengers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MessengerChannels",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    ChatId = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    ChatUrl = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ChatName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    MessengerMessageType = table.Column<int>(type: "int", nullable: false),
                    MessengerId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessengerChannels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MessengerChannels_Messengers_MessengerId",
                        column: x => x.MessengerId,
                        principalSchema: "engineer",
                        principalTable: "Messengers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MessengerChannelHistories",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    Message = table.Column<string>(type: "nvarchar(2500)", maxLength: 2500, nullable: false),
                    ErrorMessage = table.Column<string>(type: "nvarchar(2500)", maxLength: 2500, nullable: false),
                    FileUrls = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsSend = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    MessengerChannelId = table.Column<long>(type: "bigint", nullable: false),
                    MessengerId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessengerChannelHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MessengerChannelHistories_MessengerChannels_MessengerId",
                        column: x => x.MessengerId,
                        principalSchema: "engineer",
                        principalTable: "MessengerChannels",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MessengerChannelHistories_MessengerId",
                schema: "engineer",
                table: "MessengerChannelHistories",
                column: "MessengerId");

            migrationBuilder.CreateIndex(
                name: "IX_MessengerChannels_MessengerId",
                schema: "engineer",
                table: "MessengerChannels",
                column: "MessengerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MessengerChannelHistories",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "MessengerChannels",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "Messengers",
                schema: "engineer");

        }
    }
}
