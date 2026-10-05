BEGIN TRANSACTION;
GO

DECLARE @description AS sql_variant;
SET @description = N'حجم شرح عملیات';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectOperations', 'COLUMN', N'Workload';
GO

DECLARE @description AS sql_variant;
SET @description = N'شناسه واحد اندازه گیری';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectOperations', 'COLUMN', N'UnitOfMeasurementId';
GO

DECLARE @description AS sql_variant;
SET @description = N'درصد تحمل';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectOperations', 'COLUMN', N'TolerancePercentage';
GO

DECLARE @description AS sql_variant;
SET @description = N'وضعیت شرح عملیات های پروژه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectOperations', 'COLUMN', N'ProjectOperationStatus';
GO

DECLARE @description AS sql_variant;
SET @description = N'اولویت';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectOperations', 'COLUMN', N'Priority';
GO

DECLARE @description AS sql_variant;
SET @description = N'هزینه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectOperations', 'COLUMN', N'Price';
GO

DECLARE @description AS sql_variant;
SET @description = N'حذف شدگی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectOperations', 'COLUMN', N'IsDeleted';
GO

DECLARE @description AS sql_variant;
SET @description = N'کالاهای در حال پیشرفت';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectOperations', 'COLUMN', N'GoodsInProgress';
GO

DECLARE @description AS sql_variant;
SET @description = N'توضیحات';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectOperations', 'COLUMN', N'Description';
GO

DECLARE @description AS sql_variant;
SET @description = N'ایجاد کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectOperations', 'COLUMN', N'CreatorId';
GO

DECLARE @description AS sql_variant;
SET @description = N'تاریخ ایجاد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectOperations', 'COLUMN', N'Created';
GO

DECLARE @description AS sql_variant;
SET @description = N'شناسه کمپانی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectOperations', 'COLUMN', N'CompanyId';
GO

ALTER TABLE [engineer].[ProjectOperations] ADD [BasePrice] decimal(18,2) NOT NULL DEFAULT 0.0;
DECLARE @description AS sql_variant;
SET @description = N'هزینه اولیه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectOperations', 'COLUMN', N'BasePrice';
GO

ALTER TABLE [engineer].[ProjectOperations] ADD [ChangedPrice] decimal(18,2) NOT NULL DEFAULT 0.0;
DECLARE @description AS sql_variant;
SET @description = N' آخریم هزینه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectOperations', 'COLUMN', N'ChangedPrice';
GO

DECLARE @description AS sql_variant;
SET @description = N'شناسه واحد اندازه‌گیری';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'OperationInfos', 'COLUMN', N'UnitOfMeasurementId';
GO

DECLARE @description AS sql_variant;
SET @description = N'اولویت';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'OperationInfos', 'COLUMN', N'Priority';
GO

DECLARE @description AS sql_variant;
SET @description = N'نام لاتین شرح عملیات';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'OperationInfos', 'COLUMN', N'OperationLatinName';
GO

DECLARE @description AS sql_variant;
SET @description = N'نام شرح عملیات';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'OperationInfos', 'COLUMN', N'OperationInfoName';
GO

DECLARE @description AS sql_variant;
SET @description = N'کد شرح عملیات';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'OperationInfos', 'COLUMN', N'OperationInfoCode';
GO

DECLARE @description AS sql_variant;
SET @description = N'حذف شدگی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'OperationInfos', 'COLUMN', N'IsDeleted';
GO

DECLARE @description AS sql_variant;
SET @description = N'وضعیت فعال یا غیر فعال بودن';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'OperationInfos', 'COLUMN', N'IsActive';
GO

DECLARE @description AS sql_variant;
SET @description = N'ایجاد کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'OperationInfos', 'COLUMN', N'CreatorId';
GO

DECLARE @description AS sql_variant;
SET @description = N'تاریخ ایجاد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'OperationInfos', 'COLUMN', N'Created';
GO

DECLARE @description AS sql_variant;
SET @description = N'شناسه کمپانی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'OperationInfos', 'COLUMN', N'CompanyId';
GO

ALTER TABLE [engineer].[OperationInfos] ADD [BasePrice] decimal(18,2) NOT NULL DEFAULT 0.0;
DECLARE @description AS sql_variant;
SET @description = N'قیمت پایه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'OperationInfos', 'COLUMN', N'BasePrice';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251118094200_AddBaseAndChangePrice', N'8.0.8');
GO

COMMIT;
GO



