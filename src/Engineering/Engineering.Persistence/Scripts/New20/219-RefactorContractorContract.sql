BEGIN TRANSACTION;
GO

DECLARE @description AS sql_variant;
SET @description = N'ویرایش کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContractTypes', 'COLUMN', N'UpdaterId';
GO

DECLARE @description AS sql_variant;
SET @description = N'تاریخ ویرایش';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContractTypes', 'COLUMN', N'Updated';
GO

DECLARE @description AS sql_variant;
SET @description = N'حذف شدگی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContractTypes', 'COLUMN', N'IsDeleted';
GO

DECLARE @description AS sql_variant;
SET @description = N'وضعیت فعال یا غیر فعال بودن';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContractTypes', 'COLUMN', N'IsActive';
GO

DECLARE @description AS sql_variant;
SET @description = N'ایجاد کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContractTypes', 'COLUMN', N'CreatorId';
GO

DECLARE @description AS sql_variant;
SET @description = N'تاریخ ایجاد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContractTypes', 'COLUMN', N'Created';
GO

DECLARE @description AS sql_variant;
SET @description = N'شناسه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContractTypes', 'COLUMN', N'Id';
GO

DECLARE @description AS sql_variant;
SET @description = N'ویرایش کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContracts', 'COLUMN', N'UpdaterId';
GO

DECLARE @description AS sql_variant;
SET @description = N'تاریخ ویرایش';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContracts', 'COLUMN', N'Updated';
GO

DECLARE @description AS sql_variant;
SET @description = N'حذف شدگی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContracts', 'COLUMN', N'IsDeleted';
GO

DECLARE @description AS sql_variant;
SET @description = N'ایجاد کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContracts', 'COLUMN', N'CreatorId';
GO

DECLARE @description AS sql_variant;
SET @description = N'تاریخ ایجاد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContracts', 'COLUMN', N'Created';
GO


DECLARE @description AS sql_variant;
SET @description = N'شناسه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContracts', 'COLUMN', N'Id';
GO

DECLARE @description AS sql_variant;
SET @description = N'ویرایش کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContractHistories', 'COLUMN', N'UpdaterId';
GO

DECLARE @description AS sql_variant;
SET @description = N'تاریخ ویرایش';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContractHistories', 'COLUMN', N'Updated';
GO

DECLARE @description AS sql_variant;
SET @description = N'حذف شدگی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContractHistories', 'COLUMN', N'IsDeleted';
GO

DECLARE @description AS sql_variant;
SET @description = N'ایجاد کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContractHistories', 'COLUMN', N'CreatorId';
GO

DECLARE @description AS sql_variant;
SET @description = N'تاریخ ایجاد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContractHistories', 'COLUMN', N'Created';
GO


DECLARE @description AS sql_variant;
SET @description = N'شناسه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContractHistories', 'COLUMN', N'Id';
GO

DECLARE @description AS sql_variant;
SET @description = N'ویرایش کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContractHeaders', 'COLUMN', N'UpdaterId';
GO

DECLARE @description AS sql_variant;
SET @description = N'تاریخ ویرایش';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContractHeaders', 'COLUMN', N'Updated';
GO

DECLARE @description AS sql_variant;
SET @description = N'حذف شدگی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContractHeaders', 'COLUMN', N'IsDeleted';
GO

DECLARE @description AS sql_variant;
SET @description = N'ایجاد کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContractHeaders', 'COLUMN', N'CreatorId';
GO

DECLARE @description AS sql_variant;
SET @description = N'تاریخ ایجاد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContractHeaders', 'COLUMN', N'Created';
GO

DECLARE @description AS sql_variant;
SET @description = N'شناسه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContractHeaders', 'COLUMN', N'Id';
GO

DECLARE @description AS sql_variant;
SET @description = N'ویرایش کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContractHeaderHistories', 'COLUMN', N'UpdaterId';
GO

DECLARE @description AS sql_variant;
SET @description = N'تاریخ ویرایش';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContractHeaderHistories', 'COLUMN', N'Updated';
GO

DECLARE @description AS sql_variant;
SET @description = N'حذف شدگی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContractHeaderHistories', 'COLUMN', N'IsDeleted';
GO

DECLARE @description AS sql_variant;
SET @description = N'ایجاد کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContractHeaderHistories', 'COLUMN', N'CreatorId';
GO

DECLARE @description AS sql_variant;
SET @description = N'تاریخ ایجاد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContractHeaderHistories', 'COLUMN', N'Created';
GO

DECLARE @description AS sql_variant;
SET @description = N'شناسه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContractHeaderHistories', 'COLUMN', N'Id';
GO

DECLARE @description AS sql_variant;
SET @description = N'ویرایش کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContractHeaderDocuments', 'COLUMN', N'UpdaterId';
GO

DECLARE @description AS sql_variant;
SET @description = N'تاریخ ویرایش';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContractHeaderDocuments', 'COLUMN', N'Updated';
GO

DECLARE @description AS sql_variant;
SET @description = N'حذف شدگی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContractHeaderDocuments', 'COLUMN', N'IsDeleted';
GO

DECLARE @description AS sql_variant;
SET @description = N'ایجاد کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContractHeaderDocuments', 'COLUMN', N'CreatorId';
GO

DECLARE @description AS sql_variant;
SET @description = N'تاریخ ایجاد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContractHeaderDocuments', 'COLUMN', N'Created';
GO

DECLARE @description AS sql_variant;
SET @description = N'شناسه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContractHeaderDocuments', 'COLUMN', N'Id';
GO

DECLARE @description AS sql_variant;
SET @description = N'ویرایش کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContractDetailThirdParties', 'COLUMN', N'UpdaterId';
GO

