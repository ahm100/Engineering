using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddFinancialPeriodSynonym : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DECLARE @CurrentDb NVARCHAR(128);
                DECLARE @FinancialDb NVARCHAR(128);
                DECLARE @Sql NVARCHAR(MAX);

                SET @CurrentDb = DB_NAME();
                SET @FinancialDb = REPLACE(@CurrentDb, 'Engineering', 'Financial');

                IF NOT EXISTS (
                    SELECT 1
                    FROM sys.synonyms
                    WHERE name = 'ViewFinancialPeriod'
                    AND schema_id = SCHEMA_ID('engineer')
                )
                BEGIN
                SET @Sql = N'
                CREATE SYNONYM [engineer].[ViewFinancialPeriod]
                FOR ' + QUOTENAME(@FinancialDb) + N'.[finantial].[FinancialPeriods];
                ';

                EXEC sp_executesql @Sql;
                END;
            ");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                IF EXISTS (
                    SELECT 1
                    FROM sys.synonyms
                    WHERE name = 'ViewFinancialPeriod'
                      AND schema_id = SCHEMA_ID('engineer')
                )
                BEGIN
                    DROP SYNONYM [engineer].[ViewFinancialPeriod];
                END;
            ");
        }
    }
}
