using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class changeFkMachineType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.Sql($@"
                select c.Id,c.CabinTypeCode into #cabinTemp from engineer.MachineTypes m
                inner join engineer.CabinTypes c on m.CabinTypeCode = c.CabinTypeCode
                                  ");

            migrationBuilder.DropForeignKey(
                name: "FK_MachineTypes_CabinTypes_CabinTypeCode",
                schema: "engineer",
                table: "MachineTypes");

            migrationBuilder.DropIndex(
                name: "IX_MachineTypes_CabinTypeCode",
                schema: "engineer",
                table: "MachineTypes");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_CabinTypes_CabinTypeCode",
                schema: "engineer",
                table: "CabinTypes");

            migrationBuilder.AddColumn<long>(
                name: "CabinTypeId",
                schema: "engineer",
                table: "MachineTypes",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.Sql($@"
                update m set CabinTypeId = t.Id
                from engineer.MachineTypes m
                inner join #cabinTemp t on t.CabinTypeCode = m.CabinTypeCode
                                  ");

            migrationBuilder.DropColumn(
                name: "CabinTypeCode",
                schema: "engineer",
                table: "MachineTypes");

            migrationBuilder.CreateIndex(
                name: "IX_MachineTypes_CabinTypeId",
                schema: "engineer",
                table: "MachineTypes",
                column: "CabinTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_MachineTypes_CabinTypes_CabinTypeId",
                schema: "engineer",
                table: "MachineTypes",
                column: "CabinTypeId",
                principalSchema: "engineer",
                principalTable: "CabinTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MachineTypes_CabinTypes_CabinTypeId",
                schema: "engineer",
                table: "MachineTypes");

            migrationBuilder.DropIndex(
                name: "IX_MachineTypes_CabinTypeId",
                schema: "engineer",
                table: "MachineTypes");

            migrationBuilder.DropColumn(
                name: "CabinTypeId",
                schema: "engineer",
                table: "MachineTypes");

            migrationBuilder.AddColumn<int>(
                name: "CabinTypeCode",
                schema: "engineer",
                table: "MachineTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_CabinTypes_CabinTypeCode",
                schema: "engineer",
                table: "CabinTypes",
                column: "CabinTypeCode");

            migrationBuilder.CreateIndex(
                name: "IX_MachineTypes_CabinTypeCode",
                schema: "engineer",
                table: "MachineTypes",
                column: "CabinTypeCode");

            migrationBuilder.AddForeignKey(
                name: "FK_MachineTypes_CabinTypes_CabinTypeCode",
                schema: "engineer",
                table: "MachineTypes",
                column: "CabinTypeCode",
                principalSchema: "engineer",
                principalTable: "CabinTypes",
                principalColumn: "CabinTypeCode");
        }
    }
}
