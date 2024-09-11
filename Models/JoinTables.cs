namespace Resturent2.Models
{
    public class JoinTables
    {
        public Rproduct Products { get; set; }
        public Rcustomer Customers { get; set; }
        public Rcategory Categories { get; set; }
        public Rproductcustomer ProductsCustomers { get; set; }
    }
}
