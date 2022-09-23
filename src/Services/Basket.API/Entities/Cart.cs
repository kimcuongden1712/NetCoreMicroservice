namespace Basket.API.Entities
{
    public class Cart
    {
        public string UserName { get; set; }

        public List<CartItem> Items { get; set; }

        public Cart()
        { }

        public Cart(string username)
        {
            UserName = username;
        }

        public decimal TotalPrice
        {
            get
            {
                return Items != null ? Items.Sum(item => item.ProductPrice * item.Quantity) : 0;
            }
        }
    }
}