DECLARE @description AS sql_variant;
SET @description = N'تاریخ ویرایش';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContractDetailThirdParties', 'COLUMN', N'Updated';
GO

DECLARE @description AS sql_variant;
SET @description = N'حذف شدگی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContractDetailThirdParties', 'COLUMN', N'IsDeleted';
GO

DECLARE @description AS sql_variant;
SET @description = N'ایجاد کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContractDetailThirdParties', 'COLUMN', N'CreatorId';
GO

DECLARE @description AS sql_variant;
SET @description = N'تاریخ ایجاد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContractDetailThirdParties', 'COLUMN', N'Created';
GO

DECLARE @description AS sql_variant;
SET @description = N'شناسه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContractDetailThirdParties', 'COLUMN', N'Id';
GO

DECLARE @description AS sql_variant;
SET @description = N'ویرایش کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContractDetailSkills', 'COLUMN', N'UpdaterId';
GO

DECLARE @description AS sql_variant;
SET @description = N'تاریخ ویرایش';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContractDetailSkills', 'COLUMN', N'Updated';
GO

DECLARE @description AS sql_variant;
SET @description = N'حذف شدگی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContractDetailSkills', 'COLUMN', N'IsDeleted';
GO

DECLARE @description AS sql_variant;
SET @description = N'ایجاد کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContractDetailSkills', 'COLUMN', N'CreatorId';
GO

DECLARE @description AS sql_variant;
SET @description = N'تاریخ ایجاد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContractDetailSkills', 'COLUMN', N'Created';
GO

DECLARE @description AS sql_variant;
SET @description = N'شناسه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContractDetailSkills', 'COLUMN', N'Id';
GO

DECLARE @description AS sql_variant;
SET @description = N'ویرایش کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContractDetailServices', 'COLUMN', N'UpdaterId';
GO

DECLARE @description AS sql_variant;
SET @description = N'تاریخ ویرایش';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContractDetailServices', 'COLUMN', N'Updated';
GO

DECLARE @description AS sql_variant;
SET @description = N'حذف شدگی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContractDetailServices', 'COLUMN', N'IsDeleted';
GO

DECLARE @description AS sql_variant;
SET @description = N'ایجاد کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContractDetailServices', 'COLUMN', N'CreatorId';
GO

DECLARE @description AS sql_variant;
SET @description = N'تاریخ ایجاد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContractDetailServices', 'COLUMN', N'Created';
GO

DECLARE @description AS sql_variant;
SET @description = N'شناسه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContractDetailServices', 'COLUMN', N'Id';
GO

DECLARE @description AS sql_variant;
SET @description = N'ویرایش کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContractDetails', 'COLUMN', N'UpdaterId';
GO

DECLARE @description AS sql_variant;
SET @description = N'تاریخ ویرایش';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContractDetails', 'COLUMN', N'Updated';
GO

DECLARE @description AS sql_variant;
SET @description = N'حذف شدگی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContractDetails', 'COLUMN', N'IsDeleted';
GO

DECLARE @description AS sql_variant;
SET @description = N'وضعیت فعال یا غیر فعال بودن';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContractDetails', 'COLUMN', N'IsActive';
GO

DECLARE @description AS sql_variant;
SET @description = N'ایجاد کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContractDetails', 'COLUMN', N'CreatorId';
GO

DECLARE @description AS sql_variant;
SET @description = N'تاریخ ایجاد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContractDetails', 'COLUMN', N'Created';
GO

DECLARE @description AS sql_variant;
SET @description = N'شناسه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContractDetails', 'COLUMN', N'Id';
GO

DECLARE @description AS sql_variant;
SET @description = N'ویرایش کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContractDetailPrices', 'COLUMN', N'UpdaterId';
GO

DECLARE @description AS sql_variant;
SET @description = N'تاریخ ویرایش';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContractDetailPrices', 'COLUMN', N'Updated';
GO

DECLARE @description AS sql_variant;
SET @description = N'حذف شدگی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContractDetailPrices', 'COLUMN', N'IsDeleted';
GO

DECLARE @description AS sql_variant;
SET @description = N'وضعیت فعال یا غیر فعال بودن';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContractDetailPrices', 'COLUMN', N'IsActive';
GO

DECLARE @description AS sql_variant;
SET @description = N'ایجاد کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContractDetailPrices', 'COLUMN', N'CreatorId';
GO

DECLARE @description AS sql_variant;
SET @description = N'تاریخ ایجاد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContractDetailPrices', 'COLUMN', N'Created';
GO

DECLARE @description AS sql_variant;
SET @description = N'شناسه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContractDetailPrices', 'COLUMN', N'Id';
GO

DECLARE @description AS sql_variant;
SET @description = N'ویرایش کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContractDetailCostOvers', 'COLUMN', N'UpdaterId';
GO

DECLARE @description AS sql_variant;
SET @description = N'تاریخ ویرایش';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContractDetailCostOvers', 'COLUMN', N'Updated';
GO

DECLARE @description AS sql_variant;
SET @description = N'حذف شدگی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContractDetailCostOvers', 'COLUMN', N'IsDeleted';
GO

DECLARE @description AS sql_variant;
SET @description = N'ایجاد کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContractDetailCostOvers', 'COLUMN', N'CreatorId';
GO

DECLARE @description AS sql_variant;
SET @description = N'تاریخ ایجاد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContractDetailCostOvers', 'COLUMN', N'Created';
GO

DECLARE @description AS sql_variant;
SET @description = N'شناسه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContractDetailCostOvers', 'COLUMN', N'Id';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251022125041_RefactorContractorContract', N'8.0.8');
GO

COMMIT;
GO



