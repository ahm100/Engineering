BEGIN TRANSACTION;
GO

CREATE TABLE [engineer].[ProjectCostCenters] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [ProjectId] bigint NOT NULL,
    [CostCenterId] bigint NOT NULL,
    [IsDefault] bit NOT NULL DEFAULT CAST(0 AS bit),
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_ProjectCostCenters] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ProjectCostCenters_CostCenters_CostCenterId] FOREIGN KEY ([CostCenterId]) REFERENCES [engineer].[CostCenters] ([Id]),
    CONSTRAINT [FK_ProjectCostCenters_Projects_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [engineer].[Projects] ([Id])
);
DECLARE @description AS sql_variant;
SET @description = N'شناسه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCostCenters', 'COLUMN', N'Id';
SET @description = N'تاریخ ایجاد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCostCenters', 'COLUMN', N'Created';
SET @description = N'ایجاد کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCostCenters', 'COLUMN', N'CreatorId';
SET @description = N'تاریخ ویرایش';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCostCenters', 'COLUMN', N'Updated';
SET @description = N'ویرایش کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCostCenters', 'COLUMN', N'UpdaterId';
SET @description = N'حذف شدگی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCostCenters', 'COLUMN', N'IsDeleted';
GO

CREATE INDEX [IX_ProjectCostCenters_CostCenterId] ON [engineer].[ProjectCostCenters] ([CostCenterId]);
GO

CREATE INDEX [IX_ProjectCostCenters_ProjectId] ON [engineer].[ProjectCostCenters] ([ProjectId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260802123418_CreateProjectCostCenter', N'8.0.8');
GO

COMMIT;
GO



