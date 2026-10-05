BEGIN TRANSACTION;
GO

CREATE TABLE [engineer].[OperationInfoHistories] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [OperationInfoName] nvarchar(250) NOT NULL,
    [OperationInfoCode] nvarchar(100) NOT NULL,
    [OperationLatinName] nvarchar(250) NULL,
    [Priority] int NULL,
    [UnitOfMeasurementId] bigint NOT NULL,
    [HaveStandard] bit NOT NULL,
    [IsActive] bit NOT NULL,
    [OperationInfoId] bigint NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit),
    CONSTRAINT [PK_OperationInfoHistories] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_OperationInfoHistories_OperationInfos_OperationInfoId] FOREIGN KEY ([OperationInfoId]) REFERENCES [engineer].[OperationInfos] ([Id])
);
GO

CREATE INDEX [IX_OperationInfoHistories_OperationInfoId] ON [engineer].[OperationInfoHistories] ([OperationInfoId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250517090034_AddOperationInfoHistories', N'8.0.8');
GO

COMMIT;
GO
