   BEGIN TRANSACTION;
GO

CREATE TABLE [engineer].[FixAssetMachineryDocuments] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [Url] nvarchar(max) NOT NULL,
    [FixAssetMachineryId] bigint NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit),
    CONSTRAINT [PK_FixAssetMachineryDocuments] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_FixAssetMachineryDocuments_FixAssetMachineries_FixAssetMachineryId] FOREIGN KEY ([FixAssetMachineryId]) REFERENCES [engineer].[FixAssetMachineries] ([Id])
);
GO

CREATE TABLE [engineer].[FixAssetNotWorkDocuments] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [Url] nvarchar(max) NOT NULL,
    [FixAssetMachineryNotWorkId] bigint NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit),
    CONSTRAINT [PK_FixAssetNotWorkDocuments] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_FixAssetNotWorkDocuments_FixAssetMachineryNotWorks_FixAssetMachineryNotWorkId] FOREIGN KEY ([FixAssetMachineryNotWorkId]) REFERENCES [engineer].[FixAssetMachineryNotWorks] ([Id])
);
GO

CREATE INDEX [IX_FixAssetMachineryDocuments_FixAssetMachineryId] ON [engineer].[FixAssetMachineryDocuments] ([FixAssetMachineryId]);
GO

CREATE INDEX [IX_FixAssetNotWorkDocuments_FixAssetMachineryNotWorkId] ON [engineer].[FixAssetNotWorkDocuments] ([FixAssetMachineryNotWorkId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250407040123_AddFixAssetDocuments', N'8.0.8');
GO

COMMIT;
GO