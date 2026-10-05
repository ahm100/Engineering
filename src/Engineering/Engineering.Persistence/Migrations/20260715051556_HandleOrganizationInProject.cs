using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class HandleOrganizationInProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
            DECLARE @CurrentDb NVARCHAR(128) = DB_NAME();
            DECLARE @MetaDb NVARCHAR(128);

            -- Convert the current engineering database name to the corresponding metadata database
            SET @MetaDb = REPLACE(@CurrentDb, 'EngineeringDb', 'MetaDataDb');

            IF NOT EXISTS (
                SELECT 1
                FROM sys.synonyms
                WHERE name = 'ViewOrganization'
                  AND SCHEMA_NAME(schema_id) = 'engineer'
            )
            BEGIN
                DECLARE @Sql NVARCHAR(MAX) =
                    N'CREATE SYNONYM [engineer].[ViewOrganization] FOR [' +
                    @MetaDb +
                    N'].[meta].[Organizations]';

                EXEC(@Sql);
            END
            """);

            migrationBuilder.AlterColumn<long>(
                name: "ProjectTypesId",
                schema: "engineer",
                table: "Projects",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "ProjectTypeId",
                schema: "engineer",
                table: "Projects",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<bool>(
                name: "IsOrganizationUnit",
                schema: "engineer",
                table: "Projects",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "واحد سازمانی هست یا نه");

            migrationBuilder.AddColumn<long>(
                name: "OrganizationId",
                schema: "engineer",
                table: "Projects",
                type: "bigint",
                nullable: true,
                comment: "شناسه سازمان");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_OrganizationId",
                schema: "engineer",
                table: "Projects",
                column: "OrganizationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
            IF EXISTS (
                SELECT 1
                FROM sys.synonyms
                WHERE name = 'ViewOrganization'
                  AND SCHEMA_NAME(schema_id) = 'engineer'
            )
            DROP SYNONYM [engineer].[ViewOrganization];
            """);

            migrationBuilder.DropIndex(
                name: "IX_Projects_OrganizationId",
                schema: "engineer",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "IsOrganizationUnit",
                schema: "engineer",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                schema: "engineer",
                table: "Projects");

            migrationBuilder.AlterColumn<long>(
                name: "ProjectTypesId",
                schema: "engineer",
                table: "Projects",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ProjectTypeId",
                schema: "engineer",
                table: "Projects",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);
        }
    }
}
