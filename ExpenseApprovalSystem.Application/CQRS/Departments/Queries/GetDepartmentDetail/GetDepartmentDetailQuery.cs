using ExpenseApprovalSystem.Application.DTOs;
using MediatR;
using ExpenseApprovalSystem.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseApprovalSystem.Application.CQRS.Departments.Queries.GetDepartmentDetail;

public record GetDepartmentDeatilQuery(int DepartmentId) : IRequest<DepartmentDTO?>;

public class GetDepartmentDeatilQueryHandler : IRequestHandler<GetDepartmentDeatilQuery, DepartmentDTO?>
{
    private readonly IDepartmentService _departmentService;

    public GetDepartmentDeatilQueryHandler(IDepartmentService departmentService)
    {
        _departmentService = departmentService;
    }
    public async Task<DepartmentDTO?> Handle(GetDepartmentDeatilQuery request, CancellationToken cancellationToken)
    {
        return await _departmentService.GetByIdAsync(request.DepartmentId);
    }

}