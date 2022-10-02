using MediatR;
using PosApplication.Application.Abstraction.Commands.Basket;
using PosApplication.Domain.Repositories;

namespace PosApplication.Application.Commands.Basket
{
    public class BasketUpdateCommandHandler : IRequestHandler<BasketUpdateCommand, Domain.Entities.Basket>
    {
        private readonly IBasketRepository _basketRepository;

        public BasketUpdateCommandHandler(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        public async Task<Domain.Entities.Basket> Handle(BasketUpdateCommand command, CancellationToken cancellationToken)
        {
            var entity = await _basketRepository.CreateOrUpdateAsync(command.Basket);
            await _basketRepository.SaveChangesAsync(cancellationToken);
            return entity;
        }
    }
}
