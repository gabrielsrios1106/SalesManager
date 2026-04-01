using AutoMapper;
using DataTransferObjects.Products;
using DataTransferObjects.StockMovement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using SalesManager.API.Interfaces;
using System.Data;

namespace SalesManager.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class StockMovementController : Controller
    {
        private readonly IStockMovementService _stockMovementService;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public StockMovementController(IStockMovementService stockMovementService, IMapper mapper, IProductService productService)
        {
            _stockMovementService = stockMovementService;
            _mapper = mapper;
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<List<StockMovementGetDTO>>> GetStockMovementAsync([FromQuery] string value, [FromQuery] int idUser)
        {
            List<StockMovementGetDTO> stockMovementGetDTO = await _stockMovementService.GetStockMovementAsync(value, idUser);

            return Ok(stockMovementGetDTO);
        }

        [HttpGet]
        [Route("GetStockMovementById/{StockMovementId}")]
        public async Task<ActionResult<StockMovementGetDTO>> GetStockMovementByIdAsync([FromRoute] int StockMovementId)
        {
            StockMovement stockMovement = await _stockMovementService.GetStockMovementByIdAsync(StockMovementId);

            if (stockMovement == null)
            {
                return NotFound("Nenhum registro encontrado");
            }

            StockMovementGetDTO stockMovementGetDTO = _mapper.Map<StockMovementGetDTO>(stockMovement);

            return Ok(stockMovementGetDTO);
        }

        [HttpPost]
        public async Task<ActionResult<StockMovementGetDTO>> PostStockMovementeAsync([FromBody] StockMovementPurchasePostDTO stockMovementPostDTO)
        {
            if (stockMovementPostDTO.MovementType == MovementType.venda)
            {
                bool sufficientStock = _productService.SufficientStock(stockMovementPostDTO.ProductId, stockMovementPostDTO.Quantity);

                if (!sufficientStock)
                {
                    return BadRequest("Estoque insuficiente");
                }
            }

            if (string.IsNullOrWhiteSpace(stockMovementPostDTO.Message))
            {
                switch (stockMovementPostDTO.MovementType)
                {
                    case MovementType.Compra:
                        stockMovementPostDTO.Message = "Compra de produto para reposição de estoque";
                        break;
                    case MovementType.venda:
                        stockMovementPostDTO.Message = "Venda de produto";
                        break;
                    default:
                        break;
                }
            }
            
            StockMovement stockMovement = _mapper.Map<StockMovement>(stockMovementPostDTO);
            await _stockMovementService.InsertAsync(stockMovement);

            return Created();
        }

        [HttpDelete]
        [Route("{stockMovementId}")]
        public async Task<ActionResult> DeleteStockMovementAsync([FromRoute] int stockMovementId)
        {
            StockMovement stockMovement = await _stockMovementService.GetStockMovementByIdAsync(stockMovementId);

            if (stockMovement == null)
            {
                return NotFound($"Nenhum registro encontrado com o id {stockMovementId}");
            }

            try
            {   
                await _stockMovementService.DeleteAsync(stockMovement);
            }
            catch (DBConcurrencyException e)
            {
                return BadRequest($"Erro interno do sistema: {e.Message}");
            }

            return NoContent();
        }
    }
}
