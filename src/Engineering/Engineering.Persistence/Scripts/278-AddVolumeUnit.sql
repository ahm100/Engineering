BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[FixAssetMachineryRates] ADD [VolumeRate] decimal(18,2) NULL;
GO

ALTER TABLE [engineer].[FixAssetMachineries] ADD [VolumeRate] decimal(18,2) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260209074505_AddVolumeUnit', N'8.0.8');
GO

COMMIT;
GO



