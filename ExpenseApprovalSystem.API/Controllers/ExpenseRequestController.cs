using ExpenseApprovalSystem.Application.CQRS.ExpenseAttachments.Commands.AddExpenseAttachment;
using ExpenseApprovalSystem.Application.CQRS.ExpenseAttachments.Commands.DeleteExpenseAttachment;
using ExpenseApprovalSystem.Application.CQRS.ExpenseAttachments.Queries.GetExpenseAttachmentsByRequest;
using ExpenseApprovalSystem.Application.CQRS.ExpenseRequests.Commands.CreateExpenseRequest;
using ExpenseApprovalSystem.Application.CQRS.ExpenseRequests.Commands.SoftDeleteExpenseRequest;
using ExpenseApprovalSystem.Application.CQRS.ExpenseRequests.Commands.UpdateExpenseRequest;
using ExpenseApprovalSystem.Application.CQRS.ExpenseRequests.Queries.GetAllExpenseRequests;
using ExpenseApprovalSystem.Application.CQRS.ExpenseRequests.Queries.GetExpenseRequestDetail;
using ExpenseApprovalSystem.Application.CQRS.ExpenseRequests.Queries.GetExpenseRequestsByEmployee;
using ExpenseApprovalSystem.Application.DTOs;
using ExpenseApprovalSystem.Application.Services.Interfaces;
using ExpenseApprovalSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseApprovalSystem.API.Controllers;

[Authorize]
[ApiController]
[Route("api/expense-requests")]
public class ExpenseRequestController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IApprovalStepService _approvalStepService;
    private const int SystemUserId = 1;

    public ExpenseRequestController(IMediator mediator, IApprovalStepService approvalStepService)
    {
        _mediator = mediator;
        _approvalStepService = approvalStepService;
    }

   
    [HttpPost]
    public async Task<ActionResult<ExpenseRequestDTO>> CreateAsync([FromBody] CreateExpenseRequestDTO dto)
    {
        var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);

        //// E�er token bozuksa veya ID yoksa hata d�n
        if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int currentUserId))
        {
            return Unauthorized("Token i�inde ge�erli bir User ID bulunamad�.");
        }
        dto.EmployeeId = currentUserId;
        var result = await _mediator.Send(new CreateExpenseRequestCommand(dto, SystemUserId));
        return CreatedAtRoute(nameof(GetByIdAsync), new { id = result.ExpenseRequestID }, result);
    }

    [HttpGet("{id:int}", Name = nameof(GetByIdAsync))]
    public async Task<ActionResult<ExpenseRequestDTO>> GetByIdAsync(int id)
    {
        var result = await _mediator.Send(new GetExpenseRequestDetailQuery(id));
        if (result is null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ExpenseRequestListDTO>>> ListAsync([FromQuery] int? employeeId = null)
    {
        IReadOnlyList<ExpenseRequestListDTO> result = employeeId.HasValue
            ? await _mediator.Send(new GetExpenseRequestsByEmployeeQuery(employeeId.Value))
            : await _mediator.Send(new GetAllExpenseRequestsQuery());

        return Ok(result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateAsync(int id, UpdateExpenseRequestDTO dto)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        await _mediator.Send(new UpdateExpenseRequestCommand(id, dto, SystemUserId));
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> SoftDeleteAsync(int id)
    {
        await _mediator.Send(new SoftDeleteExpenseRequestCommand(id, SystemUserId));
        return NoContent();
    }

    [HttpGet("{id:int}/approval-steps")]
    public async Task<ActionResult<IReadOnlyList<ApprovalStepDTO>>> GetApprovalStepsAsync(int id)
    {
        var steps = await _approvalStepService.GetStepsForRequestAsync(id);
        return Ok(steps);
    }

    [HttpPost("{id:int}/approval-steps")]
    public async Task<ActionResult<ApprovalStepDTO>> AddApprovalStepAsync(int id, CreateApprovalStepDTO dto)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }
        var created = await _approvalStepService.AddStepAsync(id, dto);
        //return CreatedAtAction(nameof(GetApprovalStepsAsync), new { id }, created);
        return Ok(created);
    }

    [HttpGet("{id:int}/attachments")]
    public async Task<ActionResult<IReadOnlyList<ExpenseAttachmentDTO>>> GetAttachmentsAsync(int id)
    {
        var attachments = await _mediator.Send(new GetExpenseAttachmentsByRequestQuery(id));
        return Ok(attachments);
    }

    [Consumes("multipart/form-data")]
    [HttpPost("{id:int}/attachments")]
    public async Task<ActionResult<ExpenseAttachmentDTO>> AddAttachmentAsync(int id, [FromForm] CreateExpenseAttachmentDTO dto)
    {
        dto.ExpenseRequestId = id;
        var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
        if (userIdClaim != null) dto.UploadedBy = int.Parse(userIdClaim.Value);

        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var created = await _mediator.Send(new AddExpenseAttachmentCommand(id, dto));
        return Ok(created);
    }

    [HttpDelete("{expenseRequestId:int}/attachments/{attachmentId:int}")]
    public async Task<IActionResult> DeleteAttachmentAsync(int expenseRequestId, int attachmentId)
    {
        await _mediator.Send(new DeleteExpenseAttachmentCommand(expenseRequestId, attachmentId, SystemUserId));
        return NoContent();
    }
}

