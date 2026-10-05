BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[TransportationCargos] ADD [SecurityConfirm] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [engineer].[TransportationCargos] ADD [SecurityConfirmDate] datetime2 NULL;
GO

CREATE TABLE [engineer].[TransportationCargoDocuments] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [Url] nvarchar(max) NOT NULL,
    [TransportationCargoId] bigint NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit),
    CONSTRAINT [PK_TransportationCargoDocuments] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TransportationCargoDocuments_TransportationCargos_TransportationCargoId] FOREIGN KEY ([TransportationCargoId]) REFERENCES [engineer].[TransportationCargos] ([Id]) ON DELETE NO ACTION
);
GO

CREATE INDEX [IX_TransportationCargoDocuments_TransportationCargoId] ON [engineer].[TransportationCargoDocuments] ([TransportationCargoId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260614152728_AddCargoDocs', N'8.0.8');
GO

COMMIT;
GO



