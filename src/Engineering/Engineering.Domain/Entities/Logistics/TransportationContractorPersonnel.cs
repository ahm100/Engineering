using Engineering.Domain.Entities.Synonyms.MetaData.ThirdParties;
using System.ComponentModel.DataAnnotations.Schema;

namespace Engineering.Domain.Entities.Logistics;

public class TransportationContractorPersonnel : ActivateEntity<TransportationContractorPersonnel, long>
{
    [Description(GlobalCmts.ThirdParty)]
    [ForeignKey("ThirdParty")]
    public long ThirdPartyId { get; set; }
    public virtual ViewThirdParty ThirdParty { get; set; }
    [Description(TransportationContractorMachineCmts.CertificateNumber)]
    public string? CertificateNumber { get; set; }
    [Description(GlobalCmts.LegacyId)]
    public long? LegacyId { get; private set; }
    [Description(TransportationContractorMachineCmts.TransportationContractor)]
    public long TransportationContractorId { get; set; }
    public TransportationContractor TransportationContractor { get; set; }

    public TransportationContractorPersonnel(
        long thirdParty,
        string? certificateNumber,
        long? legacyId,
        TransportationContractor transportationContractor) : this()
    {
        SetThirdPartyId(thirdParty);
        SetTransportationContractor(transportationContractor);
        SetLegacyId(legacyId);
        SetCertificateNumber(certificateNumber);
        SetActive();
    }

    public void Update(
        long thirdParty,
        string? certificateNumber,
        long? legacyId,
        bool isActive,
        TransportationContractor transportationContractor)
    {
        SetThirdPartyId(thirdParty);
        SetTransportationContractor(transportationContractor);
        SetLegacyId(legacyId);
        SetActive();
        SetCertificateNumber(certificateNumber);
    }

    public void SetThirdParty(ViewThirdParty value)
    {
        ThirdParty = Guard.Against.Null(value, nameof(value));
        ThirdPartyId = value.Id;
    }

    public void SetCertificateNumber(string? value)
    {
        CertificateNumber = value;
    }

    public void SetThirdPartyId(long value)
    {
        ThirdPartyId = value;
    }

    public void SetLegacyId(long? value)
    {
        LegacyId = value;
    }

    public void SetTransportationContractor(TransportationContractor value)
    {
        TransportationContractor = Guard.Against.Null(value, nameof(value));
        TransportationContractorId = value.Id;
    }
    public void SetPersonnel(List<TransportationContractorMachine>? contractorMachines)
    {
        _contractorPersonnelMachines.ForEach(contractorPersonnelMachine => contractorPersonnelMachine.SoftDelete());

        if (contractorMachines is not null && contractorMachines.Count > 0)
            foreach (var item in contractorMachines)
                _contractorPersonnelMachines.Add(new TransportationContractorPersonnelMachine(this, item));
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

    [Description(TransportationContractorCmts.TransportationContractorPersonnelMachine)]
    private List<TransportationContractorPersonnelMachine> _contractorPersonnelMachines;
    public IReadOnlyList<TransportationContractorPersonnelMachine> ContractorPersonnelMachines => _contractorPersonnelMachines;
    private TransportationContractorPersonnel()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    {
        _contractorPersonnelMachines = [];
    }

}

