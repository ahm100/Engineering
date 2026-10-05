namespace Engineering.Application.WebServices.MetaDataServices.GetMachineryOperators;

public class MachineryOperatorAppointment
{
    public MachineryOperatorAppointment()
    {

    }

    public MachineryOperatorAppointment(long id, int totalInquiriesCount)
    {
        Id = id;
        TotalInquiriesCount = totalInquiriesCount;
    }

    public long Id { get; set; }
    public int TotalInquiriesCount { get; set; }
}
