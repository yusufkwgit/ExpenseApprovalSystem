using ExpenseApprovalSystem.Domain.Enums;

namespace ExpenseApprovalSystem.Application.DTOs
{
    public class UserDTO
    {
        public int UserID { get; set; }
        public string NameSurname { get; set; }
        public string Email { get; set; }
        public UserRole Role { get; set; }
        public int DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }

    }

    public class CreateUserDTO
    {
        public string NameSurname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
        public int DepartmentId { get; set; }
    }

    public class UserBasicDTO
    {
        public int UserID { get; set; }
        public string NameSurname { get; set; }
    }
}


