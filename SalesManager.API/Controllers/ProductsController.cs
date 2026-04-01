using AutoMapper;
using DataTransferObjects.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using SalesManager.API.Interfaces;
using System.Data;

namespace SalesManager.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productsService;
        private readonly IStockMovementService _stockMovementService;
        private readonly IFinancialManagerService _financialManagerService;
        private readonly IMapper _mapper;

        public ProductsController(IProductService productsService, IMapper mapper, IStockMovementService stockMovementService, IFinancialManagerService financialManagerService)
        {
            _productsService = productsService;
            _mapper = mapper;
            _stockMovementService = stockMovementService;
            _financialManagerService = financialManagerService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductGetDTO>>> GetProductsAsync([FromQuery] string value, [FromQuery] int idUser, [FromQuery] string orderBy, [FromQuery] bool showInactive = true)
        {
            List<ProductGetDTO> productsGetDTO = await _productsService.GetProductAsync(value, idUser, orderBy, showInactive);

            return Ok(productsGetDTO);
        }

        [HttpGet]
        [Route("GetProductById/{productId}")]
        public async Task<ActionResult<ProductGetDTO>> GetProductsByIdAsync([FromRoute] int productId)
        {
            Product product = await _productsService.GetProductByIdAsync(productId);

            if (product == null)
            {
                return NotFound("Nenhum registro encontrado");
            }

            ProductGetDTO productGetDTO = _mapper.Map<ProductGetDTO>(product);

            return Ok(productGetDTO);
        }

        [HttpPost]
        public async Task<ActionResult<ProductGetDTO>> PostProductAsync([FromBody] ProductPostDTO productPostDTO)
        {
            Product product1 = await _productsService.ExistsByNameAsync(productPostDTO.ProductName, productPostDTO.UserId);

            if (product1 != null)
            {
                string textMsg = product1.Status == 0 ? $"inativo (código {product1.Id}) " : string.Empty;
                return BadRequest($"Já existe um produto {textMsg}  com o nome {productPostDTO.ProductName}");
            }

            Product product = _mapper.Map<Product>(productPostDTO);

            int balanceStock = product.BalanceStock;
            product.BalanceStock = 0;

            await _productsService.InsertAsync(product);

            StockMovement stockMovement = new StockMovement()
            {
                MovementType = MovementType.Compra,
                Quantity = balanceStock,
                SalePrice = product.Price,
                Message = "Estoque inicial",
                ProductId = product.Id,
                UserId = product.UserId
            };

            await _stockMovementService.InsertAsync(stockMovement);

            return Created();
        }

        [HttpPut]
        public async Task<ActionResult> PutProductAsync([FromBody] ProductPutDTO productPutDTO)
        {
            Product product = await _productsService.GetProductByIdAsync(productPutDTO.Id);

            if (product == null)
            {
                return NotFound($"Nenhum registro encontrado com o id {productPutDTO.Id}");
            }

            Product product1 = await _productsService.ExistsByNameUpdateAsync(productPutDTO.ProductName, productPutDTO.Id, productPutDTO.UserId);

            if (product1 != null)
            {
                string textMsg = product1.Status == 0 ? $"inativo (código {product1.Id}) " : string.Empty;
                return BadRequest($"Já existe um produto {textMsg} com o nome {productPutDTO.ProductName}");
            }

            product.ProductName = productPutDTO.ProductName;
            product.Price = productPutDTO.Price;
            product.BalanceStock = productPutDTO.BalanceStock;
            product.DepartmentId = productPutDTO.DepartmentId;
            product.MinimumStock = productPutDTO.MinimumStock;
            product.Status = productPutDTO.Status;

            try
            {
                await _productsService.UpdateAsync(product);
            }
            catch (DbUpdateConcurrencyException e)
            {
                return BadRequest($"Erro interno do sistema: {e.Message}");
            }

            return NoContent();
        }

        [HttpDelete]
        [Route("{productId}")]
        public async Task<ActionResult> DeleteDepartmentAsync([FromRoute] int productId)
        {
            Product product = await _productsService.GetProductByIdAsync(productId);

            if (product == null)
            {
                return NotFound($"Nenhum registro encontrado com o id {productId}");
            }

            try
            {
                if (await _productsService.ExistsStockMovement(productId, product.UserId))
                {
                    product.Status = 0;
                    await _productsService.UpdateAsync(product);
                }
                else
                {
                    FinancialManager financialManager = await _financialManagerService.GetFinancialManagerById(productId);

                    if (financialManager != null)
                    {
                        await _financialManagerService.DeleteAsync(financialManager);
                    }

                    await _productsService.DeleteAsync(product);
                }
            }
            catch (DBConcurrencyException e)
            {
                return BadRequest($"Erro interno do sistema: {e.Message}");
            }

            return NoContent();
        }
    }
}
