using AutoMapper;
using ExpenseApprovalSystem.Application.DTOs;
using ExpenseApprovalSystem.Application.Interfaces;
using ExpenseApprovalSystem.Application.Services.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseApprovalSystem.Application.CQRS.Departments.Queries.GetAllDepartment;

public record GetAllDepartmentsQuery : IRequest<IReadOnlyList<DepartmentDTO>>;

public class GetAllDepartmentsQueryHandler : IRequestHandler<GetAllDepartmentsQuery, IReadOnlyList<DepartmentDTO>>
{
    private readonly IDepartmentService _departmentService;
    public GetAllDepartmentsQueryHandler(IDepartmentService departmentService)
    {
        _departmentService = departmentService;
    }
    public async Task<IReadOnlyList<DepartmentDTO>> Handle(GetAllDepartmentsQuery request, CancellationToken cancellationToken)
    {
        return await _departmentService.GetAllAsync();
    }
}
