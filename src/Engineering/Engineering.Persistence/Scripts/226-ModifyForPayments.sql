BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[ContractorStatusStatementPayments] DROP CONSTRAINT [FK_ContractorStatusStatementPayments_ContractorStatusStatements_ContractorStatusStatementId];
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[ContractorStatusStatementPayments]') AND [c].[name] = N'FinalManagerConfirmedAmount');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[ContractorStatusStatementPayments] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [engineer].[ContractorStatusStatementPayments] DROP COLUMN [FinalManagerConfirmedAmount];
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[ContractorStatusStatementPayments]') AND [c].[name] = N'IsPaid');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[ContractorStatusStatementPayments] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [engineer].[ContractorStatusStatementPayments] DROP COLUMN [IsPaid];
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[ContractorStatusStatementPayments]') AND [c].[name] = N'ManagerConfirmedAmount');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[ContractorStatusStatementPayments] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [engineer].[ContractorStatusStatementPayments] DROP COLUMN [ManagerConfirmedAmount];
GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[ContractorStatusStatementPayments]') AND [c].[name] = N'PrimaryManagerConfirmedAmount');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[ContractorStatusStatementPayments] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [engineer].[ContractorStatusStatementPayments] DROP COLUMN [PrimaryManagerConfirmedAmount];
GO

DECLARE @description AS sql_variant;
SET @description = N'ویرایش کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorStatusStatementPayments', 'COLUMN', N'UpdaterId';
GO

DECLARE @description AS sql_variant;
SET @description = N'تاریخ ویرایش';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorStatusStatementPayments', 'COLUMN', N'Updated';
GO

DECLARE @description AS sql_variant;
SET @description = N'مبلغ پرداخت شده توسط خزانه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorStatusStatementPayments', 'COLUMN', N'TreasuryPaid';
GO

DECLARE @description AS sql_variant;
SET @description = N'شناسه پرداخت';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorStatusStatementPayments', 'COLUMN', N'PaymentOrderId';
GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[ContractorStatusStatementPayments]') AND [c].[name] = N'PaymentDate');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[ContractorStatusStatementPayments] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [engineer].[ContractorStatusStatementPayments] ALTER COLUMN [PaymentDate] datetime NULL;
DECLARE @description AS sql_variant;
SET @description = N'تاریخ پرداخت';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorStatusStatementPayments', 'COLUMN', N'PaymentDate';
GO

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[ContractorStatusStatementPayments]') AND [c].[name] = N'IsDeleted');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[ContractorStatusStatementPayments] DROP CONSTRAINT [' + @var5 + '];');
DECLARE @description AS sql_variant;
SET @description = N'حذف شدگی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorStatusStatementPayments', 'COLUMN', N'IsDeleted';
GO

DECLARE @description AS sql_variant;
SET @description = N'ایجاد کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorStatusStatementPayments', 'COLUMN', N'CreatorId';
GO

DECLARE @description AS sql_variant;
SET @description = N'تاریخ ایجاد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorStatusStatementPayments', 'COLUMN', N'Created';
GO

DROP INDEX [IX_ContractorStatusStatementPayments_ContractorStatusStatementId] ON [engineer].[ContractorStatusStatementPayments];
DECLARE @var6 sysname;
SELECT @var6 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[ContractorStatusStatementPayments]') AND [c].[name] = N'ContractorStatusStatementId');
IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[ContractorStatusStatementPayments] DROP CONSTRAINT [' + @var6 + '];');
UPDATE [engineer].[ContractorStatusStatementPayments] SET [ContractorStatusStatementId] = CAST(0 AS bigint) WHERE [ContractorStatusStatementId] IS NULL;
ALTER TABLE [engineer].[ContractorStatusStatementPayments] ALTER COLUMN [ContractorStatusStatementId] bigint NOT NULL;
ALTER TABLE [engineer].[ContractorStatusStatementPayments] ADD DEFAULT CAST(0 AS bigint) FOR [ContractorStatusStatementId];
CREATE INDEX [IX_ContractorStatusStatementPayments_ContractorStatusStatementId] ON [engineer].[ContractorStatusStatementPayments] ([ContractorStatusStatementId]);
GO

DECLARE @description AS sql_variant;
SET @description = N'شناسه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorStatusStatementPayments', 'COLUMN', N'Id';
GO

ALTER TABLE [engineer].[ContractorStatusStatementPayments] ADD [CalculatedAmount] decimal(18,2) NOT NULL DEFAULT 0.0;
DECLARE @description AS sql_variant;
SET @description = N'مبلغ محاسبه شده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorStatusStatementPayments', 'COLUMN', N'CalculatedAmount';
GO

