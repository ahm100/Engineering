BEGIN TRANSACTION;
GO

DECLARE @description AS sql_variant;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', N'engineer', 'TABLE', N'EngineeringServices', 'COLUMN', N'PreferentialReferenceCode';
SET @description = N'˜Ï ãÑÌÚ ÊÝÕ?á?';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EngineeringServices', 'COLUMN', N'PreferentialReferenceCode';
GO

CREATE TABLE [engineer].[ContractorContractHeaderVersions] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [Content] nvarchar(MAX) NOT NULL,
    [Version] int NOT NULL,
    [CreatedPaymentDate] datetime NULL,
    [ContractorContractHeaderId] bigint NOT NULL,
    [ContractorStatusStatementId] bigint NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_ContractorContractHeaderVersions] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ContractorContractHeaderVersions_ContractorContractHeaders_ContractorContractHeaderId] FOREIGN KEY ([ContractorContractHeaderId]) REFERENCES [engineer].[ContractorContractHeaders] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_ContractorContractHeaderVersions_ContractorStatusStatements_ContractorStatusStatementId] FOREIGN KEY ([ContractorStatusStatementId]) REFERENCES [engineer].[ContractorStatusStatements] ([Id]) ON DELETE NO ACTION
);
DECLARE @description AS sql_variant;
SET @description = N'شناسه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContractHeaderVersions', 'COLUMN', N'Id';
SET @description = N'محتوای ورژن قرارداد پیمانکار';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContractHeaderVersions', 'COLUMN', N'Content';
SET @description = N'ورژن';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContractHeaderVersions', 'COLUMN', N'Version';
SET @description = N'تاریخ صدور پرداخت';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContractHeaderVersions', 'COLUMN', N'CreatedPaymentDate';
SET @description = N'شناسه قرارداد پیمانکار';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContractHeaderVersions', 'COLUMN', N'ContractorContractHeaderId';
SET @description = N'شناسه صورت وضعیت  پیمانکار';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContractHeaderVersions', 'COLUMN', N'ContractorStatusStatementId';
SET @description = N'تاریخ ایجاد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContractHeaderVersions', 'COLUMN', N'Created';
SET @description = N'ایجاد کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContractHeaderVersions', 'COLUMN', N'CreatorId';
SET @description = N'تاریخ ویرایش';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContractHeaderVersions', 'COLUMN', N'Updated';
SET @description = N'ویرایش کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContractHeaderVersions', 'COLUMN', N'UpdaterId';
SET @description = N'حذف شدگی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContractHeaderVersions', 'COLUMN', N'IsDeleted';
GO

CREATE INDEX [IX_ContractorContractHeaderVersions_ContractorContractHeaderId] ON [engineer].[ContractorContractHeaderVersions] ([ContractorContractHeaderId]);
GO

CREATE INDEX [IX_ContractorContractHeaderVersions_ContractorStatusStatementId] ON [engineer].[ContractorContractHeaderVersions] ([ContractorStatusStatementId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251005113727_AddCCHVerEntity', N'8.0.8');
GO

COMMIT;
GO



