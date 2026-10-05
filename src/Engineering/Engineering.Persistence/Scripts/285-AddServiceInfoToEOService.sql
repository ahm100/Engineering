BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[ProjectOperationDetailConsumableVolumeMachineries]') AND [c].[name] = N'FinalValue');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[ProjectOperationDetailConsumableVolumeMachineries] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [engineer].[ProjectOperationDetailConsumableVolumeMachineries] ALTER COLUMN [FinalValue] decimal(18,3) NOT NULL;
GO

ALTER TABLE [engineer].[EmployerOperationServices] ADD [ServiceInfoId] bigint NULL;
GO

CREATE INDEX [IX_EmployerOperationServices_ServiceInfoId] ON [engineer].[EmployerOperationServices] ([ServiceInfoId]);
GO

ALTER TABLE [engineer].[EmployerOperationServices] ADD CONSTRAINT [FK_EmployerOperationServices_EngineeringServices_ServiceInfoId] FOREIGN KEY ([ServiceInfoId]) REFERENCES [engineer].[EngineeringServices] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260406113845_AddServiceInfoToEOService', N'8.0.8');
GO

COMMIT;
GO



