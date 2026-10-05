using Engineering.Application.Abstractions.Data.SessionRecords;
using Engineering.Application.Services.SessionRecords.Contracts.GetSessionRecordDetail;
using Engineering.Application.Services.SessionRecords.Contracts.GetSessionRecords;
using Engineering.Domain.Entities.SessionRecords;
using Engineering.Domain.Entities.SessionRecords.Enums;
using Gita.Backend.Shared.Domain.Base.Results;
using Gita.Backend.Shared.Domain.Extensions;

namespace Engineering.Persistence.Repositories.SessionRecords;

public class SessionRecordRepository : BaseRepository<EngineeringDBContext, SessionRecord>, ISessionRecordRepository
{
    public SessionRecordRepository(EngineeringDBContext context) : base (context)
    {
    }

    public async Task<SessionRecord?> GetById(
        long id)
        => await DbSet.FirstOrDefaultAsync(e => !e.IsDeleted && e.Id == id);

    public async Task<Result<(List<GetSessionRecordsResponseModel> Data, int RowCount)>> GetSessionRecords(
        string? titleFa, string? titleEn, string? code,
        SessionCategory? category, SessionType? type, long? projectId,
        int pageIndex, int pageSize, CT ct)
    {
        var query = DbSet.Where(e => !e.IsDeleted);

        if (projectId > 0)
            query = query.Where(e => e.ProjctId == projectId);

        if (titleFa != null)
            query = query.Where(e => e.TitleFa.Contains(titleFa));

        if (titleEn != null)
            query = query.Where(e => e.TitleFa.Contains(titleEn));

        if (category > 0)
            query = query.Where(e => e.SessionCategory == category);

        if (type > 0)
            query = query.Where(e => e.SessionType == type);

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 && pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var data = await query
            .Select(e => new GetSessionRecordsResponseModel
            {
                Id = e.Id,
                TitleEn = e.TitleEn,
                TitleFa = e.TitleFa,
                Category = e.SessionCategory,
                Type = e.SessionType,
                ProjectId = e.ProjctId,
                ProjectName = e.Project.ProjectName,
                ContractId = e.ContractId,
                ContractNumber = e.Contract!.ContractNumber,
                Location = e.Location,
                SessionDate = e.SessionDate,
                EndTime = e.EndTime,
                StartTime = e.StartTime
            }).ToListAsync(ct);

        return (data, count);
    }

    public async Task<GetSessionRecordDetailResponse?> GetSessionRecordDetail(
        long id, CT ct)
        => await DbSet.Where(e => e.Id == id)
            .Select(e => new GetSessionRecordDetailResponse
            {
                Id = e.Id,
                TitleEn = e.TitleEn,
                TitleFa = e.TitleFa,
                Category = e.SessionCategory,
                Type = e.SessionType,
                ProjectId = e.ProjctId,
                ProjectName = e.Project.ProjectName,
                ContractId = e.ContractId,
                ContractNumber = e.Contract!.ContractNumber,
                Location = e.Location,
                SessionDate = e.SessionDate,
                EndTime = e.EndTime,
                StartTime = e.StartTime,
                Items = e.SessionItems
                    .Where(i => !i.IsDeleted)
                    .Select(i => new SessionItemReadModel(
                        i.Id, i.Descriotion))
                    .ToList(),
                Actions = e.SessionRecordActions
                    .Where(a => !a.IsDeleted)
                    .Select(a => new SessionRecordActionReadModel{
                        Id = a.Id,
                        Description = a.Description,
                        Status = a.Status,
                        DeadLine = a.Deadline,
                        UserId = a.UserId})
                    .ToList(),
                                Docs = e.SessionRecordDocs
                    .Where(d => !d.IsDeleted)
                    .Select(d => new SessionRecordDocReadModel(
                        d.Id,
                        d.Type,
                        d.Type.GetEnumDescription(),
                        d.URL
                        ))
                    .ToList(),
                                Invitees = e.SessionInvitees
                    .Where(i => !i.IsDeleted)
                    .Select(i => new SessionInviteeReadModel{
                        Id = i.Id,
                        UserId = i.UserId,
                        Status = i.Status,
                        CompanyId = i.CompanyId
                    })
                    .ToList()
            }).FirstOrDefaultAsync(ct);
}
