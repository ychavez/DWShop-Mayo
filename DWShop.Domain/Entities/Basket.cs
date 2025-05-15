using DWShop.Domain.Contracts;

namespace DWShop.Domain.Entities
{
    public class Basket : AuditableEntity<int>
    {
        public string UserName { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
