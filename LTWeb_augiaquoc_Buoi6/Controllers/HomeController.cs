using LTWeb_augiaquoc_Buoi6.Models;
using LTWeb_augiaquoc_Buoi6.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LTWeb_augiaquoc_Buoi6.Controllers
{
    public class HomeController : Controller
    {
        DuLieu csdl = new DuLieu();

        public ActionResult Index(string tenkh, string makh)
        {
            ViewBag.TenKH = tenkh;
            ViewBag.MaKH = makh;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangNhap(string sodt, string pass)
        {
            KhachHang kh = csdl.DangNhap(sodt, pass);
            if (kh != null)
            {
                if (csdl.dsSP.Any())
                {
                    string masp = csdl.dsSP.First().masp;
                    return RedirectToAction("HienThiChiTietSP", new { masp = masp, tenkh = kh.tenkh, makh = kh.makh });
                }
                return RedirectToAction("Index", new { tenkh = kh.tenkh, makh = kh.makh });
            }
            else
            {
                return RedirectToAction("HienThiSPTheoMaLoai");
            }
        }

        [HttpGet]
        public ActionResult HienThiChiTietSP(string masp, string tenkh, string makh)
        {
            SanPham sanpham = csdl.ChiTietSP(masp);
            ViewBag.TenKH = tenkh;
            ViewBag.MaKH = makh;
            return View(sanpham);
        }

        [HttpGet]
        public ActionResult HienThiSPTheoMaLoai(string maloai, string tenkh, string makh)
        {
            Loai_SP_ViewModel vm = new Loai_SP_ViewModel();
            if (maloai == null || maloai == "")
            {
                if (csdl.dsLoai.Any())
                    maloai = csdl.dsLoai.First().maloai;
                else
                    return View(vm);
            }
            List<SanPham> SanPham = maloai != "" ? csdl.LaySP_TheoMaLoai(maloai) : csdl.dsSP;
            List<Loai> Loai = csdl.dsLoai;
            vm.sanphams = SanPham;
            vm.loais = Loai;
            ViewBag.TenKH = tenkh;
            ViewBag.MaKH = makh;

            return View(vm);
        }

      

        [HttpGet]
        public ActionResult TimKiem(string tenkh, string makh)
        {
            Loai_SP_ViewModel vm = new Loai_SP_ViewModel();
            vm.loais = csdl.dsLoai;
            vm.sanphams = csdl.dsSP;
            ViewBag.TenKH = tenkh;
            ViewBag.MaKH = makh;
            return View(vm);
        }

        [HttpPost]
        public ActionResult TimKiem(string maloai, string sapxep, string tenkh, string makh)
        {
            Loai_SP_ViewModel vm = new Loai_SP_ViewModel();
            vm.loais = csdl.dsLoai;
            vm.sanphams = csdl.TimKiemSP(maloai, sapxep);
            ViewBag.TenKH = tenkh;
            ViewBag.MaKH = makh;
            return View(vm);
        }

        [HttpGet]
        public ActionResult LichSuGiaoDich(string makh, string tenkh)
        {
            ViewBag.TenKH = tenkh;
            ViewBag.MaKH = makh;

            // Nếu chưa đăng nhập, trả về view với Model null
            if (string.IsNullOrEmpty(makh))
            {
                return View(new List<HoaDon>());
            }

            // Lấy danh sách hóa đơn
            List<HoaDon> dsHoaDon = csdl.LayLichSuGiaoDich(makh);
            return View(dsHoaDon);
        }

        [HttpGet]
        public ActionResult ChiTietHoaDon(string mahoadon, string makh, string tenkh)
        {
            ViewBag.TenKH = tenkh;
            ViewBag.MaKH = makh;
            ViewBag.MaHoaDon = mahoadon;

            if (string.IsNullOrEmpty(makh))
            {
                return RedirectToAction("LichSuGiaoDich");
            }

            List<ChiTiet> dsChiTiet = csdl.LayChiTietHoaDon(mahoadon);
            return View(dsChiTiet);
        }
    }
}