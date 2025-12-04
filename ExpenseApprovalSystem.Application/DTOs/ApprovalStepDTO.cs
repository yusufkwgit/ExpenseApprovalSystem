using ExpenseApprovalSystem.Domain.Enums;

namespace ExpenseApprovalSystem.Application.DTOs
{
    public class ApprovalStepDTO
    {
        public int ApprovalStepID { get; set; }
        public int ExpenseRequestId { get; set; }
        public int StepNumber { get; set; }
        public int ApproverId { get; set; }
        public string? ApproverName { get; set; }
        public ApprovalStepType Type { get; set; }
        public ApprovalStatus Status { get; set; }
        public DateTime? ActionDate { get; set; }
        public DateTime? DueDate { get; set; }  // ???????????? gerek olmayabilir
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class CreateApprovalStepDTO
    {
        public int StepNumber { get; set; }
        public int ApproverId { get; set; }
        public ApprovalStepType Type { get; set; }
        public DateTime? DueDate { get; set; }
    }

    public class UpdateApprovalStepDTO
    {
        public ApprovalStatus? Status { get; set; }
        public string? Comment { get; set; }
    }
}



