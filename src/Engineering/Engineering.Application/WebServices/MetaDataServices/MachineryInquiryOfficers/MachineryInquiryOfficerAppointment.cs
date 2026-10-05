namespace Engineering.Application.WebServices.MetaDataServices.MachineryInquiryOfficers;

public class OperatorInquiryAppointment
{
    public OperatorInquiryAppointment()
    {

    }

    public OperatorInquiryAppointment(long id, int totalInquiriesCount)
    {
        Id = id;
        TotalInquiriesCount = totalInquiriesCount;
    }

    public long Id { get; set; }
    public int TotalInquiriesCount { get; set; }
}
