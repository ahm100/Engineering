using Engineering.Application.Abstractions.Data;
using Engineering.Application.Abstractions.Data.MetaEntities;
using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Application.Abstractions.Data.Synonyms.Meta.Organizations;
using Engineering.Application.Services.GoodsManagerAssignments.Contracts.CreateGoodsManagerAssignment;
using Engineering.Application.Services.GoodsManagerAssignments.Contracts.DeleteGoodsManagerAssignment;
using Engineering.Application.Services.GoodsManagerAssignments.Contracts.GetFilteredGoodsManagerAssignments;
using Engineering.Application.Services.GoodsManagerAssignments.Contracts.GetGoodsManagerAssignmentHistory;
using Engineering.Application.Services.GoodsManagerAssignments.Contracts.GetGoodsManagerAssignmentsById;
using Engineering.Application.Services.GoodsManagerAssignments.Contracts.UpdateGoodsManagerAssignment;
using Engineering.Domain.Errors.RequestGoodsSupplies;

namespace Engineering.Application.Services.GoodsManagerAssignments;

public partial class GoodsManagerAssignmentLogic : IGoodsManagerAssignmentLogic
{
    private readonly IMediator _mediator;
    private readonly ILogger<GoodsManagerAssignmentLogic> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGoodsManagerAssignmentRepository _repository;
    private readonly IGoodsManagerAssignmentHistoryRepository _historyRepo;
    private readonly IViewProductRepository _productRepo;
    private readonly IViewThirdPartyRepository _thirdPartyRepo;
    private readonly IViewOrganizationRepository _organizationRepo;

    public GoodsManagerAssignmentLogic(
        IMediator mediator,
        ILogger<GoodsManagerAssignmentLogic> logger,
        IUnitOfWork unitOfWork,
        IGoodsManagerAssignmentRepository repository,
        IGoodsManagerAssignmentHistoryRepository historyRepo,
        IViewProductRepository productRepo,
        IViewThirdPartyRepository thirdPartyRepo,
        IViewOrganizationRepository organizationRepo)
    {
        _mediator = mediator;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _repository = repository;
        _historyRepo = historyRepo;
        _productRepo = productRepo;
        _thirdPartyRepo = thirdPartyRepo;
        _organizationRepo = organizationRepo;
    }

    public async Task<Result<CreateGoodsManagerAssignmentResponse?>> CreateGoodsManagerAssignment(
        CreateGoodsManagerAssignmentRequest request, CT ct)
    {
        // 1. Log
        _logger.LogInformation("CreateGoodsManagerAssignment");

        // 2. Validate
        var isValidRequest = await request.IsValidAsync<CreateGoodsManagerAssignmentValidator, CreateGoodsManagerAssignmentRequest>(ct);
        if (isValidRequest.IsBad())
            return isValidRequest.Failure<CreateGoodsManagerAssignmentResponse>()!;

        // 3. Internal Command
        var result = await CreateGoodsManagerAssignmentCommand(request, ct);
        if (result.IsBad())
            return result.Failure<CreateGoodsManagerAssignmentResponse>()!;

        // 4. Commit (ONLY here)
        await _unitOfWork.CommitAsync(ct);

        // 5. Return
        return new CreateGoodsManagerAssignmentResponse(true);
    }

    public async Task<Result<UpdateGoodsManagerAssignmentResponse?>> UpdateGoodsManagerAssignment(
        UpdateGoodsManagerAssignmentRequest request, CT ct)
    {
        // 1. Log
        _logger.LogInformation("UpdateGoodsManagerAssignment");

        // 2. Validate
        var isValidRequest = await request.IsValidAsync<UpdateGoodsManagerAssignmentValidator, UpdateGoodsManagerAssignmentRequest>(ct);
        if (isValidRequest.IsBad())
            return isValidRequest.Failure<UpdateGoodsManagerAssignmentResponse>()!;

        // 3. Internal Command
        var result = await UpdateGoodsManagerAssignmentCommand(request, ct);
        if (result.IsBad())
            return result.Failure<UpdateGoodsManagerAssignmentResponse>()!;

        // 4. Commit (ONLY here)
        await _unitOfWork.CommitAsync(ct);

        // 5. Return
        return new UpdateGoodsManagerAssignmentResponse(true);
    }

