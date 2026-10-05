BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[DailyProjectOperationServices] ADD [ThirdPartyId] bigint NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260622082849_AddThirdPartyIdToDPOService', N'8.0.8');
GO

COMMIT;
GO



