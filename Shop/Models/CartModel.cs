namespace Shop.Models
{
    public class CartModel
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public UserModel User { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
