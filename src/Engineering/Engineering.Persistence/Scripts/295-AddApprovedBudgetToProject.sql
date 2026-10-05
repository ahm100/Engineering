BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[Projects] ADD [ApprovedBudget] decimal(18,2) NULL;
DECLARE @description AS sql_variant;
SET @description = N'بودجه ی مصوب';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'Projects', 'COLUMN', N'ApprovedBudget';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260606095247_AddApprovedBudgetToProject', N'8.0.8');
GO

COMMIT;
GO



