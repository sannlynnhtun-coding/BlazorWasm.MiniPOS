namespace POSBlazorWebAssembly.Models
{
    public class SaleVoucherHeadDataModel
    {
        public Guid sale_voucher_head_id { get; set; }
        public Guid sale_voucher_no { get; set; }
        public int sale_total_amount { get; set; }
        public DateTime sale_date { get; set; }
    }
}
