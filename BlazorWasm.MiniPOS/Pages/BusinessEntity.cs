namespace BlazorWasm.MiniPOS.Pages
{
    public partial class ProductSalePage
    {
        //Business objects

        public class BusinessEntity
        {
            public string ProductName { get; set; }

            public string ProductPrice { get; set; }

            public string Qty { get; set; }

            public string Amount { get; set; }

            public BusinessEntity(string name, string alias, string names, string aliass)
            {
                ProductName = name;
                ProductPrice = alias;
                Qty = names;
                Amount = aliass;
            }
            //public string product_name { get; set; }
            //public string product_price { get; set; }
            //public string qty { get; set; }
            //public string amount { get; set; }

            //public BusinessEntity(string product_name, string product_price)
            //{
            //    product_name = product_name;
            //    product_price = product_price;
            //    qty = qty;
            //    amount = amount;
            //}
        }
    }
}
