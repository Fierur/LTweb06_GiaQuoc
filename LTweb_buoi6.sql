create database ql_nhasach
go
use ql_nhasach
go

DROP TABLE chitiet;
DROP TABLE hoadon;
DROP TABLE sanpham;
DROP TABLE khachhang;
DROP TABLE loai;
DROP TABLE nhasanxuat;

create table nhasanxuat
(
	MaNSX varchar(10),
	TenNSX nvarchar(50),
	primary key(MaNSX)
)

create table loai
(
	MaLoai varchar(10),
	TenLoai nvarchar(50),
	primary key(MaLoai)
)

create table khachhang
(
	MaKhachHang varchar(10),
	TenKH nvarchar(50),
	SoDT varchar(15),
	MatKhau varchar(50),
	primary key(MaKhachHang)
)

create table hoadon
(
	MaHoaDon varchar(10),
	NgayTao date,
	MaKH varchar(10),
	primary key(MaHoaDon),
	foreign key(MaKH) references khachhang(MaKhachHang)
)

create table sanpham
(
	MaSanPham varchar(10),
	TenSP nvarchar(50),
	MaL varchar(10),
	MaSX varchar(10),
	Gia int,
	GhiChu nvarchar(200),
	Hinh varchar(200),
	primary key(MaSanPham),
	foreign key(MaL) references loai(MaLoai),
	foreign key(MaSX) references nhasanxuat(MaNSX)
)

	CREATE TABLE chitiet
	(
	MaHD VARCHAR(10),
	MaSP VARCHAR(10),
	SoLuong INT,
	PRIMARY KEY (MaHD, MaSP),
	FOREIGN KEY (MaHD) REFERENCES hoadon(MaHoaDon),
	FOREIGN KEY (MaSP) REFERENCES sanpham(MaSanPham)
	);
-- Insert dữ liệu cho bảng loai (Loại sách)

INSERT INTO loai (MaLoai, TenLoai) VALUES
('L01', N'Sách giáo khoa'),
('L02', N'Sách từ điển'),
('L03', N'Sách đại học'),
('L04', N'Truyện tranh');

-- Insert dữ liệu cho bảng nhasanxuat (nhà sản xuất, giả sử bạn cần để khóa ngoại)

INSERT INTO nhasanxuat (MaNSX, TenNSX) VALUES
('NSX01', N'Nhà xuất bản Giáo dục'),
('NSX02', N'Nhà xuất bản Từ điển'),
('NSX03', N'Nhà xuất bản Đại học'),
('NSX04', N'Nhà xuất bản Truyện tranh');

INSERT INTO khachhang (MaKhachHang, TenKH, SoDT, MatKhau) VALUES
('KH01', N'Nguyễn Văn A', '0912345678', '123456'),
('KH02', N'Trần Thị B', '0987654321', 'abcdef'),
('KH03', N'Lê Văn C', '0909123456', 'password'),
('KH04', N'Phạm Thị D', '0978123456', 'qwerty'),
('KH05', N'Hoàng Văn E', '0933123456', '654321');

-- Insert dữ liệu cho bảng sanpham (Sản phẩm - sách)

-- Sách giáo khoa (L01) có 3 quyển

INSERT INTO sanpham (MaSanPham, TenSP, MaL, MaSX, Gia, GhiChu, Hinh) VALUES
('SP01', N'Toán lớp 1', 'L01', 'NSX01', 50000, N'Sách giáo khoa Toán lớp 1', '1.jpg'),
('SP02', N'Tiếng Việt lớp 1', 'L01', 'NSX01', 52000, N'Sách giáo khoa Tiếng Việt lớp 1', '2.jpg'),
('SP03', N'Tự nhiên và Xã hội lớp 1', 'L01', 'NSX01', 48000, N'Sách giáo khoa Tự nhiên và Xã hội lớp 1', '3.jpg');

-- Sách từ điển (L02) có 5 quyển

INSERT INTO sanpham (MaSanPham, TenSP, MaL, MaSX, Gia, GhiChu, Hinh) VALUES
('SP04', N'Từ điển 1000 từ', 'L02', 'NSX02', 150000, N'Từ điển 1000 từ phổ biến', '4.jpg'),
('SP05', N'Từ điển Anh-Anh', 'L02', 'NSX02', 200000, N'Từ điển Anh-Anh đầy đủ', '5.jpg'),
('SP06', N'Từ điển Hoa-Việt', 'L02', 'NSX02', 180000, N'Từ điển Hoa-Việt hữu ích', '6.jpg'),
('SP07', N'Từ điển Anh-Việt 500 từ', 'L02', 'NSX02', 160000, N'Từ điển Anh-Việt căn bản', '7.jpg'),
('SP08', N'Từ điển Việt-Anh', 'L02', 'NSX02', 170000, N'Từ điển Việt-Anh chuyên sâu', '8.jpg');

-- Sách đại học (L03) có 2 quyển

INSERT INTO sanpham (MaSanPham, TenSP, MaL, MaSX, Gia, GhiChu, Hinh) VALUES
('SP09', N'Giải tích đại số', 'L03', 'NSX03', 120000, N'Sách đại học Giải tích', '9.jpg'),
('SP10', N'Vật lý đại cương', 'L03', 'NSX03', 130000, N'Sách đại học Vật lý', '10.jpg');

-- Truyện tranh (L04) có 3 quyển

INSERT INTO sanpham (MaSanPham, TenSP, MaL, MaSX, Gia, GhiChu, Hinh) VALUES
('SP11', N'Naruto tập 1', 'L04', 'NSX04', 60000, N'Truyện tranh Naruto', '11.jpg'),
('SP12', N'Doraemon tập 2', 'L04', 'NSX04', 55000, N'Truyện tranh Doraemon', '12.jpg'),
('SP13', N'One Piece tập 3', 'L04', 'NSX04', 65000, N'Truyện tranh One Piece', '13.jpg');

INSERT INTO hoadon (MaHoaDon, NgayTao, MaKH) VALUES
('HD01', '2025-10-01', 'KH01'),
('HD02', '2025-10-02', 'KH02'),
('HD03', '2025-10-03', 'KH03'),
('HD04', '2025-10-04', 'KH04'),
('HD05', '2025-10-05', 'KH05'),
('HD06', '2025-10-06', 'KH01');
INSERT INTO chitiet (MaHD, MaSP, SoLuong) VALUES
-- Hóa đơn HD01
('HD01', 'SP01', 2),
('HD01', 'SP04', 1),

-- Hóa đơn HD02
('HD02', 'SP11', 3),
('HD02', 'SP12', 2),

-- Hóa đơn HD03
('HD03', 'SP09', 1),
('HD03', 'SP10', 1),
('HD03', 'SP05', 1),

-- Hóa đơn HD04
('HD04', 'SP07', 2),
('HD04', 'SP08', 1),

-- Hóa đơn HD05
('HD05', 'SP03', 2),
('HD05', 'SP06', 1),

-- Hóa đơn HD06
('HD06', 'SP02', 1),
('HD06', 'SP13', 4);

select * from loai