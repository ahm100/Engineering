BEGIN TRANSACTION;
GO

CREATE TABLE [engineer].[EmployerStatusStatementProjectOperationDetailDailies] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [ContractorLength] decimal(18,2) NOT NULL,
    [ContractorWidth] decimal(18,2) NOT NULL,
    [ContractorHeight] decimal(18,2) NOT NULL,
    [ContractorWeight] decimal(18,2) NOT NULL,
    [ContractorNumber] decimal(18,2) NOT NULL,
    [ContractorVolume] decimal(18,2) NOT NULL,
    [ContractorDescription] nvarchar(1500) NULL,
    [SupervisorLength] decimal(18,2) NOT NULL,
    [SupervisorWidth] decimal(18,2) NOT NULL,
    [SupervisorHeight] decimal(18,2) NOT NULL,
    [SupervisorWeight] decimal(18,2) NOT NULL,
    [SupervisorNumber] decimal(18,2) NOT NULL,
    [SupervisorVolume] decimal(18,2) NOT NULL,
    [SupervisorDescription] nvarchar(1500) NULL,
    [ConsultantLength] decimal(18,2) NOT NULL,
    [ConsultantWidth] decimal(18,2) NOT NULL,
    [ConsultantHeight] decimal(18,2) NOT NULL,
    [ConsultantWeight] decimal(18,2) NOT NULL,
    [ConsultantNumber] decimal(18,2) NOT NULL,
    [ConsultantVolume] decimal(18,2) NOT NULL,
    [ConsultantDescription] nvarchar(1500) NULL,
    [EmployerRepresentativeLength] decimal(18,2) NOT NULL,
    [EmployerRepresentativeWidth] decimal(18,2) NOT NULL,
    [EmployerRepresentativeHeight] decimal(18,2) NOT NULL,
    [EmployerRepresentativeWeight] decimal(18,2) NOT NULL,
    [EmployerRepresentativeNumber] decimal(18,2) NOT NULL,
    [EmployerRepresentativeVolume] decimal(18,2) NOT NULL,
    [EmployerRepresentativeDescription] nvarchar(1500) NULL,
    [EmployerStatusStatementProjectOperationDetailId] bigint NOT NULL,
    [DailyProjectOperationId] bigint NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit),
    CONSTRAINT [PK_EmployerStatusStatementProjectOperationDetailDailies] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_EmployerStatusStatementProjectOperationDetailDailies_DailyProjectOperations_DailyProjectOperationId] FOREIGN KEY ([DailyProjectOperationId]) REFERENCES [engineer].[DailyProjectOperations] ([Id]),
    CONSTRAINT [FK_EmployerStatusStatementProjectOperationDetailDailies_EmployerStatusStatementProjectOperationDetails_EmployerStatusStatementP~] FOREIGN KEY ([EmployerStatusStatementProjectOperationDetailId]) REFERENCES [engineer].[EmployerStatusStatementProjectOperationDetails] ([Id])
);
GO

CREATE INDEX [IX_EmployerStatusStatementProjectOperationDetailDailies_DailyProjectOperationId] ON [engineer].[EmployerStatusStatementProjectOperationDetailDailies] ([DailyProjectOperationId]);
GO

CREATE INDEX [IX_EmployerStatusStatementProjectOperationDetailDailies_EmployerStatusStatementProjectOperationDetailId] ON [engineer].[EmployerStatusStatementProjectOperationDetailDailies] ([EmployerStatusStatementProjectOperationDetailId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250421065011_AddNewEntityESSPODD', N'8.0.8');
GO

COMMIT;
GO
