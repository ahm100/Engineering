BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[CostOvers] ADD [ApprovalStatus] int NOT NULL DEFAULT 0;
GO

ALTER TABLE [engineer].[CostOvers] ADD [CurrentApprovalAttemptId] uniqueidentifier NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260914115710_ChangesForTestCostOverOnWF', N'8.0.8');
GO

COMMIT;
GO



