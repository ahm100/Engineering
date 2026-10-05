namespace Engineering.Application.Services.TransportationContractors.Contracts.ChangeTransportationContractorState;

public class ChangeTransportationContractorStateValidator : AbstractValidator<ChangeTransportationContractorStateRequest>
{
    public ChangeTransportationContractorStateValidator()
    {
        RuleForEach(c => c.Ids)
            .IsPositive(TransportationContractorCmts.TransportationContractorId);
    }
}
