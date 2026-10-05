BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[EmployerContracts]') AND [c].[name] = N'Description');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[EmployerContracts] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [engineer].[EmployerContracts] ALTER COLUMN [Description] nvarchar(1500) NULL;
DECLARE @description AS sql_variant;
SET @description = N'توضیحات';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerContracts', 'COLUMN', N'Description';
GO

ALTER TABLE [engineer].[EmployerContracts] ADD [LastDescription] nvarchar(1500) NULL;
DECLARE @description AS sql_variant;
SET @description = N'آخرین توضیحات';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerContracts', 'COLUMN', N'LastDescription';
GO

CREATE TABLE [engineer].[EmployerContractHistories] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [IsFirst] bit NOT NULL,
    [Status] int NOT NULL,
    [Code] nvarchar(250) NULL,
    [CurrencyRate] decimal(18,2) NULL,
    [StartDate] datetime2 NULL,
    [EndDate] datetime2 NULL,
    [TotalAmount] decimal(18,2) NOT NULL,
    [AdvancePayment] decimal(18,2) NOT NULL,
    [Description] nvarchar(1500) NULL,
    [EmployerContractId] bigint NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_EmployerContractHistories] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_EmployerContractHistories_EmployerContracts_EmployerContractId] FOREIGN KEY ([EmployerContractId]) REFERENCES [engineer].[EmployerContracts] ([Id]) ON DELETE NO ACTION
);
DECLARE @description AS sql_variant;
SET @description = N'شناسه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerContractHistories', 'COLUMN', N'Id';
SET @description = N'اولین قرارداد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerContractHistories', 'COLUMN', N'IsFirst';
SET @description = N'وضعیت قرارداد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerContractHistories', 'COLUMN', N'Status';
SET @description = N'کد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerContractHistories', 'COLUMN', N'Code';
SET @description = N'تاریخ شروع';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerContractHistories', 'COLUMN', N'StartDate';
SET @description = N'تاریخ پایان';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerContractHistories', 'COLUMN', N'EndDate';
SET @description = N'مجموع مبالغ';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerContractHistories', 'COLUMN', N'TotalAmount';
SET @description = N'پیش پرداخت';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerContractHistories', 'COLUMN', N'AdvancePayment';
SET @description = N'توضیحات';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerContractHistories', 'COLUMN', N'Description';
SET @description = N'تاریخ ایجاد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerContractHistories', 'COLUMN', N'Created';
SET @description = N'ایجاد کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerContractHistories', 'COLUMN', N'CreatorId';
SET @description = N'تاریخ ویرایش';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerContractHistories', 'COLUMN', N'Updated';
SET @description = N'ویرایش کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerContractHistories', 'COLUMN', N'UpdaterId';
SET @description = N'حذف شدگی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerContractHistories', 'COLUMN', N'IsDeleted';
SET @description = N'وضعیت فعال یا غیر فعال بودن';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerContractHistories', 'COLUMN', N'IsActive';
GO

CREATE INDEX [IX_EmployerContractHistories_EmployerContractId] ON [engineer].[EmployerContractHistories] ([EmployerContractId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251029050029_AddedEContractHistory', N'8.0.8');
GO

COMMIT;
GO



