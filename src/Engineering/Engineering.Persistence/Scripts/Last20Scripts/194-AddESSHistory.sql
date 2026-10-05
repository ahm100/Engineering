BEGIN TRANSACTION;
GO

CREATE TABLE [engineer].[EmployerStatusStatementHistories] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [StatusStatementCode] nvarchar(250) NULL,
    [Status] int NOT NULL,
    [StartDate] datetime2 NOT NULL,
    [EndDate] datetime2 NOT NULL,
    [PercentageOfWorkDone] decimal(18,2) NOT NULL,
    [CalculatedAmount] decimal(18,2) NOT NULL,
    [StatusStatementVolume] decimal(18,2) NOT NULL,
    [Description] nvarchar(1500) NULL,
    [EmployerStatusStatementId] bigint NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit),
    CONSTRAINT [PK_EmployerStatusStatementHistories] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_EmployerStatusStatementHistories_EmployerStatusStatements_EmployerStatusStatementId] FOREIGN KEY ([EmployerStatusStatementId]) REFERENCES [engineer].[EmployerStatusStatements] ([Id])
);
GO

CREATE INDEX [IX_EmployerStatusStatementHistories_EmployerStatusStatementId] ON [engineer].[EmployerStatusStatementHistories] ([EmployerStatusStatementId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250428065320_AddESSHistory', N'8.0.8');
GO

COMMIT;
GO
