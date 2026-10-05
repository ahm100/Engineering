using Engineering.Application.Services.RequestMachineries.Models.RequestMachineryDocumentModel;
using Engineering.Domain.Entities.RequestMachineries.Enums;

namespace Engineering.Application.Services.RequestMachineries.Models.UpdateRequestMachinery;

public record UpdateRequestMachineryRequest(long RequestMachineryId,
                                            long ProjectId,
                                            long MachineryId,
                                            string TimeRequired,
                                            RequestMachineryUnit Unit,
                                            int RequestCount,
                                            DateTime FromDate,
                                            DateTime ToDate,
                                            TimeSpan? FromTime,
                                            TimeSpan? ToTime,
                                            string? Description,
                                            string MachineryIdentifier,
                                            List<long>? ProjectOperationIds,
                                            List<long>? ProjectOperationDetailsIds,
                                            List<RequestMachineryDocumentRequestModel>? RequestMachineryDocuments) : IHttpRequest;
