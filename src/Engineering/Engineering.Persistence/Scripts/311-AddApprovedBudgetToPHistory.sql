BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[ProjectHistories] ADD [ApprovedBudget] decimal(18,2) NULL;
DECLARE @description AS sql_variant;
SET @description = N'بودجه ی مصوب';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectHistories', 'COLUMN', N'ApprovedBudget';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260623124146_AddApprovedBudgetToPHistory', N'8.0.8');
GO

COMMIT;
GO



