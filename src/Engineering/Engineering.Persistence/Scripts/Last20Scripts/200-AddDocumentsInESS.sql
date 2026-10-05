BEGIN TRANSACTION;
GO

CREATE TABLE [engineer].[EmployerStatusStatementDailyDocuments] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [Url] nvarchar(1500) NOT NULL,
    [EmployerStatusStatementProjectOperationDetailDailyId] bigint NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit),
    CONSTRAINT [PK_EmployerStatusStatementDailyDocuments] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_EmployerStatusStatementDailyDocuments_EmployerStatusStatementProjectOperationDetailDailies_EmployerStatusStatementProjectOpe~] FOREIGN KEY ([EmployerStatusStatementProjectOperationDetailDailyId]) REFERENCES [engineer].[EmployerStatusStatementProjectOperationDetailDailies] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [engineer].[EmployerStatusStatementDocuments] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [Url] nvarchar(1500) NOT NULL,
    [EmployerStatusStatementId] bigint NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit),
    CONSTRAINT [PK_EmployerStatusStatementDocuments] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_EmployerStatusStatementDocuments_EmployerStatusStatements_EmployerStatusStatementId] FOREIGN KEY ([EmployerStatusStatementId]) REFERENCES [engineer].[EmployerStatusStatements] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [engineer].[EmployerStatusStatementProjectOperationDocuments] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [Url] nvarchar(1500) NOT NULL,
    [EmployerStatusStatementProjectOperationId] bigint NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit),
    CONSTRAINT [PK_EmployerStatusStatementProjectOperationDocuments] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_EmployerStatusStatementProjectOperationDocuments_EmployerStatusStatementProjectOperations_EmployerStatusStatementProjectOper~] FOREIGN KEY ([EmployerStatusStatementProjectOperationId]) REFERENCES [engineer].[EmployerStatusStatementProjectOperations] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_EmployerStatusStatementDailyDocuments_EmployerStatusStatementProjectOperationDetailDailyId] ON [engineer].[EmployerStatusStatementDailyDocuments] ([EmployerStatusStatementProjectOperationDetailDailyId]);
GO

CREATE INDEX [IX_EmployerStatusStatementDocuments_EmployerStatusStatementId] ON [engineer].[EmployerStatusStatementDocuments] ([EmployerStatusStatementId]);
GO

CREATE INDEX [IX_EmployerStatusStatementProjectOperationDocuments_EmployerStatusStatementProjectOperationId] ON [engineer].[EmployerStatusStatementProjectOperationDocuments] ([EmployerStatusStatementProjectOperationId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250504074624_AddDocumentsInESS', N'8.0.8');
GO

COMMIT;
GO