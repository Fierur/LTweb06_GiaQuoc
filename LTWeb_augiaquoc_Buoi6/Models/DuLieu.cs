using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LTWeb_augiaquoc_Buoi6.Models
{
    public class DuLieu
    {
        static string conStr = ConfigurationManager.ConnectionStrings["ShopConnection"].ConnectionString;
        SqlConnection con = new SqlConnection(conStr);
        public List<Loai> dsLoai = new List<Loai>();
        public List<SanPham> dsSP = new List<SanPham>();

        public DuLieu()
        {
            ThietLap_Loai();
            ThietLap_SP();

        }

        public void ThietLap_SP()
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from sanpham", con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            foreach(DataRow dr in dt.Rows)
            {
                var sp = new SanPham();
                sp.masp = dr["MaSanPham"].ToString();
                sp.tensp = dr["TenSP"].ToString();
                sp.maloai = dr["MaL"].ToString();
                sp.gia = int.Parse(dr["Gia"].ToString());
                sp.ghichu = dr["GhiChu"].ToString();
                sp.hinh = dr["Hinh"].ToString();

                dsSP.Add(sp);
            }
        }

        public void ThietLap_Loai()
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from loai", con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                var loai = new Loai();
                loai.maloai = dr["MaLoai"].ToString();
                loai.tenloai = dr["TenLoai"].ToString();

                dsLoai.Add(loai);

            }
        }

        public List<SanPham> LaySP_TheoMaLoai(string maloai)
        {
            List<SanPham> sanPhams = new List<SanPham>();
            //trong sql MaL = 'L01' nen phai co dau ''
            SqlDataAdapter da = new SqlDataAdapter("select * from sanpham where MaL = '"+maloai + "'", con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            foreach(DataRow dr in dt.Rows)
            {
                var spml = new SanPham();
                spml.masp = dr["MaSanPham"].ToString();
                spml.tensp = dr["TenSP"].ToString();
                spml.maloai = dr["MaL"].ToString();
                spml.gia = int.Parse(dr["Gia"].ToString());
                spml.ghichu = dr["GhiChu"].ToString();
                spml.hinh = dr["Hinh"].ToString();

                sanPhams.Add(spml);
            }
            return sanPhams;
        }

        public SanPham ChiTietSP(string masp)
        {
            SanPham sanpham = new SanPham();
            SqlDataAdapter da = new SqlDataAdapter("select * from sanpham where MaSanPham = '" + masp + "'", con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            sanpham.masp = dt.Rows[0]["MaSanPham"].ToString();
            sanpham.tensp = dt.Rows[0]["TenSP"].ToString();
            sanpham.maloai = dt.Rows[0]["MaL"].ToString();
            sanpham.gia = int.Parse(dt.Rows[0]["Gia"].ToString());
            sanpham.ghichu = dt.Rows[0]["GhiChu"].ToString();
            sanpham.hinh = dt.Rows[0]["Hinh"].ToString();

            return sanpham;

        }

        public KhachHang DangNhap(string sodt, string pass)
        {
            KhachHang kh = new KhachHang();
            SqlDataAdapter da = new SqlDataAdapter("select * from khachhang where SoDT = '" + sodt + "' and MatKhau = '" + pass + "'", con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                kh.makh = dt.Rows[0]["MaKhachHang"].ToString();
                kh.tenkh = dt.Rows[0]["TenKH"].ToString();
                kh.dienthoai = dt.Rows[0]["SoDT"].ToString();
                kh.matkhau = dt.Rows[0]["MatKhau"].ToString();
                return kh;
            }
            return null;
        }

        public List<SanPham> TimKiemSP(string maloai, string sapxep)
        {
            List<SanPham> sanPhams = new List<SanPham>();
            string query = "select * from sanpham";
            if (!string.IsNullOrEmpty(maloai))
            {
                query += " where MaL = '" + maloai + "'";
            }

            if (sapxep == "cao")
            {
                query += " order by Gia DESC";
            }
            else if (sapxep == "thap")
            {
                query += " order by Gia ASC";
            }

            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            
            foreach (DataRow dr in dt.Rows)
            {
                var sp = new SanPham();
                sp.masp = dr["MaSanPham"].ToString();
                sp.tensp = dr["TenSP"].ToString();
                sp.maloai = dr["MaL"].ToString();
                sp.gia = int.Parse(dr["Gia"].ToString());
                sp.ghichu = dr["GhiChu"].ToString();
                sp.hinh = dr["Hinh"].ToString();
                sanPhams.Add(sp);
            }
            return sanPhams;
        }

        public List<HoaDon> LayLichSuGiaoDich(string makh)
        {
            List<HoaDon> dsHoaDon = new List<HoaDon>();
            SqlDataAdapter da = new SqlDataAdapter("select * from hoadon where MaKH = '" + makh + "'", con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                HoaDon hd = new HoaDon();
                hd.mahoadon = dr["MaHoaDon"].ToString();
                hd.ngaytao = Convert.ToDateTime(dr["NgayTao"]);
                hd.makh = dr["MaKH"].ToString();
                dsHoaDon.Add(hd);
            }
            return dsHoaDon;
        }

        public List<ChiTiet> LayChiTietHoaDon(string mahoadon)
        {
            List<ChiTiet> dsChiTiet = new List<ChiTiet>();
            SqlDataAdapter da = new SqlDataAdapter("select * from chitiet where MaHD = '" + mahoadon + "'", con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                ChiTiet ct = new ChiTiet();
                ct.mahoadon = dr["MaHD"].ToString();
                ct.masp = dr["MaSP"].ToString();
                ct.soluong = int.Parse(dr["SoLuong"].ToString());
                dsChiTiet.Add(ct);
            }
            return dsChiTiet;
        }
    }
}