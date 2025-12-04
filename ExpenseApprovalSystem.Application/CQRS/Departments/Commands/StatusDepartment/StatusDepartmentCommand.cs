using ExpenseApprovalSystem.Application.Services.Interfaces;
using MediatR;

namespace ExpenseApprovalSystem.Application.CQRS.Departments.Commands.DeActiveDepartment;

public record StatusDepartmentCommand(int DepartmentId) : IRequest;

public class DeActiveDepartmentCommandHandler : IRequestHandler<StatusDepartmentCommand>
{
    private readonly IDepartmentService _departmentService;

    public DeActiveDepartmentCommandHandler(IDepartmentService departmentService)
    {
        _departmentService = departmentService;
    }
    public async Task Handle(StatusDepartmentCommand request, CancellationToken cancellationToken)
    {
        await _departmentService.StatusDepartment(request.DepartmentId);
    }

}