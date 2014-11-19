using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace THKQKT.Models
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "{0} không được để trống")]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu hiện tại")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "{0} không được để trống")]
        [StringLength(100, ErrorMessage = "{0} phải có tối thiểu {2} ký tự.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu mới")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Nhập lại mật khẩu mới")]
        [Compare("NewPassword", ErrorMessage = "Mật khẩu mới được nhập lại không đúng.")]
        public string ConfirmPassword { get; set; }
    }

    //public interface ChangeInfoViewModel
    //{
    //    [Required(ErrorMessage = "Không được để trống!")]
    //    [MaxLength(50)]
    //    [Display(Name = "Họ và tên")]
    //    public string FullName { get; set; }

    //    [Required(ErrorMessage = "Chọn một đơn vị")]
    //    [Display(Name = "Đơn vị công tác")]
    //    public string MaDonVi { get; set; }

    //    [Required(ErrorMessage = "Không được để trống!")]
    //    [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Email không đúng kiểu!")]
    //    [Display(Name = "Email")]
    //    public string Email { get; set; }

    //    [Display(Name = "Mã thẻ Kiểm toán viên")]
    //    public string MaKTV { get; set; }

    //    [Display(Name = "Chức vụ")]
    //    public string ChucVu { get; set; }

    //    [Display(Name = "Số điện thoại")]
    //    public string PhoneNumber { get; set; }
    //}

}