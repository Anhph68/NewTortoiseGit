using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TDKT.Models
{
    public class RoleViewModel
    {
        public string Id { get; set; }
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "RoleName")]
        public string Name { get; set; }
    }

    public class EditUserViewModel
    {
        public string Id { get; set; }

        //[Required(AllowEmptyStrings = false)]
        //[Display(Name = "Email")]
        //[EmailAddress]
        //public string Email { get; set; }

        public IEnumerable<SelectListItem> RolesList { get; set; }

        [Required(ErrorMessage = "Không được để trống!")]
        [MaxLength(50)]
        [Display(Name = "Họ và tên")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Chọn một đơn vị")]
        [Display(Name = "Đơn vị công tác")]
        public string DonVi { get; set; }

        [Required(ErrorMessage = "Không được để trống!")]
        [Display(Name = "Tên đăng nhập")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Không được để trống!")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Email không đúng kiểu!")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Mã thẻ Kiểm toán viên")]
        public string MaKTV { get; set; }

        [Display(Name = "Chức vụ")]
        public string ChucVu { get; set; }

        [Display(Name = "Ghi chú")]
        public string GhiChu { get; set; }

        [Display(Name = "Số điện thoại")]
        public string PhoneNumber { get; set; }
    }
}