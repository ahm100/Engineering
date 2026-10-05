BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[ProjectCostCenterRequests]') AND [c].[name] = N'RequestedCostCenterName');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[ProjectCostCenterRequests] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [engineer].[ProjectCostCenterRequests] ALTER COLUMN [RequestedCostCenterName] nvarchar(250) NULL;
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[ProjectCostCenterRequests]') AND [c].[name] = N'Description');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[ProjectCostCenterRequests] DROP CONSTRAINT [' + @var1 + '];');
UPDATE [engineer].[ProjectCostCenterRequests] SET [Description] = N'' WHERE [Description] IS NULL;
ALTER TABLE [engineer].[ProjectCostCenterRequests] ALTER COLUMN [Description] nvarchar(1500) NOT NULL;
ALTER TABLE [engineer].[ProjectCostCenterRequests] ADD DEFAULT N'' FOR [Description];
GO

ALTER TABLE [engineer].[ProjectCostCenterRequests] ADD [RejectionReason] nvarchar(1000) NULL;
DECLARE @description AS sql_variant;
SET @description = N'╪»┘ä█î┘ä ╪▒╪» ╪»╪▒╪«┘ê╪º╪│╪¬';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCostCenterRequests', 'COLUMN', N'RejectionReason';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260810124651_ProjectCostCenterRequestTableAddParameters', N'8.0.8');
GO

COMMIT;
GO



