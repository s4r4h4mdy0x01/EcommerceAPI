using AutoMapper;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Catalog.Core.Repository;
using MediatR;

namespace Catalog.Application.Handlers.Queries
{
    public class GetAllBrandHandlers : IRequestHandler<GetAllBrandQuery, IList<BrandResponseDto>>
    {

        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;
        public GetAllBrandHandlers(IBrandRepository brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }


        public async Task<IList<BrandResponseDto>> Handle(GetAllBrandQuery request, CancellationToken cancellationToken)
        {
            var brands = await _brandRepository.GetAllBrandsAsync();
            var brandDtos = _mapper.Map<IList<ProductBrand>, IList<BrandResponseDto>>(brands.ToList());
            return brandDtos;
        }
    }
}
