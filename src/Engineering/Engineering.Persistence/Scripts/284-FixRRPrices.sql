BEGIN TRANSACTION;

ALTER TABLE [engineer].[RequestRewardHistories]
ALTER COLUMN [OfferedPrice] decimal(18,2) NOT NULL;

ALTER TABLE [engineer].[RequestRewardHistories]
ALTER COLUMN [ConfirmedPrice] decimal(18,2) NOT NULL;

ALTER TABLE [engineer].[RequestRewards]
ALTER COLUMN [ConfirmedPrice] decimal(18,2) NOT NULL;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260408103701_FixRRPrices', N'8.0.8');

COMMIT;