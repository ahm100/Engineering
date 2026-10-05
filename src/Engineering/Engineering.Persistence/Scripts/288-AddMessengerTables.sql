BEGIN TRANSACTION;
GO

CREATE TABLE [engineer].[Messengers] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [MessengerType] int NOT NULL,
    [MessengerTargetType] int NOT NULL,
    [TargetId] bigint NOT NULL,
    [Description] nvarchar(500) NULL,
    [CompanyId] bigint NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit),
    [IsActive] bit NOT NULL DEFAULT CAST(1 AS bit),
    CONSTRAINT [PK_Messengers] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [engineer].[MessengerChannels] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [ChatId] nvarchar(250) NOT NULL,
    [ChatUrl] nvarchar(250) NULL,
    [ChatName] nvarchar(250) NULL,
    [MessengerMessageType] int NOT NULL,
    [MessengerId] bigint NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit),
    CONSTRAINT [PK_MessengerChannels] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_MessengerChannels_Messengers_MessengerId] FOREIGN KEY ([MessengerId]) REFERENCES [engineer].[Messengers] ([Id])
);
GO

CREATE TABLE [engineer].[MessengerChannelHistories] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [Message] nvarchar(2500) NOT NULL,
    [ErrorMessage] nvarchar(2500) NOT NULL,
    [FileUrls] nvarchar(max) NULL,
    [IsSend] bit NOT NULL DEFAULT CAST(0 AS bit),
    [MessengerChannelId] bigint NOT NULL,
    [MessengerId] bigint NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit),
    CONSTRAINT [PK_MessengerChannelHistories] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_MessengerChannelHistories_MessengerChannels_MessengerId] FOREIGN KEY ([MessengerId]) REFERENCES [engineer].[MessengerChannels] ([Id])
);
GO

CREATE INDEX [IX_MessengerChannelHistories_MessengerId] ON [engineer].[MessengerChannelHistories] ([MessengerId]);
GO

CREATE INDEX [IX_MessengerChannels_MessengerId] ON [engineer].[MessengerChannels] ([MessengerId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260508103141_AddMessengerTables', N'8.0.8');
GO

COMMIT;
GO



