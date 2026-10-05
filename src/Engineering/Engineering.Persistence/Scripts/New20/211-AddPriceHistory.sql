BEGIN TRANSACTION;
GO

CREATE TABLE [engineer].[ContractorContractDetailPriceHistories] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [StartDate] datetime2 NOT NULL,
    [EndDate] datetime2 NOT NULL,
    [Price] decimal(18,2) NOT NULL,
    [CurrencyId] bigint NOT NULL,
    [ContractorContractDetailPriceId] bigint NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_ContractorContractDetailPriceHistories] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ContractorContractDetailPriceHistories_ContractorContractDetailPrices_ContractorContractDetailPriceId] FOREIGN KEY ([ContractorContractDetailPriceId]) REFERENCES [engineer].[ContractorContractDetailPrices] ([Id]) ON DELETE NO ACTION
);
DECLARE @description AS sql_variant;
SET @description = N'شناسه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContractDetailPriceHistories', 'COLUMN', N'Id';
SET @description = N'تاریخ ایجاد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContractDetailPriceHistories', 'COLUMN', N'Created';
SET @description = N'ایجاد کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContractDetailPriceHistories', 'COLUMN', N'CreatorId';
SET @description = N'تاریخ ویرایش';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContractDetailPriceHistories', 'COLUMN', N'Updated';
SET @description = N'ویرایش کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContractDetailPriceHistories', 'COLUMN', N'UpdaterId';
SET @description = N'حذف شدگی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContractDetailPriceHistories', 'COLUMN', N'IsDeleted';
SET @description = N'وضعیت فعال یا غیر فعال بودن';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContractDetailPriceHistories', 'COLUMN', N'IsActive';
GO

CREATE INDEX [IX_ContractorContractDetailPriceHistories_ContractorContractDetailPriceId] ON [engineer].[ContractorContractDetailPriceHistories] ([ContractorContractDetailPriceId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250926130002_AddPriceHistory', N'8.0.8');
GO

COMMIT;
GO



