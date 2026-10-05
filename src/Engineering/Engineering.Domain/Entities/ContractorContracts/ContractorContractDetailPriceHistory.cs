
namespace Engineering.Domain.Entities.ContractorContracts;

[Description(CCCmts.ContractorContractDetailPriceHistory)]
public class ContractorContractDetailPriceHistory : ActivateEntity<ContractorContractDetailPriceHistory, long>
{
    [Description(GlobalCmts.StartDate)]
    public DateTime StartDate { get; private set; }
    [Description(GlobalCmts.EndDate)]
    public DateTime EndDate { get; private set; }
    [Description(CCCmts.Price)]
    public decimal Price { get; private set; }
    [Description(CCCmts.CurrencyId)]
    public long CurrencyId { get; private set; }
    [Description(CCCmts.ContractorContractDetailPriceId)]
    public long ContractorContractDetailPriceId { get; private set; }
    [Description(CCCmts.ContractorContractDetailPrice)]
    public ContractorContractDetailPrice ContractorContractDetailPrice { get; private set; } = default!;


    public ContractorContractDetailPriceHistory(
        ContractorContractDetailPrice contractorContractDetailPrice,
        DateTime startDate,
        DateTime endDate,
        decimal price,
        long currencyId,
        bool isActive) : this()
    {
        SetContractorContractDetailPrice(contractorContractDetailPrice);
        SetStartDate(startDate);
        SetEndDate(endDate);
        SetPrice(price);
        SetCurrencyId(currencyId);
        if (isActive)
            SetActive();
        else
            SetDeactivate();
    }

    public static ContractorContractDetailPriceHistory Create(
        ContractorContractDetailPrice contractorContractDetailPrice,
        DateTime startDate,
        DateTime endDate,
        decimal price,
        long currencyId,
        bool isActive)
    {
        return new ContractorContractDetailPriceHistory(
            contractorContractDetailPrice,
            startDate,
            endDate,
            price,
            currencyId,
            isActive);
    }

    private ContractorContractDetailPriceHistory()
    {
    }

    #region 
    public void SetContractorContractDetailPrice(ContractorContractDetailPrice value)
    {
        ContractorContractDetailPrice = Guard.Against.Null(value, nameof(value));
        ContractorContractDetailPriceId = Guard.Against.Null(value.Id, nameof(value.Id));
    }
    public void SetStartDate(DateTime value)
    {
        StartDate = Guard.Against.Null(value, nameof(value));
    }
    public void SetEndDate(DateTime value)
    {
        EndDate = Guard.Against.Null(value, nameof(value));
    }
    public void SetPrice(decimal value)
    {
        Price = Guard.Against.Null(value, nameof(value));
    }
    public void SetCurrencyId(long value)
    {
        CurrencyId = Guard.Against.Null(value, nameof(value));
    }
    #endregion
}
