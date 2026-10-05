namespace Engineering.Domain.Entities.FiduciaryProducts;

/// <summary>
/// پیوست کالای امانی
/// </summary>
public class FiduciaryProductDetailReturnDocument : AuditableEntity<FiduciaryProductDetailReturnDocument>
{
    #region Properties

    [Description(GlobalCmts.Url)]
    public string Url { get; private set; } = string.Empty;

    [Description(FiduciaryProductCmts.FiduciaryProductDetailReturn)]
    public long FiduciaryProductDetailReturnId { get; private set; }
    public FiduciaryProductDetailReturn FiduciaryProductDetailReturn { get; private set; }

    #endregion

    public FiduciaryProductDetailReturnDocument(FiduciaryProductDetailReturn fiduciaryProductDetailReturn,
        string url) : this()
    {
        SetFiduciaryProductDetailReturn(fiduciaryProductDetailReturn);
        SetUrl(url);
    }

    public void SetIsDeleted()
    {
        IsDeleted = true;
    }

    public void SetUrl(string value)
    {
        Url = Guard.Against.Null(value, nameof(value));
    }

    public void SetFiduciaryProductDetailReturn(FiduciaryProductDetailReturn value)
    {
        FiduciaryProductDetailReturn = Guard.Against.Null(value, nameof(value));
        FiduciaryProductDetailReturnId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    #region Constructors

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private FiduciaryProductDetailReturnDocument() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    #endregion

    #region Commands

    #endregion
}
