BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[ProjectDocs] ADD [Code] nvarchar(256) NOT NULL DEFAULT N'';
GO

ALTER TABLE [engineer].[ProjectDocs] ADD [Description] nvarchar(1500) NOT NULL DEFAULT N'';
GO

ALTER TABLE [engineer].[ProjectDocs] ADD [Revision] int NOT NULL DEFAULT 0;
GO

ALTER TABLE [engineer].[ProjectDocs] ADD [Sequence] int NOT NULL DEFAULT 0;
GO

ALTER TABLE [engineer].[ProjectDocs] ADD [Status] int NOT NULL DEFAULT 0;
GO

ALTER TABLE [engineer].[ProjectDocs] ADD [ThirdPartyId] bigint NOT NULL DEFAULT CAST(0 AS bigint);
GO

ALTER TABLE [engineer].[ProjectDocs] ADD [Url] nvarchar(256) NOT NULL DEFAULT N'';
GO

CREATE TABLE [engineer].[ProjectDocHistories] (
    [Id] bigint NOT NULL IDENTITY,
    [ProjectId] bigint NOT NULL,
    [DisciplineId] bigint NOT NULL,
    [DisciplineDocId] bigint NOT NULL,
    [Url] nvarchar(256) NOT NULL,
    [Description] nvarchar(1500) NOT NULL,
    [ThirdPartyId] bigint NOT NULL,
    [Code] nvarchar(256) NOT NULL,
    [Revision] int NOT NULL,
    [Sequence] int NOT NULL,
    [Status] int NOT NULL,
    [ProjectDocId] bigint NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_ProjectDocHistories] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ProjectDocHistories_DisciplineDocs_DisciplineDocId] FOREIGN KEY ([DisciplineDocId]) REFERENCES [engineer].[DisciplineDocs] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ProjectDocHistories_Disciplines_DisciplineId] FOREIGN KEY ([DisciplineId]) REFERENCES [engineer].[Disciplines] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ProjectDocHistories_ProjectDocs_ProjectDocId] FOREIGN KEY ([ProjectDocId]) REFERENCES [engineer].[ProjectDocs] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_ProjectDocHistories_Projects_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [engineer].[Projects] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_ProjectDocHistories_DisciplineDocId] ON [engineer].[ProjectDocHistories] ([DisciplineDocId]);
GO

CREATE INDEX [IX_ProjectDocHistories_DisciplineId] ON [engineer].[ProjectDocHistories] ([DisciplineId]);
GO

CREATE INDEX [IX_ProjectDocHistories_ProjectDocId] ON [engineer].[ProjectDocHistories] ([ProjectDocId]);
GO

CREATE INDEX [IX_ProjectDocHistories_ProjectId] ON [engineer].[ProjectDocHistories] ([ProjectId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260907112429_AddProjectDocumentHistory', N'8.0.8');
GO

COMMIT;
GO



