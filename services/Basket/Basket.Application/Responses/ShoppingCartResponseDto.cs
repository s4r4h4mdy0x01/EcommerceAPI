namespace Basket.Application.Responses
{
    public class ShoppingCartResponseDto
    {
        public string UserName { get; set; }
        public List<ShoppingCartItemResponseDto> Items { get; set; } = new List<ShoppingCartItemResponseDto>();
        public ShoppingCartResponseDto()
        {
        }
        public ShoppingCartResponseDto(string userName)
        {
            UserName = userName;
        }
        public decimal TotalPrice
        {
            get
            {
                decimal totalPrice = 0;
                foreach (var item in Items)
                {
                    totalPrice += item.Price * item.Quantity;
                }
                return totalPrice;
            }
        }

    }
}
