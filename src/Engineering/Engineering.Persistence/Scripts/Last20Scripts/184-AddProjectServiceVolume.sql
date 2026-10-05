BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[RequestContractors]') AND [c].[name] = N'RequestNumber');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[RequestContractors] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [engineer].[RequestContractors] ADD DEFAULT (NEXT VALUE FOR engineer.RequestContractor_RequestNumber) FOR [RequestNumber];
GO

ALTER TABLE [engineer].[DailyProjectOperationServices] ADD [ProjectServiceVolume] decimal(18,5) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250413065205_AddProjectServiceVolume', N'8.0.8');
GO

COMMIT;
GO
