BEGIN TRANSACTION;
GO

CREATE TABLE [engineer].[GoodsManagerAssignments] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [ProductCategoryId] bigint NULL,
    [ProductGroupId] bigint NULL,
    [ProductId] bigint NULL,
    [GoodsManagerThirdPartyId] bigint NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_GoodsManagerAssignments] PRIMARY KEY ([Id])
);
DECLARE @description AS sql_variant;
SET @description = N'╪┤┘å╪º╪│┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'GoodsManagerAssignments', 'COLUMN', N'Id';
SET @description = N'╪»╪│╪¬┘ç ╪¿┘å╪»█î ┘à╪¡╪╡┘ê┘ä';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'GoodsManagerAssignments', 'COLUMN', N'ProductCategoryId';
SET @description = N'┌»╪▒┘ê┘ç ┘à╪¡╪╡┘ê┘ä';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'GoodsManagerAssignments', 'COLUMN', N'ProductGroupId';
SET @description = N'┘à╪¡╪╡┘ê┘ä';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'GoodsManagerAssignments', 'COLUMN', N'ProductId';
SET @description = N'┘à╪»█î╪▒ ┌⌐╪º┘ä╪º';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'GoodsManagerAssignments', 'COLUMN', N'GoodsManagerThirdPartyId';
SET @description = N'╪¬╪º╪▒█î╪« ╪º█î╪¼╪º╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'GoodsManagerAssignments', 'COLUMN', N'Created';
SET @description = N'╪º█î╪¼╪º╪» ┌⌐┘å┘å╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'GoodsManagerAssignments', 'COLUMN', N'CreatorId';
SET @description = N'╪¬╪º╪▒█î╪« ┘ê█î╪▒╪º█î╪┤';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'GoodsManagerAssignments', 'COLUMN', N'Updated';
SET @description = N'┘ê█î╪▒╪º█î╪┤ ┌⌐┘å┘å╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'GoodsManagerAssignments', 'COLUMN', N'UpdaterId';
SET @description = N'╪¡╪░┘ü ╪┤╪»┌»█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'GoodsManagerAssignments', 'COLUMN', N'IsDeleted';
GO

CREATE TABLE [engineer].[GoodsManagerAssignmentHistory] (
    [Id] bigint NOT NULL IDENTITY,
    [ProductCategoryId] bigint NULL,
    [ProductGroupId] bigint NULL,
    [ProductId] bigint NULL,
    [GoodsManagerThirdPartyId] bigint NOT NULL,
    [Description] nvarchar(max) NULL,
    [GoodsManagerAssignmentId] bigint NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_GoodsManagerAssignmentHistory] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_GoodsManagerAssignmentHistory_GoodsManagerAssignments_GoodsManagerAssignmentId] FOREIGN KEY ([GoodsManagerAssignmentId]) REFERENCES [engineer].[GoodsManagerAssignments] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_GoodsManagerAssignmentHistory_GoodsManagerAssignmentId] ON [engineer].[GoodsManagerAssignmentHistory] ([GoodsManagerAssignmentId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260815125823_GoodsManagerAssignmentBugFix', N'8.0.8');
GO

COMMIT;
GO



