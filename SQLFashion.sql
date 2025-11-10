
CREATE DATABASE MaleFashion;
GO


USE MaleFashion;
GO

--Bảng tài khoản người dùng
CREATE TABLE TaiKhoan (
    MaTK INT IDENTITY(1,1) PRIMARY KEY,
    HoTen NVARCHAR(100),
    Email NVARCHAR(100) UNIQUE,
    MatKhau NVARCHAR(100),
    SoDienThoai NVARCHAR(20),
    DiaChi NVARCHAR(255),
    NgayTao DATETIME DEFAULT GETDATE(),
    VaiTro NVARCHAR(50) -- 'KhachHang', 'Admin'
);


--Bảng danh mục sản phẩm
CREATE TABLE DanhMuc (
    MaDanhMuc INT IDENTITY(1,1) PRIMARY KEY,
    TenDanhMuc NVARCHAR(100),
    MoTa NVARCHAR(255)
);


--Bảng sản phẩm
CREATE TABLE SanPham (
    MaSP INT IDENTITY(1,1) PRIMARY KEY,
    TenSP NVARCHAR(200),
    Gia DECIMAL(18,2),
    MoTa NVARCHAR(MAX),
    AnhChinh NVARCHAR(255),
    MaDanhMuc INT,
    SoLuongTon INT DEFAULT 0,
    TrangThai BIT DEFAULT 1, -- 1: hiển thị, 0: ẩn
    FOREIGN KEY (MaDanhMuc) REFERENCES DanhMuc(MaDanhMuc)
);


--Bảng ảnh sản phẩm phụ
CREATE TABLE AnhSanPham (
    MaAnh INT IDENTITY(1,1) PRIMARY KEY,
    MaSP INT,
    DuongDan NVARCHAR(255),
    FOREIGN KEY (MaSP) REFERENCES SanPham(MaSP)
);


--Bảng giỏ hàng
CREATE TABLE GioHang (
    MaGioHang INT IDENTITY(1,1) PRIMARY KEY,
    MaTK INT,
    NgayCapNhat DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (MaTK) REFERENCES TaiKhoan(MaTK)
);


--Bảng chi tiết giỏ hàng
CREATE TABLE ChiTietGioHang (
    MaGioHang INT,
    MaSP INT,
    SoLuong INT DEFAULT 1,
    PRIMARY KEY (MaGioHang, MaSP),
    FOREIGN KEY (MaGioHang) REFERENCES GioHang(MaGioHang),
    FOREIGN KEY (MaSP) REFERENCES SanPham(MaSP)
);


--Bảng đơn hàng
CREATE TABLE DonHang (
    MaDH INT IDENTITY(1,1) PRIMARY KEY,
    MaTK INT,
    NgayDat DATETIME DEFAULT GETDATE(),
    TongTien DECIMAL(18,2),
    TrangThai NVARCHAR(50), -- Đang xử lý, Đã giao, Đã hủy
    DiaChiGiao NVARCHAR(255),
    FOREIGN KEY (MaTK) REFERENCES TaiKhoan(MaTK)
);


--Chi tiết đơn hàng
CREATE TABLE ChiTietDonHang (
    MaDH INT,
    MaSP INT,
    SoLuong INT,
    Gia DECIMAL(18,2),
    PRIMARY KEY (MaDH, MaSP),
    FOREIGN KEY (MaDH) REFERENCES DonHang(MaDH),
    FOREIGN KEY (MaSP) REFERENCES SanPham(MaSP)
);


--Thanh toán
CREATE TABLE ThanhToan (
    MaTT INT IDENTITY(1,1) PRIMARY KEY,
    MaDH INT,
    PhuongThuc NVARCHAR(50), -- COD, VNPAY, Momo,...
    TrangThai NVARCHAR(50),
    NgayThanhToan DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (MaDH) REFERENCES DonHang(MaDH)
);


--Đánh giá sản phẩm
CREATE TABLE DanhGia (
    MaDG INT IDENTITY(1,1) PRIMARY KEY,
    MaSP INT,
    MaTK INT,
    Diem INT CHECK(Diem BETWEEN 1 AND 5),
    NoiDung NVARCHAR(500),
    NgayDanhGia DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (MaSP) REFERENCES SanPham(MaSP),
    FOREIGN KEY (MaTK) REFERENCES TaiKhoan(MaTK)
);


--Blog
CREATE TABLE BaiViet (
    MaBV INT IDENTITY(1,1) PRIMARY KEY,
    TieuDe NVARCHAR(200),
    NoiDung NVARCHAR(MAX),
    AnhBia NVARCHAR(255),
    NgayDang DATETIME DEFAULT GETDATE(),
    MaTK INT,
    FOREIGN KEY (MaTK) REFERENCES TaiKhoan(MaTK)
);


