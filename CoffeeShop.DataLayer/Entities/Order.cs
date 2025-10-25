using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeShop.DataLayer.Entities
{
    public class Order
    {
        public Order()
        {

        }

        [Key]
        public int OrderId { get; set; }

        [Required]
        public string UserId { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.Now;

        public bool IsFinally { get; set; }

        //Relations
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
