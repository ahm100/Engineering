BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[ProjectScheduleTasks] ADD [PhysicalPercentComplete] decimal(18,2) NOT NULL DEFAULT 0.0;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260914052747_AddPhysicalPercentFieldToProjectScheduleTask', N'8.0.8');
GO

COMMIT;
GO



