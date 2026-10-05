using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixMessengerChannelHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"update engineer.MessengerChannelHistories Set MessengerChannelId = MessengerId");

            migrationBuilder.DropForeignKey(
                name: "FK_MessengerChannelHistories_MessengerChannels_MessengerId",
                schema: "engineer",
                table: "MessengerChannelHistories");

            migrationBuilder.DropIndex(
                name: "IX_MessengerChannelHistories_MessengerId",
                schema: "engineer",
                table: "MessengerChannelHistories");

            migrationBuilder.DropColumn(
                name: "MessengerId",
                schema: "engineer",
                table: "MessengerChannelHistories");

            migrationBuilder.CreateIndex(
                name: "IX_MessengerChannelHistories_MessengerChannelId",
                schema: "engineer",
                table: "MessengerChannelHistories",
                column: "MessengerChannelId");

            migrationBuilder.AddForeignKey(
                name: "FK_MessengerChannelHistories_MessengerChannels_MessengerChannelId",
                schema: "engineer",
                table: "MessengerChannelHistories",
                column: "MessengerChannelId",
                principalSchema: "engineer",
                principalTable: "MessengerChannels",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MessengerChannelHistories_MessengerChannels_MessengerChannelId",
                schema: "engineer",
                table: "MessengerChannelHistories");

            migrationBuilder.DropIndex(
                name: "IX_MessengerChannelHistories_MessengerChannelId",
                schema: "engineer",
                table: "MessengerChannelHistories");

            migrationBuilder.AddColumn<long>(
                name: "MessengerId",
                schema: "engineer",
                table: "MessengerChannelHistories",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_MessengerChannelHistories_MessengerId",
                schema: "engineer",
                table: "MessengerChannelHistories",
                column: "MessengerId");

            migrationBuilder.AddForeignKey(
                name: "FK_MessengerChannelHistories_MessengerChannels_MessengerId",
                schema: "engineer",
                table: "MessengerChannelHistories",
                column: "MessengerId",
                principalSchema: "engineer",
                principalTable: "MessengerChannels",
                principalColumn: "Id");
        }
    }
}
