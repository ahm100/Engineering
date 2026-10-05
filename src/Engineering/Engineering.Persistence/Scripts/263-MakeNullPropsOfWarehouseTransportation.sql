BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[TransportationRequestWarehouses]') AND [c].[name] = N'WarehouseId');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[TransportationRequestWarehouses] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [engineer].[TransportationRequestWarehouses] ALTER COLUMN [WarehouseId] bigint NULL;
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[TransportationRequestWarehouses]') AND [c].[name] = N'Price');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[TransportationRequestWarehouses] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [engineer].[TransportationRequestWarehouses] ALTER COLUMN [Price] decimal(18,2) NULL;
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[TransportationRequestWarehouses]') AND [c].[name] = N'PackingProductId');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[TransportationRequestWarehouses] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [engineer].[TransportationRequestWarehouses] ALTER COLUMN [PackingProductId] bigint NULL;
GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[TransportationRequestWarehouses]') AND [c].[name] = N'PackingNumber');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[TransportationRequestWarehouses] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [engineer].[TransportationRequestWarehouses] ALTER COLUMN [PackingNumber] bigint NULL;
GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[TransportationRequestWarehouses]') AND [c].[name] = N'PackingId');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[TransportationRequestWarehouses] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [engineer].[TransportationRequestWarehouses] ALTER COLUMN [PackingId] bigint NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251215075046_MakeNullPropsOfWarehouseTransportation', N'8.0.8');
GO

COMMIT;
GO



