using Engineering.ClientSdk.Enums;
using Engineering.Domain.Entities.Synonyms.MetaData.ThirdParties;
using Engineering.Domain.Entities.Transportations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace Engineering.Domain.Entities.Logistics;

public class TransportationContractor : ActivateEntity<TransportationContractor, long>
{
    [Description(TransportationContractorCmts.ThirdPartyId)]
    [ForeignKey("ThirdParty")]
    public long? ThirdPartyId { get; set; }
    public virtual ViewThirdParty? ThirdParty { get; set; }

    [Description(TransportationContractorCmts.StartOfContract)]
    public string? Title { get; private set; }

    [Description(TransportationContractorCmts.StartOfContract)]
    public DateTime? StartOfContract { get; private set; }

    [Description(TransportationContractorCmts.EndOfContract)]
    public DateTime? EndOfContract { get; private set; }

    [Description(TransportationContractorCmts.DeliveryMethod)]
    public DeliveryMethod? DeliveryMethod { get; private set; }

    [Description(TransportationContractorCmts.DeliveryType)]
    public DeliveryType? DeliveryType { get; private set; }

    [Description(TransportationContractorCmts.Type)]
    public TransportationContractorCalculateType Type { get; private set; }

    [Description(GlobalCmts.CompanyId)]
    public long CompanyId { get; private set; }

    public long? LegacyId { get; private set; }

    [Description(TransportationContractorCmts.PercentageValue)]
    public decimal? PercentageValue { get; private set; }

    [Description(TransportationContractorCmts.FixedNumber)]
    public decimal? FixedNumber { get; private set; }

    [Description(TransportationContractorCmts.FixedNumber)]
    public decimal? TaxPercent { get; private set; }

    [Description(TransportationContractorCmts.FixedNumber)]
    public decimal? ServicePrice { get; private set; }

    [Description(TransportationContractorCmts.FirstPrefix)]
    public string? FirstPrefix { get; private set; }

    [Description(TransportationContractorCmts.SecondPrefix)]
    public long? SecondPrefix { get; private set; }

    [Description(TransportationContractorCmts.TransportationContractorManagers)]
    private List<TransportationContractorManager> _contractorManagers = [];
    public IReadOnlyList<TransportationContractorManager> ContractorManagers => _contractorManagers;

    [Description(TransportationContractorCmts.TransportationContractorPersonnels)]
    private List<TransportationContractorPersonnel> _contractorPersonnels;
    public IReadOnlyList<TransportationContractorPersonnel> ContractorPersonnels => _contractorPersonnels;

    [Description(TransportationContractorCmts.TransportationContractorDocuments)]
    private List<TransportationContractorDocument> _contractorDocuments;
    public IReadOnlyList<TransportationContractorDocument> ContractorDocuments => _contractorDocuments;

    [Description(TransportationContractorCmts.ShippingCosts)]
    private List<TransportationContractorPriceWeight> _priceWeights;
    public IReadOnlyList<TransportationContractorPriceWeight> PriceWeights => _priceWeights;

    [Description(TransportationContractorCmts.TransportationContractorDocuments)]
    private List<ShippingCost> _shippingCosts;
    public IReadOnlyList<ShippingCost> ShippingCosts => _shippingCosts;

    [Description(GlobalCmts.TransportationRequest)]
    private List<TransportationRequest> _transportationRequests;
    public IReadOnlyList<TransportationRequest> TransportationRequests => _transportationRequests;

    [Description(TransportationContractorCmts.TransportationContractorMachine)]
    private List<TransportationContractorMachine> _transportationContractorMachines;
    public IReadOnlyList<TransportationContractorMachine> TransportationContractorMachines => _transportationContractorMachines;

    [Description(TransportationContractorInsuranceCmts.TransportationContractorInsurances)]
    private List<TransportationContractorInsurance> _transportationContractorInsurances;
    public IReadOnlyList<TransportationContractorInsurance> TransportationContractorInsurances => _transportationContractorInsurances;

    private List<TransportationCargoPallet> _transportationCargoPallets;
    public IReadOnlyList<TransportationCargoPallet> TransportationCargoPallets => _transportationCargoPallets;
    private TransportationContractor()
    {
        _contractorManagers = [];
        _contractorPersonnels = [];
        _contractorDocuments = [];
        _shippingCosts = [];
        _transportationRequests = [];
        _transportationContractorMachines = [];
        _priceWeights = [];
        _transportationContractorInsurances = [];
        _transportationCargoPallets = [];
    }

