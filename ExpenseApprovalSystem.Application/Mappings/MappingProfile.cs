using AutoMapper;
using ExpenseApprovalSystem.Application.DTOs;
using ExpenseApprovalSystem.Domain.Entities;
using ExpenseApprovalSystem.Domain.Enums;

namespace ExpenseApprovalSystem.Application.Mappings
{
    // Entityler ile DTO'lar arasındaki mapping kuralları
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // ExpenseRequest Mappings
            CreateMap<ExpenseRequest, ExpenseRequestDTO>()
                .ForMember(dest => dest.EmployeeName,
                    opt => opt.MapFrom(src => src.Employee != null ? src.Employee.NameSurname : null));

            // Liste ekranı için sade DTO
            CreateMap<ExpenseRequest, ExpenseRequestListDTO>()
                .ForMember(dest => dest.EmployeeName,
                    opt => opt.MapFrom(src => src.Employee != null ? src.Employee.NameSurname : string.Empty));

            CreateMap<CreateExpenseRequestDTO, ExpenseRequest>()
                .ForMember(dest => dest.ExpenseRequestID, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => RequestStatus.Pending))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false))
                .ForMember(dest => dest.Employee, opt => opt.Ignore())
                .ForMember(dest => dest.ApprovalSteps, opt => opt.Ignore())
                .ForMember(dest => dest.RequestLogs, opt => opt.Ignore())
                .ForMember(dest => dest.ExpenseAttachments, opt => opt.Ignore());

            // Sadece null olmayan alanları güncelle
            CreateMap<UpdateExpenseRequestDTO, ExpenseRequest>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // User Mappings
            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.DepartmentName,
                    opt => opt.MapFrom(src => src.Department != null ? src.Department.Name : null));

            // Basit listeleme / dropdown için
            CreateMap<User, UserBasicDTO>();

            CreateMap<CreateUserDTO, User>()
                .ForMember(dest => dest.UserID, opt => opt.Ignore())
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.Department, opt => opt.Ignore())
                .ForMember(dest => dest.ExpenseRequests, opt => opt.Ignore())
                .ForMember(dest => dest.ApprovalSteps, opt => opt.Ignore())
                .ForMember(dest => dest.RequestLogs, opt => opt.Ignore());

            // ApprovalStep Mappings
            CreateMap<ApprovalStep, ApprovalStepDTO>()
                .ForMember(dest => dest.ApproverName,
                    opt => opt.MapFrom(src => src.Approver != null ? src.Approver.NameSurname : null));

            CreateMap<CreateApprovalStepDTO, ApprovalStep>()
                .ForMember(dest => dest.ApprovalStepID, opt => opt.Ignore())
                .ForMember(dest => dest.ExpenseRequestId, opt => opt.Ignore())
                .ForMember(dest => dest.ExpenseRequest, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.MapFrom(_ => ApprovalStatus.Pending))
                .ForMember(dest => dest.ActionDate, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.Approver, opt => opt.Ignore());

            // Sadece nullable alanları güncelle (Status, Comment)
            CreateMap<UpdateApprovalStepDTO, ApprovalStep>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<ApprovalStepDTO, ApprovalStep>()
                .ForMember(dest => dest.ExpenseRequest, opt => opt.Ignore())
                .ForMember(dest => dest.Approver, opt => opt.Ignore());

            // Department Mappings
            CreateMap<Department, DepartmentDTO>();

            CreateMap<CreateDepartmentDTO, Department>()
                .ForMember(dest => dest.DepartmentID, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.Users, opt => opt.Ignore())
                .ForMember(dest => dest.SubDepartments, opt => opt.Ignore());

            CreateMap<UpdateDepartmentDTO, Department>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // RequestLog Mappings
            CreateMap<RequestLog, RequestLogDTO>()
                .ForMember(dest => dest.UserName,
                    opt => opt.MapFrom(src => src.User != null ? src.User.NameSurname : null));

            CreateMap<CreateRequestLogDTO, RequestLog>()
                .ForMember(dest => dest.RequestLogID, opt => opt.Ignore())
                .ForMember(dest => dest.ActionDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.ExpenseRequest, opt => opt.Ignore())
                .ForMember(dest => dest.ActionBy, opt => opt.Ignore())
                .ForMember(dest => dest.OldValue, opt => opt.Ignore())
                .ForMember(dest => dest.NewValue, opt => opt.Ignore())
                .ForMember(dest => dest.IpAddress, opt => opt.Ignore());

            // Attachment mappings
            CreateMap<ExpenseAttachment, ExpenseAttachmentDTO>()
               .ForMember(dest => dest.UploadedByName,
                          opt => opt.MapFrom(src => src.UploadedByUser != null ? src.UploadedByUser.NameSurname : null));

            CreateMap<CreateExpenseAttachmentDTO, ExpenseAttachment>()
                .ForMember(dest => dest.ExpenseAttachmentID, opt => opt.Ignore())
                .ForMember(dest => dest.ExpenseRequest, opt => opt.Ignore())
                .ForMember(dest => dest.UploadedByUser, opt => opt.Ignore())
                .ForMember(dest => dest.UploadedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false))
                .ForMember(dest => dest.DeletedAt, opt => opt.Ignore());
        }
        
    }
}
