using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Projeto.ReferenciaDireta.Web.Models
{
    public class Produto
    {
        [Key]
        public int ProdutoId { get; set; }

        [DisplayName("Produto")]
        [Required(ErrorMessage = "Preencha o campo Produto")]
        [MaxLength(100, ErrorMessage = "Tamano máximo é {1}")]
        public string Nome { get; set; }

        [DisplayName("Preço")]
        [Required(ErrorMessage = "Preencha o campo Preço")]
        [DataType(DataType.Currency, ErrorMessage = "O campo deve ser um número")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Preco { get; set; }

        public string Imagem { get; set; }
    }
}