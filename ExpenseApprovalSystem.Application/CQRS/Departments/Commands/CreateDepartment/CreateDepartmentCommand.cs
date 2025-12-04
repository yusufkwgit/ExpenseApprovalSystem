using ExpenseApprovalSystem.Application.DTOs;
using ExpenseApprovalSystem.Application.Services.Interfaces;
using MediatR;

namespace ExpenseApprovalSystem.Application.CQRS.Departments.Commands.CreateDepartment;

public sealed record CreateDepartmentCommand(CreateDepartmentDTO Dto) : IRequest<DepartmentDTO>;

public sealed class CreateDepartmentCommandHandler
    : IRequestHandler<CreateDepartmentCommand, DepartmentDTO>
{
    private readonly IDepartmentService _departmentService;

    public CreateDepartmentCommandHandler(IDepartmentService departmentService)
    {
        _departmentService = departmentService;
    }

    public async Task<DepartmentDTO> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
    {
        return await _departmentService.CreateAsync(request.Dto);
    }
}







