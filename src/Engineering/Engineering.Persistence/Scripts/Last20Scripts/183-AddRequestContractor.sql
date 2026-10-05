BEGIN TRANSACTION;
GO

CREATE TABLE [engineer].[RequestContractors] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [RequestNumber] bigint NULL DEFAULT (NEXT VALUE FOR engineer.RequestContractor_RequestNumber),
    [Status] int NOT NULL DEFAULT 1,
    [Volume] decimal(18,2) NOT NULL DEFAULT 0.0,
    [StatusDescription] nvarchar(1500) NULL,
    [Description] nvarchar(1500) NULL,
    [ProjectOperationDetailId] bigint NOT NULL,
    [ServiceInfoId] bigint NOT NULL,
    [CompanyId] bigint NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit),
    CONSTRAINT [PK_RequestContractors] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_RequestContractors_EngineeringServices_ServiceInfoId] FOREIGN KEY ([ServiceInfoId]) REFERENCES [engineer].[EngineeringServices] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_RequestContractors_ProjectOperationDetails_ProjectOperationDetailId] FOREIGN KEY ([ProjectOperationDetailId]) REFERENCES [engineer].[ProjectOperationDetails] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [engineer].[RequestContractorHistories] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [Status] int NOT NULL DEFAULT 1,
    [Volume] decimal(18,2) NOT NULL DEFAULT 0.0,
    [StatusDescription] nvarchar(1500) NULL,
    [Description] nvarchar(1500) NULL,
    [RequestContractorId] bigint NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit),
    CONSTRAINT [PK_RequestContractorHistories] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_RequestContractorHistories_RequestContractors_RequestContractorId] FOREIGN KEY ([RequestContractorId]) REFERENCES [engineer].[RequestContractors] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [engineer].[RequestContractorInquiries] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [ContractorId] bigint NOT NULL,
    [CurrencyId] bigint NOT NULL,
    [Amount] decimal(18,2) NOT NULL,
    [Discount] decimal(18,2) NULL,
    [Tax] decimal(18,2) NULL,
    [TotalAmount] decimal(18,2) NOT NULL,
    [Type] int NOT NULL,
    [FromDate] datetime2 NULL,
    [ToDate] datetime2 NULL,
    [IsConfirmed] bit NOT NULL DEFAULT CAST(0 AS bit),
    [ConfirmedUser] bigint NULL,
    [Description] nvarchar(1500) NULL,
    [RequestContractorId] bigint NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit),
    CONSTRAINT [PK_RequestContractorInquiries] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_RequestContractorInquiries_RequestContractors_RequestContractorId] FOREIGN KEY ([RequestContractorId]) REFERENCES [engineer].[RequestContractors] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [engineer].[RequestContractorInquiryDocuments] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [Url] nvarchar(max) NOT NULL,
    [RequestContractorInquiryId] bigint NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit),
    CONSTRAINT [PK_RequestContractorInquiryDocuments] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_RequestContractorInquiryDocuments_RequestContractorInquiries_RequestContractorInquiryId] FOREIGN KEY ([RequestContractorInquiryId]) REFERENCES [engineer].[RequestContractorInquiries] ([Id])
);
GO

CREATE INDEX [IX_RequestContractorHistories_RequestContractorId] ON [engineer].[RequestContractorHistories] ([RequestContractorId]);
GO

CREATE INDEX [IX_RequestContractorInquiries_RequestContractorId] ON [engineer].[RequestContractorInquiries] ([RequestContractorId]);
GO

CREATE INDEX [IX_RequestContractorInquiryDocuments_RequestContractorInquiryId] ON [engineer].[RequestContractorInquiryDocuments] ([RequestContractorInquiryId]);
GO

CREATE INDEX [IX_RequestContractors_ProjectOperationDetailId] ON [engineer].[RequestContractors] ([ProjectOperationDetailId]);
GO

CREATE INDEX [IX_RequestContractors_ServiceInfoId] ON [engineer].[RequestContractors] ([ServiceInfoId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250410152827_AddRequestContractor', N'8.0.8');
GO

COMMIT;
GO