--Bình luận blog
CREATE TABLE BinhLuan (
    MaBL INT IDENTITY(1,1) PRIMARY KEY,
    MaBV INT,
    MaTK INT,
    NoiDung NVARCHAR(500),
    NgayBinhLuan DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (MaBV) REFERENCES BaiViet(MaBV),
    FOREIGN KEY (MaTK) REFERENCES TaiKhoan(MaTK)
);


--Khuyến mãi / Mã giảm giá
CREATE TABLE KhuyenMai (
    MaKM INT IDENTITY(1,1) PRIMARY KEY,
    MaSP INT NULL,
    MaDanhMuc INT NULL,
    MaCode NVARCHAR(50) UNIQUE,
    TyLeGiam DECIMAL(5,2),
    NgayBatDau DATETIME,
    NgayKetThuc DATETIME,
    FOREIGN KEY (MaSP) REFERENCES SanPham(MaSP),
    FOREIGN KEY (MaDanhMuc) REFERENCES DanhMuc(MaDanhMuc)
);

-- 1. BẢNG BANNER
CREATE TABLE Banner (
    MaBanner INT IDENTITY(1,1) PRIMARY KEY,
    TieuDe NVARCHAR(200),
    MoTa NVARCHAR(255),
    Anh NVARCHAR(255),
    LienKet NVARCHAR(255),
    TrangThai BIT DEFAULT 1
);
GO


-- BẢNG DEAL OF THE WEEK
CREATE TABLE DealOfWeek (
    MaDeal INT IDENTITY(1,1) PRIMARY KEY,
    MaSP INT,
    TieuDe NVARCHAR(200),
    TyLeGiam DECIMAL(5,2),
    NgayKetThuc DATETIME,
    FOREIGN KEY (MaSP) REFERENCES SanPham(MaSP)
);
GO


-- BẢNG INSTAGRAM IMAGE
CREATE TABLE InstagramImage (
    MaAnh INT IDENTITY(1,1) PRIMARY KEY,
    DuongDan NVARCHAR(255),
    TrangThai BIT DEFAULT 1
);
GO


-- 4. BẢNG THÔNG TIN SHOP
CREATE TABLE ThongTinShop (
    MaTT INT IDENTITY(1,1) PRIMARY KEY,
    TenShop NVARCHAR(100),
    DiaChi NVARCHAR(255),
    Email NVARCHAR(100),
    SoDienThoai NVARCHAR(50),
    MoTa NVARCHAR(500),
    Logo NVARCHAR(255)
);
GO







-- =============== DANH MỤC ===============
INSERT INTO DanhMuc (TenDanhMuc, MoTa) VALUES
(N'Áo', N'Áo sơ mi, polo, t-shirt - cao cấp'),
(N'Quần', N'Quần jeans, chino, trousers - cao cấp'),
(N'Giày', N'Giày thể thao & giày da cao cấp'),
(N'Phụ Kiện', N'Thắt lưng, đồng hồ, túi xách cao cấp'),
(N'Áo Khoác', N'Jacket, blazer, coat - cao cấp'),
(N'Đồ Thể Thao', N'Hoodie, sweats, sportwear thương hiệu'),

