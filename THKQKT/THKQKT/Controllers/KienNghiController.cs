using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using THKQKT.Models;
using THKQKT.Controllers;

namespace THKQKT.Controllers
{
    [Authorize]
    public class KienNghiController : Controller
    {
        private THKQKTEntities db = new THKQKTEntities();
        // GET: KienNghi
        public ActionResult Index()
        {
            Session["Url"] = Request.RawUrl;
            if (Session["year"] == null)
            {
                HttpContext.GetOwinContext().Authentication.SignOut();
                return RedirectToAction("Login", "Account");
            }
            ViewBag.Donvi = new SelectList(db.getDonViDo(Session["year"].ToString()), "MA", "TEN");
            ViewBag.LinhVuc = new SelectList(db.getLinhVuc(), "TEN", "TEN");
            return View();
        }

        /// <summary>
        /// Hiển thị danh sách chỉ tiêu của cuộc kiểm toán
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ChiTieuKienNghi(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Index", "Home");
            }

            getCuocByID_Result cuoc = db.getCuocByID(id).FirstOrDefault();
            Session["CuocID"] = cuoc.ID;
            Session["NgayThucHien"] = cuoc.NgayBatDauThucHien;

            return View(cuoc);
        }

        /// <summary>
        /// get danh sách các chỉ tiêu kiến nghị
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActionResult getKienNghiList(jQueryDataTableParamModel param)
        {
            if (Session["CuocID"] == null || Session["NgayThucHien"] == null)
            {
                HttpContext.GetOwinContext().Authentication.SignOut();
                return RedirectToAction("Login", "Account");
            }

            IEnumerable<sp_ThucHienKienNghi_List_Result> allResult = db.sp_ThucHienKienNghi_List(int.Parse(Session["CuocID"].ToString()), DateTime.Parse(Session["NgayThucHien"].ToString())).ToList();
            var tmpCount = allResult.Count();

            if (!string.IsNullOrEmpty(param.sSearch))
            {
                //Optionally check whether the columns are searchable at all 
                var Searchable_0 = Convert.ToBoolean(Request["bSearchable_0"]);
                int tmp = int.TryParse(param.sSearch, out tmp) ? tmp : 0;

                allResult = allResult
                    .Where(c => Searchable_0 && c.TenChiTieuMoi.ToLower().Contains(param.sSearch.ToLower()));
            }
            //else filteredResult = allResult;

            //var displayed = filteredResult.Skip(param.iDisplayStart).Take(param.iDisplayLength);
            var result = allResult.Select(c => new
            {
                col0 = c.TenChiTieuMoi,
                col1 = removeSymbol(string.Format(new CultureInfo("vi-VN"), "{0:C0}", c.SoKiemToan)),
                col2 = removeSymbol(string.Format(new CultureInfo("vi-VN"), "{0:C0}", c.SoDCGiam)),
                col3 = removeSymbol(string.Format(new CultureInfo("vi-VN"), "{0:C0}", c.SoDCTang)),
                col4 = removeSymbol(string.Format(new CultureInfo("vi-VN"), "{0:C0}", c.SoThucHien)),
                col5 = removeSymbol(string.Format(new CultureInfo("vi-VN"), "{0:C0}", c.ConLai)),
                col6 = c.Tyle,
                col7 = c.isCongZon,
                col8 = c.MaChiTieu
            });

            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = tmpCount,
                iTotalDisplayRecords = allResult.Count(),
                aaData = result
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult EditChiTieuKienNghi(int? key1, int? key2, string key3)
        {
            if (!key1.HasValue || !key2.HasValue)
            {
                return RedirectToAction("Index", "Home");
            }

            Session["MaChitieu"] = key1;
            Session["MaCuoc"] = key2;
            ViewBag.TenChiTieu = key3;

            return PartialView();
        }

        public ActionResult getSoLieuDieuChinh(tblChiTieuParamModel param)
        {
            IEnumerable<sp_GetSoLieuDieuChinh_Result> allResult = db.sp_GetSoLieuDieuChinh(int.Parse(param.MaChiTieu), int.Parse(param.MaCuoc)).ToList();
            var tmpCount = allResult.Count();

            var result = allResult.Select(c => new
            {
                col0 = c.MaSoLieuDieuChinh,
                col1 = @String.Format("{0:dd//MM/yyyy}", c.ThoiGian),
                col2 = c.NoiDung,
                col3 = removeSymbol(string.Format(new CultureInfo("vi-VN"), "{0:C0}", c.SoTienDCTang)),
                col4 = removeSymbol(string.Format(new CultureInfo("vi-VN"), "{0:C0}", c.SoTienDCGiam))
            });

            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = tmpCount,
                iTotalDisplayRecords = allResult.Count(),
                aaData = result
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public string SoLieuDieuChinh(SoLieuKienNghiViewModel soLieu)
        {
            tblSoLieuDieuChinh tmp = new tblSoLieuDieuChinh();

            tmp.MaCuoc = int.Parse(Session["MaCuoc"].ToString());
            tmp.MaChiTieu = long.Parse(Session["MaChitieu"].ToString());
            tmp.NoiDung = soLieu.SoLieuDieuChinh.NoiDung;
            tmp.ThoiGian = soLieu.SoLieuDieuChinh.ThoiGian;
            tmp.SoTienDCTang = decimal.Parse(soLieu.SoLieuDieuChinh.SoDCTang, NumberStyles.Currency, CultureInfo.CurrentCulture.NumberFormat);
            tmp.SoTienDCGiam = decimal.Parse(soLieu.SoLieuDieuChinh.SoDCGiam, NumberStyles.Currency, CultureInfo.CurrentCulture.NumberFormat); 
            string result = null;
            if (!soLieu.SoLieuDieuChinh.MaSoLieuChiTieu.HasValue)
            {
                try
                {
                    //Create new
                    db.tblSoLieuDieuChinhs.Add(tmp);
                    db.SaveChanges();

                    result = "Cập nhật thành công!";
                }
                catch (Exception)
                {
                    result = "Có lỗi";
                }

            }
            else
            {
                try
                {
                    //Update
                    tmp.MaSoLieuDieuChinh = long.Parse(soLieu.SoLieuDieuChinh.MaSoLieuChiTieu.ToString());
                    db.Entry(tmp).State = EntityState.Modified;
                    db.SaveChanges();

                    result = "Cập nhật thành công!";
                }
                catch (Exception)
                {
                    result = "Có lỗi";
                }

            }
            return result;
        }

        [HttpPost]
        public string DelSoLieuDieuChinh(string key)
        {
            if (string.IsNullOrEmpty(key))
                return "Có lỗi";
            var tmp1 = long.Parse(key);
            var tmp = db.tblSoLieuDieuChinhs.FirstOrDefault(c => c.MaSoLieuDieuChinh == tmp1);
            if (tmp.MaCuoc != int.Parse(Session["MaCuoc"].ToString()) || tmp.MaChiTieu != int.Parse(Session["MaChitieu"].ToString()))
                return "Có lỗi";
            try
            {
                db.tblSoLieuDieuChinhs.Remove(tmp);
                db.SaveChanges();
            }
            catch (Exception)
            {
                return "Có lỗi";
            }

            return "Xóa thành công!";
        }

        public ActionResult getSoLieuThucHien(tblChiTieuParamModel param)
        {
            IEnumerable<sp_GetSoLieuTHKienNghi_Result> allResult = db.sp_GetSoLieuTHKienNghi(int.Parse(param.MaChiTieu), int.Parse(param.MaCuoc)).ToList();
            var tmpCount = allResult.Count();

            var result = allResult.Select(c => new
            {
                col0 = c.MaSoLieuTHKienNghi,
                col1 = string.Format("{0:dd//MM/yyyy}", c.ThoiGian),
                col2 = c.NoiDung,
                col3 = removeSymbol(string.Format(new CultureInfo("vi-VN"), "{0:C0}", c.SoTien)),
            });

            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = tmpCount,
                iTotalDisplayRecords = allResult.Count(),
                aaData = result
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public string SoLieuThucHien(SoLieuKienNghiViewModel soLieu)
        {
            tblSoLieuTHKienNghi tmp = new tblSoLieuTHKienNghi();

            tmp.MaCuoc = int.Parse(Session["MaCuoc"].ToString());
            tmp.MaChiTieu = long.Parse(Session["MaChitieu"].ToString());
            tmp.NoiDung = soLieu.SoLieuThucHien.NoiDung;
            tmp.ThoiGian = soLieu.SoLieuThucHien.ThoiGian;
            tmp.SoTien = decimal.Parse(soLieu.SoLieuThucHien.SoTien, NumberStyles.Currency, CultureInfo.CurrentCulture.NumberFormat); 
            string result = null;
            if (!soLieu.SoLieuThucHien.MaSoLieuChiTieu.HasValue)
            {
                try
                {
                    //Create new
                    db.tblSoLieuTHKienNghis.Add(tmp);
                    db.SaveChanges();

                    result = "Cập nhật thành công!";
                }
                catch (Exception)
                {
                    result = "Có lỗi";
                }

            }
            else
            {
                try
                {
                    //Update
                    tmp.MaSoLieuTHKienNghi = long.Parse(soLieu.SoLieuThucHien.MaSoLieuChiTieu.ToString());
                    db.Entry(tmp).State = EntityState.Modified;
                    db.SaveChanges();

                    result = "Cập nhật thành công!";
                }
                catch (Exception)
                {
                    result = "Có lỗi";
                }

            }
            return result;
        }

        [HttpPost]
        public string DelSoLieuThucHien(string key)
        {
            if (string.IsNullOrEmpty(key))
                return "Có lỗi";
            var tmp1 = long.Parse(key);
            var tmp = db.tblSoLieuTHKienNghis.FirstOrDefault(c => c.MaSoLieuTHKienNghi == tmp1);
            if (tmp.MaCuoc != int.Parse(Session["MaCuoc"].ToString()) || tmp.MaChiTieu != int.Parse(Session["MaChitieu"].ToString()))
                return "Có lỗi";
            try
            {
                db.tblSoLieuTHKienNghis.Remove(tmp);
                db.SaveChanges();
            }
            catch (Exception)
            {
                return "Có lỗi";
            }

            return "Xóa thành công!";
        }

        public string removeSymbol(string c)
        {
            return c.Substring(0, c.Length - 2);
        }
    }
}