using Engineering.Application.Services.RequestMachineries.Models.RequestMachineryDocumentModel;
using Engineering.Domain.Entities.RequestMachineries.Enums;

namespace Engineering.Application.Services.RequestMachineries.Models.CreateRequestMachinery;

public record CreateRequestMachineryRequest(long? Id,
                                            long ProjectId,
                                            List<long>? ProjectOperationIds,
                                            List<long>? ProjectOperationDetailIds,
                                            long MachineryId,
                                            string TimeRequired,
                                            RequestMachineryUnit Unit,
                                            int RequestCount,
                                            DateTime FromDate,
                                            DateTime ToDate,
                                            TimeSpan? FromTime,
                                            TimeSpan? ToTime,
                                            string? Description,
                                            bool IsDeleted,
                                            List<RequestMachineryDocumentRequestModel>? RequestMachineryDocuments) : IHttpRequest;
