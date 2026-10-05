BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[RequestMachineryHistories] ADD [ManagerDescription] nvarchar(max) NULL;
GO

ALTER TABLE [engineer].[RequestMachineries] ADD [ManagerDescription] nvarchar(1500) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251214142154_AddManagerDescToRequestMachinery', N'8.0.8');
GO

COMMIT;
GO