(N'Áo Sơ Mi', N'Áo sơ mi thời trang nam cao cấp từ ZARA'),
(N'Áo Thun', N'Áo thun cotton mềm mại'),
(N'Quần Jean', N'Quần jean dáng slim fit trẻ trung'),
(N'Áo Khoác', N'Áo khoác mùa đông ấm áp'),
(N'Áo Vest', N'Áo vest công sở sang trọng'),
(N'Quần Tây', N'Quần tây văn phòng thanh lịch'),
(N'Áo Polo', N'Áo polo thể thao trẻ trung'),
(N'Giày Sneaker', N'Giày sneaker thời trang'),
(N'Giày Da', N'Giày da thật cao cấp'),
(N'Túi Xách', N'Túi xách nam tiện dụng'),
(N'Ví Da', N'Ví da bò cao cấp'),
(N'Dây Nịt', N'Dây nịt da thật'),
(N'Kính Mát', N'Kính mát thời trang nam'),
(N'Đồng Hồ', N'Đồng hồ đeo tay phong cách'),
(N'Balo', N'Balo da cao cấp'),
(N'Áo Hoodie', N'Áo hoodie trẻ trung năng động'),
(N'Quần Short', N'Quần short nam mùa hè'),
(N'Giày Lười', N'Giày lười da mềm'),
(N'Giày Boots', N'Giày boots cổ thấp'),
(N'Phụ Kiện Khác', N'Phụ kiện thời trang nam'),
(N'Áo Khoác Da', N'Áo khoác da thật nam tính'),
(N'Áo Blazer', N'Áo blazer hiện đại'),
(N'Áo Len', N'Áo len mùa đông'),
(N'Áo Gió', N'Áo gió thể thao'),
(N'Giày Tây', N'Giày tây công sở'),
(N'Thắt Lưng', N'Thắt lưng da cao cấp'),
(N'Mũ Lưỡi Trai', N'Mũ lưỡi trai nam'),
(N'Tất Nam', N'Tất cotton mềm mại'),
(N'Áo Thun In Hình', N'Áo thun in hình cá tính'),
(N'Giày Running', N'Giày chạy bộ Adidas, Nike'),
(N'Giày Thể Thao', N'Giày thể thao đa năng'),
(N'Áo Sơ Mi Họa Tiết', N'Áo sơ mi họa tiết độc đáo'),
(N'Áo Tanktop', N'Áo tanktop mùa hè'),
(N'Áo Dạ', N'Áo dạ Hàn Quốc'),
(N'Áo Bomber', N'Áo bomber cá tính'),
(N'Áo Khoác Gió', N'Áo khoác gió thể thao'),
(N'Quần Jogger', N'Quần jogger phong cách đường phố'),
(N'Giày Sandal', N'Giày sandal nam'),
(N'Áo Thể Thao', N'Áo thể thao Gym'),
(N'Giày Cổ Cao', N'Giày cổ cao Converse'),
(N'Áo Khoác Lông', N'Áo khoác lông mùa đông'),
(N'Giày Slip-on', N'Giày slip-on da mềm'),
(N'Áo Blazer Kẻ', N'Áo blazer kẻ caro sang trọng'),
(N'Áo Sơ Mi Trơn', N'Áo sơ mi trơn phong cách tối giản'),
(N'Áo Khoác Jean', N'Áo khoác jean phong cách trẻ'),
(N'Áo Vest Slimfit', N'Áo vest slimfit trẻ trung'),
(N'Áo Polo Dài Tay', N'Áo polo tay dài'),
(N'Áo Sweater', N'Áo sweater mùa thu'),
(N'Áo Khoác Măng Tô', N'Áo măng tô lịch lãm'),
(N'Giày Casual', N'Giày casual everyday'),
(N'Áo Thun Basic', N'Áo thun basic nam tính');
GO


-- ========== 50 SẢN PHẨM (Ảnh: picsum.photos, mở trực tiếp được) ==========
INSERT INTO SanPham (TenSP, Gia, MoTa, AnhChinh, MaDanhMuc, SoLuongTon)
VALUES
-- 1
(N'Zara Linen Shirt', 49.99, N'Áo linen nam, form regular, thoáng mát.', 'https://picsum.photos/id/100/800/600', 1, 42),
(N'Massimo Dutti Cotton Shirt', 89.00, N'Áo sơ mi cotton cao cấp, kiểu dáng thanh lịch.', 'https://picsum.photos/id/101/800/600', 1, 55),
(N'Uniqlo Oxford Shirt', 39.99, N'Áo Oxford trẻ trung, dễ phối đồ.', 'https://picsum.photos/id/102/800/600', 1, 60),
(N'COS Relaxed Shirt', 95.00, N'Áo form relaxed, cotton cao cấp.', 'https://picsum.photos/id/103/800/600', 1, 50),
(N'H&M Slim Fit Shirt', 29.99, N'Áo sơ mi slim fit, phong cách hiện đại.', 'https://picsum.photos/id/104/800/600', 1, 35),
(N'Levi’s Western Shirt', 69.00, N'Áo denim phong cách western.', 'https://picsum.photos/id/105/800/600', 1, 40),
(N'Tommy Classic Shirt', 99.00, N'Áo sơ mi cotton cổ điển, bền bỉ.', 'https://picsum.photos/id/106/800/600', 1, 38),
(N'Checked Casual Shirt', 45.50, N'Áo sơ mi caro, phong cách casual.', 'https://picsum.photos/id/107/800/600', 1, 47),

-- 9..16 (Quần)
(N'Mango Slim-fit Trousers', 79.99, N'Quần tây slim-fit, hoàn thiện sang trọng.', 'https://picsum.photos/id/108/800/600', 2, 48),
(N'Zara Regular Jeans', 59.99, N'Jeans regular fit, denim cao cấp.', 'https://picsum.photos/id/109/800/600', 2, 70),
(N'Massimo Dutti Chino', 99.00, N'Chino vải mềm, dáng thanh lịch.', 'https://picsum.photos/id/110/800/600', 2, 31),
(N'Levi’s 511 Slim', 69.00, N'Quần jeans slim của Levi’s.', 'https://picsum.photos/id/111/800/600', 2, 60),
(N'Uniqlo Smart Pants', 49.90, N'Quần smart pants co giãn thoải mái.', 'https://picsum.photos/id/112/800/600', 2, 64),
(N'H&M Jogger', 39.99, N'Quần jogger thoải mái, năng động.', 'https://picsum.photos/id/113/800/600', 2, 44),
(N'Calvin Klein Dress Pants', 119.00, N'Quần tây công sở cao cấp.', 'https://picsum.photos/id/114/800/600', 2, 28),
(N'Cargo Street Pants', 69.00, N'Quần cargo phong cách đường phố.', 'https://picsum.photos/id/115/800/600', 2, 53),

