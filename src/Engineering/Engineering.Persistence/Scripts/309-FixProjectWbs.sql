BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[ProjectWbses] DROP CONSTRAINT [FK_ProjectWbses_Projects_ProjectId];
GO

ALTER TABLE [engineer].[ProjectWbses] DROP CONSTRAINT [FK_ProjectWbses_WbsTemplates_WbsTemplateId];
GO

ALTER TABLE [engineer].[ProjectWbses] ADD CONSTRAINT [FK_ProjectWbses_Projects_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [engineer].[Projects] ([Id]) ON DELETE NO ACTION;
GO

ALTER TABLE [engineer].[ProjectWbses] ADD CONSTRAINT [FK_ProjectWbses_WbsTemplates_WbsTemplateId] FOREIGN KEY ([WbsTemplateId]) REFERENCES [engineer].[WbsTemplates] ([Id]) ON DELETE NO ACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260623054329_FixProjectWbs', N'8.0.8');
GO

COMMIT;
GO



