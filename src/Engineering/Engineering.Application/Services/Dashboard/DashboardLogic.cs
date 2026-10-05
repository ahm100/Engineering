using Engineering.Application.Services.Dashboard.Contracts.GetMainDashboard;
using Engineering.Application.Services.Dashboard.Contracts.GetProjectDashboard;
using Engineering.Application.Services.Dashboard.Queries.GetMainDashboard;
using Engineering.Application.Services.Dashboard.Queries.GetProjectDashboard;

namespace Engineering.Application.Services.Dashboard;

public partial class DashboardLogic : IDashboardLogic
{
    private readonly IMediator _mediator;
    private readonly ILogger<DashboardLogic> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserInfoService _userInfoService;

    public DashboardLogic(
        IMediator mediator,
        ILogger<DashboardLogic> logger,
        IUnitOfWork unitOfWork,
        IUserInfoService userInfoService)
    {
        _mediator = mediator;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _userInfoService = userInfoService;
    }

    public async Task<Result<GetMainDashboardResponse?>> GetMainDashboard(
        GetMainDashboardRequest request, CT ct)
    {
        _logger.LogInformation("GetMainDashboard");
        var result = await _mediator.Send(new GetMainDashboardQuery(), ct);
        if (result.IsBad())
            return result.Failure<GetMainDashboardResponse>()!;

        return result;
    }

    public async Task<Result<GetProjectDashboardResponse?>> GetProjectDashboard(
        GetProjectDashboardRequest request, CT ct)
    {
        var result = await _mediator.Send(new GetProjectDashboardQuery(request.ProjectId), ct);
        if (result.IsBad()) return result.Failure<GetProjectDashboardResponse>()!;

        return result;
    }
}