namespace Engineering.Domain.Entities.ContractorContracts;
[Description(CCCmts.ContractorContractDetailPrice)]
public class ContractorContractDetailPrice : ActivateEntity<ContractorContractDetailPrice, long>
{
    [Description(GlobalCmts.StartDate)]
    public DateTime StartDate { get; private set; }
    [Description(GlobalCmts.EndDate)]
    public DateTime EndDate { get; private set; }
    [Description(CCCmts.Price)]
    public decimal Price { get; private set; }
    [Description(CCCmts.CurrencyId)]
    public long CurrencyId { get; private set; }
    [Description(CCCmts.ContractorContractDetailId)]
    public long ContractorContractDetailId { get; private set; }
    [Description(CCCmts.ContractorContractDetail)]
    public ContractorContractDetail ContractorContractDetail { get; private set; } = default!;

    public ContractorContractDetailPrice(
        ContractorContractDetail contractorContractDetail,
        DateTime startDate,
        DateTime endDate,
        decimal price,
        long currencyId,
        bool isActive) : this()
    {
        SetContractorContractDetail(contractorContractDetail);
        SetStartDate(startDate);
        SetEndDate(endDate);
        SetPrice(price);
        SetCurrencyId(currencyId);
        AddHistory();

        if (isActive)
            SetActive();
        else
            SetDeactivate();
    }

    public static ContractorContractDetailPrice Create(
        ContractorContractDetail contractorContractDetail,
        DateTime startDate,
        DateTime endDate,
        decimal price,
        long currencyId,
        bool isActive)
    {
        return new ContractorContractDetailPrice(
            contractorContractDetail,
            startDate,
            endDate,
            price,
            currencyId,
            isActive);
    }
    public void SetContractorContractDetail(ContractorContractDetail value)
    {
        ContractorContractDetail = Guard.Against.Null(value, nameof(value));
        ContractorContractDetailId = Guard.Against.Null(value.Id, nameof(value.Id));
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
    public void AddHistory()
    {
        _contractorContractDetailPriceHistories.Add(ContractorContractDetailPriceHistory.Create(
            this,
            StartDate,
            EndDate,
            Price,
            CurrencyId,
            IsActive
            ));
    }
    [Description(CCCmts.ContractorContractDetailPriceHistory)]
    private List<ContractorContractDetailPriceHistory> _contractorContractDetailPriceHistories;
    public IReadOnlyList<ContractorContractDetailPriceHistory> ContractorContractDetailPriceHistories => _contractorContractDetailPriceHistories;
    private ContractorContractDetailPrice()
    {
        _contractorContractDetailPriceHistories = [];
    }

}