    public TransportationContractor(
        string? title,
        long? thirdParty,
        DateTime? startOfContract,
        DateTime? endOfContract,
        DeliveryMethod[]? deliveryMethods,
        DeliveryType[]? deliveryTypes,
        TransportationContractorCalculateType type,
        decimal? percentageValue,
        decimal? fixedNumber,
        string? firstPrefix,
        long? secondPrefix,
        decimal? taxPercent,
        decimal? servicePrice,
        long companyId,
        long? legacyId) : this()
    {
        SetTitle(title);
        SetThirdPartyId(thirdParty);
        SetStartOfContract(startOfContract);
        SetEndOfContract(endOfContract);
        SetType(type);
        SetActive();
        IsDeleted = false;
        SetCompanyId(companyId);
        SetLegacyId(legacyId);
        SetPercentageValue(percentageValue);
        SetFixedNumber(fixedNumber);
        SetFirstPrefix(firstPrefix);
        SetSecondPrefix(secondPrefix);
        SetTaxPercent(taxPercent);
        SetServicePrice(servicePrice);
        if (deliveryMethods != null)
        {
            SetDeliveryMethods(deliveryMethods);
        }
        if (deliveryTypes != null)
        {
            SetDeliveryTypes(deliveryTypes);
        }
    }

    public TransportationContractor(
        string? title,
        DeliveryMethod[]? deliveryMethods,
        DeliveryType[]? deliveryTypes,
        TransportationContractorCalculateType type,
        string? firstPrefix,
        long? secondPrefix,
        long companyId,
        DateTime? startOfContract,
        DateTime? endOfContract,
        long? legacyId) : this()
    {
        SetTitle(title);
        SetType(type);
        SetActive();
        IsDeleted = false;
        SetCompanyId(companyId);
        SetLegacyId(legacyId);
        SetFirstPrefix(firstPrefix);
        SetSecondPrefix(secondPrefix);
        SetStartOfContract(startOfContract);
        SetEndOfContract(endOfContract);
        if (deliveryMethods != null)
        {
            SetDeliveryMethods(deliveryMethods);
        }
        if (deliveryTypes != null)
        {
            SetDeliveryTypes(deliveryTypes);
        }
    }

    public void Update(
        string? title,
        DeliveryMethod[]? deliveryMethods,
        DeliveryType[]? deliveryTypes,
        TransportationContractorCalculateType type,
        string? firstPrefix,
        long? secondPrefix,
        long? legacyId,
        DateTime? startOfContract,
        DateTime? endOfContract,
        bool? isActive
        )
    {
        SetTitle(title);
        SetStartOfContract(startOfContract);
        SetEndOfContract(endOfContract);
        if (isActive == true)
        {
            SetActive();
        }
        else if (isActive == false)
        {
            SetDeactivate();
        }
        SetLegacyId(legacyId);
        SetType(type);
        SetFirstPrefix(firstPrefix);
        SetSecondPrefix(secondPrefix);
        if (deliveryMethods != null)
        {
            SetDeliveryMethods(deliveryMethods);
        }
        if (deliveryTypes != null)
        {
            SetDeliveryTypes(deliveryTypes);
        }
    }

    public void Update(
        string? title,
        long? thirdParty,
        DateTime? startOfContract,
        DateTime? endOfContract,
        DeliveryMethod[]? deliveryMethods,
        DeliveryType[]? deliveryTypes,
        TransportationContractorCalculateType type,
        decimal? percentageValue,
        decimal? fixedNumber,
        string? firstPrefix,
        long? secondPrefix,
        decimal? taxPercent,
        decimal? servicePrice,
        long? legacyId,
        bool? isActive
        )
    {
        SetTitle(title);
        SetThirdPartyId(thirdParty);
        SetStartOfContract(startOfContract);
        SetEndOfContract(endOfContract);
        if (isActive == true)
        {
            SetActive();
        }
        else if (isActive == false)
        {
            SetDeactivate();
        }
        SetLegacyId(legacyId);
        SetType(type);
        SetPercentageValue(percentageValue);
        SetFixedNumber(fixedNumber);
        SetFirstPrefix(firstPrefix);
        SetSecondPrefix(secondPrefix);
        SetTaxPercent(taxPercent);
        SetServicePrice(servicePrice);
        if (deliveryMethods != null)
        {
            SetDeliveryMethods(deliveryMethods);
        }
        if (deliveryTypes != null)
        {
            SetDeliveryTypes(deliveryTypes);
        }
    }

    public void SetThirdPartyId(long? value)
    {
        ThirdPartyId = value;
    }

    public void SetTitle(string? value)
    {
        Title = value;
    }

    public void SetLegacyId(long? value)
    {
        LegacyId = value;
    }

    public void SetPercentageValue(decimal? value)
    {
        PercentageValue = value;
    }

    public void SetTaxPercent(decimal? value)
    {
        TaxPercent = value;
    }

    public void SetServicePrice(decimal? value)
    {
        ServicePrice = value;
    }

    public void SetFixedNumber(decimal? value)
    {
        FixedNumber = value;
    }

    public void SetFirstPrefix(string? value)
    {
        FirstPrefix = value;
    }

    public void SetSecondPrefix(long? value)
    {
        SecondPrefix = value;
    }

    public void SetType(TransportationContractorCalculateType value)
    {
        Type = Guard.Against.Null(value, nameof(value));
    }

    public void SetCompanyId(long value)
    {
        CompanyId = Guard.Against.Null(value, nameof(value));
    }

    public void SetStartOfContract(DateTime? value)
    {
        StartOfContract = value;
    }

    public void SetEndOfContract(DateTime? value)
    {
        EndOfContract = value;
    }

