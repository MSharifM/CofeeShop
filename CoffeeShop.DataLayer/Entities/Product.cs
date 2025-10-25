using System.ComponentModel.DataAnnotations;

namespace CoffeeShop.DataLayer.Entities
{
    public class Product
    {
        public Product()
        {
            
        }

        [Key]
        public int ProductId { get; set; }

        [Required]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد.")]
        public string Title { get; set; }

        public string? Description { get; set; }

        [Required]
        public int Price { get; set; }

        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد.")]
        public string? ProductAvatar { get; set; }

        //Relations
        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}
