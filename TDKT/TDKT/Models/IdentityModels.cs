using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using BrockAllen.IdentityReboot;
using BrockAllen.IdentityReboot.Ef;

namespace TDKT.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityRebootUser
    {
        [Display(Name = "Họ tên")]
        [MaxLength(50)]
        public string FullName { get; set; }

        [Display(Name = "Đơn vị")]
        [MaxLength(50)]
        public string MaDonVi { get; set; }

        [Display(Name = "Mã KTV")]
        [MaxLength(30)]
        public string MaKTV { get; set; }

        [Display(Name = "Chức vụ")]
        [MaxLength(30)]
        public string ChucVu { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(
            IdentityRebootUserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}