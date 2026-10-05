BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[MessengerChannelHistories] DROP CONSTRAINT [FK_MessengerChannelHistories_MessengerChannels_MessengerId];
GO

DROP INDEX [IX_MessengerChannelHistories_MessengerId] ON [engineer].[MessengerChannelHistories];
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[MessengerChannelHistories]') AND [c].[name] = N'MessengerId');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[MessengerChannelHistories] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [engineer].[MessengerChannelHistories] DROP COLUMN [MessengerId];
GO

CREATE INDEX [IX_MessengerChannelHistories_MessengerChannelId] ON [engineer].[MessengerChannelHistories] ([MessengerChannelId]);
GO

ALTER TABLE [engineer].[MessengerChannelHistories] ADD CONSTRAINT [FK_MessengerChannelHistories_MessengerChannels_MessengerChannelId] FOREIGN KEY ([MessengerChannelId]) REFERENCES [engineer].[MessengerChannels] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260524132332_FixMessengerChannelHistory', N'8.0.8');
GO

COMMIT;
GO



