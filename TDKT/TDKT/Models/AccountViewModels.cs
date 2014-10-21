using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace TDKT.Models
{

    public class LoginViewModel
    {
        [Required(ErrorMessage = "{0} không được để trống!")]
        [Display(Name = "Tên đăng nhập")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "{0} không được để trống!")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[0-9]+.*)(?=.*[a-zA-Z]+.*).{6,}$", ErrorMessage = "{0} phải có ít nhất 1 chữ số (0-9)")]
        [MinLength(6, ErrorMessage = "{0} tối thiểu {1} kí tự")]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [Display(Name = "Nhớ đăng nhập?")]
        public bool RememberMe { get; set; }
    }

    /// <summary>
    /// Đăng ký tài khoản người sử dụng
    /// </summary>
    public class UserViewModel
    {
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

        //[Required(ErrorMessage = "Không được để trống!")]
        //[StringLength(100, ErrorMessage = "Tối thiểu {2} ký tự!", MinimumLength = 6)]
        //[DataType(DataType.Password)]
        //[Display(Name = "Mật khẩu")]
        //public string Password { get; set; }

        //[Required(ErrorMessage = "Không được để trống!")]
        //[DataType(DataType.Password)]
        //[Display(Name = "Khẳng định mật khẩu")]
        //[Compare("Password", ErrorMessage = "Mật khẩu nhập lại không đúng")]
        //public string ConfirmPassword { get; set; }

        [Display(Name = "Mã thẻ Kiểm toán viên")]
        public string MaKTV { get; set; }

        [Display(Name = "Chức vụ")]
        public string ChucVu { get; set; }

        [Display(Name = "Ghi chú")]
        public string GhiChu { get; set; }

        [Display(Name = "Số điện thoại")]
        public string PhoneNumber { get; set; }
    }

    public class SendCodeViewModel {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
    }

    public class VerifyCodeViewModel {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }
    }

    public class ForgotViewModel {
        [Required(ErrorMessage = "Không được để trống!")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Email không đúng kiểu!")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ResetPasswordViewModel {
        [Required(ErrorMessage = "Không được để trống!")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Email không đúng kiểu!")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Không được để trống!")]
        [StringLength(100, ErrorMessage = "Tối thiểu {2} ký tự!", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Không được để trống!")]
        [DataType(DataType.Password)]
        [Display(Name = "Khẳng định mật khẩu")]
        [Compare("Password", ErrorMessage = "Mật khẩu nhập lại không đúng")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel {
        [Required(ErrorMessage = "Không được để trống!")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Email không đúng kiểu!")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}