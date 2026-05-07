using Basket.Application.Responses;
using Basket.Core.Entities;
using MediatR;

namespace Basket.Application.Commands
{
    public class ShoppingCartCommand : IRequest<ShoppingCartResponseDto>
    {
        public string UserName { get; set; }
        public List<ShoppingCartItem> Items { get; set; }
        public ShoppingCartCommand(string userName, List<ShoppingCartItem> items)
        {
            UserName = userName;
            Items = items;
        }
    }
}
