BEGIN TRANSACTION;
GO

CREATE SYNONYM [engineer].[ViewGroup] FOR [WarehouseDb].[dbo].[Groups]
CREATE SYNONYM [engineer].[ViewCategory] FOR [WarehouseDb].[dbo].[Categories]
CREATE SYNONYM [engineer].[ViewDocumentGroup] FOR [WarehouseDb].[dbo].[DocumentGroups]
CREATE SYNONYM [engineer].[ViewDocumentProduct] FOR [WarehouseDb].[dbo].[DocumentProducts]
CREATE SYNONYM [engineer].[ViewDocument] FOR [WarehouseDb].[dbo].[Documents]
CREATE SYNONYM [engineer].[ViewWarehouseAsset] FOR [WarehouseDb].[dbo].[WarehouseAssets]
CREATE SYNONYM [engineer].[ViewBrand] FOR [MetaDataDb].[meta].[Brands]
CREATE SYNONYM [engineer].[ViewBrandModel] FOR [MetaDataDb].[meta].[BrandModels]
INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260225091426_AddMultipleSynonyms', N'8.0.8');
GO

COMMIT;
GO



