using Engineering.Application.Services.ProjectCostCenterRequests.Models;

namespace Engineering.Application.Services.ProjectCostCenterRequests;

public partial class ProjectCostCenterRequestLogic
{
    private async Task EnrichModels(
        List<ProjectCostCenterRequestModel> models, CT ct)
    {
        var cityIds = models.NullListed(x => x.CityId);

        if (cityIds.HasItems())
        {
            var cities = await WebServicesLogic.CityDataReceiver(cityIds, _mediator, ct);
            foreach (var m in models)
                m.CityName = cities?.FirstOrDefault(c => c?.Id == m.CityId)?.Name;
        }

        await models.SetFullName(_mediator, ct);
    }
}