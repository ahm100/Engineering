using Engineering.Application.Abstractions.Data.BillOfLadings;
using Engineering.Application.Services.BillOfLadings.Contracts.BillOfLadingCodeCreator;
using Engineering.Application.Services.BillOfLadings.Contracts.BillOfLadingExcelImports;
using Engineering.Application.Services.BillOfLadings.Contracts.ChangeBillOfLadingState;
using Engineering.Application.Services.BillOfLadings.Contracts.CreateBillOfLading;
using Engineering.Application.Services.BillOfLadings.Contracts.DeleteBillOfLadings;
using Engineering.Application.Services.BillOfLadings.Contracts.GetBillOfLadingById;
using Engineering.Application.Services.BillOfLadings.Contracts.GetsActiveBillOfLading;
using Engineering.Application.Services.BillOfLadings.Contracts.GetsBillOfLadingExcelEnum;
using Engineering.Application.Services.BillOfLadings.Contracts.GetsBillOfLadingExcelExporter;
using Engineering.Application.Services.BillOfLadings.Contracts.GetsFilteredBillOfLading;
using Engineering.Application.Services.BillOfLadings.Contracts.UpdateBillOfLading;

namespace Engineering.Application.Services.BillOfLadings;

public partial class BillOfLadingLogic : IBillOfLadingLogic
{
    private readonly IMediator _mediator;
    private readonly ILogger<BillOfLadingLogic> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserInfoService _userInfoService;
    private readonly IBillOfLadingRepository _repository;

    public BillOfLadingLogic(
        IMediator mediator,
        ILogger<BillOfLadingLogic> logger,
        IUnitOfWork unitOfWork,
        IUserInfoService userInfoService,
        IBillOfLadingRepository repository)
    {
        _mediator = mediator;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _userInfoService = userInfoService;
        _repository = repository;
    }

    public async Task<Result<CreateBillOfLadingResponse?>> CreateBillOfLading(
        CreateBillOfLadingRequest request, CT ct)
    {
        _logger.LogInformation("CreateBillOfLading");
        var companyId = CompanyValidator.GetCompanyId(_userInfoService)!.Value;
        if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
            return Result.Failure<CreateBillOfLadingResponse>(GlobalErrors.InvalidCompany);

        if (await IsDuplicateName(request.BillOfLadingName, companyId, ct))
            return Result.Failure<CreateBillOfLadingResponse>(BillOfLadingErrors.NameIsDuplicate);
        if (await IsDuplicateCode(request.BillOfLadingCode, companyId, ct))
            return Result.Failure<CreateBillOfLadingResponse>(BillOfLadingErrors.CodeIsDuplicate);

        var result = await CreateBillOfLadingHandle(request.BillOfLadingName,
            request.BillOfLadingCode, request.IsActive, companyId, ct);
        if (result.IsBad()) return result.Failure<CreateBillOfLadingResponse>()!;
        await _unitOfWork.CommitAsync(ct);
        return new CreateBillOfLadingResponse(result.Value!.Id);
    }

    public async Task<Result<BillOfLadingExcelImportsResponse?>> BillOfLadingExcelImports(
        BillOfLadingExcelImportsRequest request, CT ct)
    {
        _logger.LogInformation("BillOfLadingExcelImports");
        var billOfLadings = ExcelImporter.Import<BillOfLadingExcelImportsModel>(request.DocumentFile);
        if (billOfLadings is null)
            return Result.Failure<BillOfLadingExcelImportsResponse>(GlobalErrors.ErrorOnReadFile);
        if (billOfLadings.Count != billOfLadings.Select(s => s.BillOfLadingName).Distinct().Count())
            return Result.Failure<BillOfLadingExcelImportsResponse>(GlobalErrors.ExcelImporteredHaveNameDuplicate);
        if (billOfLadings.Count != billOfLadings.Select(s => s.BillOfLadingCode).Distinct().Count())
            return Result.Failure<BillOfLadingExcelImportsResponse>(GlobalErrors.ExcelImporteredHaveCodeDuplicate);

        var companyId = CompanyValidator.GetCompanyId(_userInfoService)!.Value;
        if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
            return Result.Failure<BillOfLadingExcelImportsResponse>(GlobalErrors.InvalidCompany);

        var names = billOfLadings.Select(s => s.BillOfLadingName).ToList();
        var codes = billOfLadings.Select(s => s.BillOfLadingCode).ToList();
        var duplicate = await GetsBillOfLadingByNamesOrCodesHandle(names, codes, companyId, ct);
        if (duplicate.Value)
            return Result.Failure<BillOfLadingExcelImportsResponse>(GlobalErrors.HaveDuplicateKey);

        foreach (var item in billOfLadings)
        {
            var result = await CreateBillOfLadingHandle(
                item.BillOfLadingName, item.BillOfLadingCode, item.IsActive, companyId, ct);
            if (result.IsBad()) return result.Failure<BillOfLadingExcelImportsResponse>()!;
        }

        await _unitOfWork.CommitAsync(ct);
        return new BillOfLadingExcelImportsResponse(true);
    }

    public async Task<Result<BillOfLadingCodeCreatorResponse?>> BillOfLadingCodeCreator(
        BillOfLadingCodeCreatorRequest request, CT ct)
    {
        _logger.LogInformation("BillOfLadingCodeCreator");
        var companyId = CompanyValidator.GetCompanyId(_userInfoService)!.Value;
        if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
            return Result.Failure<BillOfLadingCodeCreatorResponse>(GlobalErrors.InvalidCompany);

        var result = await BillOfLadingCodeCreatorHandle(companyId, ct);
        if (result.IsBad()) return result.Failure<BillOfLadingCodeCreatorResponse>()!;
        return new BillOfLadingCodeCreatorResponse(result.Value!);
    }

