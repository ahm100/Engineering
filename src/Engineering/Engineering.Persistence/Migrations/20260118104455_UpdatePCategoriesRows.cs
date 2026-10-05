using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePCategoriesRows : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO EngineeringDB.engineer.ProjectCategories
                    (ProjectId, CategoryId, Created, CreatorId, Updated, UpdaterId, IsDeleted)
                SELECT 
                    p.Id,
                    p.CategoryId,
                    p.Created,   -- Created date/time
                    p.CreatorId, -- CreatorId = p.CreatorId
                    p.Updated,   -- Updated = p.Updated
                    p.UpdaterId, -- UpdaterId = p.UpdaterId
                    p.IsDeleted  -- IsDeleted = p.IsDeleted
                FROM EngineeringDB.engineer.Projects p
                LEFT JOIN EngineeringDB.engineer.ProjectCategories pc ON pc.ProjectId = p.Id AND pc.CategoryId = p.CategoryId
                WHERE pc.Id IS NULL  
                  AND p.CategoryId IS NOT NULL 
                  AND p.IsDeleted != 1;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
