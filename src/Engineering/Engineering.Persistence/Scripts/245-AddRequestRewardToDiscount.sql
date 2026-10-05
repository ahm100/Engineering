BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[OperationInfoHistories]') AND [c].[name] = N'BasePrice');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[OperationInfoHistories] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [engineer].[OperationInfoHistories] ADD DEFAULT 0.0 FOR [BasePrice];
DECLARE @description AS sql_variant;
SET @description = N'قیمت پایه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'OperationInfoHistories', 'COLUMN', N'BasePrice';
GO

ALTER TABLE [engineer].[ContractorStatusStatementDiscounts] ADD [RequestRewardId] bigint NULL;
GO

CREATE INDEX [IX_ContractorStatusStatementDiscounts_RequestRewardId] ON [engineer].[ContractorStatusStatementDiscounts] ([RequestRewardId]);
GO

ALTER TABLE [engineer].[ContractorStatusStatementDiscounts] ADD CONSTRAINT [FK_ContractorStatusStatementDiscounts_RequestRewards_RequestRewardId] FOREIGN KEY ([RequestRewardId]) REFERENCES [engineer].[RequestRewards] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251122100957_AddRequestRewardToDiscount', N'8.0.8');
GO

COMMIT;
GO



