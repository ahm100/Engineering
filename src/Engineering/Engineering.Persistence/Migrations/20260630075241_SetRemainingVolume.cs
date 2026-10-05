using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SetRemainingVolume : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                UPDATE engineer.ProjectOperationDetailContractorServices
                SET RemainingVolume = Volume");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
