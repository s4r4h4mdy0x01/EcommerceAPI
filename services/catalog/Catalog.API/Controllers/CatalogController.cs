using Catalog.Application.Commands;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Specs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers
{

    public class CatalogController : BaseApiController
    {
        private readonly IMediator _mediator;
        public CatalogController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [Route("[action]/{id}", Name = "GetProductById")]
        [ProducesResponseType(typeof(ProductResponseDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProductById(string id)
        {
            var result = await _mediator.Send(new GetProductByIdQuery(id));
            return Ok(result);
        }
        [HttpGet]
        [Route("[action]/{name}", Name = "GetProductByName")]
        [ProducesResponseType(typeof(ProductResponseDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProductByName(string name)
        {
            var result = await _mediator.Send(new GetProductByNameQuery(name));
            return Ok(result);
        }
        [HttpGet]
        [Route("[action]/{name}", Name = "GetProductByBrandName")]
        [ProducesResponseType(typeof(ProductResponseDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProductByBrandName(string name)
        {
            var result = await _mediator.Send(new GetProductByBrandNameQuery(name));
            return Ok(result);
        }

        [HttpGet]
        [Route("[action]", Name = "GetAllProducts")]
        [ProducesResponseType(typeof(IEnumerable<ProductResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllProducts([FromQuery] CatalogSpecsParams param)
        {
            var result = await _mediator.Send(new GetAllProductQuery(param));
            return Ok(result);
        }

        [HttpGet]
        [Route("[action]", Name = "GetAllBrands")]
        [ProducesResponseType(typeof(IEnumerable<BrandResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllBrands()
        {
            var result = await _mediator.Send(new GetAllBrandQuery());
            return Ok(result);
        }

        [HttpGet]
        [Route("[action]", Name = "GetAllTypeProducts")]
        [ProducesResponseType(typeof(IEnumerable<TypeResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllTypeProducts()
        {
            var result = await _mediator.Send(new GetAllTypeQuery());
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]", Name = "CreateProduct")]
        [ProducesResponseType(typeof(ProductResponseDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command)
        {
            var result = await _mediator.Send<ProductResponseDto>(command);
            return Ok(result);
        }
        [HttpPut]
        [Route("[action]", Name = "UpdateProduct")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductCommand command)
        {
            var result = await _mediator.Send<bool>(command);
            return Ok(result);
        }
        [HttpDelete]
        [Route("[action]/{id}", Name = "DeleteProduct")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            var result = await _mediator.Send<bool>(new DeleteProductCommand(id));
            return Ok(result);
        }
    }
}