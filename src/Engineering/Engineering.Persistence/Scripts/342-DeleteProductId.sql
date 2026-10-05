BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[RequestGoodsSupplyDetailManagements]') AND [c].[name] = N'ProductId');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[RequestGoodsSupplyDetailManagements] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [engineer].[RequestGoodsSupplyDetailManagements] DROP COLUMN [ProductId];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260802053755_DeleteProductId', N'8.0.8');
GO

COMMIT;
GO



