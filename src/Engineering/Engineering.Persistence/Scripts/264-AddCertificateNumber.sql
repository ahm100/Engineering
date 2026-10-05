BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[TransportationRequestWarehouses] ADD [RefrenceId] bigint NULL;
GO

ALTER TABLE [engineer].[TransportationRequests] ADD [CertificateNumber] nvarchar(50) NULL;
GO

ALTER TABLE [engineer].[TransportationContractorPersonnels] ADD [CertificateNumber] nvarchar(50) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251222131124_AddCertificateNumber', N'8.0.8');
GO

COMMIT;
GO