    public void SetDelete()
    {
        _contractorDocuments.ForEach(contract => contract.SoftDelete());
        _contractorPersonnels.ForEach(contract => contract.SoftDelete());
        IsDeleted = true;
    }

    public void AddPersonnels(TransportationContractorPersonnel personnel)
    {
        _contractorPersonnels.Add(personnel);
    }

    public void AddPriceWeight(TransportationContractorPriceWeight priceWeight)
    {
        _priceWeights.Add(priceWeight);
    }

    public void AddInsurances(List<TransportationContractorInsurance>? insurances)
    {
        if (insurances is not null && insurances.Count > 0)
            _transportationContractorInsurances.AddRange(insurances);
    }

    public void AddDocuments(List<TransportationContractorDocument>? documents)
    {
        _contractorDocuments.ForEach(x => x.SoftDelete());

        if (documents is not null && documents.Count > 0)
            _contractorDocuments.AddRange(documents);
    }

    public void AddManagers(List<TransportationContractorManager>? managers)
    {
        _contractorManagers.ForEach(x => x.SoftDelete());

        if (managers is not null && managers.Count > 0)
            _contractorManagers.AddRange(managers);
    }

    public void SetDeliveryMethod(DeliveryMethod[] value)
    {
        SetDeliveryMethods(value);
    }

    public void SetDeliveryMethods(params DeliveryMethod[] deliveryMethods)
    {
        DeliveryMethod = EnumFlagOperations<DeliveryMethod>.CombineDeliveries(deliveryMethods);
    }

    public static IEnumerable<DeliveryMethod> YieldDeliveryMethods(DeliveryMethod? deliveryMethod)
    {
        if (deliveryMethod != null)
            return EnumFlagOperations<DeliveryMethod>.YieldDeliveries(deliveryMethod!.Value);
        else
            return Enumerable.Empty<DeliveryMethod>();
    }

    public static Expression<Func<TransportationContractor, bool>> CreateDeliveryMethodCondition(List<DeliveryMethod> types)
    {
        return EnumFlagOperations<DeliveryMethod>.CreateEnumCondition<TransportationContractor>(
            nameof(TransportationContractor.DeliveryMethod),
            types
        );
    }

    public void SetDeliveryType(DeliveryType[] value)
    {
        SetDeliveryTypes(value);
    }

    public void SetDeliveryTypes(params DeliveryType[] deliveryTypes)
    {
        DeliveryType = EnumFlagOperations<DeliveryType>.CombineDeliveries(deliveryTypes);
    }

    public static IEnumerable<DeliveryType> YieldDeliveryTypes(DeliveryType? deliveryType)
    {
        if (deliveryType != null)
            return EnumFlagOperations<DeliveryType>.YieldDeliveries(deliveryType!.Value);
        else
            return Enumerable.Empty<DeliveryType>();
    }

    public static Expression<Func<TransportationContractor, bool>> CreateDeliveryTypeCondition(List<DeliveryType> types)
    {
        return EnumFlagOperations<DeliveryType>.CreateEnumCondition<TransportationContractor>(
            nameof(TransportationContractor.DeliveryType),
            types
        );
    }
}


#region Generics
public static class EnumFlagOperations<TEnum> where TEnum : struct, Enum
{
    public static TEnum CombineDeliveries(params TEnum[] DeliveryMethods)
    {
        if (!typeof(TEnum).IsDefined(typeof(FlagsAttribute), false))
            throw new ArgumentException($"Enum type {typeof(TEnum).Name} must have [Flags] attribute");

        dynamic combined = default(TEnum);
        foreach (var ct in DeliveryMethods)
        {
            combined |= (dynamic)ct;
        }
        return combined;
    }

    public static IEnumerable<TEnum> YieldDeliveries(TEnum DeliveryMethod)
    {
        foreach (TEnum flag in Enum.GetValues(typeof(TEnum)))
        {
            if (!flag.Equals(default(TEnum)) && ((Enum)(object)DeliveryMethod).HasFlag((Enum)(object)flag))
            {
                yield return flag;
            }
        }
    }

    public static Expression<Func<TEntity, bool>> CreateEnumCondition<TEntity>(
        string propertyName,
        List<TEnum> types)
    {
        var param = Expression.Parameter(typeof(TEntity));
        var DeliveryMethodProperty = typeof(TEntity).GetProperty(propertyName);

        if (DeliveryMethodProperty is null)
            throw new Exception($"Property '{propertyName}' not found in {typeof(TEntity).Name}");

        var member = Expression.MakeMemberAccess(param, DeliveryMethodProperty);
        var converted = Expression.Convert(member, typeof(int));
        var exps = new List<BinaryExpression>(types.Count);

        foreach (var type in types)
        {
            var constant = Expression.Constant((int)(object)type);
            exps.Add(Expression.Equal(constant, Expression.And(converted, constant)));
        }

        var typesCondition = Expression.Lambda<Func<TEntity, bool>>(
            exps.Aggregate((left, right) => Expression.OrElse(left, right)),
            param
        );

        return typesCondition;
    }
}

#endregion
