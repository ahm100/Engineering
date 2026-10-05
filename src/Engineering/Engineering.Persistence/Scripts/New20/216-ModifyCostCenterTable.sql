BEGIN TRANSACTION;
GO

DECLARE @description AS sql_variant;
SET @description = N'ویرایش کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'InformedUsers', 'COLUMN', N'UpdaterId';
GO

DECLARE @description AS sql_variant;
SET @description = N'تاریخ ویرایش';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'InformedUsers', 'COLUMN', N'Updated';
GO

DECLARE @description AS sql_variant;
SET @description = N'حذف شدگی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'InformedUsers', 'COLUMN', N'IsDeleted';
GO

DECLARE @description AS sql_variant;
SET @description = N'شناسه کارمند ';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'InformedUsers', 'COLUMN', N'EmployeeId';
GO

DECLARE @description AS sql_variant;
SET @description = N'ایجاد کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'InformedUsers', 'COLUMN', N'CreatorId';
GO

DECLARE @description AS sql_variant;
SET @description = N'تاریخ ایجاد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'InformedUsers', 'COLUMN', N'Created';
GO

DECLARE @description AS sql_variant;
SET @description = N'شناسه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'InformedUsers', 'COLUMN', N'Id';
GO

DECLARE @description AS sql_variant;
SET @description = N'شناسه انبار مرکز هزینه ';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenterWarehouses', 'COLUMN', N'WarehouseId';
GO

DECLARE @description AS sql_variant;
SET @description = N'ویرایش کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenterWarehouses', 'COLUMN', N'UpdaterId';
GO

DECLARE @description AS sql_variant;
SET @description = N'تاریخ ویرایش';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenterWarehouses', 'COLUMN', N'Updated';
GO

DECLARE @description AS sql_variant;
SET @description = N'حذف شدگی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenterWarehouses', 'COLUMN', N'IsDeleted';
GO

DECLARE @description AS sql_variant;
SET @description = N'پیش‌فرض بودن';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenterWarehouses', 'COLUMN', N'IsDefault';
GO

DECLARE @description AS sql_variant;
SET @description = N'ایجاد کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenterWarehouses', 'COLUMN', N'CreatorId';
GO

DECLARE @description AS sql_variant;
SET @description = N'تاریخ ایجاد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenterWarehouses', 'COLUMN', N'Created';
GO

DECLARE @description AS sql_variant;
SET @description = N'شناسه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenterWarehouses', 'COLUMN', N'Id';
GO

DECLARE @description AS sql_variant;
SET @description = N'ویرایش کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenterVirtualGroups', 'COLUMN', N'UpdaterId';
GO

DECLARE @description AS sql_variant;
SET @description = N'تاریخ ویرایش';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenterVirtualGroups', 'COLUMN', N'Updated';
GO

DECLARE @description AS sql_variant;
SET @description = N'عنوان گروه/کانال';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenterVirtualGroups', 'COLUMN', N'Title';
GO

DECLARE @description AS sql_variant;
SET @description = N'ارسال نفر روز دیروز';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenterVirtualGroups', 'COLUMN', N'SendYesterday';
GO

DECLARE @description AS sql_variant;
SET @description = N'ارسال نفر روز امروز';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenterVirtualGroups', 'COLUMN', N'SendToday';
GO

DECLARE @description AS sql_variant;
SET @description = N'لینک';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenterVirtualGroups', 'COLUMN', N'Link';
GO

DECLARE @description AS sql_variant;
SET @description = N'حذف شدگی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenterVirtualGroups', 'COLUMN', N'IsDeleted';
GO

DECLARE @description AS sql_variant;
SET @description = N'شناسه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenterVirtualGroups', 'COLUMN', N'Identifier';
GO

DECLARE @description AS sql_variant;
SET @description = N'ایجاد کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenterVirtualGroups', 'COLUMN', N'CreatorId';
GO

DECLARE @description AS sql_variant;
SET @description = N'تاریخ ایجاد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenterVirtualGroups', 'COLUMN', N'Created';
GO

DECLARE @description AS sql_variant;
SET @description = N'شناسه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenterVirtualGroups', 'COLUMN', N'Id';
GO

DECLARE @description AS sql_variant;
SET @description = N'نام کاربری ';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenterVirtualGroupAdmins', 'COLUMN', N'UserName';
GO

DECLARE @description AS sql_variant;
SET @description = N'ویرایش کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenterVirtualGroupAdmins', 'COLUMN', N'UpdaterId';
GO

DECLARE @description AS sql_variant;
SET @description = N'تاریخ ویرایش';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenterVirtualGroupAdmins', 'COLUMN', N'Updated';
GO

DECLARE @description AS sql_variant;
SET @description = N'شناسه شخص سوم ';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenterVirtualGroupAdmins', 'COLUMN', N'ThirdPartyId';
GO

DECLARE @description AS sql_variant;
SET @description = N'حذف شدگی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenterVirtualGroupAdmins', 'COLUMN', N'IsDeleted';
GO

DECLARE @description AS sql_variant;
SET @description = N'ایجاد کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenterVirtualGroupAdmins', 'COLUMN', N'CreatorId';
GO

DECLARE @description AS sql_variant;
SET @description = N'تاریخ ایجاد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenterVirtualGroupAdmins', 'COLUMN', N'Created';
GO

DECLARE @description AS sql_variant;
SET @description = N'شناسه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenterVirtualGroupAdmins', 'COLUMN', N'Id';
GO

DECLARE @description AS sql_variant;
SET @description = N'ویرایش کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenterTypes', 'COLUMN', N'UpdaterId';
GO

DECLARE @description AS sql_variant;
SET @description = N'تاریخ ویرایش';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenterTypes', 'COLUMN', N'Updated';
GO

