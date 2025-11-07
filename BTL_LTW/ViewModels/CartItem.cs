namespace BTL_LTW.ViewModels
{
    public class CartItem
    {
        public int MaSp { get; set; }
        public string Hinh { get; set; } = string.Empty;
        public double DonGia { get; set; }
        public int SoLuong { get; set; }
        public string TenSp { get; set; } = string.Empty;
        public double TotalPrice => DonGia * SoLuong;

    }
}
