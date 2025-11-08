namespace BTL_LTW.ViewModels
{
    public class ProductVM
    {
        public int MaSP { get; set; }
        public string? TenSP { get; set; }
        public double GiaSP { get; set; }
        public string? MoTaSP { get; set; }
        public string? AnhChinhSP { get; set; }  
        public int MaDanhMuc { get; set; }
        public int SLTon { get; set; }
        public bool TrangThaiSP {  get; set; }
        public int ChatLuongSP { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string Term { get; set; }
    }
}
