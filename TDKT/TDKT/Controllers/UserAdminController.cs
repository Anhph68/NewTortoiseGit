using TDKT.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace TDKT.Controllers
{
    [Authorize(Roles = "Quản trị")]
    public class UsersAdminController : Controller
    {
        public UsersAdminController()
        {
        }

        public UsersAdminController(ApplicationUserManager userManager, ApplicationRoleManager roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
        }

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private ApplicationRoleManager _roleManager;
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        private TDKTEntities db = new TDKTEntities();

        //
        // GET: /Users/
        public ActionResult Index()
        {
            ViewBag.Donvi = new SelectList(db.getDonVi(null, DateTime.Today.Year.ToString()), "MaDonVi", "TenDonVi");
            return View();
        }

        public ActionResult AjaxHandler(jQueryDataTableParamModel param)
        {
            var allResult = db.getUsers(string.IsNullOrEmpty(param.Donvi) ? "" : param.Donvi).ToList();

            IEnumerable<getUsers_Result> filteredResult;
            //Check whether the companies should be filtered by keyword
            if (!string.IsNullOrEmpty(param.sSearch))
            {
                //Optionally check whether the columns are searchable at all 
                var Searchable_0 = Convert.ToBoolean(Request["bSearchable_0"]);
                var Searchable_2 = Convert.ToBoolean(Request["bSearchable_2"]);
                var Searchable_3 = Convert.ToBoolean(Request["bSearchable_3"]);
                var Searchable_4 = Convert.ToBoolean(Request["bSearchable_4"]);
                var Searchable_6 = Convert.ToBoolean(Request["bSearchable_6"]);
                int tmp = int.TryParse(param.sSearch, out tmp) ? tmp : 0;

                filteredResult = allResult
                   .Where(c => Searchable_2 && c.HoTen.ToLower().Contains(param.sSearch.ToLower())
                            || Searchable_3 && c.TenDangNhap.ToLower().Contains(param.sSearch.ToLower())
                            || Searchable_4 && c.Email.ToLower().Contains(param.sSearch.ToLower())
                            || Searchable_6 && c.DonVi.ToLower().Contains(param.sSearch.ToLower())
                            || Searchable_0 && c.STT.Equals(tmp)
                        );
            }
            else filteredResult = allResult;

            var Sortable_0 = Convert.ToBoolean(Request["bSortable_0"]);
            var Sortable_2 = Convert.ToBoolean(Request["bSortable_2"]);
            var Sortable_3 = Convert.ToBoolean(Request["bSortable_3"]);
            var Sortable_4 = Convert.ToBoolean(Request["bSortable_4"]);
            var Sortable_6 = Convert.ToBoolean(Request["bSortable_6"]);
            var sortColumnIndex = Convert.ToInt64(Request["iSortCol_0"]);
            Func<getUsers_Result, string> orderingFunction = (c => sortColumnIndex == 2 && Sortable_2 ? c.HoTen : sortColumnIndex == 3 && Sortable_3 ? c.TenDangNhap : sortColumnIndex == 4 && Sortable_4 ? c.Email : sortColumnIndex == 6 && Sortable_6 ? c.DonVi : "");
            Func<getUsers_Result, Int64> orderingFunction2 = (c => sortColumnIndex == 0 && Sortable_0 ? c.STT : 0);

            var sortDirection = Request["sSortDir_0"]; // asc or desc
            if (sortDirection == "asc")
                filteredResult = filteredResult.OrderBy(orderingFunction).ThenBy(orderingFunction2);
            else
                filteredResult = filteredResult.OrderByDescending(orderingFunction).ThenByDescending(orderingFunction2);

            var displayed = filteredResult.Skip(param.iDisplayStart).Take(param.iDisplayLength);
            var result = displayed.Select(c => new
                                    {
                                        col0 = c.STT,
                                        col1 = c.ID,
                                        col2 = c.HoTen,
                                        col3 = c.TenDangNhap,
                                        col4 = c.Email,
                                        col5 = ListToString(UserManager.GetRoles(c.ID).ToList()),
                                        col6 = c.DonVi
                                    });

            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = allResult.Count(),
                iTotalDisplayRecords = filteredResult.Count(),
                aaData = result
            }, JsonRequestBehavior.AllowGet);

        }

        private string ListToString(IEnumerable<string> list)
        {
            string tmp = "";
            foreach (var l in list)
            {
                tmp += "<li>" + l + "</li>";
            }

            return tmp;
        }

        [HttpGet]
        public async Task<ActionResult> ResetPassword(string key)
        {
            if (key == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var u = await UserManager.FindByIdAsync(key);
            if (u == null)
            {
                return HttpNotFound();
            }
            var userRoles = await UserManager.GetRolesAsync(u.Id);

            ViewBag.Donvi = new SelectList(db.TD_DVKT.ToList(), "MA", "TEN");

            return PartialView(new EditUserViewModel()
            {
                Id = u.Id,
                Email = u.Email,
                RolesList = RoleManager.Roles.ToList().Select(x => new SelectListItem()
                {
                    Selected = userRoles.Contains(x.Name),
                    Text = x.Name,
                    Value = x.Name
                }),
                FullName = u.FullName,
                Username = u.UserName,
                PhoneNumber = u.PhoneNumber,
                MaKTV = u.MaKTV,
                ChucVu = u.ChucVu,
                MaDonVi = u.MaDonVi,
                GhiChu = u.GhiChu
            });
        }

        [HttpPost, ActionName("ResetPassword")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPasswordConfirmed(string id)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var user = await UserManager.FindByIdAsync(id);
                var code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                var pwd = "123";
                try
                {
                    await UserManager.ResetPasswordAsync(user.Id, code, pwd);
                }
                catch (Exception)
                {
                    TempData["Msg"] = "Có lỗi!";
                }
                
                TempData["Msg"] = "Đã thiết lập mật khẩu thành công!";
            }

            return RedirectToAction("Index");
        }

        //
        // GET: /Users/Create
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            //Get the list of Roles
            ViewBag.Donvi = new SelectList(db.getDonVi(null, DateTime.Today.Year.ToString()), "MaDonVi", "TenDonVi");
            ViewBag.RoleId = new SelectList(await RoleManager.Roles.ToListAsync(), "Name", "Name");

            return PartialView();
        }

        //
        // POST: /Users/Create
        [HttpPost]
        public async Task<ActionResult> Create(UserViewModel u, params string[] selectedRoles)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = new ApplicationUser
                    {
                        UserName = u.Username,
                        Email = u.Email,
                        PhoneNumber = u.PhoneNumber,
                        FullName = u.FullName,
                        MaDonVi = u.DonVi,
                        ChucVu = u.ChucVu,
                        MaKTV = u.MaKTV,
                        GhiChu = u.GhiChu
                    };
                    
                    var pwd = "123@Ktnn";
                    var adminresult = await UserManager.CreateAsync(user, pwd);

                    //Add User to the selected Roles 
                    if (adminresult.Succeeded)
                    {
                        if (selectedRoles != null)
                        {
                            var result = await UserManager.AddUserToRolesAsync(user.Id, selectedRoles);
                            if (!result.Succeeded)
                                TempData["Msg"] = "Có lỗi";
                        }
                        TempData["Msg"] = "Đã thêm 1 tài khoản mới";
                    }
                }
                catch (Exception)
                {
                    TempData["Msg"] = "Có lỗi";
                }

            }

            return RedirectToAction("Index");
        }
        //
        // GET: /Users/Edit/1
        [HttpGet]
        public async Task<ActionResult> Edit(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var u = await UserManager.FindByIdAsync(key);
            if (u == null)
            {
                return HttpNotFound();
            }

            var userRoles = await UserManager.GetRolesAsync(u.Id);

            ViewBag.Donvi = new SelectList(db.TD_DVKT.ToList(), "MA", "TEN");

            return PartialView(new EditUserViewModel()
            {
                Id = u.Id,
                Email = u.Email,
                RolesList = RoleManager.Roles.ToList().Select(x => new SelectListItem()
                {
                    Selected = userRoles.Contains(x.Name),
                    Text = x.Name,
                    Value = x.Name
                }),
                FullName = u.FullName,
                Username = u.UserName,
                PhoneNumber = u.PhoneNumber,
                MaKTV = u.MaKTV,
                ChucVu = u.ChucVu,
                MaDonVi = u.MaDonVi,
                GhiChu = u.GhiChu
            });
        }

        //
        // POST: /Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditUserViewModel editUser, params string[] selectedRole)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var u = await UserManager.FindByIdAsync(editUser.Id);
                    if (u == null)
                    {
                        return HttpNotFound();
                    }

                    u.FullName = editUser.FullName;
                    u.PhoneNumber = editUser.PhoneNumber;
                    u.MaKTV = editUser.MaKTV;
                    u.ChucVu = editUser.ChucVu;
                    u.MaDonVi = editUser.MaDonVi;
                    u.GhiChu = editUser.GhiChu;
                    u.Email = editUser.Email;

                    var userRoles = await UserManager.GetRolesAsync(u.Id);

                    selectedRole = selectedRole ?? new string[] { };

                    var result = await UserManager.AddUserToRolesAsync(u.Id, selectedRole.Except(userRoles).ToList<string>());

                    if (!result.Succeeded) TempData["Msg"] = "Có lỗi";

                    result = await UserManager.RemoveUserFromRolesAsync(u.Id, userRoles.Except(selectedRole).ToList<string>());

                    if (!result.Succeeded) TempData["Msg"] = "Có lỗi";
                }
                catch (Exception)
                {
                    TempData["Msg"] = "Có lỗi";
                }

                TempData["Msg"] = "Đã cập nhật thành công!";
            }

            return RedirectToAction("Index");
        }

        //
        // GET: /Users/Delete/5
        [HttpGet]
        public async Task<ActionResult> Delete(string key)
        {
            if (key == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var u = await UserManager.FindByIdAsync(key);
            if (u == null)
            {
                return HttpNotFound();
            }
            var userRoles = await UserManager.GetRolesAsync(u.Id);

            ViewBag.Donvi = new SelectList(db.TD_DVKT.ToList(), "MA", "TEN");

            return PartialView(new EditUserViewModel()
            {
                Id = u.Id,
                Email = u.Email,
                RolesList = RoleManager.Roles.ToList().Select(x => new SelectListItem()
                {
                    Selected = userRoles.Contains(x.Name),
                    Text = x.Name,
                    Value = x.Name
                }),
                FullName = u.FullName,
                Username = u.UserName,
                PhoneNumber = u.PhoneNumber,
                MaKTV = u.MaKTV,
                ChucVu = u.ChucVu,
                MaDonVi = u.MaDonVi,
                GhiChu = u.GhiChu
            });
        }

        //
        // POST: /Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var user = await UserManager.FindByIdAsync(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                var result = await UserManager.DeleteAsync(user);
                if (!result.Succeeded) TempData["Msg"] = "Có lỗi!";

                TempData["Msg"] = "Đã xóa thành công!";
            }

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
