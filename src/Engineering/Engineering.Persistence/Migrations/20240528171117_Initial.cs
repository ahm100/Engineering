using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "engineer");

            migrationBuilder.CreateTable(
                name: "BillOfLadings",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    BillOfLadingName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    BillOfLadingCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillOfLadings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContractorContracts",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ContractorId = table.Column<long>(type: "bigint", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CurrencyId = table.Column<long>(type: "bigint", nullable: true),
                    WorkDonePercent = table.Column<int>(type: "int", nullable: true),
                    WorkDeliveryPercent = table.Column<int>(type: "int", nullable: true),
                    WorkCompletionPercent = table.Column<int>(type: "int", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractorContracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContractorEmployees",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    EmployeeId = table.Column<long>(type: "bigint", nullable: false),
                    ContractorId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractorEmployees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CostCenterTypes",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    CostCenterTypeCode = table.Column<string>(type: "nvarchar(50)", maxLength: 100, nullable: false),
                    CostCenterTypeTitle = table.Column<string>(type: "nvarchar(150)", maxLength: 250, nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostCenterTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CostOvers",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    CostOverName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CostOverCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostOvers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EngineeringCategories",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CategoryCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EngineeringCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EngineeringProjectTypes",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    ProjectTypeCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ProjectTypeTitle = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EngineeringProjectTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EngineeringServices",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    ServiceInfoName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    ServiceInfoCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UnitOfMeasurementId = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EngineeringServices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MachineriesGroup",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    GroupName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    GroupCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachineriesGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MachineTypes",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    MachineTypeCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MachineTypeTitle = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachineTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OperationInfos",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    OperationInfoName = table.Column<string>(type: "nvarchar(150)", maxLength: 250, nullable: false),
                    OperationInfoCode = table.Column<string>(type: "nvarchar(10)", maxLength: 100, nullable: false),
                    OperationLatinName = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    Priority = table.Column<int>(type: "int", nullable: true),
                    UnitOfMeasurementId = table.Column<long>(type: "bigint", nullable: false),
                    HaveStandard = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transportations",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    TransportationName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    TransportationCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsPassenger = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transportations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Trips",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    TripName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    TripCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trips", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContractorContractDetailSkills",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    SkillId = table.Column<long>(type: "bigint", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CurrencyId = table.Column<long>(type: "bigint", nullable: false),
                    ContractorContractId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractorContractDetailSkills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractorContractDetailSkills_ContractorContracts_ContractorContractId",
                        column: x => x.ContractorContractId,
                        principalSchema: "engineer",
                        principalTable: "ContractorContracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContractorContractHistories",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CurrencyId = table.Column<long>(type: "bigint", nullable: true),
                    WorkDonePercent = table.Column<int>(type: "int", nullable: true),
                    WorkDeliveryPercent = table.Column<int>(type: "int", nullable: true),
                    WorkCompletionPercent = table.Column<int>(type: "int", nullable: true),
                    ContractorContractId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractorContractHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractorContractHistories_ContractorContracts_ContractorContractId",
                        column: x => x.ContractorContractId,
                        principalSchema: "engineer",
                        principalTable: "ContractorContracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CostCenters",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    CostCenterCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CostCenterName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    NoOperationDays = table.Column<int>(type: "int", nullable: true),
                    CityId = table.Column<long>(type: "bigint", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(1500)", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    Latitude = table.Column<decimal>(type: "decimal(18,9)", nullable: false),
                    Longitude = table.Column<decimal>(type: "decimal(18,9)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1500)", nullable: true),
                    WeatherState = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CostCenterTypesId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostCenters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CostCenters_CostCenterTypes_CostCenterTypesId",
                        column: x => x.CostCenterTypesId,
                        principalSchema: "engineer",
                        principalTable: "CostCenterTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EngineeringBranchs",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    BranchName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    BranchCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CategoryId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EngineeringBranchs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EngineeringBranchs_EngineeringCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "engineer",
                        principalTable: "EngineeringCategories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ContractorServices",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    ContractorId = table.Column<long>(type: "bigint", nullable: false),
                    ServiceInfoId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractorServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractorServices_EngineeringServices_ServiceInfoId",
                        column: x => x.ServiceInfoId,
                        principalSchema: "engineer",
                        principalTable: "EngineeringServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Machineries",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    MachineryName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    MachineryCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    GroupId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Machineries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Machineries_MachineriesGroup_GroupId",
                        column: x => x.GroupId,
                        principalSchema: "engineer",
                        principalTable: "MachineriesGroup",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Machines",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    MachineName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    MachineCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FromWeight = table.Column<int>(type: "int", nullable: false),
                    UntilWeight = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    MachineTypesId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Machines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Machines_MachineTypes_MachineTypesId",
                        column: x => x.MachineTypesId,
                        principalSchema: "engineer",
                        principalTable: "MachineTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EngineeringStandardExperts",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    ExpertUnitId = table.Column<long>(type: "bigint", nullable: false),
                    ExpertNumber = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TimeSpant = table.Column<long>(type: "bigint", nullable: false),
                    UnusedPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OperationInfoId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EngineeringStandardExperts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EngineeringStandardExperts_OperationInfos_OperationInfoId",
                        column: x => x.OperationInfoId,
                        principalSchema: "engineer",
                        principalTable: "OperationInfos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EngineeringStandardProducts",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    ProductUnitId = table.Column<long>(type: "bigint", nullable: false),
                    Number = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnusedPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OperationInfoId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EngineeringStandardProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EngineeringStandardProducts_OperationInfos_OperationInfoId",
                        column: x => x.OperationInfoId,
                        principalSchema: "engineer",
                        principalTable: "OperationInfos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OperationInfoDependencies",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    RelationId = table.Column<long>(type: "bigint", nullable: false),
                    WorkingDays = table.Column<int>(type: "int", nullable: false),
                    DependencyType = table.Column<int>(type: "int", nullable: false),
                    OperationInfoId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationInfoDependencies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OperationInfoDependencies_OperationInfos_OperationInfoId",
                        column: x => x.OperationInfoId,
                        principalSchema: "engineer",
                        principalTable: "OperationInfos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OperationInfoServices",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    OperationInfoId = table.Column<long>(type: "bigint", nullable: false),
                    ServiceInfoId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationInfoServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OperationInfoServices_EngineeringServices_ServiceInfoId",
                        column: x => x.ServiceInfoId,
                        principalSchema: "engineer",
                        principalTable: "EngineeringServices",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OperationInfoServices_OperationInfos_OperationInfoId",
                        column: x => x.OperationInfoId,
                        principalSchema: "engineer",
                        principalTable: "OperationInfos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ContractorContractDetailThirdParties",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    ThirdPartyId = table.Column<long>(type: "bigint", nullable: false),
                    ContractorContractDetailSkillId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractorContractDetailThirdParties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractorContractDetailThirdParties_ContractorContractDetailSkills_ContractorContractDetailSkillId",
                        column: x => x.ContractorContractDetailSkillId,
                        principalSchema: "engineer",
                        principalTable: "ContractorContractDetailSkills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CostCenterAuthorizedRoles",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    AuthorizedRoleId = table.Column<long>(type: "bigint", nullable: false),
                    CostCenterId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostCenterAuthorizedRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CostCenterAuthorizedRoles_CostCenters_CostCenterId",
                        column: x => x.CostCenterId,
                        principalSchema: "engineer",
                        principalTable: "CostCenters",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CostCenterAuthorizedUsers",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    AuthorizedUserId = table.Column<long>(type: "bigint", nullable: false),
                    CostCenterId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostCenterAuthorizedUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CostCenterAuthorizedUsers_CostCenters_CostCenterId",
                        column: x => x.CostCenterId,
                        principalSchema: "engineer",
                        principalTable: "CostCenters",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CostCenterVirtualGroups",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Identifier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SendToday = table.Column<bool>(type: "bit", nullable: false),
                    TodayTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    SendYesterday = table.Column<bool>(type: "bit", nullable: false),
                    YesterdayTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    CostCenterId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostCenterVirtualGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CostCenterVirtualGroups_CostCenters_CostCenterId",
                        column: x => x.CostCenterId,
                        principalSchema: "engineer",
                        principalTable: "CostCenters",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CostCenterWarehouses",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    WarehouseId = table.Column<long>(type: "bigint", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CostCenterId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostCenterWarehouses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CostCenterWarehouses_CostCenters_CostCenterId",
                        column: x => x.CostCenterId,
                        principalSchema: "engineer",
                        principalTable: "CostCenters",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InformedUsers",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    EmployeeId = table.Column<long>(type: "bigint", nullable: false),
                    CostCenterId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InformedUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InformedUsers_CostCenters_CostCenterId",
                        column: x => x.CostCenterId,
                        principalSchema: "engineer",
                        principalTable: "CostCenters",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OperationLocations",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    OperationLocationName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    PublicName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrivateName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OperationLocationLatinName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    PrivateCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PublicCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    CostCenterId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationLocations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OperationLocations_CostCenters_CostCenterId",
                        column: x => x.CostCenterId,
                        principalSchema: "engineer",
                        principalTable: "CostCenters",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OperationLocations_OperationLocations_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "engineer",
                        principalTable: "OperationLocations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    ProjectName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    ProjectCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EmployerId = table.Column<long>(type: "bigint", nullable: false),
                    SupervisorEngineer = table.Column<long>(type: "bigint", nullable: true),
                    Advisor = table.Column<long>(type: "bigint", nullable: true),
                    ProjectManager = table.Column<long>(type: "bigint", nullable: true),
                    PlanningAssistant = table.Column<long>(type: "bigint", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CategoryId = table.Column<long>(type: "bigint", nullable: false),
                    CostCenterId = table.Column<long>(type: "bigint", nullable: false),
                    ProjectTypesId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_CostCenters_CostCenterId",
                        column: x => x.CostCenterId,
                        principalSchema: "engineer",
                        principalTable: "CostCenters",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Projects_EngineeringCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "engineer",
                        principalTable: "EngineeringCategories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Projects_EngineeringProjectTypes_ProjectTypesId",
                        column: x => x.ProjectTypesId,
                        principalSchema: "engineer",
                        principalTable: "EngineeringProjectTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EngineeringSeasons",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    SeasonName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    SeasonCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    BranchId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EngineeringSeasons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EngineeringSeasons_EngineeringBranchs_BranchId",
                        column: x => x.BranchId,
                        principalSchema: "engineer",
                        principalTable: "EngineeringBranchs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EngineeringStandardMachineries",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    MachineryNumber = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TimeSpant = table.Column<long>(type: "bigint", nullable: false),
                    UnusedPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OperationInfoId = table.Column<long>(type: "bigint", nullable: false),
                    MachineryId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EngineeringStandardMachineries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EngineeringStandardMachineries_Machineries_MachineryId",
                        column: x => x.MachineryId,
                        principalSchema: "engineer",
                        principalTable: "Machineries",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EngineeringStandardMachineries_OperationInfos_OperationInfoId",
                        column: x => x.OperationInfoId,
                        principalSchema: "engineer",
                        principalTable: "OperationInfos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CostCenterVirtualGroupAdmins",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    ThirdPartyId = table.Column<long>(type: "bigint", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CostCenterVirtualGroupId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostCenterVirtualGroupAdmins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CostCenterVirtualGroupAdmins_CostCenterVirtualGroups_CostCenterVirtualGroupId",
                        column: x => x.CostCenterVirtualGroupId,
                        principalSchema: "engineer",
                        principalTable: "CostCenterVirtualGroups",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ContractorFinancialStatements",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    StatementNumber = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NULL"),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalWorkLoad = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WorkLoadDone = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContractorContractId = table.Column<long>(type: "bigint", nullable: false),
                    CostCenterId = table.Column<long>(type: "bigint", nullable: false),
                    ProjectId = table.Column<long>(type: "bigint", nullable: false),
                    LastContractorFinancialStatementId = table.Column<long>(type: "bigint", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractorFinancialStatements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractorFinancialStatements_ContractorContracts_ContractorContractId",
                        column: x => x.ContractorContractId,
                        principalSchema: "engineer",
                        principalTable: "ContractorContracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContractorFinancialStatements_ContractorFinancialStatements_LastContractorFinancialStatementId",
                        column: x => x.LastContractorFinancialStatementId,
                        principalSchema: "engineer",
                        principalTable: "ContractorFinancialStatements",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ContractorFinancialStatements_CostCenters_CostCenterId",
                        column: x => x.CostCenterId,
                        principalSchema: "engineer",
                        principalTable: "CostCenters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContractorFinancialStatements_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "engineer",
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployerContracts",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    ContractCode = table.Column<string>(type: "nvarchar(150)", maxLength: 250, nullable: true),
                    CurrencyUnitId = table.Column<long>(type: "bigint", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PenaltyPercentage = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ProjectId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployerContracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployerContracts_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "engineer",
                        principalTable: "Projects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ImplementationAssistants",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    ImplementationAssistantUserId = table.Column<long>(type: "bigint", nullable: false),
                    ProjectId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImplementationAssistants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImplementationAssistants_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "engineer",
                        principalTable: "Projects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RequestMachineries",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    TimeRequired = table.Column<TimeSpan>(type: "time", nullable: false),
                    Unit = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    RequestCount = table.Column<int>(type: "int", nullable: false),
                    FromDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ToDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectId = table.Column<long>(type: "bigint", nullable: false),
                    MachineryId = table.Column<long>(type: "bigint", nullable: false),
                    OperatorAppoinmentId = table.Column<long>(type: "bigint", nullable: true),
                    OperatorAppoinmentUserId = table.Column<long>(type: "bigint", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestMachineries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestMachineries_Machineries_MachineryId",
                        column: x => x.MachineryId,
                        principalSchema: "engineer",
                        principalTable: "Machineries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RequestMachineries_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "engineer",
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TechnicalAssistants",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    TechnicalAssistantUserId = table.Column<long>(type: "bigint", nullable: false),
                    ProjectId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnicalAssistants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TechnicalAssistants_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "engineer",
                        principalTable: "Projects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TransportationRequests",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    RequestById = table.Column<long>(type: "bigint", nullable: false),
                    StartingCityId = table.Column<long>(type: "bigint", nullable: false),
                    DestinationCityId = table.Column<long>(type: "bigint", nullable: false),
                    ImageLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1500)", nullable: true),
                    DriverId = table.Column<long>(type: "bigint", nullable: true),
                    DriverName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostageDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReceivedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BillOfLadingImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DelivererName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecipientName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FreightNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoadWeight = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CarSpecifications = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberPlates = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankId = table.Column<long>(type: "bigint", nullable: true),
                    CardNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IBAN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    CurrencyUnitId = table.Column<long>(type: "bigint", nullable: true),
                    AccountDescription = table.Column<string>(type: "nvarchar(1500)", nullable: true),
                    TransportationRequestStatus = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    ManagerDescription = table.Column<string>(type: "nvarchar(1500)", nullable: true),
                    TransportationId = table.Column<long>(type: "bigint", nullable: false),
                    TripId = table.Column<long>(type: "bigint", nullable: false),
                    MachineId = table.Column<long>(type: "bigint", nullable: false),
                    ProjectId = table.Column<long>(type: "bigint", nullable: false),
                    BillOfLadingId = table.Column<long>(type: "bigint", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransportationRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransportationRequests_BillOfLadings_BillOfLadingId",
                        column: x => x.BillOfLadingId,
                        principalSchema: "engineer",
                        principalTable: "BillOfLadings",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TransportationRequests_Machines_MachineId",
                        column: x => x.MachineId,
                        principalSchema: "engineer",
                        principalTable: "Machines",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TransportationRequests_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "engineer",
                        principalTable: "Projects",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TransportationRequests_Transportations_TransportationId",
                        column: x => x.TransportationId,
                        principalSchema: "engineer",
                        principalTable: "Transportations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TransportationRequests_Trips_TripId",
                        column: x => x.TripId,
                        principalSchema: "engineer",
                        principalTable: "Trips",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OperationInfoSeasons",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    OperationInfoId = table.Column<long>(type: "bigint", nullable: false),
                    SeasonId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationInfoSeasons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OperationInfoSeasons_EngineeringSeasons_SeasonId",
                        column: x => x.SeasonId,
                        principalSchema: "engineer",
                        principalTable: "EngineeringSeasons",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OperationInfoSeasons_OperationInfos_OperationInfoId",
                        column: x => x.OperationInfoId,
                        principalSchema: "engineer",
                        principalTable: "OperationInfos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ContractorFinancialStatementHistories",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    Status = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalWorkLoad = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WorkLoadDone = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ContractorFinancialStatementId = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractorFinancialStatementHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractorFinancialStatementHistories_ContractorFinancialStatements_ContractorFinancialStatementId",
                        column: x => x.ContractorFinancialStatementId,
                        principalSchema: "engineer",
                        principalTable: "ContractorFinancialStatements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContractorFinancialStatementThirdParties",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    ThirdPartyId = table.Column<long>(type: "bigint", nullable: false),
                    SkillId = table.Column<long>(type: "bigint", nullable: false),
                    Count = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CurrencyId = table.Column<long>(type: "bigint", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ContractorFinancialStatementId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractorFinancialStatementThirdParties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractorFinancialStatementThirdParties_ContractorFinancialStatements_ContractorFinancialStatementId",
                        column: x => x.ContractorFinancialStatementId,
                        principalSchema: "engineer",
                        principalTable: "ContractorFinancialStatements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContractCostOvers",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Perecent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EmployerContractId = table.Column<long>(type: "bigint", nullable: false),
                    CostOverId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractCostOvers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractCostOvers_CostOvers_CostOverId",
                        column: x => x.CostOverId,
                        principalSchema: "engineer",
                        principalTable: "CostOvers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ContractCostOvers_EmployerContracts_EmployerContractId",
                        column: x => x.EmployerContractId,
                        principalSchema: "engineer",
                        principalTable: "EmployerContracts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DocumentDetails",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    DocumentType = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1500)", nullable: false),
                    Version = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 5, 28, 20, 41, 14, 10, DateTimeKind.Local).AddTicks(1491)),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    EmployerContractId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentDetails_EmployerContracts_EmployerContractId",
                        column: x => x.EmployerContractId,
                        principalSchema: "engineer",
                        principalTable: "EmployerContracts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EmployerConsiderations",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    ConsiderationType = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1500)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    EmployerContractId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployerConsiderations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployerConsiderations_EmployerContracts_EmployerContractId",
                        column: x => x.EmployerContractId,
                        principalSchema: "engineer",
                        principalTable: "EmployerContracts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EmployerStatusStatements",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    StatusStatementCode = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    SendStatusType = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PercentageOfWorkDone = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    CalculatedAmount = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    StatusStatementVolume = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    CurrencyId = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    EmployerContractId = table.Column<long>(type: "bigint", nullable: false),
                    ProjectId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployerStatusStatements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployerStatusStatements_EmployerContracts_EmployerContractId",
                        column: x => x.EmployerContractId,
                        principalSchema: "engineer",
                        principalTable: "EmployerContracts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmployerStatusStatements_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "engineer",
                        principalTable: "Projects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProjectOperations",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    Workload = table.Column<long>(type: "bigint", nullable: false),
                    TolerancePercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true, defaultValue: 0m),
                    Priority = table.Column<int>(type: "int", nullable: true),
                    UnitOfMeasurementId = table.Column<long>(type: "bigint", nullable: false),
                    ProjectOperationStatus = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Description = table.Column<string>(type: "nvarchar(1500)", nullable: true),
                    ProjectId = table.Column<long>(type: "bigint", nullable: false),
                    OperationInfoId = table.Column<long>(type: "bigint", nullable: false),
                    EmployerContractId = table.Column<long>(type: "bigint", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectOperations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectOperations_EmployerContracts_EmployerContractId",
                        column: x => x.EmployerContractId,
                        principalSchema: "engineer",
                        principalTable: "EmployerContracts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProjectOperations_OperationInfos_OperationInfoId",
                        column: x => x.OperationInfoId,
                        principalSchema: "engineer",
                        principalTable: "OperationInfos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProjectOperations_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "engineer",
                        principalTable: "Projects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RequestMachineryAssignments",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    MachineryIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequestMachineryId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestMachineryAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestMachineryAssignments_RequestMachineries_RequestMachineryId",
                        column: x => x.RequestMachineryId,
                        principalSchema: "engineer",
                        principalTable: "RequestMachineries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RequestMachineryHistories",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    RequestMachineryId = table.Column<long>(type: "bigint", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OperatorAppoinmentId = table.Column<long>(type: "bigint", nullable: true),
                    OperatorAppoinmentUserId = table.Column<long>(type: "bigint", nullable: true),
                    RequestDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestMachineryHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestMachineryHistories_RequestMachineries_RequestMachineryId",
                        column: x => x.RequestMachineryId,
                        principalSchema: "engineer",
                        principalTable: "RequestMachineries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RequestMachineryInquiryOperators",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    OperatorAppoinmentId = table.Column<long>(type: "bigint", nullable: false),
                    OperatorAppoinmentUserId = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    RequestMachineryId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestMachineryInquiryOperators", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestMachineryInquiryOperators_RequestMachineries_RequestMachineryId",
                        column: x => x.RequestMachineryId,
                        principalSchema: "engineer",
                        principalTable: "RequestMachineries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContractorFinancialStatementTransportations",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    ContractorFinancialStatementId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractorFinancialStatementTransportations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractorFinancialStatementTransportations_ContractorFinancialStatements_ContractorFinancialStatementId",
                        column: x => x.ContractorFinancialStatementId,
                        principalSchema: "engineer",
                        principalTable: "ContractorFinancialStatements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContractorFinancialStatementTransportations_TransportationRequests_ContractorFinancialStatementId",
                        column: x => x.ContractorFinancialStatementId,
                        principalSchema: "engineer",
                        principalTable: "TransportationRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContractCostOverImpacts",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    ContractCostOverId = table.Column<long>(type: "bigint", nullable: false),
                    CostOverId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractCostOverImpacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractCostOverImpacts_ContractCostOvers_ContractCostOverId",
                        column: x => x.ContractCostOverId,
                        principalSchema: "engineer",
                        principalTable: "ContractCostOvers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ContractCostOverImpacts_CostOvers_CostOverId",
                        column: x => x.CostOverId,
                        principalSchema: "engineer",
                        principalTable: "CostOvers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DocumentDetailUrls",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    DocumentUrlId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DocumentDetailId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentDetailUrls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentDetailUrls_DocumentDetails_DocumentDetailId",
                        column: x => x.DocumentDetailId,
                        principalSchema: "engineer",
                        principalTable: "DocumentDetails",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ConsiderationDependencies",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    ConsiderationId = table.Column<long>(type: "bigint", nullable: false),
                    OperationInfoId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsiderationDependencies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConsiderationDependencies_EmployerConsiderations_ConsiderationId",
                        column: x => x.ConsiderationId,
                        principalSchema: "engineer",
                        principalTable: "EmployerConsiderations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ConsiderationDependencies_OperationInfos_OperationInfoId",
                        column: x => x.OperationInfoId,
                        principalSchema: "engineer",
                        principalTable: "OperationInfos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ContractorFinancialStatementDetails",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    FinalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalFinalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ProjectOperationId = table.Column<long>(type: "bigint", nullable: false),
                    ContractorFinancialStatementId = table.Column<long>(type: "bigint", nullable: false),
                    LastContractorFinancialStatementId = table.Column<long>(type: "bigint", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractorFinancialStatementDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractorFinancialStatementDetails_ContractorFinancialStatements_ContractorFinancialStatementId",
                        column: x => x.ContractorFinancialStatementId,
                        principalSchema: "engineer",
                        principalTable: "ContractorFinancialStatements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContractorFinancialStatementDetails_ContractorFinancialStatements_LastContractorFinancialStatementId",
                        column: x => x.LastContractorFinancialStatementId,
                        principalSchema: "engineer",
                        principalTable: "ContractorFinancialStatements",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ContractorFinancialStatementDetails_ProjectOperations_ProjectOperationId",
                        column: x => x.ProjectOperationId,
                        principalSchema: "engineer",
                        principalTable: "ProjectOperations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContractorFinancialStatementMachineries",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalWorkDone = table.Column<TimeSpan>(type: "time", nullable: false),
                    TotalCount = table.Column<int>(type: "int", nullable: false),
                    CurrencyId = table.Column<long>(type: "bigint", nullable: false),
                    ProjectOperationId = table.Column<long>(type: "bigint", nullable: false),
                    MachineryId = table.Column<long>(type: "bigint", nullable: false),
                    ContractorFinancialStatementId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractorFinancialStatementMachineries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractorFinancialStatementMachineries_ContractorFinancialStatements_ContractorFinancialStatementId",
                        column: x => x.ContractorFinancialStatementId,
                        principalSchema: "engineer",
                        principalTable: "ContractorFinancialStatements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContractorFinancialStatementMachineries_Machineries_MachineryId",
                        column: x => x.MachineryId,
                        principalSchema: "engineer",
                        principalTable: "Machineries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContractorFinancialStatementMachineries_ProjectOperations_ProjectOperationId",
                        column: x => x.ProjectOperationId,
                        principalSchema: "engineer",
                        principalTable: "ProjectOperations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployerStatusStatementProjectOperations",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    TotalWorkVolume = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    DoneWorkVolume = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    StatusStatementWorkVolume = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    DonePercentage = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    TotalPercentage = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    CalculatedAmount = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    CurrencyId = table.Column<long>(type: "bigint", nullable: false),
                    EmployerStatusStatementId = table.Column<long>(type: "bigint", nullable: false),
                    ProjectOperationId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployerStatusStatementProjectOperations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployerStatusStatementProjectOperations_EmployerStatusStatements_EmployerStatusStatementId",
                        column: x => x.EmployerStatusStatementId,
                        principalSchema: "engineer",
                        principalTable: "EmployerStatusStatements",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmployerStatusStatementProjectOperations_ProjectOperations_ProjectOperationId",
                        column: x => x.ProjectOperationId,
                        principalSchema: "engineer",
                        principalTable: "ProjectOperations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FiduciaryProducts",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    RequestNumber = table.Column<long>(type: "bigint", nullable: true),
                    CostCenterId = table.Column<long>(type: "bigint", nullable: false),
                    ProjectId = table.Column<long>(type: "bigint", nullable: false),
                    ProjectOperationId = table.Column<long>(type: "bigint", nullable: false),
                    ThirdPartyId = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1500)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FiduciaryProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FiduciaryProducts_CostCenters_CostCenterId",
                        column: x => x.CostCenterId,
                        principalSchema: "engineer",
                        principalTable: "CostCenters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FiduciaryProducts_ProjectOperations_ProjectOperationId",
                        column: x => x.ProjectOperationId,
                        principalSchema: "engineer",
                        principalTable: "ProjectOperations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FiduciaryProducts_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "engineer",
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectOperationDependencies",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    RelationId = table.Column<long>(type: "bigint", nullable: false),
                    RelationDays = table.Column<int>(type: "int", nullable: false),
                    DependencyType = table.Column<int>(type: "int", nullable: false),
                    ProjectOperationId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectOperationDependencies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectOperationDependencies_ProjectOperations_ProjectOperationId",
                        column: x => x.ProjectOperationId,
                        principalSchema: "engineer",
                        principalTable: "ProjectOperations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProjectOperationDetails",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Length = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 1m),
                    Width = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 1m),
                    Height = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 1m),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 1m),
                    Number = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 1m),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    Day = table.Column<int>(type: "int", nullable: false),
                    Hour = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1500)", nullable: true),
                    ProjectOperationId = table.Column<long>(type: "bigint", nullable: false),
                    OperationLocationId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectOperationDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectOperationDetails_OperationLocations_OperationLocationId",
                        column: x => x.OperationLocationId,
                        principalSchema: "engineer",
                        principalTable: "OperationLocations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProjectOperationDetails_ProjectOperations_ProjectOperationId",
                        column: x => x.ProjectOperationId,
                        principalSchema: "engineer",
                        principalTable: "ProjectOperations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RequestMachineryProjectOperations",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    ProjectOperationId = table.Column<long>(type: "bigint", nullable: false),
                    RequestMachineryId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestMachineryProjectOperations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestMachineryProjectOperations_ProjectOperations_ProjectOperationId",
                        column: x => x.ProjectOperationId,
                        principalSchema: "engineer",
                        principalTable: "ProjectOperations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RequestMachineryProjectOperations_RequestMachineries_RequestMachineryId",
                        column: x => x.RequestMachineryId,
                        principalSchema: "engineer",
                        principalTable: "RequestMachineries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TransportationRequestProjectOperations",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    TransportationRequestId = table.Column<long>(type: "bigint", nullable: false),
                    ProjectOperationId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransportationRequestProjectOperations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransportationRequestProjectOperations_ProjectOperations_ProjectOperationId",
                        column: x => x.ProjectOperationId,
                        principalSchema: "engineer",
                        principalTable: "ProjectOperations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TransportationRequestProjectOperations_TransportationRequests_TransportationRequestId",
                        column: x => x.TransportationRequestId,
                        principalSchema: "engineer",
                        principalTable: "TransportationRequests",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RequestMachineryInquiries",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    ThirdPartyId = table.Column<long>(type: "bigint", nullable: false),
                    CurrencyId = table.Column<long>(type: "bigint", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    Unit = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsConfirmed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ConfirmedUser = table.Column<long>(type: "bigint", nullable: true),
                    RequestMachineryInquiryOperatorId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestMachineryInquiries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestMachineryInquiries_RequestMachineryInquiryOperators_RequestMachineryInquiryOperatorId",
                        column: x => x.RequestMachineryInquiryOperatorId,
                        principalSchema: "engineer",
                        principalTable: "RequestMachineryInquiryOperators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FiduciaryProductDetails",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    LoanCount = table.Column<int>(type: "int", nullable: false),
                    LoanDays = table.Column<int>(type: "int", nullable: false),
                    ConfirmedLoanDays = table.Column<int>(type: "int", nullable: true),
                    MeasureUnitId = table.Column<long>(type: "bigint", nullable: false),
                    CurrencyId = table.Column<long>(type: "bigint", nullable: false),
                    DailyLateFine = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ConfirmedDailyLateFine = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1500)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    DeliverDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FiduciaryProductId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FiduciaryProductDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FiduciaryProductDetails_FiduciaryProducts_FiduciaryProductId",
                        column: x => x.FiduciaryProductId,
                        principalSchema: "engineer",
                        principalTable: "FiduciaryProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FiduciaryProductHistories",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    CostCenterId = table.Column<long>(type: "bigint", nullable: false),
                    ProjectId = table.Column<long>(type: "bigint", nullable: false),
                    ProjectOperationId = table.Column<long>(type: "bigint", nullable: false),
                    ThirdPartyId = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1500)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    FiduciaryProductId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FiduciaryProductHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FiduciaryProductHistories_CostCenters_CostCenterId",
                        column: x => x.CostCenterId,
                        principalSchema: "engineer",
                        principalTable: "CostCenters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FiduciaryProductHistories_FiduciaryProducts_FiduciaryProductId",
                        column: x => x.FiduciaryProductId,
                        principalSchema: "engineer",
                        principalTable: "FiduciaryProducts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FiduciaryProductHistories_ProjectOperations_ProjectOperationId",
                        column: x => x.ProjectOperationId,
                        principalSchema: "engineer",
                        principalTable: "ProjectOperations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FiduciaryProductHistories_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "engineer",
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DailyProjectOperations",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    Status = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Length = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 1m),
                    Width = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 1m),
                    Height = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 1m),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 1m),
                    Number = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 1m),
                    ProjectOperationDetailId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyProjectOperations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailyProjectOperations_ProjectOperationDetails_ProjectOperationDetailId",
                        column: x => x.ProjectOperationDetailId,
                        principalSchema: "engineer",
                        principalTable: "ProjectOperationDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployerStatusStatementProjectOperationDetails",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    TotalWorkVolume = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    DoneWorkVolume = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    StatusStatementWorkVolume = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    DonePercentage = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    TotalPercentage = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    CalculatedAmount = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    CurrencyId = table.Column<long>(type: "bigint", nullable: false),
                    EmployerStatusStatementProjectOperationId = table.Column<long>(type: "bigint", nullable: false),
                    ProjectOperationDetailId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployerStatusStatementProjectOperationDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployerStatusStatementProjectOperationDetails_EmployerStatusStatementProjectOperations_EmployerStatusStatementProjectOperat~",
                        column: x => x.EmployerStatusStatementProjectOperationId,
                        principalSchema: "engineer",
                        principalTable: "EmployerStatusStatementProjectOperations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmployerStatusStatementProjectOperationDetails_ProjectOperationDetails_ProjectOperationDetailId",
                        column: x => x.ProjectOperationDetailId,
                        principalSchema: "engineer",
                        principalTable: "ProjectOperationDetails",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProjectOperationDetailCmts",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    ProjectOperationDetailId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectOperationDetailCmts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectOperationDetailCmts_ProjectOperationDetailCmts_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "engineer",
                        principalTable: "ProjectOperationDetailCmts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProjectOperationDetailCmts_ProjectOperationDetails_ProjectOperationDetailId",
                        column: x => x.ProjectOperationDetailId,
                        principalSchema: "engineer",
                        principalTable: "ProjectOperationDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectOperationDetailConsumableVolumeExperts",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    ExpertId = table.Column<long>(type: "bigint", nullable: false),
                    Number = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnusedPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsStandard = table.Column<bool>(type: "bit", nullable: false),
                    StandardValue = table.Column<long>(type: "bigint", nullable: true),
                    FinalValue = table.Column<long>(type: "bigint", nullable: false, defaultValue: 36000000000L),
                    ProjectOperationDetailId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectOperationDetailConsumableVolumeExperts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectOperationDetailConsumableVolumeExperts_ProjectOperationDetails_ProjectOperationDetailId",
                        column: x => x.ProjectOperationDetailId,
                        principalSchema: "engineer",
                        principalTable: "ProjectOperationDetails",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProjectOperationDetailConsumableVolumeMachineries",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    Number = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnusedPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsStandard = table.Column<bool>(type: "bit", nullable: false),
                    StandardValue = table.Column<long>(type: "bigint", nullable: true),
                    FinalValue = table.Column<long>(type: "bigint", nullable: false, defaultValue: 36000000000L),
                    ProjectOperationDetailId = table.Column<long>(type: "bigint", nullable: false),
                    MachineryId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectOperationDetailConsumableVolumeMachineries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectOperationDetailConsumableVolumeMachineries_Machineries_MachineryId",
                        column: x => x.MachineryId,
                        principalSchema: "engineer",
                        principalTable: "Machineries",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProjectOperationDetailConsumableVolumeMachineries_ProjectOperationDetails_ProjectOperationDetailId",
                        column: x => x.ProjectOperationDetailId,
                        principalSchema: "engineer",
                        principalTable: "ProjectOperationDetails",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProjectOperationDetailConsumableVolumeProducts",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    ProductGroupId = table.Column<long>(type: "bigint", nullable: false),
                    UnusedPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsStandard = table.Column<bool>(type: "bit", nullable: false),
                    StandardValue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    FinalValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProjectOperationDetailId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectOperationDetailConsumableVolumeProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectOperationDetailConsumableVolumeProducts_ProjectOperationDetails_ProjectOperationDetailId",
                        column: x => x.ProjectOperationDetailId,
                        principalSchema: "engineer",
                        principalTable: "ProjectOperationDetails",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProjectOperationDetailContractorServices",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    ContractorId = table.Column<long>(type: "bigint", nullable: true),
                    Volume = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    ProjectOperationDetailId = table.Column<long>(type: "bigint", nullable: false),
                    OperationInfoServiceId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectOperationDetailContractorServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectOperationDetailContractorServices_OperationInfoServices_OperationInfoServiceId",
                        column: x => x.OperationInfoServiceId,
                        principalSchema: "engineer",
                        principalTable: "OperationInfoServices",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProjectOperationDetailContractorServices_ProjectOperationDetails_ProjectOperationDetailId",
                        column: x => x.ProjectOperationDetailId,
                        principalSchema: "engineer",
                        principalTable: "ProjectOperationDetails",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProjectOperationDetailUserImplementations",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    ImplementationAssistantUserId = table.Column<long>(type: "bigint", nullable: false),
                    ProjectOperationDetailId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectOperationDetailUserImplementations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectOperationDetailUserImplementations_ProjectOperationDetails_ProjectOperationDetailId",
                        column: x => x.ProjectOperationDetailId,
                        principalSchema: "engineer",
                        principalTable: "ProjectOperationDetails",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProjectOperationDetailUserPlaners",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    UserPlanerId = table.Column<long>(type: "bigint", nullable: false),
                    ProjectOperationDetailId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectOperationDetailUserPlaners", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectOperationDetailUserPlaners_ProjectOperationDetails_ProjectOperationDetailId",
                        column: x => x.ProjectOperationDetailId,
                        principalSchema: "engineer",
                        principalTable: "ProjectOperationDetails",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProjectOperationDetailUserTechnicals",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    TechnicalAssistantUserId = table.Column<long>(type: "bigint", nullable: false),
                    ProjectOperationDetailId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectOperationDetailUserTechnicals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectOperationDetailUserTechnicals_ProjectOperationDetails_ProjectOperationDetailId",
                        column: x => x.ProjectOperationDetailId,
                        principalSchema: "engineer",
                        principalTable: "ProjectOperationDetails",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RequestGoodsSupplies",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1500)", nullable: true),
                    SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TemporaryData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContractorId = table.Column<long>(type: "bigint", nullable: true),
                    ProjectOperationId = table.Column<long>(type: "bigint", nullable: false),
                    ProjectOperationDetailId = table.Column<long>(type: "bigint", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestGoodsSupplies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestGoodsSupplies_ProjectOperationDetails_ProjectOperationDetailId",
                        column: x => x.ProjectOperationDetailId,
                        principalSchema: "engineer",
                        principalTable: "ProjectOperationDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RequestGoodsSupplies_ProjectOperations_ProjectOperationId",
                        column: x => x.ProjectOperationId,
                        principalSchema: "engineer",
                        principalTable: "ProjectOperations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RequestMachineryProjectOperationDetails",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    ProjectOperationDetailId = table.Column<long>(type: "bigint", nullable: false),
                    RequestMachineryId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestMachineryProjectOperationDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestMachineryProjectOperationDetails_ProjectOperationDetails_ProjectOperationDetailId",
                        column: x => x.ProjectOperationDetailId,
                        principalSchema: "engineer",
                        principalTable: "ProjectOperationDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RequestMachineryProjectOperationDetails_RequestMachineries_RequestMachineryId",
                        column: x => x.RequestMachineryId,
                        principalSchema: "engineer",
                        principalTable: "RequestMachineries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TransportationRequestProjectOperationDetails",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    TransportationRequestId = table.Column<long>(type: "bigint", nullable: false),
                    ProjectOperationDetailId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransportationRequestProjectOperationDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransportationRequestProjectOperationDetails_ProjectOperationDetails_ProjectOperationDetailId",
                        column: x => x.ProjectOperationDetailId,
                        principalSchema: "engineer",
                        principalTable: "ProjectOperationDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TransportationRequestProjectOperationDetails_TransportationRequests_TransportationRequestId",
                        column: x => x.TransportationRequestId,
                        principalSchema: "engineer",
                        principalTable: "TransportationRequests",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RequestMachineryInquiryDocument",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequestMachineryInquiryId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestMachineryInquiryDocument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestMachineryInquiryDocument_RequestMachineryInquiries_RequestMachineryInquiryId",
                        column: x => x.RequestMachineryInquiryId,
                        principalSchema: "engineer",
                        principalTable: "RequestMachineryInquiries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContractorFinancialStatementFiduciaryProducts",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FiduciaryProductDetailId = table.Column<long>(type: "bigint", nullable: false),
                    ContractorFinancialStatementId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractorFinancialStatementFiduciaryProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractorFinancialStatementFiduciaryProducts_ContractorFinancialStatements_ContractorFinancialStatementId",
                        column: x => x.ContractorFinancialStatementId,
                        principalSchema: "engineer",
                        principalTable: "ContractorFinancialStatements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContractorFinancialStatementFiduciaryProducts_FiduciaryProductDetails_FiduciaryProductDetailId",
                        column: x => x.FiduciaryProductDetailId,
                        principalSchema: "engineer",
                        principalTable: "FiduciaryProductDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FiduciaryProductDetailHistories",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    LoanCount = table.Column<int>(type: "int", nullable: false),
                    LoanDays = table.Column<int>(type: "int", nullable: false),
                    ConfirmedLoanDays = table.Column<int>(type: "int", nullable: true),
                    MeasureUnitId = table.Column<long>(type: "bigint", nullable: false),
                    CurrencyId = table.Column<long>(type: "bigint", nullable: false),
                    DailyLateFine = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ConfirmedDailyLateFine = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1500)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeliverDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FiduciaryProductDetailId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FiduciaryProductDetailHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FiduciaryProductDetailHistories_FiduciaryProductDetails_FiduciaryProductDetailId",
                        column: x => x.FiduciaryProductDetailId,
                        principalSchema: "engineer",
                        principalTable: "FiduciaryProductDetails",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FiduciaryProductDetailManagements",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    InvoiceId = table.Column<long>(type: "bigint", nullable: false),
                    WarehouseId = table.Column<long>(type: "bigint", nullable: false),
                    ConfirmedLoanCount = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 3),
                    FiduciaryProductDetailId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FiduciaryProductDetailManagements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FiduciaryProductDetailManagements_FiduciaryProductDetails_FiduciaryProductDetailId",
                        column: x => x.FiduciaryProductDetailId,
                        principalSchema: "engineer",
                        principalTable: "FiduciaryProductDetails",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ContractorFinancialStatementDailyOperations",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DailyProjectOperationId = table.Column<long>(type: "bigint", nullable: false),
                    ContractorFinancialStatementDetailId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractorFinancialStatementDailyOperations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractorFinancialStatementDailyOperations_ContractorFinancialStatementDetails_ContractorFinancialStatementDetailId",
                        column: x => x.ContractorFinancialStatementDetailId,
                        principalSchema: "engineer",
                        principalTable: "ContractorFinancialStatementDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContractorFinancialStatementDailyOperations_DailyProjectOperations_DailyProjectOperationId",
                        column: x => x.DailyProjectOperationId,
                        principalSchema: "engineer",
                        principalTable: "DailyProjectOperations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DailyProjectOperationDocuments",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DailyProjectOperationId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyProjectOperationDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailyProjectOperationDocuments_DailyProjectOperations_DailyProjectOperationId",
                        column: x => x.DailyProjectOperationId,
                        principalSchema: "engineer",
                        principalTable: "DailyProjectOperations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectOperationDetailCommentDocuments",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectOperationDetailCommentId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectOperationDetailCommentDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectOperationDetailCommentDocuments_ProjectOperationDetailCmts_ProjectOperationDetailCommentId",
                        column: x => x.ProjectOperationDetailCommentId,
                        principalSchema: "engineer",
                        principalTable: "ProjectOperationDetailCmts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DailyProjectOperationExperts",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    ThirdPartyId = table.Column<long>(type: "bigint", nullable: false),
                    FinalValue = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    UnusedValue = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    DailyProjectOperationId = table.Column<long>(type: "bigint", nullable: false),
                    ConsumableVolumeExpertId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyProjectOperationExperts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailyProjectOperationExperts_DailyProjectOperations_DailyProjectOperationId",
                        column: x => x.DailyProjectOperationId,
                        principalSchema: "engineer",
                        principalTable: "DailyProjectOperations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DailyProjectOperationExperts_ProjectOperationDetailConsumableVolumeExperts_ConsumableVolumeExpertId",
                        column: x => x.ConsumableVolumeExpertId,
                        principalSchema: "engineer",
                        principalTable: "ProjectOperationDetailConsumableVolumeExperts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DailyProjectOperationMachineries",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    RequestMachineryId = table.Column<long>(type: "bigint", nullable: false),
                    FinalValue = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    UnusedValue = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    DailyProjectOperationId = table.Column<long>(type: "bigint", nullable: false),
                    ConsumableVolumeMachineryId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyProjectOperationMachineries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailyProjectOperationMachineries_DailyProjectOperations_DailyProjectOperationId",
                        column: x => x.DailyProjectOperationId,
                        principalSchema: "engineer",
                        principalTable: "DailyProjectOperations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DailyProjectOperationMachineries_ProjectOperationDetailConsumableVolumeMachineries_ConsumableVolumeMachineryId",
                        column: x => x.ConsumableVolumeMachineryId,
                        principalSchema: "engineer",
                        principalTable: "ProjectOperationDetailConsumableVolumeMachineries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DailyProjectOperationMachineries_RequestMachineries_RequestMachineryId",
                        column: x => x.RequestMachineryId,
                        principalSchema: "engineer",
                        principalTable: "RequestMachineries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DailyProjectOperationProducts",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    FinalValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnusedValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DailyProjectOperationId = table.Column<long>(type: "bigint", nullable: false),
                    ConsumableVolumeProductId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyProjectOperationProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailyProjectOperationProducts_DailyProjectOperations_DailyProjectOperationId",
                        column: x => x.DailyProjectOperationId,
                        principalSchema: "engineer",
                        principalTable: "DailyProjectOperations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DailyProjectOperationProducts_ProjectOperationDetailConsumableVolumeProducts_ConsumableVolumeProductId",
                        column: x => x.ConsumableVolumeProductId,
                        principalSchema: "engineer",
                        principalTable: "ProjectOperationDetailConsumableVolumeProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContractorContractDetails",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    WorkLoad = table.Column<long>(type: "bigint", nullable: false),
                    ContractorContractId = table.Column<long>(type: "bigint", nullable: false),
                    ProjectOperationId = table.Column<long>(type: "bigint", nullable: true),
                    ProjectOperationDetailContractorServiceId = table.Column<long>(type: "bigint", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractorContractDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractorContractDetails_ContractorContracts_ContractorContractId",
                        column: x => x.ContractorContractId,
                        principalSchema: "engineer",
                        principalTable: "ContractorContracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContractorContractDetails_ProjectOperationDetailContractorServices_ProjectOperationDetailContractorServiceId",
                        column: x => x.ProjectOperationDetailContractorServiceId,
                        principalSchema: "engineer",
                        principalTable: "ProjectOperationDetailContractorServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContractorContractDetails_ProjectOperations_ProjectOperationId",
                        column: x => x.ProjectOperationId,
                        principalSchema: "engineer",
                        principalTable: "ProjectOperations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DailyProjectOperationServices",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    DailyProjectOperationId = table.Column<long>(type: "bigint", nullable: false),
                    ContractorServiceId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyProjectOperationServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailyProjectOperationServices_DailyProjectOperations_DailyProjectOperationId",
                        column: x => x.DailyProjectOperationId,
                        principalSchema: "engineer",
                        principalTable: "DailyProjectOperations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DailyProjectOperationServices_ProjectOperationDetailContractorServices_ContractorServiceId",
                        column: x => x.ContractorServiceId,
                        principalSchema: "engineer",
                        principalTable: "ProjectOperationDetailContractorServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RequestGoodsSupplyDetails",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    Importance = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    RequestedCount = table.Column<decimal>(type: "decimal(14,5)", nullable: false),
                    TotalSupplyCount = table.Column<decimal>(type: "decimal(14,5)", nullable: false),
                    TotalEstimatedCount = table.Column<decimal>(type: "decimal(14,5)", nullable: false),
                    DelivaryDeadLine = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CurrencyId = table.Column<long>(type: "bigint", nullable: true),
                    TotalPrice = table.Column<decimal>(type: "decimal(14,5)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1500)", nullable: true),
                    ConsumableVolumeProductId = table.Column<long>(type: "bigint", nullable: false),
                    RequestGoodsSupplyId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestGoodsSupplyDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestGoodsSupplyDetails_ProjectOperationDetailConsumableVolumeProducts_ConsumableVolumeProductId",
                        column: x => x.ConsumableVolumeProductId,
                        principalSchema: "engineer",
                        principalTable: "ProjectOperationDetailConsumableVolumeProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RequestGoodsSupplyDetails_RequestGoodsSupplies_RequestGoodsSupplyId",
                        column: x => x.RequestGoodsSupplyId,
                        principalSchema: "engineer",
                        principalTable: "RequestGoodsSupplies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RequestGoodsSupplyHistories",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestGoodsSupplyId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestGoodsSupplyHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestGoodsSupplyHistories_RequestGoodsSupplies_RequestGoodsSupplyId",
                        column: x => x.RequestGoodsSupplyId,
                        principalSchema: "engineer",
                        principalTable: "RequestGoodsSupplies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FiduciaryProductDetailManagementHistories",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    InvoiceId = table.Column<long>(type: "bigint", nullable: false),
                    WarehouseId = table.Column<long>(type: "bigint", nullable: false),
                    ConfirmedLoanCount = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    FiduciaryProductDetailManagementId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FiduciaryProductDetailManagementHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FiduciaryProductDetailManagementHistories_FiduciaryProductDetailManagements_FiduciaryProductDetailManagementId",
                        column: x => x.FiduciaryProductDetailManagementId,
                        principalSchema: "engineer",
                        principalTable: "FiduciaryProductDetailManagements",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FiduciaryProductDetailReturns",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    InvoiceId = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1500)", nullable: true),
                    ReturnCount = table.Column<int>(type: "int", nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LateDay = table.Column<int>(type: "int", nullable: false),
                    LateFine = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CurrencyId = table.Column<long>(type: "bigint", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    FiduciaryProductDetailManagementId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FiduciaryProductDetailReturns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FiduciaryProductDetailReturns_FiduciaryProductDetailManagements_FiduciaryProductDetailManagementId",
                        column: x => x.FiduciaryProductDetailManagementId,
                        principalSchema: "engineer",
                        principalTable: "FiduciaryProductDetailManagements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContractorFinancialStatementFiduciaryMachineries",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DailyProjectOperationMachineryId = table.Column<long>(type: "bigint", nullable: false),
                    ContractorFinancialStatementId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractorFinancialStatementFiduciaryMachineries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractorFinancialStatementFiduciaryMachineries_ContractorFinancialStatements_ContractorFinancialStatementId",
                        column: x => x.ContractorFinancialStatementId,
                        principalSchema: "engineer",
                        principalTable: "ContractorFinancialStatements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContractorFinancialStatementFiduciaryMachineries_DailyProjectOperationMachineries_DailyProjectOperationMachineryId",
                        column: x => x.DailyProjectOperationMachineryId,
                        principalSchema: "engineer",
                        principalTable: "DailyProjectOperationMachineries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContractorContractDetailPrices",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    ContractorContractDetailId = table.Column<long>(type: "bigint", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CurrencyId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractorContractDetailPrices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractorContractDetailPrices_ContractorContractDetails_ContractorContractDetailId",
                        column: x => x.ContractorContractDetailId,
                        principalSchema: "engineer",
                        principalTable: "ContractorContractDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContractorFinancialStatementProducts",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    ProductGroupId = table.Column<long>(type: "bigint", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ContractorFinancialStatementId = table.Column<long>(type: "bigint", nullable: false),
                    RequestGoodsSupplyDetailId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractorFinancialStatementProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractorFinancialStatementProducts_ContractorFinancialStatements_ContractorFinancialStatementId",
                        column: x => x.ContractorFinancialStatementId,
                        principalSchema: "engineer",
                        principalTable: "ContractorFinancialStatements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContractorFinancialStatementProducts_RequestGoodsSupplyDetails_RequestGoodsSupplyDetailId",
                        column: x => x.RequestGoodsSupplyDetailId,
                        principalSchema: "engineer",
                        principalTable: "RequestGoodsSupplyDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RequestGoodsSupplyDetailDocuments",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    Url = table.Column<string>(type: "nvarchar(1500)", nullable: false),
                    RequestGoodsSupplyDetailId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestGoodsSupplyDetailDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestGoodsSupplyDetailDocuments_RequestGoodsSupplyDetails_RequestGoodsSupplyDetailId",
                        column: x => x.RequestGoodsSupplyDetailId,
                        principalSchema: "engineer",
                        principalTable: "RequestGoodsSupplyDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RequestGoodsSupplyDetailHistories",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    Status = table.Column<int>(type: "int", nullable: false),
                    RequestedCount = table.Column<decimal>(type: "decimal(14,5)", nullable: false),
                    DelivaryDeadLine = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CurrencyId = table.Column<long>(type: "bigint", nullable: true),
                    TotalPrice = table.Column<decimal>(type: "decimal(14,5)", nullable: true),
                    RequestGoodsSupplyDetailId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestGoodsSupplyDetailHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestGoodsSupplyDetailHistories_RequestGoodsSupplyDetails_RequestGoodsSupplyDetailId",
                        column: x => x.RequestGoodsSupplyDetailId,
                        principalSchema: "engineer",
                        principalTable: "RequestGoodsSupplyDetails",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RequestGoodsSupplyDetailManagements",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    ProductId = table.Column<long>(type: "bigint", nullable: true),
                    WarehouseId = table.Column<long>(type: "bigint", nullable: true),
                    InvoiceId = table.Column<long>(type: "bigint", nullable: true),
                    RequestedCount = table.Column<decimal>(type: "decimal(14,5)", nullable: false),
                    AlternateId = table.Column<long>(type: "bigint", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    AssignmentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RequestGoodsSupplyDetailId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestGoodsSupplyDetailManagements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestGoodsSupplyDetailManagements_RequestGoodsSupplyDetails_RequestGoodsSupplyDetailId",
                        column: x => x.RequestGoodsSupplyDetailId,
                        principalSchema: "engineer",
                        principalTable: "RequestGoodsSupplyDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FiduciaryProductDetailReturnDocuments",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FiduciaryProductDetailReturnId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FiduciaryProductDetailReturnDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FiduciaryProductDetailReturnDocuments_FiduciaryProductDetailReturns_FiduciaryProductDetailReturnId",
                        column: x => x.FiduciaryProductDetailReturnId,
                        principalSchema: "engineer",
                        principalTable: "FiduciaryProductDetailReturns",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RequestRewards",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    OfferedPrice = table.Column<decimal>(type: "decimal(14,5)", nullable: false),
                    ConfirmedPrice = table.Column<decimal>(type: "decimal(14,5)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1500)", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    CurrencyId = table.Column<long>(type: "bigint", nullable: false),
                    CostCenterId = table.Column<long>(type: "bigint", nullable: true),
                    ProjectId = table.Column<long>(type: "bigint", nullable: true),
                    ProjectOperationId = table.Column<long>(type: "bigint", nullable: true),
                    ProjectOperationDetailId = table.Column<long>(type: "bigint", nullable: true),
                    FiduciaryProductDetailReturnId = table.Column<long>(type: "bigint", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestRewards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestRewards_CostCenters_CostCenterId",
                        column: x => x.CostCenterId,
                        principalSchema: "engineer",
                        principalTable: "CostCenters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RequestRewards_FiduciaryProductDetailReturns_FiduciaryProductDetailReturnId",
                        column: x => x.FiduciaryProductDetailReturnId,
                        principalSchema: "engineer",
                        principalTable: "FiduciaryProductDetailReturns",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RequestRewards_ProjectOperationDetails_ProjectOperationDetailId",
                        column: x => x.ProjectOperationDetailId,
                        principalSchema: "engineer",
                        principalTable: "ProjectOperationDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RequestRewards_ProjectOperations_ProjectOperationId",
                        column: x => x.ProjectOperationId,
                        principalSchema: "engineer",
                        principalTable: "ProjectOperations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RequestRewards_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "engineer",
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RequestGoodsSupplyDetailManagementHistories",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    WarehouseId = table.Column<long>(type: "bigint", nullable: true),
                    InvoiceId = table.Column<long>(type: "bigint", nullable: true),
                    RequestedCount = table.Column<decimal>(type: "decimal(14,5)", nullable: false),
                    AlternateId = table.Column<long>(type: "bigint", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    AssignmentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RequestGoodsSupplyManagementId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestGoodsSupplyDetailManagementHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestGoodsSupplyDetailManagementHistories_RequestGoodsSupplyDetailManagements_RequestGoodsSupplyManagementId",
                        column: x => x.RequestGoodsSupplyManagementId,
                        principalSchema: "engineer",
                        principalTable: "RequestGoodsSupplyDetailManagements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContractorFinancialStatementRewards",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    RequestRewardId = table.Column<long>(type: "bigint", nullable: false),
                    ContractorFinancialStatementId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractorFinancialStatementRewards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractorFinancialStatementRewards_ContractorFinancialStatements_ContractorFinancialStatementId",
                        column: x => x.ContractorFinancialStatementId,
                        principalSchema: "engineer",
                        principalTable: "ContractorFinancialStatements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContractorFinancialStatementRewards_RequestRewards_RequestRewardId",
                        column: x => x.RequestRewardId,
                        principalSchema: "engineer",
                        principalTable: "RequestRewards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RequestRewardDocuments",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    Url = table.Column<string>(type: "nvarchar(1500)", nullable: false),
                    RequestRewardId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestRewardDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestRewardDocuments_RequestRewards_RequestRewardId",
                        column: x => x.RequestRewardId,
                        principalSchema: "engineer",
                        principalTable: "RequestRewards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RequestRewardHistories",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    OfferedPrice = table.Column<decimal>(type: "decimal(14,5)", nullable: false),
                    ConfirmedPrice = table.Column<decimal>(type: "decimal(14,5)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1500)", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CurrencyId = table.Column<long>(type: "bigint", nullable: false),
                    RequestRewardId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestRewardHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestRewardHistories_RequestRewards_RequestRewardId",
                        column: x => x.RequestRewardId,
                        principalSchema: "engineer",
                        principalTable: "RequestRewards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RequestRewardProducts",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    CurrencyId = table.Column<long>(type: "bigint", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(14,5)", nullable: false),
                    RequestRewardId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestRewardProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestRewardProducts_RequestRewards_RequestRewardId",
                        column: x => x.RequestRewardId,
                        principalSchema: "engineer",
                        principalTable: "RequestRewards",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RequestRewardThirdParties",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    ThirdPartyId = table.Column<long>(type: "bigint", nullable: false),
                    RequestRewardId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestRewardThirdParties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestRewardThirdParties_RequestRewards_RequestRewardId",
                        column: x => x.RequestRewardId,
                        principalSchema: "engineer",
                        principalTable: "RequestRewards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConsiderationDependencies_ConsiderationId",
                schema: "engineer",
                table: "ConsiderationDependencies",
                column: "ConsiderationId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsiderationDependencies_OperationInfoId",
                schema: "engineer",
                table: "ConsiderationDependencies",
                column: "OperationInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractCostOverImpacts_ContractCostOverId",
                schema: "engineer",
                table: "ContractCostOverImpacts",
                column: "ContractCostOverId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractCostOverImpacts_CostOverId",
                schema: "engineer",
                table: "ContractCostOverImpacts",
                column: "CostOverId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractCostOvers_CostOverId",
                schema: "engineer",
                table: "ContractCostOvers",
                column: "CostOverId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractCostOvers_EmployerContractId",
                schema: "engineer",
                table: "ContractCostOvers",
                column: "EmployerContractId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractorContractDetailPrices_ContractorContractDetailId",
                schema: "engineer",
                table: "ContractorContractDetailPrices",
                column: "ContractorContractDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractorContractDetails_ContractorContractId",
                schema: "engineer",
                table: "ContractorContractDetails",
                column: "ContractorContractId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractorContractDetails_ProjectOperationDetailContractorServiceId",
                schema: "engineer",
                table: "ContractorContractDetails",
                column: "ProjectOperationDetailContractorServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractorContractDetails_ProjectOperationId",
                schema: "engineer",
                table: "ContractorContractDetails",
                column: "ProjectOperationId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractorContractDetailSkills_ContractorContractId",
                schema: "engineer",
                table: "ContractorContractDetailSkills",
                column: "ContractorContractId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractorContractDetailThirdParties_ContractorContractDetailSkillId",
                schema: "engineer",
                table: "ContractorContractDetailThirdParties",
                column: "ContractorContractDetailSkillId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractorContractHistories_ContractorContractId",
                schema: "engineer",
                table: "ContractorContractHistories",
                column: "ContractorContractId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractorFinancialStatementDailyOperations_ContractorFinancialStatementDetailId",
                schema: "engineer",
                table: "ContractorFinancialStatementDailyOperations",
                column: "ContractorFinancialStatementDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractorFinancialStatementDailyOperations_DailyProjectOperationId",
                schema: "engineer",
                table: "ContractorFinancialStatementDailyOperations",
                column: "DailyProjectOperationId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractorFinancialStatementDetails_ContractorFinancialStatementId",
                schema: "engineer",
                table: "ContractorFinancialStatementDetails",
                column: "ContractorFinancialStatementId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractorFinancialStatementDetails_LastContractorFinancialStatementId",
                schema: "engineer",
                table: "ContractorFinancialStatementDetails",
                column: "LastContractorFinancialStatementId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractorFinancialStatementDetails_ProjectOperationId",
                schema: "engineer",
                table: "ContractorFinancialStatementDetails",
                column: "ProjectOperationId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractorFinancialStatementFiduciaryMachineries_ContractorFinancialStatementId",
                schema: "engineer",
                table: "ContractorFinancialStatementFiduciaryMachineries",
                column: "ContractorFinancialStatementId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractorFinancialStatementFiduciaryMachineries_DailyProjectOperationMachineryId",
                schema: "engineer",
                table: "ContractorFinancialStatementFiduciaryMachineries",
                column: "DailyProjectOperationMachineryId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractorFinancialStatementFiduciaryProducts_ContractorFinancialStatementId",
                schema: "engineer",
                table: "ContractorFinancialStatementFiduciaryProducts",
                column: "ContractorFinancialStatementId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractorFinancialStatementFiduciaryProducts_FiduciaryProductDetailId",
                schema: "engineer",
                table: "ContractorFinancialStatementFiduciaryProducts",
                column: "FiduciaryProductDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractorFinancialStatementHistories_ContractorFinancialStatementId",
                schema: "engineer",
                table: "ContractorFinancialStatementHistories",
                column: "ContractorFinancialStatementId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractorFinancialStatementMachineries_ContractorFinancialStatementId",
                schema: "engineer",
                table: "ContractorFinancialStatementMachineries",
                column: "ContractorFinancialStatementId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractorFinancialStatementMachineries_MachineryId",
                schema: "engineer",
                table: "ContractorFinancialStatementMachineries",
                column: "MachineryId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractorFinancialStatementMachineries_ProjectOperationId",
                schema: "engineer",
                table: "ContractorFinancialStatementMachineries",
                column: "ProjectOperationId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractorFinancialStatementProducts_ContractorFinancialStatementId",
                schema: "engineer",
                table: "ContractorFinancialStatementProducts",
                column: "ContractorFinancialStatementId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractorFinancialStatementProducts_RequestGoodsSupplyDetailId",
                schema: "engineer",
                table: "ContractorFinancialStatementProducts",
                column: "RequestGoodsSupplyDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractorFinancialStatementRewards_ContractorFinancialStatementId",
                schema: "engineer",
                table: "ContractorFinancialStatementRewards",
                column: "ContractorFinancialStatementId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractorFinancialStatementRewards_RequestRewardId",
                schema: "engineer",
                table: "ContractorFinancialStatementRewards",
                column: "RequestRewardId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractorFinancialStatements_ContractorContractId",
                schema: "engineer",
                table: "ContractorFinancialStatements",
                column: "ContractorContractId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractorFinancialStatements_CostCenterId",
                schema: "engineer",
                table: "ContractorFinancialStatements",
                column: "CostCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractorFinancialStatements_LastContractorFinancialStatementId",
                schema: "engineer",
                table: "ContractorFinancialStatements",
                column: "LastContractorFinancialStatementId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractorFinancialStatements_ProjectId",
                schema: "engineer",
                table: "ContractorFinancialStatements",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractorFinancialStatementThirdParties_ContractorFinancialStatementId",
                schema: "engineer",
                table: "ContractorFinancialStatementThirdParties",
                column: "ContractorFinancialStatementId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractorFinancialStatementTransportations_ContractorFinancialStatementId",
                schema: "engineer",
                table: "ContractorFinancialStatementTransportations",
                column: "ContractorFinancialStatementId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractorServices_ServiceInfoId",
                schema: "engineer",
                table: "ContractorServices",
                column: "ServiceInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_CostCenterAuthorizedRoles_CostCenterId",
                schema: "engineer",
                table: "CostCenterAuthorizedRoles",
                column: "CostCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_CostCenterAuthorizedUsers_CostCenterId",
                schema: "engineer",
                table: "CostCenterAuthorizedUsers",
                column: "CostCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_CostCenters_CostCenterTypesId",
                schema: "engineer",
                table: "CostCenters",
                column: "CostCenterTypesId");

            migrationBuilder.CreateIndex(
                name: "IX_CostCenterVirtualGroupAdmins_CostCenterVirtualGroupId",
                schema: "engineer",
                table: "CostCenterVirtualGroupAdmins",
                column: "CostCenterVirtualGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_CostCenterVirtualGroups_CostCenterId",
                schema: "engineer",
                table: "CostCenterVirtualGroups",
                column: "CostCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_CostCenterWarehouses_CostCenterId",
                schema: "engineer",
                table: "CostCenterWarehouses",
                column: "CostCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyProjectOperationDocuments_DailyProjectOperationId",
                schema: "engineer",
                table: "DailyProjectOperationDocuments",
                column: "DailyProjectOperationId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyProjectOperationExperts_ConsumableVolumeExpertId",
                schema: "engineer",
                table: "DailyProjectOperationExperts",
                column: "ConsumableVolumeExpertId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyProjectOperationExperts_DailyProjectOperationId",
                schema: "engineer",
                table: "DailyProjectOperationExperts",
                column: "DailyProjectOperationId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyProjectOperationMachineries_ConsumableVolumeMachineryId",
                schema: "engineer",
                table: "DailyProjectOperationMachineries",
                column: "ConsumableVolumeMachineryId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyProjectOperationMachineries_DailyProjectOperationId",
                schema: "engineer",
                table: "DailyProjectOperationMachineries",
                column: "DailyProjectOperationId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyProjectOperationMachineries_RequestMachineryId",
                schema: "engineer",
                table: "DailyProjectOperationMachineries",
                column: "RequestMachineryId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyProjectOperationProducts_ConsumableVolumeProductId",
                schema: "engineer",
                table: "DailyProjectOperationProducts",
                column: "ConsumableVolumeProductId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyProjectOperationProducts_DailyProjectOperationId",
                schema: "engineer",
                table: "DailyProjectOperationProducts",
                column: "DailyProjectOperationId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyProjectOperations_ProjectOperationDetailId",
                schema: "engineer",
                table: "DailyProjectOperations",
                column: "ProjectOperationDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyProjectOperationServices_ContractorServiceId",
                schema: "engineer",
                table: "DailyProjectOperationServices",
                column: "ContractorServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyProjectOperationServices_DailyProjectOperationId",
                schema: "engineer",
                table: "DailyProjectOperationServices",
                column: "DailyProjectOperationId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentDetails_EmployerContractId",
                schema: "engineer",
                table: "DocumentDetails",
                column: "EmployerContractId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentDetailUrls_DocumentDetailId",
                schema: "engineer",
                table: "DocumentDetailUrls",
                column: "DocumentDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployerConsiderations_EmployerContractId",
                schema: "engineer",
                table: "EmployerConsiderations",
                column: "EmployerContractId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployerContracts_ProjectId",
                schema: "engineer",
                table: "EmployerContracts",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployerStatusStatementProjectOperationDetails_EmployerStatusStatementProjectOperationId",
                schema: "engineer",
                table: "EmployerStatusStatementProjectOperationDetails",
                column: "EmployerStatusStatementProjectOperationId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployerStatusStatementProjectOperationDetails_ProjectOperationDetailId",
                schema: "engineer",
                table: "EmployerStatusStatementProjectOperationDetails",
                column: "ProjectOperationDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployerStatusStatementProjectOperations_EmployerStatusStatementId",
                schema: "engineer",
                table: "EmployerStatusStatementProjectOperations",
                column: "EmployerStatusStatementId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployerStatusStatementProjectOperations_ProjectOperationId",
                schema: "engineer",
                table: "EmployerStatusStatementProjectOperations",
                column: "ProjectOperationId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployerStatusStatements_EmployerContractId",
                schema: "engineer",
                table: "EmployerStatusStatements",
                column: "EmployerContractId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployerStatusStatements_ProjectId",
                schema: "engineer",
                table: "EmployerStatusStatements",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_EngineeringBranchs_CategoryId",
                schema: "engineer",
                table: "EngineeringBranchs",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_EngineeringSeasons_BranchId",
                schema: "engineer",
                table: "EngineeringSeasons",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_EngineeringStandardExperts_OperationInfoId",
                schema: "engineer",
                table: "EngineeringStandardExperts",
                column: "OperationInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_EngineeringStandardMachineries_MachineryId",
                schema: "engineer",
                table: "EngineeringStandardMachineries",
                column: "MachineryId");

            migrationBuilder.CreateIndex(
                name: "IX_EngineeringStandardMachineries_OperationInfoId",
                schema: "engineer",
                table: "EngineeringStandardMachineries",
                column: "OperationInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_EngineeringStandardProducts_OperationInfoId",
                schema: "engineer",
                table: "EngineeringStandardProducts",
                column: "OperationInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_FiduciaryProductDetailHistories_FiduciaryProductDetailId",
                schema: "engineer",
                table: "FiduciaryProductDetailHistories",
                column: "FiduciaryProductDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_FiduciaryProductDetailManagementHistories_FiduciaryProductDetailManagementId",
                schema: "engineer",
                table: "FiduciaryProductDetailManagementHistories",
                column: "FiduciaryProductDetailManagementId");

            migrationBuilder.CreateIndex(
                name: "IX_FiduciaryProductDetailManagements_FiduciaryProductDetailId",
                schema: "engineer",
                table: "FiduciaryProductDetailManagements",
                column: "FiduciaryProductDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_FiduciaryProductDetailReturnDocuments_FiduciaryProductDetailReturnId",
                schema: "engineer",
                table: "FiduciaryProductDetailReturnDocuments",
                column: "FiduciaryProductDetailReturnId");

            migrationBuilder.CreateIndex(
                name: "IX_FiduciaryProductDetailReturns_FiduciaryProductDetailManagementId",
                schema: "engineer",
                table: "FiduciaryProductDetailReturns",
                column: "FiduciaryProductDetailManagementId");

            migrationBuilder.CreateIndex(
                name: "IX_FiduciaryProductDetails_FiduciaryProductId",
                schema: "engineer",
                table: "FiduciaryProductDetails",
                column: "FiduciaryProductId");

            migrationBuilder.CreateIndex(
                name: "IX_FiduciaryProductHistories_CostCenterId",
                schema: "engineer",
                table: "FiduciaryProductHistories",
                column: "CostCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_FiduciaryProductHistories_FiduciaryProductId",
                schema: "engineer",
                table: "FiduciaryProductHistories",
                column: "FiduciaryProductId");

            migrationBuilder.CreateIndex(
                name: "IX_FiduciaryProductHistories_ProjectId",
                schema: "engineer",
                table: "FiduciaryProductHistories",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_FiduciaryProductHistories_ProjectOperationId",
                schema: "engineer",
                table: "FiduciaryProductHistories",
                column: "ProjectOperationId");

            migrationBuilder.CreateIndex(
                name: "IX_FiduciaryProducts_CostCenterId",
                schema: "engineer",
                table: "FiduciaryProducts",
                column: "CostCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_FiduciaryProducts_ProjectId",
                schema: "engineer",
                table: "FiduciaryProducts",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_FiduciaryProducts_ProjectOperationId",
                schema: "engineer",
                table: "FiduciaryProducts",
                column: "ProjectOperationId");

            migrationBuilder.CreateIndex(
                name: "IX_ImplementationAssistants_ProjectId",
                schema: "engineer",
                table: "ImplementationAssistants",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_InformedUsers_CostCenterId",
                schema: "engineer",
                table: "InformedUsers",
                column: "CostCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_Machineries_GroupId",
                schema: "engineer",
                table: "Machineries",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Machines_MachineTypesId",
                schema: "engineer",
                table: "Machines",
                column: "MachineTypesId");

            migrationBuilder.CreateIndex(
                name: "IX_OperationInfoDependencies_OperationInfoId",
                schema: "engineer",
                table: "OperationInfoDependencies",
                column: "OperationInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_OperationInfoSeasons_OperationInfoId",
                schema: "engineer",
                table: "OperationInfoSeasons",
                column: "OperationInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_OperationInfoSeasons_SeasonId",
                schema: "engineer",
                table: "OperationInfoSeasons",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_OperationInfoServices_OperationInfoId",
                schema: "engineer",
                table: "OperationInfoServices",
                column: "OperationInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_OperationInfoServices_ServiceInfoId",
                schema: "engineer",
                table: "OperationInfoServices",
                column: "ServiceInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_OperationLocations_CostCenterId",
                schema: "engineer",
                table: "OperationLocations",
                column: "CostCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_OperationLocations_ParentId",
                schema: "engineer",
                table: "OperationLocations",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectOperationDependencies_ProjectOperationId",
                schema: "engineer",
                table: "ProjectOperationDependencies",
                column: "ProjectOperationId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectOperationDetailCommentDocuments_ProjectOperationDetailCommentId",
                schema: "engineer",
                table: "ProjectOperationDetailCommentDocuments",
                column: "ProjectOperationDetailCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectOperationDetailCmts_ParentId",
                schema: "engineer",
                table: "ProjectOperationDetailCmts",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectOperationDetailCmts_ProjectOperationDetailId",
                schema: "engineer",
                table: "ProjectOperationDetailCmts",
                column: "ProjectOperationDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectOperationDetailConsumableVolumeExperts_ProjectOperationDetailId",
                schema: "engineer",
                table: "ProjectOperationDetailConsumableVolumeExperts",
                column: "ProjectOperationDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectOperationDetailConsumableVolumeMachineries_MachineryId",
                schema: "engineer",
                table: "ProjectOperationDetailConsumableVolumeMachineries",
                column: "MachineryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectOperationDetailConsumableVolumeMachineries_ProjectOperationDetailId",
                schema: "engineer",
                table: "ProjectOperationDetailConsumableVolumeMachineries",
                column: "ProjectOperationDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectOperationDetailConsumableVolumeProducts_ProjectOperationDetailId",
                schema: "engineer",
                table: "ProjectOperationDetailConsumableVolumeProducts",
                column: "ProjectOperationDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectOperationDetailContractorServices_OperationInfoServiceId",
                schema: "engineer",
                table: "ProjectOperationDetailContractorServices",
                column: "OperationInfoServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectOperationDetailContractorServices_ProjectOperationDetailId",
                schema: "engineer",
                table: "ProjectOperationDetailContractorServices",
                column: "ProjectOperationDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectOperationDetails_OperationLocationId",
                schema: "engineer",
                table: "ProjectOperationDetails",
                column: "OperationLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectOperationDetails_ProjectOperationId",
                schema: "engineer",
                table: "ProjectOperationDetails",
                column: "ProjectOperationId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectOperationDetailUserImplementations_ProjectOperationDetailId",
                schema: "engineer",
                table: "ProjectOperationDetailUserImplementations",
                column: "ProjectOperationDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectOperationDetailUserPlaners_ProjectOperationDetailId",
                schema: "engineer",
                table: "ProjectOperationDetailUserPlaners",
                column: "ProjectOperationDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectOperationDetailUserTechnicals_ProjectOperationDetailId",
                schema: "engineer",
                table: "ProjectOperationDetailUserTechnicals",
                column: "ProjectOperationDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectOperations_EmployerContractId",
                schema: "engineer",
                table: "ProjectOperations",
                column: "EmployerContractId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectOperations_OperationInfoId",
                schema: "engineer",
                table: "ProjectOperations",
                column: "OperationInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectOperations_ProjectId",
                schema: "engineer",
                table: "ProjectOperations",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_CategoryId",
                schema: "engineer",
                table: "Projects",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_CostCenterId",
                schema: "engineer",
                table: "Projects",
                column: "CostCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ProjectTypesId",
                schema: "engineer",
                table: "Projects",
                column: "ProjectTypesId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestGoodsSupplies_ProjectOperationDetailId",
                schema: "engineer",
                table: "RequestGoodsSupplies",
                column: "ProjectOperationDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestGoodsSupplies_ProjectOperationId",
                schema: "engineer",
                table: "RequestGoodsSupplies",
                column: "ProjectOperationId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestGoodsSupplyDetailDocuments_RequestGoodsSupplyDetailId",
                schema: "engineer",
                table: "RequestGoodsSupplyDetailDocuments",
                column: "RequestGoodsSupplyDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestGoodsSupplyDetailHistories_RequestGoodsSupplyDetailId",
                schema: "engineer",
                table: "RequestGoodsSupplyDetailHistories",
                column: "RequestGoodsSupplyDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestGoodsSupplyDetailManagementHistories_RequestGoodsSupplyManagementId",
                schema: "engineer",
                table: "RequestGoodsSupplyDetailManagementHistories",
                column: "RequestGoodsSupplyManagementId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestGoodsSupplyDetailManagements_RequestGoodsSupplyDetailId",
                schema: "engineer",
                table: "RequestGoodsSupplyDetailManagements",
                column: "RequestGoodsSupplyDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestGoodsSupplyDetails_ConsumableVolumeProductId",
                schema: "engineer",
                table: "RequestGoodsSupplyDetails",
                column: "ConsumableVolumeProductId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestGoodsSupplyDetails_RequestGoodsSupplyId",
                schema: "engineer",
                table: "RequestGoodsSupplyDetails",
                column: "RequestGoodsSupplyId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestGoodsSupplyHistories_RequestGoodsSupplyId",
                schema: "engineer",
                table: "RequestGoodsSupplyHistories",
                column: "RequestGoodsSupplyId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestMachineries_MachineryId",
                schema: "engineer",
                table: "RequestMachineries",
                column: "MachineryId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestMachineries_ProjectId",
                schema: "engineer",
                table: "RequestMachineries",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestMachineryAssignments_RequestMachineryId",
                schema: "engineer",
                table: "RequestMachineryAssignments",
                column: "RequestMachineryId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestMachineryHistories_RequestMachineryId",
                schema: "engineer",
                table: "RequestMachineryHistories",
                column: "RequestMachineryId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestMachineryInquiries_RequestMachineryInquiryOperatorId",
                schema: "engineer",
                table: "RequestMachineryInquiries",
                column: "RequestMachineryInquiryOperatorId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestMachineryInquiryDocument_RequestMachineryInquiryId",
                schema: "engineer",
                table: "RequestMachineryInquiryDocument",
                column: "RequestMachineryInquiryId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestMachineryInquiryOperators_RequestMachineryId",
                schema: "engineer",
                table: "RequestMachineryInquiryOperators",
                column: "RequestMachineryId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestMachineryProjectOperationDetails_ProjectOperationDetailId",
                schema: "engineer",
                table: "RequestMachineryProjectOperationDetails",
                column: "ProjectOperationDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestMachineryProjectOperationDetails_RequestMachineryId",
                schema: "engineer",
                table: "RequestMachineryProjectOperationDetails",
                column: "RequestMachineryId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestMachineryProjectOperations_ProjectOperationId",
                schema: "engineer",
                table: "RequestMachineryProjectOperations",
                column: "ProjectOperationId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestMachineryProjectOperations_RequestMachineryId",
                schema: "engineer",
                table: "RequestMachineryProjectOperations",
                column: "RequestMachineryId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestRewardDocuments_RequestRewardId",
                schema: "engineer",
                table: "RequestRewardDocuments",
                column: "RequestRewardId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestRewardHistories_RequestRewardId",
                schema: "engineer",
                table: "RequestRewardHistories",
                column: "RequestRewardId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestRewardProducts_RequestRewardId",
                schema: "engineer",
                table: "RequestRewardProducts",
                column: "RequestRewardId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestRewards_CostCenterId",
                schema: "engineer",
                table: "RequestRewards",
                column: "CostCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestRewards_FiduciaryProductDetailReturnId",
                schema: "engineer",
                table: "RequestRewards",
                column: "FiduciaryProductDetailReturnId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestRewards_ProjectId",
                schema: "engineer",
                table: "RequestRewards",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestRewards_ProjectOperationDetailId",
                schema: "engineer",
                table: "RequestRewards",
                column: "ProjectOperationDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestRewards_ProjectOperationId",
                schema: "engineer",
                table: "RequestRewards",
                column: "ProjectOperationId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestRewardThirdParties_RequestRewardId",
                schema: "engineer",
                table: "RequestRewardThirdParties",
                column: "RequestRewardId");

            migrationBuilder.CreateIndex(
                name: "IX_TechnicalAssistants_ProjectId",
                schema: "engineer",
                table: "TechnicalAssistants",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TransportationRequestProjectOperationDetails_ProjectOperationDetailId",
                schema: "engineer",
                table: "TransportationRequestProjectOperationDetails",
                column: "ProjectOperationDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_TransportationRequestProjectOperationDetails_TransportationRequestId",
                schema: "engineer",
                table: "TransportationRequestProjectOperationDetails",
                column: "TransportationRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_TransportationRequestProjectOperations_ProjectOperationId",
                schema: "engineer",
                table: "TransportationRequestProjectOperations",
                column: "ProjectOperationId");

            migrationBuilder.CreateIndex(
                name: "IX_TransportationRequestProjectOperations_TransportationRequestId",
                schema: "engineer",
                table: "TransportationRequestProjectOperations",
                column: "TransportationRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_TransportationRequests_BillOfLadingId",
                schema: "engineer",
                table: "TransportationRequests",
                column: "BillOfLadingId");

            migrationBuilder.CreateIndex(
                name: "IX_TransportationRequests_MachineId",
                schema: "engineer",
                table: "TransportationRequests",
                column: "MachineId");

            migrationBuilder.CreateIndex(
                name: "IX_TransportationRequests_ProjectId",
                schema: "engineer",
                table: "TransportationRequests",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TransportationRequests_TransportationId",
                schema: "engineer",
                table: "TransportationRequests",
                column: "TransportationId");

            migrationBuilder.CreateIndex(
                name: "IX_TransportationRequests_TripId",
                schema: "engineer",
                table: "TransportationRequests",
                column: "TripId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConsiderationDependencies",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "ContractCostOverImpacts",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "ContractorContractDetailPrices",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "ContractorContractDetailThirdParties",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "ContractorContractHistories",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "ContractorEmployees",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "ContractorFinancialStatementDailyOperations",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "ContractorFinancialStatementFiduciaryMachineries",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "ContractorFinancialStatementFiduciaryProducts",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "ContractorFinancialStatementHistories",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "ContractorFinancialStatementMachineries",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "ContractorFinancialStatementProducts",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "ContractorFinancialStatementRewards",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "ContractorFinancialStatementThirdParties",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "ContractorFinancialStatementTransportations",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "ContractorServices",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "CostCenterAuthorizedRoles",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "CostCenterAuthorizedUsers",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "CostCenterVirtualGroupAdmins",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "CostCenterWarehouses",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "DailyProjectOperationDocuments",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "DailyProjectOperationExperts",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "DailyProjectOperationProducts",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "DailyProjectOperationServices",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "DocumentDetailUrls",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "EmployerStatusStatementProjectOperationDetails",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "EngineeringStandardExperts",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "EngineeringStandardMachineries",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "EngineeringStandardProducts",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "FiduciaryProductDetailHistories",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "FiduciaryProductDetailManagementHistories",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "FiduciaryProductDetailReturnDocuments",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "FiduciaryProductHistories",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "ImplementationAssistants",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "InformedUsers",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "OperationInfoDependencies",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "OperationInfoSeasons",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "ProjectOperationDependencies",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "ProjectOperationDetailCommentDocuments",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "ProjectOperationDetailUserImplementations",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "ProjectOperationDetailUserPlaners",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "ProjectOperationDetailUserTechnicals",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "RequestGoodsSupplyDetailDocuments",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "RequestGoodsSupplyDetailHistories",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "RequestGoodsSupplyDetailManagementHistories",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "RequestGoodsSupplyHistories",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "RequestMachineryAssignments",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "RequestMachineryHistories",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "RequestMachineryInquiryDocument",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "RequestMachineryProjectOperationDetails",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "RequestMachineryProjectOperations",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "RequestRewardDocuments",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "RequestRewardHistories",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "RequestRewardProducts",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "RequestRewardThirdParties",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "TechnicalAssistants",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "TransportationRequestProjectOperationDetails",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "TransportationRequestProjectOperations",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "EmployerConsiderations",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "ContractCostOvers",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "ContractorContractDetails",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "ContractorContractDetailSkills",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "ContractorFinancialStatementDetails",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "DailyProjectOperationMachineries",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "CostCenterVirtualGroups",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "ProjectOperationDetailConsumableVolumeExperts",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "DocumentDetails",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "EmployerStatusStatementProjectOperations",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "EngineeringSeasons",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "ProjectOperationDetailCmts",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "RequestGoodsSupplyDetailManagements",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "RequestMachineryInquiries",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "RequestRewards",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "TransportationRequests",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "CostOvers",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "ProjectOperationDetailContractorServices",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "ContractorFinancialStatements",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "DailyProjectOperations",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "ProjectOperationDetailConsumableVolumeMachineries",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "EmployerStatusStatements",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "EngineeringBranchs",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "RequestGoodsSupplyDetails",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "RequestMachineryInquiryOperators",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "FiduciaryProductDetailReturns",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "BillOfLadings",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "Machines",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "Transportations",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "Trips",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "OperationInfoServices",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "ContractorContracts",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "ProjectOperationDetailConsumableVolumeProducts",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "RequestGoodsSupplies",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "RequestMachineries",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "FiduciaryProductDetailManagements",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "MachineTypes",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "EngineeringServices",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "ProjectOperationDetails",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "Machineries",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "FiduciaryProductDetails",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "OperationLocations",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "MachineriesGroup",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "FiduciaryProducts",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "ProjectOperations",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "EmployerContracts",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "OperationInfos",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "Projects",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "CostCenters",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "EngineeringCategories",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "EngineeringProjectTypes",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "CostCenterTypes",
                schema: "engineer");
        }
    }
}
