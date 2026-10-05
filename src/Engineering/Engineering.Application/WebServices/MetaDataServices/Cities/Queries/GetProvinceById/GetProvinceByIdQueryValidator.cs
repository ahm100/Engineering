namespace Engineering.Application.WebServices.MetaDataServices.Cities.Queries.GetProvinceById;

public class GetProvinceByIdQueryValidator : AbstractValidator<GetProvinceByIdQuery>
{
    public GetProvinceByIdQueryValidator()
    {
        RuleFor(oo => oo.Id)
            .IsPositive(MetaDataErrors.IdIsEmpty);
    }
}