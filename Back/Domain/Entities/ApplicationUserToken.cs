using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class ApplicationUserToken : IdentityUserToken<string>
    {
        public bool IsExpired { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
