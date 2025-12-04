using ExpenseApprovalSystem.Application.DTOs;
using ExpenseApprovalSystem.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

[Authorize]
[ApiController]
[Route("api/approval-steps")]
public class ApprovalStepsController : ControllerBase
{
    private readonly IApprovalStepService _approvalStepService;

    public ApprovalStepsController(IApprovalStepService approvalStepService)
    {
        _approvalStepService = approvalStepService;
    }

    [HttpPut("by-request/{expenseRequestId:int}")]
    public async Task<IActionResult> UpdateByRequestAsync(int expenseRequestId, [FromBody] UpdateApprovalStepDTO dto)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim == null)
        {
            return Unauthorized("Kimlik do�rulanamad�.");
        }

        int currentUserId = int.Parse(userIdClaim.Value);

        await _approvalStepService.UpdateStepByRequestAsync(expenseRequestId, dto, currentUserId);

        return NoContent();
    }
}