-- 17..24 (Giày)
(N'Clarks Leather Derby', 150.00, N'Giày da Derby, làm thủ công.', 'https://picsum.photos/id/116/800/600', 3, 22),
(N'Nike Air Force 1', 120.00, N'Sneaker cổ điển, độ bền cao.', 'https://picsum.photos/id/117/800/600', 3, 45),
(N'Adidas Ultraboost', 180.00, N'Sneaker hiệu năng, êm chân.', 'https://picsum.photos/id/118/800/600', 3, 30),
(N'Massimo Loafers', 210.00, N'Loafer da cao cấp, công sở.', 'https://picsum.photos/id/119/800/600', 3, 34),
(N'New Balance Retro', 139.00, N'Sneaker phong cách retro.', 'https://picsum.photos/id/120/800/600', 3, 29),
(N'Chelsea Leather Boot', 159.00, N'Boot Chelsea da thật, thời thượng.', 'https://picsum.photos/id/121/800/600', 3, 26),
(N'Casual Slip-on Shoes', 49.99, N'Giày slip-on thoải mái, hàng ngày.', 'https://picsum.photos/id/122/800/600', 3, 52),
(N'Running Performance Shoes', 129.00, N'Giày chạy bộ nhẹ, đàn hồi tốt.', 'https://picsum.photos/id/123/800/600', 3, 33),

-- 25..32 (Phụ kiện)
(N'Hugo Boss Leather Belt', 129.00, N'Thắt lưng da thật, mặt kim loại mạ sáng.', 'https://picsum.photos/id/124/800/600', 4, 66),
(N'Fossil Chronograph Watch', 199.00, N'Đồng hồ dây da cổ điển.', 'https://picsum.photos/id/125/800/600', 4, 28),
(N'Coach Leather Wallet', 180.00, N'Ví da nam cao cấp.', 'https://picsum.photos/id/126/800/600', 4, 53),
(N'Ray-Ban Sunglasses', 159.00, N'Kính mát Ray-Ban cổ điển.', 'https://picsum.photos/id/127/800/600', 4, 77),
(N'Tumi Business Backpack', 499.00, N'Balo cao cấp cho doanh nhân.', 'https://picsum.photos/id/128/800/600', 4, 19),
(N'Wool Scarf Premium', 69.00, N'Khăn len cao cấp giữ ấm.', 'https://picsum.photos/id/129/800/600', 4, 41),
(N'Leather Gloves', 59.00, N'Găng tay da thật, ấm và bền.', 'https://picsum.photos/id/130/800/600', 4, 36),
(N'Canvas Tote Bag', 39.00, N'Túi vải canvas đa dụng.', 'https://picsum.photos/id/131/800/600', 4, 47),

-- 33..40 (Áo khoác)
(N'Zara Tailored Blazer', 169.00, N'Blazer may đo, phong cách công sở.', 'https://picsum.photos/id/132/800/600', 5, 27),
(N'Massimo Dutti Wool Coat', 299.00, N'Áo khoác len wool cao cấp, giữ ấm tốt.', 'https://picsum.photos/id/133/800/600', 5, 18),
(N'Mango Short Jacket', 139.99, N'Jacket ngắn tối giản, tinh tế.', 'https://picsum.photos/id/134/800/600', 5, 42),
(N'Uniqlo Down Jacket', 129.00, N'Áo phao siêu nhẹ giữ ấm.', 'https://picsum.photos/id/135/800/600', 5, 36),
(N'H&M Bomber Jacket', 79.99, N'Bomber trẻ trung, năng động.', 'https://picsum.photos/id/136/800/600', 5, 50),
(N'Classic Trench Coat', 219.00, N'Áo măng tô cổ điển, chống gió.', 'https://picsum.photos/id/137/800/600', 5, 24),
(N'Leather Biker Jacket', 289.00, N'Jacket da phong cách biker.', 'https://picsum.photos/id/138/800/600', 5, 15),
(N'Sport Softshell Jacket', 119.00, N'Áo khoác softshell chống nước nhẹ.', 'https://picsum.photos/id/139/800/600', 5, 31),

