using Engineering.Domain.Entities.Synonyms.MetaData.ThirdParties;
using System.ComponentModel.DataAnnotations.Schema;

namespace Engineering.Domain.Entities.Logistics;

public class TransportationContractorManager : ActivateEntity<TransportationContractorManager, long>
{
    [Description(GlobalCmts.ThirdParty)]
    [ForeignKey("ThirdParty")]
    public long ThirdPartyId { get; set; }
    public virtual ViewThirdParty ThirdParty { get; set; }

    [Description(TransportationContractorMachineCmts.TransportationContractor)]
    public long TransportationContractorId { get; set; }
    public TransportationContractor TransportationContractor { get; set; }

    public TransportationContractorManager(
        long thirdParty,
        TransportationContractor transportationContractor) : this()
    {
        SetThirdPartyId(thirdParty);
        SetTransportationContractor(transportationContractor);
        SetActive();
    }

    public void Update(
        long thirdParty,
        bool isActive,
        TransportationContractor transportationContractor)
    {
        SetThirdPartyId(thirdParty);
        SetTransportationContractor(transportationContractor);
        SetActive();
    }

    public void SetThirdParty(ViewThirdParty value)
    {
        ThirdParty = Guard.Against.Null(value, nameof(value));
        ThirdPartyId = value.Id;
    }

    public void SetThirdPartyId(long value)
    {
        ThirdPartyId = value;
    }

    public void SetTransportationContractor(TransportationContractor value)
    {
        TransportationContractor = Guard.Against.Null(value, nameof(value));
        TransportationContractorId = value.Id;
    }

    private TransportationContractorManager()
    {
    }

}

