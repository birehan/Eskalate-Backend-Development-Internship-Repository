﻿using AutoMapper;
using HR.LeaveManagement.Application.DTOs.LeaveAllocation.Validators;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Requests.Commands;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocations.Handlers.Commands
{
    public class CreateLeaveAllocationCommandHandler : IRequestHandler<CreateLeaveAllocationCommand,int>
    {
        
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;

        public CreateLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocationRepository,ILeaveTypeRepository leaveTypeRepository ,IMapper mapper)
        {
            _leaveAllocationRepository = leaveAllocationRepository;
            _leaveTypeRepository = leaveTypeRepository;
            _mapper = mapper;
        }
        public async Task<int> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            var validation = new CreateLeaveAllocatorDtoValidator(_leaveTypeRepository);
            var validationResult = await validation.ValidateAsync(request.LeaveAllocationDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var leaveAllocation = _mapper.Map<LeaveAllocation>(request.LeaveAllocationDto);
            leaveAllocation = await _leaveAllocationRepository.Add(leaveAllocation);

            return leaveAllocation.Id;
        }

  
    }
}