DECLARE @description AS sql_variant;
SET @description = N'حذف شدگی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenterTypes', 'COLUMN', N'IsDeleted';
GO

DECLARE @description AS sql_variant;
SET @description = N'وضعیت فعال یا غیر فعال بودن';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenterTypes', 'COLUMN', N'IsActive';
GO

DECLARE @description AS sql_variant;
SET @description = N'ایجاد کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenterTypes', 'COLUMN', N'CreatorId';
GO

DECLARE @description AS sql_variant;
SET @description = N'تاریخ ایجاد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenterTypes', 'COLUMN', N'Created';
GO

DECLARE @description AS sql_variant;
SET @description = N'شناسه مرکز هزینه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenterTypes', 'COLUMN', N'CostCenterTypeCode';
GO

DECLARE @description AS sql_variant;
SET @description = N'شناسه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenterTypes', 'COLUMN', N'Id';
GO

DECLARE @description AS sql_variant;
SET @description = N'وضعیت آب و هوا';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenters', 'COLUMN', N'WeatherState';
GO

DECLARE @description AS sql_variant;
SET @description = N'ویرایش کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenters', 'COLUMN', N'UpdaterId';
GO

DECLARE @description AS sql_variant;
SET @description = N'تاریخ ویرایش';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenters', 'COLUMN', N'Updated';
GO

DECLARE @description AS sql_variant;
SET @description = N'کد پستی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenters', 'COLUMN', N'PostalCode';
GO

DECLARE @description AS sql_variant;
SET @description = N'روزهای بدون پروژه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenters', 'COLUMN', N'NoOperationDays';
GO

DECLARE @description AS sql_variant;
SET @description = N'طول جغرافیایی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenters', 'COLUMN', N'Longitude';
GO

DECLARE @description AS sql_variant;
SET @description = N'عرض جغرافیایی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenters', 'COLUMN', N'Latitude';
GO

DECLARE @description AS sql_variant;
SET @description = N'حذف شدگی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenters', 'COLUMN', N'IsDeleted';
GO

DECLARE @description AS sql_variant;
SET @description = N'وضعیت فعال یا غیر فعال بودن';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenters', 'COLUMN', N'IsActive';
GO

DECLARE @description AS sql_variant;
SET @description = N'توضیحات';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenters', 'COLUMN', N'Description';
GO

DECLARE @description AS sql_variant;
SET @description = N'ایجاد کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenters', 'COLUMN', N'CreatorId';
GO

DECLARE @description AS sql_variant;
SET @description = N'تاریخ ایجاد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenters', 'COLUMN', N'Created';
GO

DECLARE @description AS sql_variant;
SET @description = N'نام مرکز هزینه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenters', 'COLUMN', N'CostCenterName';
GO

DECLARE @description AS sql_variant;
SET @description = N'کد مرکز هزینه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenters', 'COLUMN', N'CostCenterCode';
GO

DECLARE @description AS sql_variant;
SET @description = N'شناسه کمپانی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenters', 'COLUMN', N'CompanyId';
GO

DECLARE @description AS sql_variant;
SET @description = N'شناسه شهر';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenters', 'COLUMN', N'CityId';
GO

DECLARE @description AS sql_variant;
SET @description = N'آدرس مرکز هزینه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenters', 'COLUMN', N'Address';
GO

DECLARE @description AS sql_variant;
SET @description = N'شناسه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenters', 'COLUMN', N'Id';
GO

DECLARE @description AS sql_variant;
SET @description = N'ویرایش کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenterAuthorizedUsers', 'COLUMN', N'UpdaterId';
GO

DECLARE @description AS sql_variant;
SET @description = N'تاریخ ویرایش';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenterAuthorizedUsers', 'COLUMN', N'Updated';
GO

DECLARE @description AS sql_variant;
SET @description = N'حذف شدگی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenterAuthorizedUsers', 'COLUMN', N'IsDeleted';
GO

DECLARE @description AS sql_variant;
SET @description = N'ایجاد کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenterAuthorizedUsers', 'COLUMN', N'CreatorId';
GO

DECLARE @description AS sql_variant;
SET @description = N'تاریخ ایجاد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenterAuthorizedUsers', 'COLUMN', N'Created';
GO

DECLARE @description AS sql_variant;
SET @description = N'شناسه کاربران مجاز برای دیدن مرکز هزینه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenterAuthorizedUsers', 'COLUMN', N'AuthorizedUserId';
GO

DECLARE @description AS sql_variant;
SET @description = N'شناسه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenterAuthorizedUsers', 'COLUMN', N'Id';
GO

DECLARE @description AS sql_variant;
SET @description = N'ویرایش کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenterAuthorizedRoles', 'COLUMN', N'UpdaterId';
GO

DECLARE @description AS sql_variant;
SET @description = N'تاریخ ویرایش';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenterAuthorizedRoles', 'COLUMN', N'Updated';
GO

DECLARE @description AS sql_variant;
SET @description = N'حذف شدگی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenterAuthorizedRoles', 'COLUMN', N'IsDeleted';
GO

DECLARE @description AS sql_variant;
SET @description = N'ایجاد کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenterAuthorizedRoles', 'COLUMN', N'CreatorId';
GO

DECLARE @description AS sql_variant;
SET @description = N'تاریخ ایجاد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenterAuthorizedRoles', 'COLUMN', N'Created';
GO

DECLARE @description AS sql_variant;
SET @description = N'شناسه نقش های مجاز برای دیدن مرکز هزینه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenterAuthorizedRoles', 'COLUMN', N'AuthorizedRoleId';
GO

DECLARE @description AS sql_variant;
SET @description = N'شناسه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenterAuthorizedRoles', 'COLUMN', N'Id';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251026051238_ModifyCostCenterTable', N'8.0.8');
GO

COMMIT;
GO



