BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[Advertisements] ADD [CompanyId] bigint NULL;
GO

CREATE TABLE [engineer].[AdvertisementDocument] (
    [Id] bigint NOT NULL IDENTITY,
    [Url] nvarchar(max) NOT NULL,
    [AdvertisementId] bigint NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_AdvertisementDocument] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AdvertisementDocument_Advertisements_AdvertisementId] FOREIGN KEY ([AdvertisementId]) REFERENCES [engineer].[Advertisements] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [engineer].[ServiceInfoDocument] (
    [Id] bigint NOT NULL IDENTITY,
    [Url] nvarchar(max) NOT NULL,
    [ServiceInfoId] bigint NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_ServiceInfoDocument] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ServiceInfoDocument_EngineeringServices_ServiceInfoId] FOREIGN KEY ([ServiceInfoId]) REFERENCES [engineer].[EngineeringServices] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_AdvertisementDocument_AdvertisementId] ON [engineer].[AdvertisementDocument] ([AdvertisementId]);
GO

CREATE INDEX [IX_ServiceInfoDocument_ServiceInfoId] ON [engineer].[ServiceInfoDocument] ([ServiceInfoId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260726095255_AddAdv&ServiceDocuments', N'8.0.8');
GO

COMMIT;
GO



