BEGIN TRANSACTION;
GO

CREATE TABLE [engineer].[TransportationRequestHistories] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [StartDate] datetime2 NULL,
    [EndDate] datetime2 NULL,
    [Description] nvarchar(1500) NULL,
    [DriverId] bigint NULL,
    [DriverName] nvarchar(max) NULL,
    [AccountNumber] nvarchar(50) NULL,
    [BankId] bigint NULL,
    [CardNumber] nvarchar(30) NULL,
    [AccountName] nvarchar(250) NULL,
    [IBAN] nvarchar(50) NULL,
    [Price] decimal(18,2) NULL,
    [CurrencyUnitId] bigint NULL,
    [AccountDescription] nvarchar(1500) NULL,
    [TransportationRequestStatus] int NULL,
    [TransportationPaymentType] int NULL,
    [ManagerDescription] nvarchar(1500) NULL,
    [ConfirmUserId] bigint NULL,
    [ConfirmDate] datetime2 NULL,
    [PaymentOrderId] bigint NULL,
    [PaymentDate] datetime2 NULL,
    [TransportationRequestId] bigint NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit),
    CONSTRAINT [PK_TransportationRequestHistories] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TransportationRequestHistories_TransportationRequests_TransportationRequestId] FOREIGN KEY ([TransportationRequestId]) REFERENCES [engineer].[TransportationRequests] ([Id])
);
GO

CREATE INDEX [IX_TransportationRequestHistories_TransportationRequestId] ON [engineer].[TransportationRequestHistories] ([TransportationRequestId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250504091417_AddTransportationHistory', N'8.0.8');
GO

COMMIT;
GO