-- 41..50 (Đồ thể thao)
(N'Nike Tech Fleece Hoodie', 129.00, N'Hoodie Tech Fleece nhẹ và ấm.', 'https://picsum.photos/id/140/800/600', 6, 48),
(N'Adidas Track Pants', 89.00, N'Quần track thoáng khí, co giãn.', 'https://picsum.photos/id/141/800/600', 6, 38),
(N'Lululemon Pullover', 128.00, N'Pullover vải mềm mịn cao cấp.', 'https://picsum.photos/id/142/800/600', 6, 23),
(N'Under Armour Tee', 59.00, N'Áo thun tập co giãn, thoáng mát.', 'https://picsum.photos/id/143/800/600', 6, 60),
(N'Puma Running Shoes', 139.00, N'Giày chạy bộ nhẹ và bền.', 'https://picsum.photos/id/144/800/600', 6, 33),
(N'Sports Compression Shorts', 29.99, N'Quần nén tập luyện, thoáng khí.', 'https://picsum.photos/id/145/800/600', 6, 41),
(N'Performance Training Jacket', 99.00, N'Áo khoác tập luyện, chống gió.', 'https://picsum.photos/id/146/800/600', 6, 26),
(N'Gym Duffel Bag', 69.00, N'Túi gym dung tích lớn, bền.', 'https://picsum.photos/id/147/800/600', 6, 37);


-- =============== ẢNH SẢN PHẨM ===============
INSERT INTO AnhSanPham (MaSP, DuongDan) VALUES
(1, 'https://www.zara.com/uk/en/man-shirts-linen-l754.html'),
(2, 'https://www.massimodutti.com/gb/men/shirts-n1356'),
(3, 'https://www.cos.com/en-us/men/shirts'),
(4, 'https://shop.mango.com/us/en/p/men/pants/slim-fit/slim-fit-trousers_17061137?c=08'),
(5, 'https://www.massimodutti.com/gb/men/trousers-n1399'),
(6, 'https://www.zara.com/us/en/man-jeans-l659.html'),
(7, 'https://www.clarks.com/en-lv/mens'),
(8, 'https://www.massimodutti.com/us/men/shoes-n1420'),
(9, 'https://www.zara.com/us/en/man-shoes-trainers-l657.html'),
(10, 'https://www.hugoboss.com/'),
(11, 'https://www.fossil.com/'),
(12, 'https://www.coach.com/'),
(13, 'https://www.zara.com/uk/en/man-blazers-l756.html'),
(14, 'https://www.massimodutti.com/gb/men/coats-n1361'),
(15, 'https://shop.mango.com/us/en/c/men/jackets'),
(16, 'https://www.nike.com/'),
(17, 'https://www.adidas.com/'),
(18, 'https://shop.lululemon.com/'),
--Bổ sung ảnh
(19, 'https://picsum.photos/id/200/800/600'), -- Nike Air Force 1
(20, 'https://picsum.photos/id/201/800/600'), -- Adidas Ultraboost
(21, 'https://picsum.photos/id/202/800/600'), -- Massimo Loafers
(22, 'https://picsum.photos/id/203/800/600'), -- New Balance Retro
(23, 'https://picsum.photos/id/204/800/600'), -- Chelsea Leather Boot
(24, 'https://picsum.photos/id/205/800/600'), -- Casual Slip-on Shoes
(25, 'https://picsum.photos/id/206/800/600'), -- Running Performance Shoes
(26, 'https://picsum.photos/id/207/800/600'), -- Hugo Boss Leather Belt
(27, 'https://picsum.photos/id/208/800/600'), -- Fossil Chronograph Watch
(28, 'https://picsum.photos/id/209/800/600'), -- Coach Leather Wallet
(29, 'https://picsum.photos/id/210/800/600'), -- Ray-Ban Sunglasses
(30, 'https://picsum.photos/id/211/800/600'), -- Tumi Business Backpack
(31, 'https://picsum.photos/id/212/800/600'), -- Wool Scarf Premium
(32, 'https://picsum.photos/id/213/800/600'), -- Leather Gloves
(33, 'https://picsum.photos/id/214/800/600'), -- Canvas Tote Bag
(34, 'https://picsum.photos/id/215/800/600'), -- Zara Tailored Blazer
(35, 'https://picsum.photos/id/216/800/600'), -- Massimo Dutti Wool Coat
(36, 'https://picsum.photos/id/217/800/600'), -- Mango Short Jacket
(37, 'https://picsum.photos/id/218/800/600'), -- Uniqlo Down Jacket
(38, 'https://picsum.photos/id/219/800/600'); -- H&M Bomber Jacket

-- =============== TÀI KHOẢN ===============
INSERT INTO TaiKhoan (HoTen, Email, MatKhau, SoDienThoai, DiaChi, VaiTro) VALUES
(N'Nguyễn Văn Nam', 'vana@example.com', '123456', '0901000001', N'Hà Nội',  N'KhachHang'),
(N'Trần Văn B', 'tranb@example.com', '123456', '0901000002', N'Hồ Chí Minh',  N'KhachHang'),
(N'Lê Hữu Cường', 'lecuong@example.com', '123456', '0901000003', N'Đà Nẵng',  N'KhachHang'),

