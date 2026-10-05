using Microsoft.EntityFrameworkCore.Migrations;

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddMultipleSynonyms : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE SYNONYM [engineer].[ViewGroup] FOR [WarehouseDb].[dbo].[Group];
                CREATE SYNONYM [engineer].[ViewCategory] FOR [WarehouseDb].[dbo].[Categories];
                CREATE SYNONYM [engineer].[ViewDocument] FOR [WarehouseDb].[dbo].[Documents];
                CREATE SYNONYM [engineer].[ViewDocumentGroup] FOR [WarehouseDb].[dbo].[DocumentGroups];
                CREATE SYNONYM [engineer].[ViewDocumentProduct] FOR [WarehouseDb].[dbo].[DocumentProducts];
                CREATE SYNONYM [engineer].[ViewWarehouseAsset] FOR [WarehouseDb].[dbo].[WarehouseAssets];
                CREATE SYNONYM [engineer].[ViewBrand] FOR [MetaDataDb].[meta].[Brands];
                CREATE SYNONYM [engineer].[ViewBrandModel] FOR [MetaDataDb].[meta].[BrandModels];
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                IF OBJECT_ID('[engineer].[ViewGroup]', 'SN') IS NOT NULL
                    DROP SYNONYM [engineer].[ViewGroup];

                IF OBJECT_ID('[engineer].[ViewCategory]', 'SN') IS NOT NULL
                    DROP SYNONYM [engineer].[ViewCategory];

                IF OBJECT_ID('[engineer].[ViewDocumentGroup]', 'SN') IS NOT NULL
                    DROP SYNONYM [engineer].[ViewDocumentGroup];

                IF OBJECT_ID('[engineer].[ViewDocumentProduct]', 'SN') IS NOT NULL
                    DROP SYNONYM [engineer].[ViewDocumentProduct];

                IF OBJECT_ID('[engineer].[ViewDocument]', 'SN') IS NOT NULL
                    DROP SYNONYM [engineer].[ViewDocument];

                IF OBJECT_ID('[engineer].[ViewWarehouseAsset]', 'SN') IS NOT NULL
                    DROP SYNONYM [engineer].[ViewWarehouseAsset];

                IF OBJECT_ID('[engineer].[ViewBrand]', 'SN') IS NOT NULL
                    DROP SYNONYM [engineer].[ViewBrand];

                IF OBJECT_ID('[engineer].[ViewBrandModel]', 'SN') IS NOT NULL
                    DROP SYNONYM [engineer].[ViewBrandModel];
            ");
        }
    }
}