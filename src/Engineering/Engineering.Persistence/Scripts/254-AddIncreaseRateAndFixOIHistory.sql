BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[EmployerOperationServices] DROP CONSTRAINT [FK_EmployerOperationServices_OperationInfoServices_OperationInfoServiceId];
GO

ALTER TABLE [engineer].[ProjectOperations] ADD [IncreaseRate] decimal(5,2) NOT NULL DEFAULT 1.0;
DECLARE @description AS sql_variant;
SET @description = N'ضریب افزایش';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectOperations', 'COLUMN', N'IncreaseRate';
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[OperationInfoHistories]') AND [c].[name] = N'OperationInfoName');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[OperationInfoHistories] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [engineer].[OperationInfoHistories] ALTER COLUMN [OperationInfoName] nvarchar(max) NOT NULL;
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[EmployerOperationServices]') AND [c].[name] = N'OperationInfoServiceId');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[EmployerOperationServices] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [engineer].[EmployerOperationServices] ALTER COLUMN [OperationInfoServiceId] bigint NULL;
GO

ALTER TABLE [engineer].[EmployerOperationServices] ADD CONSTRAINT [FK_EmployerOperationServices_OperationInfoServices_OperationInfoServiceId] FOREIGN KEY ([OperationInfoServiceId]) REFERENCES [engineer].[OperationInfoServices] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251203095005_AddIncreaseRateAndFixOIHistory', N'8.0.8');
GO

COMMIT;
GO