(N'Nguyễn Văn An', 'nguyenvana@example.com', '123456', '+84 912345678', N'Hanoi, Vietnam', 'KhachHang'),
(N'Trần Thị Diệu', 'tranthib@example.com', '123456', '+84 912345679', N'Ho Chi Minh, Vietnam', 'KhachHang'),
('Admin', 'admin@malefashion.com', 'admin123', '+84 900000000', N'Ho Chi Minh City', 'Admin'),

(N'Phạm Đức Huy', 'huypham@example.com', '123456', '0902345678', N'Hải Phòng, Việt Nam', N'KhachHang'),
(N'Lương Thu Hà', 'thuhaluong@example.com', 'ha2025', '0908765432', N'Đà Lạt, Việt Nam', N'KhachHang'),
(N'Trần Quốc Minh', 'minhtq@example.com', 'minh123', '+84 912345690', N'Cần Thơ, Việt Nam', N'KhachHang');

-- =============== GIỎ HÀNG + CHI TIẾT GIỎ HÀNG ===============
INSERT INTO GioHang (MaTK) VALUES (1), (2);
INSERT INTO ChiTietGioHang (MaGioHang, MaSP, SoLuong) VALUES (1, 1, 1), (1, 7, 1), (2, 5, 1);

INSERT INTO GioHang (MaTK, NgayCapNhat)
VALUES
(3, '2025-08-02 10:12:45.123'),
(8, '2025-08-03 14:55:21.980'),
(1, '2025-08-05 09:45:33.550'),
(6, '2025-08-06 11:22:59.620'),
(9, '2025-08-07 17:48:10.217'),
(4, '2025-08-08 15:30:41.912'),
(2, '2025-08-09 19:05:10.765'),
(5, '2025-08-10 20:22:55.321'),
(7, '2025-08-11 08:10:24.884'),
(3, '2025-08-12 13:33:19.558'),

(1, '2025-09-01 09:11:42.667'),
(4, '2025-09-03 16:25:30.105'),
(6, '2025-09-04 10:44:51.340'),
(9, '2025-09-05 20:10:13.927'),
(8, '2025-09-07 11:33:22.540'),
(2, '2025-09-09 18:45:19.415'),
(5, '2025-09-10 14:55:48.005'),
(7, '2025-09-12 08:12:54.882'),
(3, '2025-09-13 21:20:30.648'),
(4, '2025-09-15 19:45:13.224'),

(9, '2025-10-02 12:10:22.518'),
(8, '2025-10-04 17:22:31.819'),
(2, '2025-10-06 11:43:19.912'),
(6, '2025-10-08 20:55:22.178'),
(1, '2025-10-10 09:17:50.665'),
(7, '2025-10-12 13:31:15.437'),
(3, '2025-10-15 15:42:33.982'),
(4, '2025-10-18 19:55:49.871'),
(5, '2025-10-22 08:24:37.456'),
(9, '2025-10-28 16:40:12.793');

-- =============== ĐƠN HÀNG + CHI TIẾT ===============
INSERT INTO DonHang (MaTK, NgayDat, TongTien, TrangThai, DiaChiGiao) VALUES
(1, GETDATE(), 289.99, N'Da giao', 'Hanoi, Vietnam'),
(2, GETDATE(), 349.00, N'Dang xu ly', 'Ho Chi Minh, Vietnam');

--Dữ liệu thống kê Đơn Hàng
INSERT INTO DonHang (MaTK, NgayDat, TongTien, TrangThai, DiaChiGiao)
VALUES
(3, '2024-10-05 14:32:18', 429.50, N'Hoàn tất', N'Hà Nội, Việt Nam'),
(7, '2024-10-14 10:12:55', 379.99, N'Hoàn tất', N'Hồ Chí Minh, Việt Nam'),
(1, '2024-10-23 19:20:44', 520.25, N'Hoàn tất', N'Đà Nẵng, Việt Nam'),
(5, '2024-10-29 08:30:15', 285.90, N'Hoàn tất', N'Hải Phòng, Việt Nam'),
(8, '2024-10-30 15:40:05', 610.10, N'Hoàn tất', N'Bình Dương, Việt Nam'),

(4, '2024-11-01 13:11:23', 799.50, N'Hoàn tất', N'Vũng Tàu, Việt Nam'),
(6, '2024-11-04 09:50:33', 460.25, N'Hoàn tất', N'Hà Nội, Việt Nam'),
(9, '2024-11-09 18:22:54', 730.40, N'Hoàn tất', N'Hồ Chí Minh, Việt Nam'),
(2, '2024-11-15 16:33:11', 312.99, N'Hoàn tất', N'Đà Nẵng, Việt Nam'),
(5, '2024-11-19 11:29:45', 409.80, N'Hoàn tất', N'Cần Thơ, Việt Nam'),

