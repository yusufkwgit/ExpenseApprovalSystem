using ExpenseApprovalSystem.Application.CQRS.Departments.Commands.CreateDepartment;
using ExpenseApprovalSystem.Application.CQRS.Departments.Commands.DeActiveDepartment;
using ExpenseApprovalSystem.Application.CQRS.Departments.Commands.UpdateDepartment;
using ExpenseApprovalSystem.Application.CQRS.Departments.Queries.GetAllDepartment;
using ExpenseApprovalSystem.Application.CQRS.Departments.Queries.GetDepartmentDetail;
using ExpenseApprovalSystem.Application.CQRS.ExpenseRequests.Commands.UpdateExpenseRequest;
using ExpenseApprovalSystem.Application.DTOs;
using ExpenseApprovalSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseApprovalSystem.API.Controllers;

[ApiController]
[Route("api/departments")]
public class DepartmentController : ControllerBase
{
    private readonly IMediator _mediator;

    public DepartmentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<DepartmentDTO>>> GetAsync()
    {
        var departments = await _mediator.Send(new GetAllDepartmentsQuery());
        return Ok(departments);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<DepartmentDTO>> GetByIdAsync(int id)
    {
        var value = await _mediator.Send(new GetDepartmentDeatilQuery(id));
        if (value is null)
        {
            return NotFound();
        }

        return Ok(value);
    }

    [HttpPost]
    public async Task<ActionResult<DepartmentDTO>> CreateAsync(CreateDepartmentDTO dto) //[FromBody]
    {
        var value = await _mediator.Send(new CreateDepartmentCommand(dto));
        return value;
        //CreatedAtRoute(RouteName.GetDepartmentById, new { id = value.DepartmentID }, value);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateDepartmentAsync(int id, UpdateDepartmentDTO dto)
    {
        await _mediator.Send(new UpdateDepartmentCommand(id, dto));
        return NoContent();
    }

    [HttpPost("{id:int}/change-status")]
    public async Task<IActionResult> DepartmentStatus(int id)
    {
        await _mediator.Send(new StatusDepartmentCommand(id));
        return NoContent();
    }

    
}
