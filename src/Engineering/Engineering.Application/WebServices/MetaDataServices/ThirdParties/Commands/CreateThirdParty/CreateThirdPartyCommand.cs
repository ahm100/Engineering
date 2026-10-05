using Engineering.Application.Services.TransportationContractors.Contracts.CreateTransportationContractor;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.CreateThirdParty;

namespace Engineering.Application.WebServices.MetaDataServices.ThirdParties.Commands.CreateThirdParty;

public class CreateThirdPartyCommand() : ICommand<CreateThirdPartyResponse?>
{
    public CreateTransportationContractorRequest Item { get; }
    public long? CompanyId { get; }
    public CreateThirdPartyCommand(
        CreateTransportationContractorRequest item,
        long? companyId) : this()
    {
        Item = item;
        CompanyId = companyId;
    }
}