    public async Task<Result<DeleteGoodsManagerAssignmentResponse?>> DeleteGoodsManagerAssignment(
        DeleteGoodsManagerAssignmentRequest request,
        CT ct)
    {
        // 1. Log
        _logger.LogInformation("DeleteGoodsManagerAssignment");

        // 2. Validate
        var isValidRequest = await request.IsValidAsync<DeleteGoodsManagerAssignmentValidator, DeleteGoodsManagerAssignmentRequest>(ct);
        if (isValidRequest.IsBad())
            return isValidRequest.Failure<DeleteGoodsManagerAssignmentResponse>()!;

        // 3. Internal Command
        var result = await DeleteGoodsManagerAssignmentCommand(request.OrganizationId, ct);
        if (result.IsBad())
            return result.Failure<DeleteGoodsManagerAssignmentResponse>()!;

        // 4. Commit (ONLY here)
        await _unitOfWork.CommitAsync(ct);

        // 5. Return
        return new DeleteGoodsManagerAssignmentResponse(true);
    }

    public async Task<Result<GetFilteredGoodsManagerAssignmentsResponse?>> GetFilteredGoodsManagerAssignments(
        GetFilteredGoodsManagerAssignmentsRequest request, CT ct)
    {
        // 1. Log
        _logger.LogInformation("GetFilteredGoodsManagerAssignments");

        // 2. Validate
        var isValidRequest = await request.IsValidAsync<GetFilteredGoodsManagerAssignmentsValidator, GetFilteredGoodsManagerAssignmentsRequest>(ct);
        if (isValidRequest.IsBad())
            return isValidRequest.Failure<GetFilteredGoodsManagerAssignmentsResponse>()!;

        // 3. Load
        var entities = await _repository.GetFiltered(
            request.Ids, request.OrganizationId, request.ProductId, ct);

        // 4. Enrich
        var result = await EnrichAssignments(entities, ct);

        // 5. Search
        result = ApplyFilter(result, request.FilterData);

        // RowCount must be calculated AFTER filtering and BEFORE pagination.
        var rowCount = result.Count;

        // 6. Pagination
        if (request.PageIndex > 0 && request.PageSize > 0)
        {
            result = result
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();
        }

        var ids = result.NullListed(x => x.ManagerId);
        var thirdParties = await _thirdPartyRepo.GetByIds(ids, ct);
        if (thirdParties.Any())
            foreach (var item in result)
                if (item.ManagerId is not null)
                {
                    var thirdParty = thirdParties.FirstOrDefault(x => x.Id == item.ManagerId);
                    if (thirdParty is not null)
                        item.Manager = thirdParty.FirstName + " " + thirdParty.LastName;
                }

        await result!.SetFullName(_mediator, ct);

        // 7. Return
        return new GetFilteredGoodsManagerAssignmentsResponse(result, rowCount);
    }

    public async Task<Result<GetGoodsManagerAssignmentHistoryResponse?>> GetGoodsManagerAssignmentHistory(
        GetGoodsManagerAssignmentHistoryRequest request, CT ct)
    {
        // 1. Log
        _logger.LogInformation("GetGoodsManagerAssignmentHistory");

        // 2. Validate
        var isValidRequest = await request.IsValidAsync<
            GetGoodsManagerAssignmentHistoryValidator,
            GetGoodsManagerAssignmentHistoryRequest>(ct);
        if (isValidRequest.IsBad())
            return isValidRequest.Failure<GetGoodsManagerAssignmentHistoryResponse>()!;

        // 3. Load (Tuple returned, NO IsBad() check needed)
        var result = await _historyRepo.GetByAssignmentId(
            request.Id,
            request.PageIndex,
            request.PageSize,
            ct);

        // 4. Map and Enrich
        var models = result.Data
            .Select(x => new GetGoodsManagerAssignmentHistoryModel(
                x.Id,
                x.OrganizationId,
                x.ProductId,
                x.Description,
                x.CreatorId, // ⬅️ Removed the extra 'null' argument
                x.Created,
                x.Created.ToShamsi()))
            .ToList();

        await models.SetFullName(_mediator, ct);

        // 5. Return
        return new GetGoodsManagerAssignmentHistoryResponse(models, result.RowCount);
    }

    public async Task<Result<GetGoodsManagerAssignmentsByIdResponse?>> GetGoodsManagerAssignmentsById(
        GetGoodsManagerAssignmentsByIdRequest request, CT ct)
    {
        _logger.LogInformation("GetGoodsManagerAssignmentsById");

        var isValidRequest = await request.IsValidAsync<GetGoodsManagerAssignmentsByIdValidator,
            GetGoodsManagerAssignmentsByIdRequest>(ct);
        if (isValidRequest.IsBad())
            return isValidRequest.Failure<GetGoodsManagerAssignmentsByIdResponse>()!;

        var entity = await _repository.GetManagerById(request.Id, ct);
        if (entity is null)
            return Result.Failure<GetGoodsManagerAssignmentsByIdResponse>(
                GoodsManagerAssignmentErrors.NotFound);

        // 4. Enrich
        var result = await EnrichAssignments(entity, ct);
        return new GetGoodsManagerAssignmentsByIdResponse(result.FirstOrDefault()!);
    }
}