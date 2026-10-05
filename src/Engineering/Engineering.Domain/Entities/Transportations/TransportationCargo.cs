using Engineering.Domain.Entities.Synonyms.MetaData.ThirdParties;
using Engineering.Domain.Entities.Synonyms.Warehouse.Packings;

namespace Engineering.Domain.Entities.Transportations;

public class TransportationCargo : AuditableEntity<TransportationCargo>
{
    [Description(TransportationRequestWarehouseComment.PackingNumber)]
    public long PackingNumber { get; set; }

    [Description(TransportationRequestWarehouseComment.PackingId)]
    public long PackingId { get; set; }
    public ViewPacking? Packing { get; set; }

    [Description(TransportationRequestWarehouseComment.ThirdPartyId)]
    public long? ThirdPartyId { get; set; }
    public ViewThirdParty? ThirdParty { get; set; }
    [Description(TransportationRequestWarehouseComment.ThirdPartyName)]
    public string? ThirdPartyName { get; set; }

    [Description(TransportationRequestWarehouseComment.SecurityConfirm)]
    public bool SecurityConfirm { get; set; } = false;

    [Description(TransportationRequestWarehouseComment.SecurityConfirm)]
    public DateTime? SecurityConfirmDate { get; set; }



    public TransportationCargo(
        long packingNumber,
        long packingId,
        long? thirdPartyId,
        string? thirdParty
        ) : this()
    {
        SetPackingNumber(packingNumber);
        SetPackingId(packingId);
        SetThirdPartyId(thirdPartyId);
        SetThirdParty(thirdParty);
    }

    public void Update(
        long packingNumber,
        long packingId,
        long? thirdPartyId,
        string? thirdParty
        )
    {
        SetPackingNumber(packingNumber);
        SetPackingId(packingId);
        SetThirdPartyId(thirdPartyId);
        SetThirdParty(thirdParty);
    }

    public void SetPackingNumber(long value)
    {
        PackingNumber = value;
    }
    public void SetPackingId(long value)
    {
        PackingId = value;
    }

    public void SetThirdPartyId(long? value)
    {
        ThirdPartyId = value;
    }

    public void SetThirdParty(string? value)
    {
        ThirdPartyName = value;
    }

    public void SetSecurityConfirm(bool value)
    {
        SecurityConfirm = value;
        SecurityConfirmDate = value == true ? DateTime.UtcNow : null;
    }

    public void Remove()
    {
        IsDeleted = true;
    }

    public void AddCargoPallets(TransportationCargoPallet pallet)
    {
        _transportationCargoPallets.Add(pallet);
    }

    public void AddCargoDocuments(List<string>? docs)
    {
        _transportationCargoDocuments.ForEach(item =>
        {
            item.SoftDelete();
        });

        if (docs != null && docs.Count > 0)
        {
            foreach (var item in docs)
                _transportationCargoDocuments
                    .Add(new TransportationCargoDocument(item, this));
        }
    }


    private List<TransportationCargoDocument> _transportationCargoDocuments;
    public IReadOnlyList<TransportationCargoDocument> TransportationCargoDocuments => _transportationCargoDocuments;


    private List<TransportationCargoPallet> _transportationCargoPallets;
    public IReadOnlyList<TransportationCargoPallet> TransportationCargoPallets => _transportationCargoPallets;

    /// <summary>
    ///  For EF core, never thoch this
    /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private TransportationCargo()
    {
        _transportationCargoPallets = [];
        _transportationCargoDocuments = [];
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
