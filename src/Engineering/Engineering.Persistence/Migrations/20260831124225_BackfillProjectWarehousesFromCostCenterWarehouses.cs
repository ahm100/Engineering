using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class BackfillProjectWarehousesFromCostCenterWarehouses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                """
        SET NOCOUNT ON;
        SET XACT_ABORT ON;

        IF OBJECT_ID('tempdb..#ProjectWarehouseBackfill') IS NOT NULL
            DROP TABLE #ProjectWarehouseBackfill;

        CREATE TABLE #ProjectWarehouseBackfill
        (
            ProjectId   BIGINT        NOT NULL,
            WarehouseId BIGINT        NOT NULL,
            IsDefault   BIT           NOT NULL,
            Created     DATETIME2     NOT NULL,
            CreatorId   BIGINT        NOT NULL,
            Updated     DATETIME2     NULL,
            UpdaterId   BIGINT        NULL,

            CONSTRAINT PK_ProjectWarehouseBackfill
                PRIMARY KEY (ProjectId, WarehouseId)
        );

        /* ============================================================
           Build Project -> Warehouse snapshot from the current
           ProjectCostCenter -> CostCenterWarehouse graph.

           When one Project reaches the same Warehouse through more
           than one CostCenter, keep only one ProjectWarehouse row.

           A Warehouse is Project-default only when BOTH:
           - the ProjectCostCenter is default
           - the CostCenterWarehouse is default
           ============================================================ */

        ;WITH SourceRows AS
        (
            SELECT
                pcc.ProjectId,
                ccw.WarehouseId,

                CAST(
                    CASE
                        WHEN pcc.IsDefault = 1
                         AND ccw.IsDefault = 1
                            THEN 1
                        ELSE 0
                    END
                    AS BIT
                ) AS IsDefault,

                ccw.Created,
                ccw.CreatorId,
                ccw.Updated,
                ccw.UpdaterId,

                ROW_NUMBER() OVER
                (
                    PARTITION BY
                        pcc.ProjectId,
                        ccw.WarehouseId

                    ORDER BY
                        /* Prefer the row that represents Project default */
                        CASE
                            WHEN pcc.IsDefault = 1
                             AND ccw.IsDefault = 1
                                THEN 0
                            ELSE 1
                        END,

                        /* Then prefer the Warehouse default relation */
                        CASE
                            WHEN ccw.IsDefault = 1
                                THEN 0
                            ELSE 1
                        END,

                        ccw.Created,
                        ccw.Id
                ) AS RowNumber
            FROM [engineer].[ProjectCostCenters] AS pcc

            INNER JOIN [engineer].[CostCenterWarehouses] AS ccw
                ON ccw.CostCenterId = pcc.CostCenterId
               AND ccw.IsDeleted = 0

            INNER JOIN [engineer].[Projects] AS p
                ON p.Id = pcc.ProjectId
               AND p.IsDeleted = 0

            INNER JOIN [engineer].[CostCenters] AS cc
                ON cc.Id = pcc.CostCenterId
               AND cc.IsDeleted = 0

            WHERE pcc.IsDeleted = 0
        )
        INSERT INTO #ProjectWarehouseBackfill
        (
            ProjectId,
            WarehouseId,
            IsDefault,
            Created,
            CreatorId,
            Updated,
            UpdaterId
        )
        SELECT
            ProjectId,
            WarehouseId,
            IsDefault,
            Created,
            CreatorId,
            Updated,
            UpdaterId
        FROM SourceRows
        WHERE RowNumber = 1;


        /* ============================================================
           Safety check #1
           Every Project that will be migrated must resolve to exactly
           one default ProjectWarehouse.

           We do NOT silently choose a fallback default because that
           would invent business data instead of mirroring legacy data.
           ============================================================ */

        IF EXISTS
        (
            SELECT 1
            FROM #ProjectWarehouseBackfill
            GROUP BY ProjectId
            HAVING
                SUM(
                    CASE
                        WHEN IsDefault = 1 THEN 1
                        ELSE 0
                    END
                ) <> 1
        )
        BEGIN
            THROW 50001,
                  'ProjectWarehouse backfill aborted: one or more Projects do not resolve to exactly one default Warehouse.',
                  1;
        END;


        /* ============================================================
           Safety check #2
           Do not mix the migration snapshot with already-active
           ProjectWarehouse data.

           This is intentional. If the new table already contains live
           data for a Project being migrated, that state must be reviewed
           instead of silently merged.
           ============================================================ */

        IF EXISTS
        (
            SELECT 1
            FROM [engineer].[ProjectWarehouses] AS pw
            INNER JOIN
            (
                SELECT DISTINCT ProjectId
                FROM #ProjectWarehouseBackfill
            ) AS sourceProjects
                ON sourceProjects.ProjectId = pw.ProjectId
            WHERE pw.IsDeleted = 0
        )
        BEGIN
            THROW 50002,
                  'ProjectWarehouse backfill aborted: active ProjectWarehouse data already exists for one or more source Projects.',
                  1;
        END;


        /* ============================================================
           Insert snapshot

           Id is identity.
           RowVersion is generated by SQL Server.
           Only current/active legacy relations are copied.
           ============================================================ */

        INSERT INTO [engineer].[ProjectWarehouses]
        (
            ProjectId,
            WarehouseId,
            IsDefault,
            Created,
            CreatorId,
            Updated,
            UpdaterId,
            IsDeleted
        )
        SELECT
            ProjectId,
            WarehouseId,
            IsDefault,
            Created,
            CreatorId,
            Updated,
            UpdaterId,
            0
        FROM #ProjectWarehouseBackfill;


        DROP TABLE #ProjectWarehouseBackfill;
        """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                """
        THROW 50003,
              'BackfillProjectWarehousesFromCostCenterWarehouses is an irreversible data migration and cannot be safely rolled back automatically.',
              1;
        """);
        }
    }
}
