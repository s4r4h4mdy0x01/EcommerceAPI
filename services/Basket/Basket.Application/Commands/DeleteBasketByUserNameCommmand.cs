using MediatR;

namespace Basket.Application.Commands
{
    public class DeleteBasketByUserNameCommmand : IRequest<Unit>
    {
        public string UserName { get; set; }
        public DeleteBasketByUserNameCommmand(string userName)
        {
            UserName = userName;
        }

    }
}
