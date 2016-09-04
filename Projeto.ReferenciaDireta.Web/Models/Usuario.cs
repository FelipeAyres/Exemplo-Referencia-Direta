using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Projeto.ReferenciaDireta.Web.Models
{
    public class Usuario
    {
        [Key]
        public int UsuarioId { get; set; }

        public string Nome { get; set; }

        [DisplayName("Usuário")]
        [Required(ErrorMessage="Preencha o campo Usuário")]
        [MaxLength(45, ErrorMessage = "Tamano máximo é {1}")]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Preencha o campo Senha")]
        [MaxLength(45, ErrorMessage = "Tamano máximo é {1}")]
        public string Senha { get; set; }

        [DisplayName("CPF")]
        [MaxLength(14, ErrorMessage = "Tamano máximo é {1}")]
        public string Cpf { get; set; }
    }
}