using ExpenseApprovalSystem.Application.DTOs;
using ExpenseApprovalSystem.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseApprovalSystem.API.Controllers;

[ApiController]
[Route("api/expense-requests/{expenseRequestId:int}/logs")]
public sealed class RequestLogsController : ControllerBase
{
    private readonly IRequestLogService _requestLogService;

    public RequestLogsController(IRequestLogService requestLogService)
    {
        _requestLogService = requestLogService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<RequestLogDTO>>> GetAsync(int expenseRequestId)
    {
        var logs = await _requestLogService.GetByRequestAsync(expenseRequestId);
        return Ok(logs);
    }

    [HttpPost]
    public async Task<ActionResult<RequestLogDTO>> AddAsync(
        int expenseRequestId,
        [FromBody] CreateRequestLogDTO dto)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        dto.ExpenseRequestId = expenseRequestId;
        var created = await _requestLogService.AddLogAsync(dto);
        return CreatedAtAction(nameof(GetAsync), new { expenseRequestId }, created);
    }
}




