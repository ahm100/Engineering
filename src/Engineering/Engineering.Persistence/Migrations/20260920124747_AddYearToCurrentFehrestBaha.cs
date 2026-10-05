using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddYearToCurrentFehrestBaha : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                UPDATE oi
                SET oi."YearId" = (
                    SELECT TOP 1 vfp."Id"
                    FROM [engineer].[ViewFinancialPeriod] AS vfp
                    WHERE vfp."IsCurrent" = 1
                      AND vfp."IsDeleted" = 0
                      AND vfp."IsActive" = 1
                      AND vfp."CompanyId" = oi."CompanyId"
                    ORDER BY vfp."StartDate" DESC, vfp."Id" DESC
                        )
                FROM [engineer].[OperationInfos]  AS oi
                WHERE oi."IsPriceList" = 1
                  AND oi."YearId" IS NULL;
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
            UPDATE oi
            SET oi."YearId" = NULL
            FROM [engineer].[OperationInfos] AS oi
            WHERE oi."IsPriceList" = 1
              AND oi."YearId" = (
                  SELECT TOP 1 vfp."Id"
                  FROM [engineer].[ViewFinancialPeriod] AS vfp
                  WHERE vfp."IsCurrent" = 1
                    AND vfp."IsDeleted" = 0
                    AND vfp."IsActive" = 1
                    AND vfp."CompanyId" = oi."CompanyId"
                  ORDER BY vfp."StartDate" DESC, vfp."Id" DESC
              );
            """);
        }


    }
}
