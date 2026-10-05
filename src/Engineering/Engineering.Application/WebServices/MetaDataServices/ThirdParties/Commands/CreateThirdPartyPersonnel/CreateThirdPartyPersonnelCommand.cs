using Engineering.Application.Services.TransportationContractors.Contracts.CreateTransportationContractor;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.CreateThirdParty;

namespace Engineering.Application.WebServices.MetaDataServices.ThirdParties.Commands.CreateThirdPartyPersonnel;

public class CreateThirdPartyPersonnelCommand() : ICommand<CreateThirdPartyResponse?>
{
    public CreateContractorPersonnelModel Item { get; }
    public long? CompanyId { get; }
    public CreateThirdPartyPersonnelCommand(
        CreateContractorPersonnelModel item,
        long? companyId) : this()
    {
        Item = item;
        CompanyId = companyId;
    }
}
