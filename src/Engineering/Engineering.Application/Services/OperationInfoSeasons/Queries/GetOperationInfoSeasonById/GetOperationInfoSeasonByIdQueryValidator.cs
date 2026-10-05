namespace Engineering.Application.Services.OperationInfoSeasons.Queries.GetOperationInfoSeasonById;

public class GetOperationInfoSeasonByIdQueryValidator : AbstractValidator<GetOperationInfoSeasonByIdQuery>
{
    public GetOperationInfoSeasonByIdQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(OperationInfoErrors.IdIsEmpty);
    }
}