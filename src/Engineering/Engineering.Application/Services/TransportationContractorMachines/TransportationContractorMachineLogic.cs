using Engineering.Application.Abstractions.Data;
using Engineering.Application.Abstractions.Data.MachineTypes;
using Engineering.Application.Services.TransportationContractorMachines.Contracts.ChangeTransportationContractorMachineState;
using Engineering.Application.Services.TransportationContractorMachines.Contracts.CreateTransportationContractorMachine;
using Engineering.Application.Services.TransportationContractorMachines.Contracts.DeleteTransportationContractorMachine;
using Engineering.Application.Services.TransportationContractorMachines.Contracts.GetsActiveTransportationContractorMachine;
using Engineering.Application.Services.TransportationContractorMachines.Contracts.GetsFilteredTransportationContractorMachine;
using Engineering.Application.Services.TransportationContractorMachines.Contracts.GetsTransportationContractorMachineExcelEnum;
using Engineering.Application.Services.TransportationContractorMachines.Contracts.GetsTransportationContractorMachineExcelExporter;
using Engineering.Application.Services.TransportationContractorMachines.Contracts.GetTransportationContractorMachineById;
using Engineering.Application.Services.TransportationContractorMachines.Contracts.UpdateTransportationContractorMachine;

namespace Engineering.Application.Services.TransportationContractorMachines;

public partial class TransportationContractorMachineLogic : ITransportationContractorMachineLogic
{
    private readonly IMediator _mediator;
    private readonly ILogger<TransportationContractorMachineLogic> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserProfileService _userProfileService;
    private readonly IUserInfoService _userInfoService;
    private readonly ITransportationContractorMachineRepository _repository;
    private readonly ITransportationContractorRepository _contractorRepository;
    private readonly ITransportationContractorPersonnelRepository _contractorPersonnelRepository;
    private readonly IMachineTypeRepository _machineTypeRepository;

    public TransportationContractorMachineLogic(IMediator mediator,
        ILogger<TransportationContractorMachineLogic> logger,
        IUnitOfWork unitOfWork,
        IUserProfileService userProfileService,
        IUserInfoService userInfoService,
        ITransportationContractorRepository contractorRepository,
        ITransportationContractorMachineRepository repository,
        IMachineTypeRepository machineTypeRepository,
        ITransportationContractorPersonnelRepository contractorPersonnelRepository)
    {
        _mediator = mediator;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _userProfileService = userProfileService;
        _userInfoService = userInfoService;
        _contractorRepository = contractorRepository;
        _repository = repository;
        _machineTypeRepository = machineTypeRepository;
        _contractorPersonnelRepository = contractorPersonnelRepository;
    }

    public async Task<Result<CreateTransportationContractorMachineResponse?>> CreateTransportationContractorMachine(CreateTransportationContractorMachineRequest request, CT ct)
    {
        _logger.LogInformation("Request for CreateTransportationContractorMachine");
        var response = await CreateTransportationContractorMachineExecute(request, ct);
        if (response.IsFailure) return Result.Failure<CreateTransportationContractorMachineResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new CreateTransportationContractorMachineResponse(true);
    }

    public async Task<Result<DeleteTransportationContractorMachineResponse?>> DeleteTransportationContractorMachine(DeleteTransportationContractorMachineRequest request, CT ct)
    {
        _logger.LogInformation("Request for DeleteTransportationContractorMachine, Ids:{Ids}", request.Ids);
        var response = await DeleteTransportationContractorMachineExecute(request, ct);
        if (response.IsFailure) return Result.Failure<DeleteTransportationContractorMachineResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new DeleteTransportationContractorMachineResponse(response.Value!.Id, response.Value!.IsDeleted);
    }

    public async Task<Result<UpdateTransportationContractorMachineResponse?>> UpdateTransportationContractorMachine(UpdateTransportationContractorMachineRequest request, CT ct)
    {
        _logger.LogInformation("Request for UpdateTransportationContractorMachine");
        var response = await UpdateTransportationContractorMachineExecute(request, ct);
        if (response.IsFailure) return Result.Failure<UpdateTransportationContractorMachineResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new UpdateTransportationContractorMachineResponse(response.Value!.Id);
    }

    public async Task<Result<ChangeTransportationContractorMachineStateResponse?>> ChangeTransportationContractorMachineState(ChangeTransportationContractorMachineStateRequest request, CT ct)
    {
        _logger.LogInformation("ChangeTransportationContractorMachineState");
        var result = await ChangeTransportationContractorMachineStateExecute(request, ct);
        if (result.IsFailure) return result.Failure<ChangeTransportationContractorMachineStateResponse>()!;

        await _unitOfWork.CommitAsync(ct);
        return new ChangeTransportationContractorMachineStateResponse(true);
    }

    public async Task<Result<GetTransportationContractorMachineByIdResponse?>> GetTransportationContractorMachineById(GetTransportationContractorMachineByIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetTransportationContractorMachineById, id:{Id}", request.Id);
        var response = await GetTransportationContractorMachineByIdExecute(request, ct);
        if (response.IsFailure) return Result.Failure<GetTransportationContractorMachineByIdResponse>(response.Error!);
        return response.Value!;
    }

    public async Task<Result<GetsActiveTransportationContractorMachineResponse?>> GetsActiveTransportationContractorMachine(GetsActiveTransportationContractorMachineRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetActiveTransportationContractorMachine");
        var response = await GetsActiveTransportationContractorMachineExecute(request, ct);
        if (response.IsFailure) return Result.Failure<GetsActiveTransportationContractorMachineResponse>(response.Error!);
        return new GetsActiveTransportationContractorMachineResponse(
            response.Value!.Data ?? new List<GetsActiveTransportationContractorMachineResponseModel>(0), response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsFilteredTransportationContractorMachineResponse?>> GetsFilteredTransportationContractorMachine(GetsFilteredTransportationContractorMachineRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsTransportationContractorMachine");
        var response = await GetsFilteredTransportationContractorMachineExecute(request, ct);
        if (response.IsFailure) return Result.Failure<GetsFilteredTransportationContractorMachineResponse>(response.Error!);
        return new GetsFilteredTransportationContractorMachineResponse(
            response.Value!.Data ?? new List<GetsFilteredTransportationContractorMachineResponseModel>(0), response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsTransportationContractorMachineExcelExporterResponse?>> GetsTransportationContractorMachineExcelExporter(GetsTransportationContractorMachineExcelExporterRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsTransportationContractorMachineExcelExporter");

        var response = await GetsFilteredTransportationContractorMachineExecute(request.Adapt<GetsFilteredTransportationContractorMachineRequest>(), ct);
        if (response.IsFailure) return Result.Failure<GetsTransportationContractorMachineExcelExporterResponse>(response.Error!);

        var file = new FileContentResult(TransportationContractorMachineExcels.TransportationContractorMachineToExcel(response.Value!.Data!, request.ExcelFilters),
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = $"TransportationContractorMachines-{TimeCalculator.DatePiker(DateTime.UtcNow)}.xlsx",
            LastModified = DateTime.UtcNow
        };

        return new GetsTransportationContractorMachineExcelExporterResponse(file);
    }

    public async Task<Result<GetsTransportationContractorMachineExcelEnumResponse?>> GetsTransportationContractorMachineExcelEnum(GetsTransportationContractorMachineExcelEnumRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsTransportationContractorMachineExcelEnum");
        var response = await Task.Run(() => EnumExt.GetEnumObjectList<TransportationContractorMachineExcelEnum>());
        return new GetsTransportationContractorMachineExcelEnumResponse(response);
    }
}