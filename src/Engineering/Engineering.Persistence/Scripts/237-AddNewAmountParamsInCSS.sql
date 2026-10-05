BEGIN TRANSACTION;
GO

DECLARE @description AS sql_variant;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', N'engineer', 'TABLE', N'EngineeringServices', 'COLUMN', N'PreferentialReferenceCode';
SET @description = N'کد مرجع تفص?ل?';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EngineeringServices', 'COLUMN', N'PreferentialReferenceCode';
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[ContractorStatusStatements]') AND [c].[name] = N'RewardsAmount');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[ContractorStatusStatements] DROP CONSTRAINT [' + @var0 + '];');
UPDATE [engineer].[ContractorStatusStatements] SET [RewardsAmount] = 0.0 WHERE [RewardsAmount] IS NULL;
ALTER TABLE [engineer].[ContractorStatusStatements] ALTER COLUMN [RewardsAmount] decimal(18,2) NOT NULL;
ALTER TABLE [engineer].[ContractorStatusStatements] ADD DEFAULT 0.0 FOR [RewardsAmount];
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[ContractorStatusStatements]') AND [c].[name] = N'ProductsAmount');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[ContractorStatusStatements] DROP CONSTRAINT [' + @var1 + '];');
UPDATE [engineer].[ContractorStatusStatements] SET [ProductsAmount] = 0.0 WHERE [ProductsAmount] IS NULL;
ALTER TABLE [engineer].[ContractorStatusStatements] ALTER COLUMN [ProductsAmount] decimal(18,2) NOT NULL;
ALTER TABLE [engineer].[ContractorStatusStatements] ADD DEFAULT 0.0 FOR [ProductsAmount];
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[ContractorStatusStatements]') AND [c].[name] = N'PaymentedAmount');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[ContractorStatusStatements] DROP CONSTRAINT [' + @var2 + '];');
UPDATE [engineer].[ContractorStatusStatements] SET [PaymentedAmount] = 0.0 WHERE [PaymentedAmount] IS NULL;
ALTER TABLE [engineer].[ContractorStatusStatements] ALTER COLUMN [PaymentedAmount] decimal(18,2) NOT NULL;
ALTER TABLE [engineer].[ContractorStatusStatements] ADD DEFAULT 0.0 FOR [PaymentedAmount];
GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[ContractorStatusStatements]') AND [c].[name] = N'PayableAmount');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[ContractorStatusStatements] DROP CONSTRAINT [' + @var3 + '];');
UPDATE [engineer].[ContractorStatusStatements] SET [PayableAmount] = 0.0 WHERE [PayableAmount] IS NULL;
ALTER TABLE [engineer].[ContractorStatusStatements] ALTER COLUMN [PayableAmount] decimal(18,2) NOT NULL;
ALTER TABLE [engineer].[ContractorStatusStatements] ADD DEFAULT 0.0 FOR [PayableAmount];
GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[ContractorStatusStatements]') AND [c].[name] = N'FinesAmount');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[ContractorStatusStatements] DROP CONSTRAINT [' + @var4 + '];');
UPDATE [engineer].[ContractorStatusStatements] SET [FinesAmount] = 0.0 WHERE [FinesAmount] IS NULL;
ALTER TABLE [engineer].[ContractorStatusStatements] ALTER COLUMN [FinesAmount] decimal(18,2) NOT NULL;
ALTER TABLE [engineer].[ContractorStatusStatements] ADD DEFAULT 0.0 FOR [FinesAmount];
GO

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[ContractorStatusStatements]') AND [c].[name] = N'DiscountPrice');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[ContractorStatusStatements] DROP CONSTRAINT [' + @var5 + '];');
UPDATE [engineer].[ContractorStatusStatements] SET [DiscountPrice] = 0.0 WHERE [DiscountPrice] IS NULL;
ALTER TABLE [engineer].[ContractorStatusStatements] ALTER COLUMN [DiscountPrice] decimal(18,2) NOT NULL;
ALTER TABLE [engineer].[ContractorStatusStatements] ADD DEFAULT 0.0 FOR [DiscountPrice];
GO

DECLARE @var6 sysname;
SELECT @var6 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[ContractorStatusStatements]') AND [c].[name] = N'CostOversAmount');
IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[ContractorStatusStatements] DROP CONSTRAINT [' + @var6 + '];');
UPDATE [engineer].[ContractorStatusStatements] SET [CostOversAmount] = 0.0 WHERE [CostOversAmount] IS NULL;
ALTER TABLE [engineer].[ContractorStatusStatements] ALTER COLUMN [CostOversAmount] decimal(18,2) NOT NULL;
ALTER TABLE [engineer].[ContractorStatusStatements] ADD DEFAULT 0.0 FOR [CostOversAmount];
GO

DECLARE @var7 sysname;
SELECT @var7 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[ContractorStatusStatements]') AND [c].[name] = N'CanPayableAmount');
IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[ContractorStatusStatements] DROP CONSTRAINT [' + @var7 + '];');
UPDATE [engineer].[ContractorStatusStatements] SET [CanPayableAmount] = 0.0 WHERE [CanPayableAmount] IS NULL;
ALTER TABLE [engineer].[ContractorStatusStatements] ALTER COLUMN [CanPayableAmount] decimal(18,2) NOT NULL;
ALTER TABLE [engineer].[ContractorStatusStatements] ADD DEFAULT 0.0 FOR [CanPayableAmount];
GO

ALTER TABLE [engineer].[ContractorStatusStatements] ADD [FixedAmount] decimal(18,2) NOT NULL DEFAULT 0.0;
GO

ALTER TABLE [engineer].[ContractorStatusStatements] ADD [RemainingAmount] decimal(18,2) NOT NULL DEFAULT 0.0;
GO

ALTER TABLE [engineer].[ContractorStatusStatements] ADD [ServicedAmount] decimal(18,2) NOT NULL DEFAULT 0.0;
GO

DECLARE @description AS sql_variant;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', N'engineer', 'TABLE', N'ContractorStatusStatementPayments', 'COLUMN', N'PayableAmount';
SET @description = N'مبلغ تایید پرداخت';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorStatusStatementPayments', 'COLUMN', N'PayableAmount';
GO

ALTER TABLE [engineer].[ContractorStatusStatementPayments] ADD [ConfirmedBankAccountId] bigint NULL;
GO

ALTER TABLE [engineer].[ContractorStatusStatementDetails] ADD [FixedContractPctAmount] decimal(18,2) NULL;
GO

ALTER TABLE [engineer].[ContractorStatusStatementDetails] ADD [ManagerFixedContractPctAmount] decimal(18,2) NULL;
GO

ALTER TABLE [engineer].[ContractorStatusStatementDetails] ADD [ProjectFixedContractPctAmount] decimal(18,2) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251110171219_AddNewAmountParamsInCSS', N'8.0.8');
GO

COMMIT;
GO



