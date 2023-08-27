namespace BlazorWasm.MiniPOS.Models
{
    public class SaleVoucherDetailDataModel
    {
        public Guid sale_voucher_detail_id { get; set; }
        public Guid sale_voucher_head_id { get; set; }
        public string product_id { get; set; }
        public string product_name { get; set; }
        public int product_price { get; set; }
        public int product_qty { get; set; }
        public DateTime detail_date { get; set; }
    }
}
