using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using THKQKT.Models;

namespace THKQKT.Controllers
{
    [Authorize]
    public class TongHopController : Controller
    {

        private THKQKTEntities db = new THKQKTEntities();

        // GET: /SoLieu
        /// <summary>
        /// Hiển thị danh sách các cuộc kiểm toán
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public ActionResult Index()
        {
            Session["Url"] = Request.RawUrl;
            if (Session["year"] == null)
            {
                HttpContext.GetOwinContext().Authentication.SignOut();
                return RedirectToAction("Login", "Account");
            }
            //ViewBag.Type = type;
            ViewBag.Donvi = new SelectList(db.getDonViDo(Session["year"].ToString()), "MA", "TEN");
            ViewBag.LinhVuc = new SelectList(db.getLinhVuc(), "TEN", "TEN");
            return View();
        }

        /// <summary>
        /// Lọc danh sách các cuộc kiểm toán
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActionResult AjaxHandler(tblCuocKiemtoanParamModel param)
        {
            IEnumerable<getCuocStatus_thkqkt_Result> allResult = db.getCuocStatus_thkqkt(param.Year, DateTime.Parse(Session["date"].ToString()), string.IsNullOrEmpty(param.Status) ? "" : param.Status, (Session["donvi"] != null) ? Session["donvi"].ToString() : "");
            var filterDonVi = string.IsNullOrEmpty(param.Donvi) ? "" : param.Donvi;
            var filterLinhVuc = string.IsNullOrEmpty(param.LinhVuc) ? "" : param.LinhVuc;
            allResult = (filterDonVi == "") ? allResult.ToList() : allResult.Where(r => r.MaDonVi == filterDonVi).ToList();
            allResult = (filterLinhVuc == "") ? allResult.ToList() : allResult.Where(r => r.linhvuc == filterLinhVuc).ToList();
            IEnumerable<getCuocStatus_thkqkt_Result> filteredResult;
            //Check whether the companies should be filtered by keyword
            if (!string.IsNullOrEmpty(param.sSearch))
            {
                //Optionally check whether the columns are searchable at all 
                var Searchable_0 = Convert.ToBoolean(Request["bSearchable_0"]);
                var Searchable_2 = Convert.ToBoolean(Request["bSearchable_2"]);
                var Searchable_3 = Convert.ToBoolean(Request["bSearchable_3"]);
                var Searchable_4 = Convert.ToBoolean(Request["bSearchable_4"]);
                var Searchable_5 = Convert.ToBoolean(Request["bSearchable_5"]);
                int tmp = int.TryParse(param.sSearch, out tmp) ? tmp : 0;

                filteredResult = allResult
                    .Where(c => Searchable_2 && c.TenCuoc.ToLower().Contains(param.sSearch.ToLower())
                             || Searchable_3 && c.DonVi.ToLower().Contains(param.sSearch.ToLower())
                             || Searchable_4 && c.SoQuyetDinh.ToLower().Contains(param.sSearch.ToLower())
                             || Searchable_5 && c.linhvuc.ToLower().Contains(param.sSearch.ToLower())
                             || Searchable_0 && c.STT.Equals(tmp)
                     );
            }
            else filteredResult = allResult;

            var Sortable_0 = Convert.ToBoolean(Request["bSortable_0"]);
            var Sortable_2 = Convert.ToBoolean(Request["bSortable_2"]);
            var Sortable_3 = Convert.ToBoolean(Request["bSortable_3"]);
            var Sortable_4 = Convert.ToBoolean(Request["bSortable_4"]);
            var Sortable_5 = Convert.ToBoolean(Request["bSortable_5"]);
            var sortColumnIndex = Convert.ToInt64(Request["iSortCol_0"]);
            Func<getCuocStatus_thkqkt_Result, string> orderingFunction = (c => sortColumnIndex == 2 && Sortable_2 ? c.TenCuoc :
                                                            sortColumnIndex == 3 && Sortable_3 ? c.DonVi :
                                                            sortColumnIndex == 4 && Sortable_4 ? c.SoQuyetDinh :
                                                            sortColumnIndex == 5 && Sortable_5 ? c.linhvuc :
                                                            "");
            Func<getCuocStatus_thkqkt_Result, Int64> orderingFunction2 = (c => sortColumnIndex == 0 && Sortable_0 ? c.STT : 0);

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
                                 col2 = c.TenCuoc,
                                 col3 = c.DonVi,
                                 col4 = c.SoQuyetDinh,
                                 col5 = c.linhvuc
                             });
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = allResult.Count(),
                iTotalDisplayRecords = filteredResult.Count(),
                aaData = result
            }, JsonRequestBehavior.AllowGet);

        }

        /// <summary>
        /// Hiển thị danh sách chỉ tiêu của cuộc kiểm toán
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ChiTieuTongHop(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Index", "Home");
            }

            getCuocByID_Result cuoc = db.getCuocByID(id).FirstOrDefault();
            Session["CuocID"] = cuoc.ID;
            Session["NgayThucHien"] = cuoc.NgayBatDauThucHien;
            //var tmp = db.sp_TongHopKetQua_List(id, cuoc.NgayBatDauThucHien);
            return View(cuoc);
        }


        [HttpPost]
        public ActionResult EditChiTieuTonghop(int? key1, int? key2, string key3)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public string SoLieuTongHop(SoLieuTongHopViewModel soLieu)
        {
            tblSoLieuChiTieu tmp = new tblSoLieuChiTieu();

            tmp.MaCuoc = int.Parse(Session["MaCuoc"].ToString());
            tmp.MaChiTieu = long.Parse(Session["MaChitieu"].ToString());
            tmp.NoiDung = soLieu.NoiDung;
            tmp.ThoiGian = soLieu.ThoiGian;
            tmp.SoTien = decimal.Parse(soLieu.SoTien, NumberStyles.Currency, CultureInfo.CurrentCulture.NumberFormat);

            string result = null;
            if (!soLieu.MaSoLieuChiTieu.HasValue)
            {
                try
                {
                    //Create new
                    db.tblSoLieuChiTieux.Add(tmp);
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
                    tmp.MaSoLieuChiTieu = long.Parse(soLieu.MaSoLieuChiTieu.ToString());
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
        public string DelSoLieuTongHop(string key)
        {
            if (string.IsNullOrEmpty(key))
                return "Có lỗi";
            var tmp1 = long.Parse(key);
            var tmp = db.tblSoLieuChiTieux.FirstOrDefault(c => c.MaSoLieuChiTieu == tmp1);
            if (tmp.MaCuoc != int.Parse(Session["MaCuoc"].ToString()) || tmp.MaChiTieu != int.Parse(Session["MaChitieu"].ToString()))
                return "Có lỗi";
            try
            {
                db.tblSoLieuChiTieux.Remove(tmp);
                db.SaveChanges();
            }
            catch (Exception)
            {
                return "Có lỗi!";
            }

            return "Xóa thành công!";
        }

        /// <summary>
        /// Dùng để get các lần nhập cho 1 chỉ tiêu
        /// </summary>
        /// <param name="key1">Mã chỉ tiêu</param>
        /// <param name="key2">Mã cuộc</param>
        /// <returns></returns>
        public ActionResult getSoLieuChiTieu(tblChiTieuParamModel param)
        {
            IEnumerable<sp_GetSoLieuChiTieu_Result> allResult = db.sp_GetSoLieuChiTieu(int.Parse(param.MaChiTieu), int.Parse(param.MaCuoc)).ToList();
            var tmpCount = allResult.Count();

            var result = allResult.Select(c => new
            {
                col0 = c.MaSoLieuChiTieu,
                col1 = String.Format("{0:dd//MM/yyyy}", c.ThoiGian),
                col2 = c.NoiDung,
                col3 = removeSymbol(string.Format("{0:C0}", c.SoTien)),
            });

            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = tmpCount,
                iTotalDisplayRecords = allResult.Count(),
                aaData = result
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// get danh sách các chỉ tiêu tổng hợp
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActionResult getTongHopList(jQueryDataTableParamModel param)
        {
            if (Session["CuocID"] == null || Session["NgayThucHien"] == null)
            {
                HttpContext.GetOwinContext().Authentication.SignOut();
                return RedirectToAction("Login", "Account");
            }

            IEnumerable<sp_TongHopKetQua_List_Result> allResult = db.sp_TongHopKetQua_List(int.Parse(Session["CuocID"].ToString()), DateTime.Parse(Session["NgayThucHien"].ToString())).ToList();
            var tmpCount = allResult.Count();

            //IEnumerable<sp_TongHopKetQua_List_Result> filteredResult;
            //Check whether the companies should be filtered by keyword
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
                col1 = removeSymbol(string.Format("{0:C0}", c.SoTien)),
                col2 = c.isCongZon,
                col3 = c.MaChiTieu
            });

            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = tmpCount,
                iTotalDisplayRecords = allResult.Count(),
                aaData = result
            }, JsonRequestBehavior.AllowGet);
        }

        public string removeSymbol(string c)
        {
            return c.Substring(0, c.Length - 2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult QuyetToanCanDoi()
        {
            return View();
        }

        public ActionResult QuyetToanThu()
        {
            return View();
        }

        public ActionResult QuyetToanChi()
        {
            return View();
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