CREATE DATABASE WebDocTruyen;
go
use WebDocTruyen
go

CREATE TABLE TheLoai(
	Ma_Theloai int primary Key,
	Ten_Theloai nvarchar(50),
)

go
CREATE TABLE Nguoidung(
	Ma_Nguoidung int primary Key,
	Ten_Nguoidung nvarchar(50),
	Email_Nguoidung nvarchar(50),
	Pass_Nguoidung nvarchar(10),
	Quyen_Nguoidung bit,
)

go 
CREATE TABLE Truyen(
	Ma_Truyen int primary Key,
	Ten_Truyen nvarchar(MAX),
	HinhDaiDien nvarchar(50),
	MoTa_Truyen nvarchar(MAX),
	NgayDang_Truyen datetime,
	Ma_Theloai int FOREIGN KEY REFERENCES TheLoai(Ma_Theloai),
	Ma_Nguoidung int FOREIGN KEY REFERENCES Nguoidung(Ma_Nguoidung),
	
)

go 
CREATE TABLE Chuong(
	Ma_Chuong nvarchar(10) primary Key,
	So_Chuong int,
	Ten_Chuong nvarchar(MAX),
	NoiDung_Chuong nvarchar(MAX),
	Luotview_Chuong int,
	Ma_Truyen int FOREIGN KEY REFERENCES Truyen(Ma_Truyen),
)

go
CREATE TABLE DanhGia(
	Ma_DanhGia int primary Key,
	NoiDung_DanhGia nvarchar(MAX),
	Ma_Nguoidung int FOREIGN KEY REFERENCES Nguoidung(Ma_Nguoidung),
	Ma_Truyen int FOREIGN KEY REFERENCES Truyen(Ma_Truyen),
)