    public async Task<Result<DeleteBillOfLadingsResponse?>> DeleteBillOfLadings(
        DeleteBillOfLadingsRequest request, CT ct)
    {
        _logger.LogInformation("DeleteBillOfLadings");
        var result = await DeleteBillOfLadingsHandle(request.Ids!, ct);
        if (result.IsBad()) return result.Failure<DeleteBillOfLadingsResponse>()!;

        await _unitOfWork.CommitAsync(ct);
        return new DeleteBillOfLadingsResponse(true);
    }

    public async Task<Result<UpdateBillOfLadingResponse?>> UpdateBillOfLading(
        UpdateBillOfLadingRequest request, CT ct)
    {
        _logger.LogInformation("UpdateBillOfLading");
        var companyId = CompanyValidator.GetCompanyId(_userInfoService)!.Value;
        if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
            return Result.Failure<UpdateBillOfLadingResponse>(GlobalErrors.InvalidCompany);

        var nameIsDuplicate = await GetBillOfLadingByNameHandle(request.BillOfLadingName, companyId, ct);
        if (nameIsDuplicate is { IsSuccess: true, Value: not null, } && nameIsDuplicate.Value.Id != request.Id)
            return Result.Failure<UpdateBillOfLadingResponse>(BillOfLadingErrors.NameIsDuplicate);

        var codeIsDuplicate = await GetBillOfLadingByCodeHandle(request.BillOfLadingCode, companyId, ct);
        if (codeIsDuplicate is { IsSuccess: true, Value: not null, } && codeIsDuplicate.Value.Id != request.Id)
            return Result.Failure<UpdateBillOfLadingResponse>(BillOfLadingErrors.CodeIsDuplicate);

        var result = await UpdateBillOfLadingHandle(request, ct);
        if (result.IsBad()) return result.Failure<UpdateBillOfLadingResponse>()!;

        await _unitOfWork.CommitAsync(ct);
        return new UpdateBillOfLadingResponse(true);
    }

    public async Task<Result<ChangeBillOfLadingStateResponse?>> ChangeBillOfLadingState(
        ChangeBillOfLadingStateRequest request, CT ct)
    {
        _logger.LogInformation("ChangeBillOfLadingState");
        var result = await ChangeBillOfLadingStateHandle(request, ct);
        if (result.IsBad()) return result.Failure<ChangeBillOfLadingStateResponse>()!;

        await _unitOfWork.CommitAsync(ct);
        return new ChangeBillOfLadingStateResponse(true);
    }

    public async Task<Result<GetBillOfLadingByIdResponse?>> GetBillOfLadingById(
    GetBillOfLadingByIdRequest request, CT ct)
    {
        _logger.LogInformation("GetBillOfLadingById");
        var result = await GetBillOfLadingByIdForResponseHandle(request.Id, ct);
        if (result.IsBad()) return result.Failure<GetBillOfLadingByIdResponse>()!;
        return result;
    }

    public async Task<Result<GetsActiveBillOfLadingResponse?>> GetsActiveBillOfLading(
        GetsActiveBillOfLadingRequest request, CT ct)
    {
        _logger.LogInformation("GetsActiveBillOfLading");
        var companyId = CompanyValidator.GetCompanyId(_userInfoService);
        var result = await GetsActiveBillOfLadingHandle(request, companyId, ct);
        if (result.IsBad()) return result.Failure<GetsActiveBillOfLadingResponse>()!;

        return new GetsActiveBillOfLadingResponse(result.Value!.Data ?? new(), result.Value.RowCount);
    }

    public async Task<Result<GetsFilteredBillOfLadingResponse?>> GetsFilteredBillOfLading(
        GetsFilteredBillOfLadingRequest request, CT ct)
    {
        _logger.LogInformation("GetsFilteredBillOfLading");
        var companyId = CompanyValidator.GetCompanyId(_userInfoService);
        var result = await GetsFilteredBillOfLadingHandle(request, null, companyId, ct);
        if (result.IsBad()) return result.Failure<GetsFilteredBillOfLadingResponse>()!;

        return new GetsFilteredBillOfLadingResponse(result.Value!.Data ?? new(), result.Value.RowCount);
    }

    public async Task<Result<GetsBillOfLadingExcelExporterResponse?>> GetsBillOfLadingExcelExporter(
        GetsBillOfLadingExcelExporterRequest request, CT ct)
    {
        _logger.LogInformation("GetsBillOfLadingExcelExporter");
        var companyId = CompanyValidator.GetCompanyId(_userInfoService)!.Value;
        if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
            return Result.Failure<GetsBillOfLadingExcelExporterResponse>(GlobalErrors.InvalidCompany);

        var result = await GetsFilteredBillOfLadingForExcelHandle(
            request.Adapt<GetsFilteredBillOfLadingRequest>(), request.Ids, companyId, ct);
        if (result.IsBad()) return result.Failure<GetsBillOfLadingExcelExporterResponse>()!;

        var file = new FileContentResult(BillOfLadingExcels.BillOfLadingToExcel(
            result.Value!.Data!, request.ExcelFilters), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = $"BillOfLadings-{TimeCalculator.DatePiker(DateTime.UtcNow)}.xlsx",
            LastModified = DateTime.UtcNow
        };
        return new GetsBillOfLadingExcelExporterResponse(file);
    }

    public async Task<Result<GetsBillOfLadingExcelEnumResponse?>> GetsBillOfLadingExcelEnum(
        GetsBillOfLadingExcelEnumRequest request, CT ct)
    {
        _logger.LogInformation("GetsBillOfLadingExcelEnum");
        var response = await Task.Run(() => EnumExt.GetEnumObjectList<BillOfLadingExcelEnum>());
        return new GetsBillOfLadingExcelEnumResponse(response);
    }

}