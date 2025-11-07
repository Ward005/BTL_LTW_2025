namespace BTL_LTW.ViewModels
{
    public class ShoppingCartVM
    {
        public int MaSP { get; set; }
        public string TenSP { get; set; } = string.Empty;
        public string AnhChinhSP { get; set; } = string.Empty;

        public string MoTa { get; set; } = string.Empty;
        public double GiaSP { get; set; }
        public int SoLuong { get; set; }

        public double Total => SoLuong * GiaSP;


    }
}
