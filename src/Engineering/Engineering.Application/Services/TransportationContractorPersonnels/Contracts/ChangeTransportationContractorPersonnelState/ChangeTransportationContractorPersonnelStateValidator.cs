namespace Engineering.Application.Services.TransportationContractorPersonnels.Contracts.ChangeTransportationContractorPersonnelState;

public class ChangeTransportationContractorPersonnelStateValidator : AbstractValidator<ChangeTransportationContractorPersonnelStateRequest>
{
    public ChangeTransportationContractorPersonnelStateValidator()
    {
        RuleForEach(c => c.Ids)
            .IsPositive(TransportationContractorCmts.TransportationContractorPersonnelId);
    }
}
