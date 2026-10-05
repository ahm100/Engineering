using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddReferencesViewTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
    DECLARE @WarehouseDatabase NVARCHAR(128);
    DECLARE @EngineeringDatabase NVARCHAR(128);
    DECLARE @Sql NVARCHAR(MAX);

    -- Get database names
    SET @WarehouseDatabase = REPLACE(DB_NAME(), 'Engineering', 'Warehouse');
    SET @EngineeringDatabase = DB_NAME();

    SET @Sql = '
        CREATE VIEW engineer.vwReferenceItemReport
        AS

        -- Products
        SELECT
            p.Id,
            CAST(1 AS INT) AS SupplyType,
            p.Name,
            p.NameEn,
            p.Code,
            p.TechnicalCode,
            p.Created,
            g.CompanyId
        FROM [' + @WarehouseDatabase + '].dbo.Products p
        INNER JOIN [' + @WarehouseDatabase + '].dbo.Groups g
            ON p.GroupId = g.Id
        WHERE p.IsDeleted = 0

        UNION ALL

        -- Services
        SELECT
            s.Id,
            CAST(2 AS INT) AS SupplyType,
            s.ServiceInfoName AS Name,
            s.ServiceInfoEnName AS NameEn,
            s.ServiceInfoCode AS Code,
            s.ServiceInfoCode AS TechnicalCode,
            s.Created,
            s.CompanyId
        FROM [' + @EngineeringDatabase + '].engineer.EngineeringServices s
        WHERE s.IsDeleted = 0

        UNION ALL

        -- Advertisements
        SELECT
            a.Id,
            CAST(3 AS INT) AS SupplyType,
            a.TitleFa AS Name,
            a.TitleEn AS NameEn,
            a.TechnicalCode AS Code,
            a.TechnicalCode AS TechnicalCode,
            a.Created,
            a.CompanyId
        FROM [' + @EngineeringDatabase + '].engineer.Advertisements a
        WHERE a.IsDeleted = 0;
    ';

    EXEC sp_executesql @Sql;
");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
