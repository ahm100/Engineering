
namespace Engineering.Application.Services.DetailContractorServices.Queries.GetsDetailContractorServiceByIds;

public class GetsDetailContractorServiceByIdsQueryValidator : AbstractValidator<GetsDetailContractorServiceByIdsQuery>
{
    public GetsDetailContractorServiceByIdsQueryValidator()
    {
        RuleFor(oo => oo.Items).NotNull().WithError(GlobalErrors.IdsIsEmpty);
    }
}
