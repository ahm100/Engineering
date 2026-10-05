using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ContractHistoryIsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*
             * Existing ContractChanges cannot safely receive zero-valued legal snapshots.
             * If legacy rows exist, their Previous/Change/Final amounts must be backfilled
             * through an explicitly approved data migration.
             */
            migrationBuilder.Sql(
                """
                IF EXISTS
                (
                    SELECT 1
                    FROM [engineer].[ContractChanges]
                )
                BEGIN
                    THROW 51020,
                        'Existing ContractChanges require legal amount backfill before ContractHistoryIsAdded can be applied.',
                        1;
                END;
                """);

            migrationBuilder.DropIndex(
                name: "IX_ContractAdjustmentIndexes_ContractAdjustmentReferenceId",
                schema: "engineer",
                table: "ContractAdjustmentIndexes");

            /*
             * ContractChange legal financial snapshots.
             *
             * No default value is used intentionally.
             * The guard above guarantees that there are no legacy rows receiving
             * fabricated zero-valued legal amounts.
             */
            migrationBuilder.AddColumn<decimal>(
                name: "FinalContractAmount",
                schema: "engineer",
                table: "ContractChanges",
                type: "decimal(18,2)",
                nullable: false,
                comment: "مبلغ نهایی قرارداد");

            migrationBuilder.AddColumn<decimal>(
                name: "FinancialChangeAmount",
                schema: "engineer",
                table: "ContractChanges",
                type: "decimal(18,2)",
                nullable: false,
                comment: "مبلغ مالی تغییر قرارداد");

            migrationBuilder.AddColumn<decimal>(
                name: "PreviousContractAmount",
                schema: "engineer",
                table: "ContractChanges",
                type: "decimal(18,2)",
                nullable: false,
                comment: "مبلغ قرارداد پیش از تغییر");

            /*
             * Code columns are added nullable first because legacy rows do not have Code.
             * They are deterministically backfilled before being changed to NOT NULL.
             */
            migrationBuilder.AddColumn<string>(
                name: "Code",
                schema: "engineer",
                table: "ContractAdjustmentReferences",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                comment: "کد");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                schema: "engineer",
                table: "ContractAdjustmentIndexes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                comment: "کد");

            migrationBuilder.Sql(
                """
                UPDATE [engineer].[ContractAdjustmentReferences]
                SET [Code] = CONCAT(N'LEGACY-REF-', [Id])
                WHERE [Code] IS NULL
                   OR LTRIM(RTRIM([Code])) = N'';

                UPDATE [engineer].[ContractAdjustmentIndexes]
                SET [Code] = CONCAT(N'LEGACY-IDX-', [Id])
                WHERE [Code] IS NULL
                   OR LTRIM(RTRIM([Code])) = N'';
                """);

            /*
             * Validate the backfill before enforcing NOT NULL / UNIQUE constraints.
             */
            migrationBuilder.Sql(
                """
                IF EXISTS
                (
                    SELECT 1
                    FROM [engineer].[ContractAdjustmentReferences]
                    WHERE [Code] IS NULL
                       OR LTRIM(RTRIM([Code])) = N''
                )
                BEGIN
                    THROW 51010,
                        'ContractAdjustmentReference Code backfill failed.',
                        1;
                END;

                IF EXISTS
                (
                    SELECT [Code]
                    FROM [engineer].[ContractAdjustmentReferences]
                    WHERE [IsDeleted] = 0
                    GROUP BY [Code]
                    HAVING COUNT(*) > 1
                )
                BEGIN
                    THROW 51011,
                        'Duplicate ContractAdjustmentReference Code detected after backfill.',
                        1;
                END;

                IF EXISTS
                (
                    SELECT 1
                    FROM [engineer].[ContractAdjustmentIndexes]
                    WHERE [Code] IS NULL
                       OR LTRIM(RTRIM([Code])) = N''
                )
                BEGIN
                    THROW 51012,
                        'ContractAdjustmentIndex Code backfill failed.',
                        1;
                END;

                IF EXISTS
                (
                    SELECT
                        [ContractAdjustmentReferenceId],
                        [Code]
                    FROM [engineer].[ContractAdjustmentIndexes]
                    WHERE [IsDeleted] = 0
                    GROUP BY
                        [ContractAdjustmentReferenceId],
                        [Code]
                    HAVING COUNT(*) > 1
                )
                BEGIN
                    THROW 51013,
                        'Duplicate ContractAdjustmentIndex Code detected after backfill.',
                        1;
                END;
                """);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "engineer",
                table: "ContractAdjustmentReferences",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                comment: "کد",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true,
                oldComment: "کد");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "engineer",
                table: "ContractAdjustmentIndexes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                comment: "کد",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true,
                oldComment: "کد");

            migrationBuilder.CreateTable(
                name: "ContractLegalSnapshots",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(
                            type: "bigint",
                            nullable: false,
                            comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),

                    ContractId = table.Column<long>(
                        type: "bigint",
                        nullable: false,
                        comment: "شناسه قرارداد"),

                    ProjectId = table.Column<long>(
                        type: "bigint",
                        nullable: false,
                        comment: "شناسه پروژه"),

                    ContractPartyId = table.Column<long>(
                        type: "bigint",
                        nullable: false,
                        comment: "شناسه طرف قرارداد"),

                    FaTitle = table.Column<string>(
                        type: "nvarchar(250)",
                        maxLength: 250,
                        nullable: false,
                        comment: "عنوان فارسی"),

                    EnTitle = table.Column<string>(
                        type: "nvarchar(250)",
                        maxLength: 250,
                        nullable: false,
                        comment: "عنوان انگلیسی"),

                    Description = table.Column<string>(
                        type: "nvarchar(1500)",
                        maxLength: 1500,
                        nullable: true,
                        comment: "توضیحات"),

                    StartDate = table.Column<DateTime>(
                        type: "datetime2",
                        nullable: false,
                        comment: "تاریخ شروع"),

                    Duration = table.Column<int>(
                        type: "int",
                        nullable: false,
                        comment: "مدت زمان"),

                    DurationUnit = table.Column<int>(
                        type: "int",
                        nullable: false,
                        comment: "واحد مدت زمان"),

                    EndDate = table.Column<DateTime>(
                        type: "datetime2",
                        nullable: false,
                        comment: "تاریخ پایان"),

                    Status = table.Column<int>(
                        type: "int",
                        nullable: false,
                        comment: "وضعیت"),

                    CurrencyId = table.Column<long>(
                        type: "bigint",
                        nullable: true,
                        comment: "ارز"),

                    InitialAmount = table.Column<decimal>(
                        type: "decimal(18,2)",
                        nullable: false,
                        comment: "مبلغ اولیه قرارداد"),

                    FinalAmount = table.Column<decimal>(
                        type: "decimal(18,2)",
                        nullable: false,
                        comment: "مبلغ نهایی قرارداد"),

                    RowVersion = table.Column<byte[]>(
                        type: "rowversion",
                        rowVersion: true,
                        nullable: false),

                    Created = table.Column<DateTime>(
                        type: "datetime2",
                        nullable: false,
                        comment: "تاریخ ایجاد"),

                    CreatorId = table.Column<long>(
                        type: "bigint",
                        nullable: false,
                        comment: "ایجاد کننده"),

                    Updated = table.Column<DateTime>(
                        type: "datetime2",
                        nullable: true,
                        comment: "تاریخ ویرایش"),

                    UpdaterId = table.Column<long>(
                        type: "bigint",
                        nullable: true,
                        comment: "ویرایش کننده"),

                    IsDeleted = table.Column<bool>(
                        type: "bit",
                        nullable: false,
                        comment: "حذف شدگی")
                },
                constraints: table =>
                {
                    table.PrimaryKey(
                        "PK_ContractLegalSnapshots",
                        x => x.Id);

                    table.ForeignKey(
                        name: "FK_ContractLegalSnapshots_Contracts_ContractId",
                        column: x => x.ContractId,
                        principalSchema: "engineer",
                        principalTable: "Contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);

                    table.ForeignKey(
                        name: "FK_ContractLegalSnapshots_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "engineer",
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContractStatusHistories",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(
                            type: "bigint",
                            nullable: false,
                            comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),

                    ContractId = table.Column<long>(
                        type: "bigint",
                        nullable: false,
                        comment: "شناسه قرارداد"),

                    FromStatus = table.Column<int>(
                        type: "int",
                        nullable: false,
                        comment: "وضعیت قبلی"),

                    ToStatus = table.Column<int>(
                        type: "int",
                        nullable: false,
                        comment: "وضعیت جدید"),

                    RowVersion = table.Column<byte[]>(
                        type: "rowversion",
                        rowVersion: true,
                        nullable: false),

                    Created = table.Column<DateTime>(
                        type: "datetime2",
                        nullable: false,
                        comment: "تاریخ ایجاد"),

                    CreatorId = table.Column<long>(
                        type: "bigint",
                        nullable: false,
                        comment: "ایجاد کننده"),

                    Updated = table.Column<DateTime>(
                        type: "datetime2",
                        nullable: true,
                        comment: "تاریخ ویرایش"),

                    UpdaterId = table.Column<long>(
                        type: "bigint",
                        nullable: true,
                        comment: "ویرایش کننده"),

                    IsDeleted = table.Column<bool>(
                        type: "bit",
                        nullable: false,
                        comment: "حذف شدگی")
                },
                constraints: table =>
                {
                    table.PrimaryKey(
                        "PK_ContractStatusHistories",
                        x => x.Id);

                    table.ForeignKey(
                        name: "FK_ContractStatusHistories_Contracts_ContractId",
                        column: x => x.ContractId,
                        principalSchema: "engineer",
                        principalTable: "Contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            /*
             * A Reference Code is globally unique among non-deleted rows.
             */
            migrationBuilder.CreateIndex(
                name: "IX_ContractAdjustmentReferences_Code",
                schema: "engineer",
                table: "ContractAdjustmentReferences",
                column: "Code",
                unique: true,
                filter: "[IsDeleted] = 0");

            /*
             * An Index Code is unique only inside its Reference.
             */
            migrationBuilder.CreateIndex(
                name: "IX_ContractAdjustmentIndexes_ContractAdjustmentReferenceId_Code",
                schema: "engineer",
                table: "ContractAdjustmentIndexes",
                columns: new[]
                {
                    "ContractAdjustmentReferenceId",
                    "Code"
                },
                unique: true,
                filter: "[IsDeleted] = 0");

            /*
             * Only one legal conclusion snapshot per Contract.
             */
            migrationBuilder.CreateIndex(
                name: "IX_ContractLegalSnapshots_ContractId",
                schema: "engineer",
                table: "ContractLegalSnapshots",
                column: "ContractId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContractLegalSnapshots_ProjectId",
                schema: "engineer",
                table: "ContractLegalSnapshots",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractStatusHistories_ContractId",
                schema: "engineer",
                table: "ContractStatusHistories",
                column: "ContractId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContractLegalSnapshots",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "ContractStatusHistories",
                schema: "engineer");

            migrationBuilder.DropIndex(
                name: "IX_ContractAdjustmentReferences_Code",
                schema: "engineer",
                table: "ContractAdjustmentReferences");

            migrationBuilder.DropIndex(
                name: "IX_ContractAdjustmentIndexes_ContractAdjustmentReferenceId_Code",
                schema: "engineer",
                table: "ContractAdjustmentIndexes");

            migrationBuilder.DropColumn(
                name: "FinalContractAmount",
                schema: "engineer",
                table: "ContractChanges");

            migrationBuilder.DropColumn(
                name: "FinancialChangeAmount",
                schema: "engineer",
                table: "ContractChanges");

            migrationBuilder.DropColumn(
                name: "PreviousContractAmount",
                schema: "engineer",
                table: "ContractChanges");

            migrationBuilder.DropColumn(
                name: "Code",
                schema: "engineer",
                table: "ContractAdjustmentReferences");

            migrationBuilder.DropColumn(
                name: "Code",
                schema: "engineer",
                table: "ContractAdjustmentIndexes");

            migrationBuilder.CreateIndex(
                name: "IX_ContractAdjustmentIndexes_ContractAdjustmentReferenceId",
                schema: "engineer",
                table: "ContractAdjustmentIndexes",
                column: "ContractAdjustmentReferenceId");
        }
    }
}