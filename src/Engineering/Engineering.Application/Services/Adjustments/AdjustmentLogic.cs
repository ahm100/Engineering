using Engineering.Application.Abstractions.Data.Adjustments;
using Engineering.Application.Abstractions.Data.Branchs;
using Engineering.Application.Abstractions.Data.Categories;
using Engineering.Application.Abstractions.Data.Seasons;
using Engineering.Application.Abstractions.Data.Synonyms.FinanicalPeriod;
using Engineering.Application.Services.Adjustments.Contracts;
using Engineering.Application.Services.Adjustments.Contracts.CreateAdjustmentIndex;
using Engineering.Application.Services.Adjustments.Contracts.DeleteAdjustmentIndex;
using Engineering.Application.Services.Adjustments.Contracts.GetAdjustmentIndexById;
using Engineering.Application.Services.Adjustments.Contracts.GetAdjustmentIndexes;
using Engineering.Application.Services.Adjustments.Contracts.UpdateAdjustmentIndex;
using Engineering.Application.Services.OperationInfos;

namespace Engineering.Application.Services.Adjustments;

public partial class AdjustmentLogic : IAdjustmentLogic
{
    private readonly IMediator _mediator;
    private readonly ILogger<OperationInfoLogic> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IBranchRepository _branchRepository;
    private readonly ISeasonRepository _seasonRepository;
    private readonly IViewFinancialPeriodRepository _viewFinancialPeriodRepository;
    private readonly IAdjustmentIndexRepository _adjustmentIndexRepository;
    private readonly IAdjustmentReferenceRepository _adjustmentReferenceRepository;

    public AdjustmentLogic(IMediator mediator, ILogger<OperationInfoLogic> logger, IUnitOfWork unitOfWork, ICategoryRepository categoryRepository,
        IBranchRepository branchRepository, ISeasonRepository seasonRepository, IViewFinancialPeriodRepository viewFinancialPeriodRepository,
        IAdjustmentIndexRepository adjustmentIndexRepository, IAdjustmentReferenceRepository adjustmentReferenceRepository)
    {
        _mediator = mediator;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _categoryRepository = categoryRepository;
        _branchRepository = branchRepository;
        _seasonRepository = seasonRepository;
        _viewFinancialPeriodRepository = viewFinancialPeriodRepository;
        _adjustmentIndexRepository = adjustmentIndexRepository;
        _adjustmentReferenceRepository = adjustmentReferenceRepository;
    }

    public async Task<Result<AdjustmentExcelImportsResponse>> AdjustmentExcelImports(AdjustmentExcelImportsRequest request, CT ct)
    {
        _logger.LogInformation("Engineering - AdjustmentExcelImports");

        var transactionOptions = new System.Transactions.TransactionOptions
        {
            IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        };

        using var scope = new System.Transactions.TransactionScope(
            System.Transactions.TransactionScopeOption.Required, transactionOptions, System.Transactions.TransactionScopeAsyncFlowOption.Enabled);

        try
        {
            var (indexes, values) = AdjustmentExcelImporter.Import(request.DocumentFile);

            var result = await CreateAdjustmentCommand(indexes, values, request.CompanyId, request.YearId, request.NotificationFileName, ct);

            if (result.IsBad())
                return result.Failure<AdjustmentExcelImportsResponse>()!;

            await _unitOfWork.CommitAsync(ct);

            scope.Complete();

            return new AdjustmentExcelImportsResponse(
                true,
                result.Value!.SeasonAdjustments,
                result.Value.BranchAdjustments,
                result.Value.Errors);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in AdjustmentExcelImports");
            return Result.Failure<AdjustmentExcelImportsResponse>(
                SharedErrors.UnknownError)!;
        }
    }

    public async Task<Result<CreateAdjustmentIndexResponse?>> CreateAdjustmentIndex(CreateAdjustmentIndexRequest request, CT ct)
    {
        _logger.LogInformation(
            "Request for CreateAdjustmentIndex, Code:{Code}",
            request.Code);

        var result =
            await CreateAdjustmentIndexCommand(request, ct);

        if (result.IsBad())
            return result.Failure<CreateAdjustmentIndexResponse>()!;

        await _unitOfWork.CommitAsync(ct);

        return new CreateAdjustmentIndexResponse(
            result.Value!.Id,
            true);
    }

    public async Task<Result<UpdateAdjustmentIndexResponse?>> UpdateAdjustmentIndex(UpdateAdjustmentIndexRequest request, CT ct)
    {
        _logger.LogInformation(
            "Request for UpdateAdjustmentIndex, Id:{Id}",
            request.Id);

        var result =
            await UpdateAdjustmentIndexCommand(request, ct);

        if (result.IsBad())
            return result.Failure<UpdateAdjustmentIndexResponse>()!;

        await _unitOfWork.CommitAsync(ct);

        return new UpdateAdjustmentIndexResponse(true);
    }

    public async Task<Result<DeleteAdjustmentIndexResponse?>> DeleteAdjustmentIndex(DeleteAdjustmentIndexRequest request, CT ct)
    {
        _logger.LogInformation(
            "Request for DeleteAdjustmentIndex, Id:{Id}",
            request.Id);

        var result =
            await DeleteAdjustmentIndexCommand(request, ct);

        if (result.IsBad())
            return result.Failure<DeleteAdjustmentIndexResponse>()!;

        await _unitOfWork.CommitAsync(ct);

        return result;
    }

    public async Task<Result<GetAdjustmentIndexByIdResponse?>> GetAdjustmentIndexById(GetAdjustmentIndexByIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetAdjustmentIndexById, Id:{Id}", request.Id);
        return await GetAdjustmentIndexByIdCommand(request, ct);
    }

    public async Task<Result<GetAdjustmentIndexesResponse?>> GetAdjustmentIndexes(GetAdjustmentIndexesRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetAdjustmentIndexes");

        return await GetAdjustmentIndexesCommand(request, ct);
    }
}

