  BEGIN TRANSACTION;
GO

CREATE TABLE [engineer].[FixAssetMachineryRates] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [StartDate] datetime2 NOT NULL,
    [EndDate] datetime2 NOT NULL,
    [HourlyRate] decimal(18,2) NULL,
    [DailyRate] decimal(18,2) NULL,
    [ServiceRate] decimal(18,2) NULL,
    [FixAssetMachineryId] bigint NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit),
    CONSTRAINT [PK_FixAssetMachineryRates] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_FixAssetMachineryRates_FixAssetMachineries_FixAssetMachineryId] FOREIGN KEY ([FixAssetMachineryId]) REFERENCES [engineer].[FixAssetMachineries] ([Id]) ON DELETE NO ACTION
);
GO

CREATE INDEX [IX_FixAssetMachineryRates_FixAssetMachineryId] ON [engineer].[FixAssetMachineryRates] ([FixAssetMachineryId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250407134504_AddFixAssetRates', N'8.0.8');
GO

COMMIT;
GO