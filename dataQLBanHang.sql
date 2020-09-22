create Database DataQLBanHang
go

create table tblSanPham(
	MSP char(5) not null primary key(MSP),
	TenSP nvarchar(50) not null,
	Loai nvarchar(50) not null,
	Gia float null ,
	SoLuongCon int null,
)
go
insert into tblSanPham
	values  ('SP001',N'Điên thoai oppo RENO3',N'Điện thoại',7000 ,20),
			('SP002',N'Điên thoai samsung Galaxy A71',N'Điện thoại',8000 ,30),
			('SP003',N'Laptop hp15s du0063TU ',N'Laptop',11000 ,12),
			('SP004',N'Điên thoai iphone 11 XS max',N'Điện thoại',40000 ,10),
			('SP005',N'Apple watch s5',N'Đồng hồ',6000 ,70),
			('SP006',N'Laptop Apple MacBook 2020 i3',N'Laptop',28990 ,30),
			('SP007',N'Tai nghe AirPods Pro',N'Tai nghe',8000 ,10)

create table tblKhachHang(
	MKH char(5) not null primary key(MKH),
	TenKH nvarchar(50) not null ,
	NgaySinh date ,
	GioiTinh nvarchar(50),
	SDT nvarchar(50)
)
go
insert into tblKhachHang
	values  ('KH001',N'Phan Văn Tuấn',N'04/15/2000',N'Nam' ,'0763639952'),
			('KH002',N'Phan Anh Tài',N'05/14/1999',N'Nam' ,'0712334452'),
			('KH003',N'Phan Thi Hoa',N'03/18/1997',N'Nữ' ,'0762827232'),
			('KH004',N'Nguyễn Anh Tài',N'09/01/1998',N'Nam' ,'0761972552'),
			('KH005',N'Đào Quốc Sơn',N'01/03/1987',N'Nam' ,'0769273831'),
			('KH006',N'Ngô Thị Thảo',N'04/02/1992',N'Nữ' ,'0397625221'),
			('KH007',N'Lê Thi Thu',N'01/01/1991',N'Nữ' ,'0763927628')


create table tblHoaDon(
	MHD char(5) not null Primary key(MHD),
	MKH char(5) references tblKhachHang(MKH),
	TenKH nvarchar(50) not null,
	MSP char(5) references tblSanPham(MSP),
	TenSP nvarchar(50) not null,
	SLMua int not null,
	ThanhTien float 
)

insert into tblHoaDon
	values  ('HD001','KH002',N'Phan Anh Tài','SP001',N'Điên thoai oppo RENO3',2,14000),
			('HD002','KH003',N'Phan Thi Hoa','SP004',N'Điên thoai iphone 11 XS max',1,40000),
			('HD003','KH001',N'Phan Văn Tuấn','SP002',N'Điên thoai samsung Galaxy A71',1,8000),
			('HD004','KH006',N'Ngô Thị Thảo','SP007',N'Tai nghe AirPods Pro',1,8000),
			('HD005','KH007',N'Lê Thi Thu','SP005',N'Apple watch s5',1,6000),
			('HD006','KH003',N'Phan Thi Hoa','SP001',N'Điên thoai oppo RENO3',1,28990),
			('HD007','KH002',N'Phan Anh Tài','SP006',N'Laptop Apple MacBook 2020 i3',3,24000)















