BEGIN TRANSACTION;
GO

CREATE TABLE [engineer].[ProjectThirdParties] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [AuthorizedThirdPartyId] bigint NOT NULL,
    [ProjectId] bigint NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_ProjectThirdParties] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ProjectThirdParties_Projects_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [engineer].[Projects] ([Id])
);
DECLARE @description AS sql_variant;
SET @description = N'شناسه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectThirdParties', 'COLUMN', N'Id';
SET @description = N'کاربر دارای دسترسی به پروژه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectThirdParties', 'COLUMN', N'AuthorizedThirdPartyId';
SET @description = N'تاریخ ایجاد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectThirdParties', 'COLUMN', N'Created';
SET @description = N'ایجاد کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectThirdParties', 'COLUMN', N'CreatorId';
SET @description = N'تاریخ ویرایش';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectThirdParties', 'COLUMN', N'Updated';
SET @description = N'ویرایش کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectThirdParties', 'COLUMN', N'UpdaterId';
SET @description = N'حذف شدگی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectThirdParties', 'COLUMN', N'IsDeleted';
GO

CREATE INDEX [IX_ProjectThirdParties_ProjectId] ON [engineer].[ProjectThirdParties] ([ProjectId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260523121026_AddProjectThirdPartyTable', N'8.0.8');
GO

COMMIT;
GO



