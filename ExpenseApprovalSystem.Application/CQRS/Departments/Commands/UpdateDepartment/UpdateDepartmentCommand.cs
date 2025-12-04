using AutoMapper;
using ExpenseApprovalSystem.Application.DTOs;
using ExpenseApprovalSystem.Application.Interfaces;
using ExpenseApprovalSystem.Application.Services.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpenseApprovalSystem.Application.CQRS.Departments.Commands.UpdateDepartment;

public record UpdateDepartmentCommand(int DepartmentId, UpdateDepartmentDTO Dto)
    : IRequest;

public class UpdateDepartmentCommandHandler : IRequestHandler<UpdateDepartmentCommand>
{
    private readonly IDepartmentService _departmentService;
    public UpdateDepartmentCommandHandler(IDepartmentService departmentService)
    {
        _departmentService = departmentService;
    }
    public async Task Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
    {
        await _departmentService.UpdateAsync(request.DepartmentId, request.Dto);
    }
}