BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[RequestMachineryBills] ADD [BillConfirmerId] bigint NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250421075508_AddBillConfirmerIdToBills', N'8.0.8');
GO

COMMIT;
GO
