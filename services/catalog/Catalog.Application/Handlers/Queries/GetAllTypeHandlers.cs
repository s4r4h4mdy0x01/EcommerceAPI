using AutoMapper;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Catalog.Core.Repository;
using MediatR;

namespace Catalog.Application.Handlers.Queries
{


    public class GetAllTypeHandlers : IRequestHandler<GetAllTypeQuery, IList<TypeResponseDto>>
    {

        private readonly ITypeProductRepository _typeRepository;
        private readonly IMapper _mapper;
        public GetAllTypeHandlers(ITypeProductRepository typeRepository, IMapper mapper)
        {
            _typeRepository = typeRepository;
            _mapper = mapper;
        }

        public async Task<IList<TypeResponseDto>> Handle(GetAllTypeQuery request, CancellationToken cancellationToken)
        {
            var types = await _typeRepository.GetAllTypeProductsAsync();
            var typeDtos = _mapper.Map<IList<ProductType>, IList<TypeResponseDto>>(types.ToList());
            return typeDtos;
        }

    }
}
