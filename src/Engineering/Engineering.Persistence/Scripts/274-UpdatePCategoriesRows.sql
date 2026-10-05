BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260118104455_UpdatePCategoriesRows', N'8.0.8');
GO

INSERT INTO EngineeringDB.engineer.ProjectCategories
    (ProjectId, CategoryId, Created, CreatorId, Updated, UpdaterId, IsDeleted)
SELECT 
    p.Id,
    p.CategoryId,
    p.Created,   -- Created date/time
    p.CreatorId,               -- CreatorId = p.CreatorId
    p.Updated,            -- Updated = p.Updated
    p.UpdaterId,            -- UpdaterId = p.UpdaterId
    p.IsDeleted                -- IsDeleted = p.IsDeleted
FROM EngineeringDB.engineer.Projects p
LEFT JOIN EngineeringDB.engineer.ProjectCategories pc ON pc.ProjectId = p.Id AND pc.CategoryId = p.CategoryId
WHERE pc.Id IS NULL  and p.CategoryId is not null and p.IsDeleted != 1;


COMMIT;
GO



