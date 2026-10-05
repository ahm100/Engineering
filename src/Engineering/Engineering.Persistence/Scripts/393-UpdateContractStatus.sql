BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[Contracts]') AND [c].[name] = N'RegistrationTargetStatus');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[Contracts] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [engineer].[Contracts] DROP COLUMN [RegistrationTargetStatus];
GO

ALTER TABLE [engineer].[ContractStatusHistories] ADD [Description] nvarchar(max) NULL;
DECLARE @description AS sql_variant;
SET @description = N'╪¬┘ê╪╢█î╪¡╪º╪¬';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractStatusHistories', 'COLUMN', N'Description';
GO

ALTER TABLE [engineer].[ContractStatusHistories] ADD [EffectiveDate] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
DECLARE @description AS sql_variant;
SET @description = N'╪¬╪º╪▒█î╪« ╪º╪½╪▒┌»╪░╪º╪▒█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractStatusHistories', 'COLUMN', N'EffectiveDate';
GO

ALTER TABLE [engineer].[ContractStatusHistories] ADD [Reason] nvarchar(max) NULL;
DECLARE @description AS sql_variant;
SET @description = N'╪╣┘ä╪¬';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractStatusHistories', 'COLUMN', N'Reason';
GO

ALTER TABLE [engineer].[ContractStatusHistories] ADD [SuspensionDurationMonths] int NULL;
DECLARE @description AS sql_variant;
SET @description = N'┘à╪»╪¬ ╪¬╪╣┘ä█î┘é ╪¿┘ç ┘à╪º┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractStatusHistories', 'COLUMN', N'SuspensionDurationMonths';
GO

ALTER TABLE [engineer].[ContractStatusHistories] ADD [TransitionType] int NOT NULL DEFAULT 0;
DECLARE @description AS sql_variant;
SET @description = N'┘å┘ê╪╣ ╪╣┘à┘ä█î╪º╪¬ ╪¬╪║█î█î╪▒ ┘ê╪╢╪╣█î╪¬';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractStatusHistories', 'COLUMN', N'TransitionType';
GO

ALTER TABLE [engineer].[Contracts] ADD [IsRegistrationPending] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

CREATE TABLE [engineer].[ContractStatusHistoryDocuments] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [ContractStatusHistoryId] bigint NOT NULL,
    [Url] nvarchar(1500) NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_ContractStatusHistoryDocuments] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ContractStatusHistoryDocuments_ContractStatusHistories_ContractStatusHistoryId] FOREIGN KEY ([ContractStatusHistoryId]) REFERENCES [engineer].[ContractStatusHistories] ([Id]) ON DELETE CASCADE
);
DECLARE @description AS sql_variant;
SET @description = N'╪┤┘å╪º╪│┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractStatusHistoryDocuments', 'COLUMN', N'Id';
SET @description = N'┘à╪│█î╪▒ ┘ü╪º█î┘ä';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractStatusHistoryDocuments', 'COLUMN', N'Url';
SET @description = N'╪¬╪º╪▒█î╪« ╪º█î╪¼╪º╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractStatusHistoryDocuments', 'COLUMN', N'Created';
SET @description = N'╪º█î╪¼╪º╪» ┌⌐┘å┘å╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractStatusHistoryDocuments', 'COLUMN', N'CreatorId';
SET @description = N'╪¬╪º╪▒█î╪« ┘ê█î╪▒╪º█î╪┤';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractStatusHistoryDocuments', 'COLUMN', N'Updated';
SET @description = N'┘ê█î╪▒╪º█î╪┤ ┌⌐┘å┘å╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractStatusHistoryDocuments', 'COLUMN', N'UpdaterId';
SET @description = N'╪¡╪░┘ü ╪┤╪»┌»█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractStatusHistoryDocuments', 'COLUMN', N'IsDeleted';
GO

CREATE INDEX [IX_ContractStatusHistoryDocuments_ContractStatusHistoryId] ON [engineer].[ContractStatusHistoryDocuments] ([ContractStatusHistoryId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260921123001_UpdateContractStatus', N'8.0.8');
GO

COMMIT;
GO



