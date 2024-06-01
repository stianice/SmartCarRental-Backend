using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace CarRental.WebApi.Requirement
{
    public class PermissionRequirement : OperationAuthorizationRequirement
    {
        public long PermissionId { get; set; }
    }
}
