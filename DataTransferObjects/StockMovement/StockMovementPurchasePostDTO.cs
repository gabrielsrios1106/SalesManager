using Models;
using System.ComponentModel.DataAnnotations;

namespace DataTransferObjects.StockMovement
{
    public class StockMovementPurchasePostDTO
    {
        [Display(Name = "Tipo de movimento")]
        [EnumDataType(typeof(MovementType), ErrorMessage = "Valor inválido para o campo {0}")]
        public MovementType MovementType { get; set; } = MovementType.Compra;

        [Display(Name = "Quantidade")]
        [Range(1, Int32.MaxValue, ErrorMessage = "A {0} é obrigatório")]
        public int Quantity { get; set; }

        public double UnitaryValue { get; set; }

        public double MovementValue { get; set; }

        public double SalePrice { get; set; }

        public string Message { get; set; }

        [Display(Name = "Produto")]
        [Range(1, Int32.MaxValue, ErrorMessage = "O {0} é obrigatório")]
        public int ProductId { get; set; }

        public int? ClientId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public byte Status { get; set; } = 1;

        public int UserId { get; set; }

        public StockMovementPurchasePostDTO(int idUser)
        {
            UserId = idUser;
        }

        public StockMovementPurchasePostDTO() { }
    }
}
