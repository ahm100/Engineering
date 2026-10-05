using Engineering.Application.Services.TransportationContractors.Contracts.UpdateTransportationContractor;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.UpdateThirdParty;

namespace Engineering.Application.WebServices.MetaDataServices.ThirdParties.Commands.UpdateThirdParty;

public class UpdateThirdPartyPersonnelCommand() : ICommand<UpdateThirdPartyResponse?>
{
    public UpdateContractorPersonnelModel Item { get; set; }
    public UpdateThirdPartyPersonnelCommand(UpdateContractorPersonnelModel item) : this()
    {
        Item = item;
    }
}