ALTER TABLE [engineer].[ContractorStatusStatementPayments] ADD [FinalManagerAmount] decimal(18,2) NULL;
DECLARE @description AS sql_variant;
SET @description = N'مبلغ تایید مدیر پایانی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorStatusStatementPayments', 'COLUMN', N'FinalManagerAmount';
GO

ALTER TABLE [engineer].[ContractorStatusStatementPayments] ADD [FinalManagerDescription] nvarchar(1500) NULL;
DECLARE @description AS sql_variant;
SET @description = N'توضیحات تایید مدیر پایانی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorStatusStatementPayments', 'COLUMN', N'FinalManagerDescription';
GO

ALTER TABLE [engineer].[ContractorStatusStatementPayments] ADD [ManagementAmount] decimal(18,2) NULL;
DECLARE @description AS sql_variant;
SET @description = N'مبلغ تایید کارشناس ارشد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorStatusStatementPayments', 'COLUMN', N'ManagementAmount';
GO

ALTER TABLE [engineer].[ContractorStatusStatementPayments] ADD [ManagementDescription] nvarchar(1500) NULL;
DECLARE @description AS sql_variant;
SET @description = N'توضیحات تایید کارشناس ارشد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorStatusStatementPayments', 'COLUMN', N'ManagementDescription';
GO

ALTER TABLE [engineer].[ContractorStatusStatementPayments] ADD [PayableAmount] decimal(18,2) NOT NULL DEFAULT 0.0;
DECLARE @description AS sql_variant;
SET @description = N'مبلغ قابل پرداخت';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorStatusStatementPayments', 'COLUMN', N'PayableAmount';
GO

ALTER TABLE [engineer].[ContractorStatusStatementPayments] ADD [PaymentAmount] decimal(18,2) NULL;
DECLARE @description AS sql_variant;
SET @description = N'مبلغ پرداخت';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorStatusStatementPayments', 'COLUMN', N'PaymentAmount';
GO

ALTER TABLE [engineer].[ContractorStatusStatementPayments] ADD [PaymentDescription] nvarchar(1500) NULL;
DECLARE @description AS sql_variant;
SET @description = N'توضیح پرداخت';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorStatusStatementPayments', 'COLUMN', N'PaymentDescription';
GO

ALTER TABLE [engineer].[ContractorStatusStatementPayments] ADD [PrimaryManagerAmount] decimal(18,2) NULL;
DECLARE @description AS sql_variant;
SET @description = N'مبلغ تایید مدیر اولیه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorStatusStatementPayments', 'COLUMN', N'PrimaryManagerAmount';
GO

ALTER TABLE [engineer].[ContractorStatusStatementPayments] ADD [PrimaryManagerDescription] nvarchar(1500) NULL;
DECLARE @description AS sql_variant;
SET @description = N'توضیحات تایید مدیر اولیه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorStatusStatementPayments', 'COLUMN', N'PrimaryManagerDescription';
GO

ALTER TABLE [engineer].[ContractorStatusStatementPayments] ADD [ProjectManagerAmount] decimal(18,2) NULL;
DECLARE @description AS sql_variant;
SET @description = N'مبلغ تایید مدیر پروژه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorStatusStatementPayments', 'COLUMN', N'ProjectManagerAmount';
GO

ALTER TABLE [engineer].[ContractorStatusStatementPayments] ADD [ProjectManagerDescription] nvarchar(1500) NULL;
DECLARE @description AS sql_variant;
SET @description = N'توضیحات تایید مدیر پروژه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorStatusStatementPayments', 'COLUMN', N'ProjectManagerDescription';
GO

ALTER TABLE [engineer].[ContractorStatusStatementPayments] ADD [Status] int NOT NULL DEFAULT 0;
GO

ALTER TABLE [engineer].[ContractorStatusStatementPayments] ADD [UserAmount] decimal(18,2) NOT NULL DEFAULT 0.0;
DECLARE @description AS sql_variant;
SET @description = N'مبلغ تایید کاربر';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorStatusStatementPayments', 'COLUMN', N'UserAmount';
GO

ALTER TABLE [engineer].[ContractorStatusStatementPayments] ADD [UserDescription] nvarchar(1500) NULL;
DECLARE @description AS sql_variant;
SET @description = N'توضیحات تایید کاربر';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorStatusStatementPayments', 'COLUMN', N'UserDescription';
GO

ALTER TABLE [engineer].[ContractorStatusStatementPayments] ADD CONSTRAINT [FK_ContractorStatusStatementPayments_ContractorStatusStatements_ContractorStatusStatementId] FOREIGN KEY ([ContractorStatusStatementId]) REFERENCES [engineer].[ContractorStatusStatements] ([Id]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251028095102_ModifyForPayments', N'8.0.8');
GO

COMMIT;
GO



