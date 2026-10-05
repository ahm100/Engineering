BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[ShippingCosts] ADD [FromDate] datetime2 NULL;
DECLARE @description AS sql_variant;
SET @description = N'از تاریخ';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ShippingCosts', 'COLUMN', N'FromDate';
GO

ALTER TABLE [engineer].[ShippingCosts] ADD [ToDate] datetime2 NULL;
DECLARE @description AS sql_variant;
SET @description = N'تا تاریخ';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ShippingCosts', 'COLUMN', N'ToDate';
GO

DECLARE @description AS sql_variant;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', N'engineer', 'TABLE', N'EngineeringServices', 'COLUMN', N'PreferentialReferenceCode';
SET @description = N'˜Ï ãÑÌÚ ÊÝÕ?á?';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EngineeringServices', 'COLUMN', N'PreferentialReferenceCode';
GO

CREATE TABLE [engineer].[TransportationContractorMachines] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [NumberPlate] nvarchar(10) NOT NULL,
    [Vin] nvarchar(25) NULL,
    [Color] nvarchar(100) NULL,
    [MachineTypeId] bigint NOT NULL,
    [TransportationContractorId] bigint NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_TransportationContractorMachines] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TransportationContractorMachines_MachineTypes_MachineTypeId] FOREIGN KEY ([MachineTypeId]) REFERENCES [engineer].[MachineTypes] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_TransportationContractorMachines_TransportationContractors_TransportationContractorId] FOREIGN KEY ([TransportationContractorId]) REFERENCES [engineer].[TransportationContractors] ([Id]) ON DELETE NO ACTION
);
DECLARE @description AS sql_variant;
SET @description = N'شناسه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'TransportationContractorMachines', 'COLUMN', N'Id';
SET @description = N'پلاک';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'TransportationContractorMachines', 'COLUMN', N'NumberPlate';
SET @description = N'شماره شاسی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'TransportationContractorMachines', 'COLUMN', N'Vin';
SET @description = N'رنگ';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'TransportationContractorMachines', 'COLUMN', N'Color';
SET @description = N'تاریخ ایجاد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'TransportationContractorMachines', 'COLUMN', N'Created';
SET @description = N'ایجاد کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'TransportationContractorMachines', 'COLUMN', N'CreatorId';
SET @description = N'تاریخ ویرایش';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'TransportationContractorMachines', 'COLUMN', N'Updated';
SET @description = N'ویرایش کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'TransportationContractorMachines', 'COLUMN', N'UpdaterId';
SET @description = N'حذف شدگی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'TransportationContractorMachines', 'COLUMN', N'IsDeleted';
SET @description = N'وضعیت فعال یا غیر فعال بودن';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'TransportationContractorMachines', 'COLUMN', N'IsActive';
GO

CREATE TABLE [engineer].[TransportationContractorPersonnelMachines] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [TransportationContractorPersonnelId] bigint NOT NULL,
    [TransportationContractorMachineId] bigint NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_TransportationContractorPersonnelMachines] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TransportationContractorPersonnelMachines_TransportationContractorMachines_TransportationContractorMachineId] FOREIGN KEY ([TransportationContractorMachineId]) REFERENCES [engineer].[TransportationContractorMachines] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_TransportationContractorPersonnelMachines_TransportationContractorPersonnels_TransportationContractorPersonnelId] FOREIGN KEY ([TransportationContractorPersonnelId]) REFERENCES [engineer].[TransportationContractorPersonnels] ([Id]) ON DELETE NO ACTION
);
DECLARE @description AS sql_variant;
SET @description = N'شناسه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'TransportationContractorPersonnelMachines', 'COLUMN', N'Id';
SET @description = N'تاریخ ایجاد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'TransportationContractorPersonnelMachines', 'COLUMN', N'Created';
SET @description = N'ایجاد کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'TransportationContractorPersonnelMachines', 'COLUMN', N'CreatorId';
SET @description = N'تاریخ ویرایش';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'TransportationContractorPersonnelMachines', 'COLUMN', N'Updated';
SET @description = N'ویرایش کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'TransportationContractorPersonnelMachines', 'COLUMN', N'UpdaterId';
SET @description = N'حذف شدگی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'TransportationContractorPersonnelMachines', 'COLUMN', N'IsDeleted';
SET @description = N'وضعیت فعال یا غیر فعال بودن';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'TransportationContractorPersonnelMachines', 'COLUMN', N'IsActive';
GO

CREATE INDEX [IX_TransportationContractorMachines_MachineTypeId] ON [engineer].[TransportationContractorMachines] ([MachineTypeId]);
GO

CREATE INDEX [IX_TransportationContractorMachines_TransportationContractorId] ON [engineer].[TransportationContractorMachines] ([TransportationContractorId]);
GO

CREATE INDEX [IX_TransportationContractorPersonnelMachines_TransportationContractorMachineId] ON [engineer].[TransportationContractorPersonnelMachines] ([TransportationContractorMachineId]);
GO

CREATE INDEX [IX_TransportationContractorPersonnelMachines_TransportationContractorPersonnelId] ON [engineer].[TransportationContractorPersonnelMachines] ([TransportationContractorPersonnelId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251113142138_AddLogesticNewEntities', N'8.0.8');
GO

COMMIT;
GO



