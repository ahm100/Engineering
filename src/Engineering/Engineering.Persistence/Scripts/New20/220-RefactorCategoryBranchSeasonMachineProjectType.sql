BEGIN TRANSACTION;
GO

DECLARE @description AS sql_variant;
SET @description = N'ویرایش کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'MachineriesGroup', 'COLUMN', N'UpdaterId';
GO

DECLARE @description AS sql_variant;
SET @description = N'تاریخ ویرایش';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'MachineriesGroup', 'COLUMN', N'Updated';
GO

DECLARE @description AS sql_variant;
SET @description = N'حذف شدگی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'MachineriesGroup', 'COLUMN', N'IsDeleted';
GO

DECLARE @description AS sql_variant;
SET @description = N'وضعیت فعال یا غیر فعال بودن';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'MachineriesGroup', 'COLUMN', N'IsActive';
GO

DECLARE @description AS sql_variant;
SET @description = N'ایجاد کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'MachineriesGroup', 'COLUMN', N'CreatorId';
GO

DECLARE @description AS sql_variant;
SET @description = N'تاریخ ایجاد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'MachineriesGroup', 'COLUMN', N'Created';
GO

DECLARE @description AS sql_variant;
SET @description = N'شناسه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'MachineriesGroup', 'COLUMN', N'Id';
GO

DECLARE @description AS sql_variant;
SET @description = N'ویرایش کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'Machineries', 'COLUMN', N'UpdaterId';
GO

DECLARE @description AS sql_variant;
SET @description = N'تاریخ ویرایش';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'Machineries', 'COLUMN', N'Updated';
GO

DECLARE @description AS sql_variant;
SET @description = N'حذف شدگی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'Machineries', 'COLUMN', N'IsDeleted';
GO

DECLARE @description AS sql_variant;
SET @description = N'وضعیت فعال یا غیر فعال بودن';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'Machineries', 'COLUMN', N'IsActive';
GO

DECLARE @description AS sql_variant;
SET @description = N'ایجاد کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'Machineries', 'COLUMN', N'CreatorId';
GO

DECLARE @description AS sql_variant;
SET @description = N'تاریخ ایجاد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'Machineries', 'COLUMN', N'Created';
GO

DECLARE @description AS sql_variant;
SET @description = N'شناسه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'Machineries', 'COLUMN', N'Id';
GO

DECLARE @description AS sql_variant;
SET @description = N'ویرایش کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EngineeringSeasons', 'COLUMN', N'UpdaterId';
GO

DECLARE @description AS sql_variant;
SET @description = N'تاریخ ویرایش';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EngineeringSeasons', 'COLUMN', N'Updated';
GO

DECLARE @description AS sql_variant;
SET @description = N'حذف شدگی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EngineeringSeasons', 'COLUMN', N'IsDeleted';
GO

DECLARE @description AS sql_variant;
SET @description = N'وضعیت فعال یا غیر فعال بودن';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EngineeringSeasons', 'COLUMN', N'IsActive';
GO

DECLARE @description AS sql_variant;
SET @description = N'ایجاد کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EngineeringSeasons', 'COLUMN', N'CreatorId';
GO

DECLARE @description AS sql_variant;
SET @description = N'تاریخ ایجاد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EngineeringSeasons', 'COLUMN', N'Created';
GO

DECLARE @description AS sql_variant;
SET @description = N'شناسه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EngineeringSeasons', 'COLUMN', N'Id';
GO

DECLARE @description AS sql_variant;
SET @description = N'ویرایش کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EngineeringProjectTypes', 'COLUMN', N'UpdaterId';
GO

DECLARE @description AS sql_variant;
SET @description = N'تاریخ ویرایش';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EngineeringProjectTypes', 'COLUMN', N'Updated';
GO

DECLARE @description AS sql_variant;
SET @description = N'حذف شدگی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EngineeringProjectTypes', 'COLUMN', N'IsDeleted';
GO

DECLARE @description AS sql_variant;
SET @description = N'وضعیت فعال یا غیر فعال بودن';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EngineeringProjectTypes', 'COLUMN', N'IsActive';
GO

DECLARE @description AS sql_variant;
SET @description = N'ایجاد کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EngineeringProjectTypes', 'COLUMN', N'CreatorId';
GO

DECLARE @description AS sql_variant;
SET @description = N'تاریخ ایجاد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EngineeringProjectTypes', 'COLUMN', N'Created';
GO

DECLARE @description AS sql_variant;
SET @description = N'شناسه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EngineeringProjectTypes', 'COLUMN', N'Id';
GO

DECLARE @description AS sql_variant;
SET @description = N'ویرایش کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EngineeringCategories', 'COLUMN', N'UpdaterId';
GO

DECLARE @description AS sql_variant;
SET @description = N'تاریخ ویرایش';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EngineeringCategories', 'COLUMN', N'Updated';
GO

DECLARE @description AS sql_variant;
SET @description = N'حذف شدگی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EngineeringCategories', 'COLUMN', N'IsDeleted';
GO

DECLARE @description AS sql_variant;
SET @description = N'وضعیت فعال یا غیر فعال بودن';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EngineeringCategories', 'COLUMN', N'IsActive';
GO

DECLARE @description AS sql_variant;
SET @description = N'ایجاد کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EngineeringCategories', 'COLUMN', N'CreatorId';
GO

DECLARE @description AS sql_variant;
SET @description = N'تاریخ ایجاد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EngineeringCategories', 'COLUMN', N'Created';
GO

DECLARE @description AS sql_variant;
SET @description = N'شناسه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EngineeringCategories', 'COLUMN', N'Id';
GO

DECLARE @description AS sql_variant;
SET @description = N'ویرایش کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EngineeringBranchs', 'COLUMN', N'UpdaterId';
GO

DECLARE @description AS sql_variant;
SET @description = N'تاریخ ویرایش';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EngineeringBranchs', 'COLUMN', N'Updated';
GO

DECLARE @description AS sql_variant;
SET @description = N'حذف شدگی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EngineeringBranchs', 'COLUMN', N'IsDeleted';
GO

DECLARE @description AS sql_variant;
SET @description = N'وضعیت فعال یا غیر فعال بودن';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EngineeringBranchs', 'COLUMN', N'IsActive';
GO

DECLARE @description AS sql_variant;
SET @description = N'ایجاد کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EngineeringBranchs', 'COLUMN', N'CreatorId';
GO

DECLARE @description AS sql_variant;
SET @description = N'تاریخ ایجاد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EngineeringBranchs', 'COLUMN', N'Created';
GO

DECLARE @description AS sql_variant;
SET @description = N'شناسه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EngineeringBranchs', 'COLUMN', N'Id';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251023114934_RefactorCategoryBranchSeasonMachineProjectType', N'8.0.8');
GO

COMMIT;
GO