(8, '2024-12-03 10:12:15', 529.99, N'Hoàn tất', N'Hà Nội, Việt Nam'),
(4, '2024-12-07 13:22:55', 239.50, N'Hoàn tất', N'Hồ Chí Minh, Việt Nam'),
(3, '2024-12-12 08:47:28', 499.99, N'Hoàn tất', N'Đà Nẵng, Việt Nam'),
(9, '2024-12-17 09:35:10', 369.75, N'Hoàn tất', N'Hải Phòng, Việt Nam'),
(2, '2024-12-25 19:22:41', 540.85, N'Hoàn tất', N'Vũng Tàu, Việt Nam'),

(6, '2025-01-05 15:35:21', 399.99, N'Hoàn tất', N'Hà Nội, Việt Nam'),
(1, '2025-01-09 10:45:05', 612.30, N'Hoàn tất', N'Hồ Chí Minh, Việt Nam'),
(7, '2025-01-16 09:15:35', 742.60, N'Hoàn tất', N'Đà Nẵng, Việt Nam'),
(9, '2025-01-22 12:55:10', 501.20, N'Hoàn tất', N'Nha Trang, Việt Nam'),
(8, '2025-01-29 18:14:44', 420.40, N'Hoàn tất', N'Bình Dương, Việt Nam'),

(3, '2025-02-03 16:20:22', 825.50, N'Hoàn tất', N'Hà Nội, Việt Nam'),
(4, '2025-02-09 10:12:33', 305.25, N'Hoàn tất', N'Hồ Chí Minh, Việt Nam'),
(2, '2025-02-14 14:23:45', 479.10, N'Hoàn tất', N'Đà Nẵng, Việt Nam'),
(5, '2025-02-19 08:44:58', 658.99, N'Hoàn tất', N'Hải Phòng, Việt Nam'),
(9, '2025-02-26 21:32:12', 591.35, N'Hoàn tất', N'Cần Thơ, Việt Nam'),

(7, '2025-03-04 09:18:25', 319.45, N'Hoàn tất', N'Hà Nội, Việt Nam'),
(8, '2025-03-12 13:42:51', 520.99, N'Hoàn tất', N'Hồ Chí Minh, Việt Nam'),
(1, '2025-03-19 17:35:45', 659.40, N'Hoàn tất', N'Đà Nẵng, Việt Nam'),
(6, '2025-03-24 08:12:20', 385.70, N'Hoàn tất', N'Vũng Tàu, Việt Nam'),
(3, '2025-03-29 10:45:30', 452.50, N'Hoàn tất', N'Hải Phòng, Việt Nam'),

(9, '2025-04-04 09:22:19', 735.80, N'Hoàn tất', N'Cần Thơ, Việt Nam'),
(4, '2025-04-12 15:45:02', 499.99, N'Hoàn tất', N'Hà Nội, Việt Nam'),
(8, '2025-04-19 18:12:11', 609.20, N'Hoàn tất', N'Hồ Chí Minh, Việt Nam'),
(2, '2025-04-25 08:56:33', 382.10, N'Hoàn tất', N'Đà Nẵng, Việt Nam'),
(7, '2025-04-30 11:48:44', 415.00, N'Hoàn tất', N'Hải Phòng, Việt Nam'),

(1, '2025-05-05 12:32:11', 333.25, N'Hoàn tất', N'Nha Trang, Việt Nam'),
(5, '2025-05-11 08:25:29', 555.80, N'Hoàn tất', N'Hà Nội, Việt Nam'),
(9, '2025-06-04 14:15:23', 630.10, N'Hoàn tất', N'Hồ Chí Minh, Việt Nam'),
(3, '2025-07-07 09:12:45', 711.25, N'Hoàn tất', N'Đà Nẵng, Việt Nam'),
(6, '2025-08-02 15:05:18', 295.99, N'Hoàn tất', N'Hải Phòng, Việt Nam'),
(8, '2025-09-10 08:59:11', 459.45, N'Hoàn tất', N'Hà Nội, Việt Nam'),
(4, '2025-10-08 17:42:23', 839.75, N'Hoàn tất', N'Hồ Chí Minh, Việt Nam');



--======================================================End Đơn Hàng chi tiết=======================================================================
INSERT INTO ChiTietDonHang (MaDH, MaSP, SoLuong, Gia) VALUES
(1, 1, 1, 49.99),
(1, 10, 1, 240.00),
(2, 13, 1, 169.00),
(2, 16, 1, 129.00);

-- =============== THANH TOÁN ===============
INSERT INTO ThanhToan (MaDH, PhuongThuc, TrangThai)
VALUES
(1, 'COD', N'Thanh cong'),
(2, 'VNPAY', N'Dang cho xu ly');

