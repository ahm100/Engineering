using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CompanyIdBackfill : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                """
                UPDATE contract
                SET contract.CompanyId = project.CompanyId
                FROM [engineer].[Contracts] AS contract
                INNER JOIN [engineer].[Projects] AS project
                    ON project.Id = contract.ProjectId
                WHERE contract.CompanyId <= 0
                  AND project.CompanyId IS NOT NULL
                  AND project.CompanyId > 0;

                IF EXISTS
                (
                    SELECT 1
                    FROM [engineer].[Contracts] AS contract
                    WHERE contract.CompanyId <= 0
                )
                BEGIN
                    THROW 51000, 'CompanyId backfill failed for one or more Contracts.', 1;
                END;
                """);

            migrationBuilder.Sql(
                """
                UPDATE contractorContract
                SET contractorContract.CompanyId = project.CompanyId
                FROM [engineer].[ContractorContracts] AS contractorContract
                INNER JOIN [engineer].[Projects] AS project
                    ON project.Id = contractorContract.ProjectId
                WHERE contractorContract.CompanyId <= 0
                  AND project.CompanyId IS NOT NULL
                  AND project.CompanyId > 0;

                IF EXISTS
                (
                    SELECT 1
                    FROM [engineer].[ContractorContracts] AS contractorContract
                    WHERE contractorContract.CompanyId <= 0
                )
                BEGIN
                    THROW 51001, 'CompanyId backfill failed for one or more ContractorContracts.', 1;
                END;
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Data backfill is intentionally irreversible.
        }
    }
}