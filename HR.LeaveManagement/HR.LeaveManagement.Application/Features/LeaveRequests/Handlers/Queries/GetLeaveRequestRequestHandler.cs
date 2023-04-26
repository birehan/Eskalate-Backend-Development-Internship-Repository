﻿using AutoMapper;
using HR.LeaveManagement.Application.DTOs.LeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Queries;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveReqeusts.Handlers.Queries
{
    public class GetLeaveRequestRequestHandler : IRequestHandler<GetLeaveRequestListRequest, List<ListLeaveRequestDto>>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IMapper _mapper;
        public GetLeaveRequestRequestHandler(ILeaveRequestRepository leaveRequestRepository, IMapper mapper)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _mapper = mapper;
        }
        public async Task<List<ListLeaveRequestDto>> Handle(GetLeaveRequestListRequest request, CancellationToken cancellationToken)
        {
            var leaveRequests = await _leaveRequestRepository.GetLeaveRequestsWithDetails();

            return _mapper.Map<List<ListLeaveRequestDto>>(leaveRequests);
        }
    }
}