-- =============== ĐÁNH GIÁ ===============
INSERT INTO DanhGia (MaSP, MaTK, Diem, NoiDung) VALUES
(1, 1, 5, N'Áo linen mát, form chuẩn, rất ưng.'),
(7, 2, 4, N'Giày da êm, nhưng size hơi chật.'),
(16, 1, 5, N'Hoodie giữ ấm tốt, chất vải cao cấp.');

-- =============== BÀI VIẾT + BÌNH LUẬN ===============
INSERT INTO BaiViet (TieuDe, NoiDung, AnhBia, MaTK) VALUES
(N'Cách phối áo sơ mi linen sang trọng', N'Áo linen là lựa chọn hoàn hảo cho mùa hè...', 'https://www.zara.com/uk/en/man-shirts-linen-l754.html', 3),
(N'Top 5 mẫu giày da đáng mua 2025', N'Giày da Derby và Loafer luôn là biểu tượng của sự tinh tế...', 'https://www.massimodutti.com/us/men/shoes-n1420', 3);

INSERT INTO BinhLuan (MaBV, MaTK, NoiDung) VALUES
(1, 1, N'Bài viết hữu ích, cảm ơn admin!'),
(2, 2, N'Đôi loafer này mình đã mua, rất đẹp.');

-- =============== KHUYẾN MÃI ===============
INSERT INTO KhuyenMai (MaSP, MaDanhMuc, MaCode, TyLeGiam, NgayBatDau, NgayKetThuc)
VALUES
(NULL, 1, 'WELCOME10', 10.00, GETDATE(), DATEADD(month,1,GETDATE())),
(5, NULL, 'MDT15', 15.00, GETDATE(), DATEADD(month,1,GETDATE()));


-- BANNER 
INSERT INTO Banner (TieuDe, MoTa, Anh, LienKet)
VALUES
(N'ZARA Men FW25', 
 N'Khám phá bộ sưu tập Thu – Đông 2025 từ ZARA, mang phong cách tối giản và tinh tế, kết hợp chất liệu cao cấp cùng thiết kế hiện đại, tạo nên vẻ đẹp thanh lịch và thời thượng cho mùa mới.',
 N'hero-1.jpg',
 N'shop.jsp'),
(N'Massimo Dutti Premium Leather', 
 N'Bộ sưu tập da thật thủ công cao cấp – tinh hoa phong cách Ý.',
 N'hero-2.jpg',
 N'shop.jsp'),
(N'Mango Fall Style 2025', 
 N'Phong cách thu 2025 của Mango – sang trọng và hiện đại cho quý ông thành đạt.',
 N'hero-2.jpg',
 N'shop.jsp');

-- DEAL OF THE WEEK
INSERT INTO DealOfWeek (MaSP, TieuDe, TyLeGiam, NgayKetThuc)
VALUES
(7, N'Deal of the Week: Clarks Leather Derby – giảm 25%', 25.00, DATEADD(day,7,GETDATE())),
(13, N'Deal of the Week: Zara Tailored Blazer – ưu đãi 30%', 30.00, DATEADD(day,5,GETDATE())),
(8, N'Deal of the Week: Massimo Dutti Loafers – giảm giá giới hạn', 20.00, DATEADD(day,6,GETDATE()));


-- INSTAGRAM IMAGE
INSERT INTO InstagramImage (DuongDan)
VALUES
(N'https://static.zara.net/photos///2024/V/0/2/p/1234/567/800/2/w/750/12345678.jpg?ts=1728000000000'),
(N'https://static.massimodutti.net/3/photos//2024/I/0/1/p/1133/178/800/02/w/960/1133178800_1_1_16.jpg?t=1729100000000'),
(N'https://st.mngbcn.com/rcs/pics/static/T20/fotos/S20/67005874_99_B.jpg?ts=1727700000000'),
(N'https://www.clarks.co.uk/media/catalog/product/cache/4/mens-derby-shoes-brown.jpg'),
(N'https://images.coach.com/is/image/Coach/men-bags-fw25-collection?wid=1200'),
(N'https://static.nike.com/a/images/w_1920,c_limit/6e6a0d6a-02d3-4e3f-91cd-0d44c97e43ad/men-sportswear-fw25.jpg');


-- THÔNG TIN SHOP
INSERT INTO ThongTinShop (TenShop, DiaChi, Email, SoDienThoai, MoTa, Logo)
VALUES
(N'Male Fashion', 
 N'123 Nguyễn Huệ, Quận 1, TP. Hồ Chí Minh', 
 N'support@malefashion.com', 
 N'+84 909 123 456',
 N'Male Fashion mang đến trải nghiệm mua sắm thời trang nam cao cấp, cập nhật các bộ sưu tập mới nhất từ ZARA, Massimo Dutti, Mango, Clarks, Coach và Nike. Phong cách – Đẳng cấp – Chất lượng.',
 N'https://preview.colorlib.com/theme/malefashion/img/logo.png');
