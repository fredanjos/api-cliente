using System.ComponentModel.DataAnnotations;

namespace ClienteApi.Models
{
  // A classe Cliente representa um modelo de cliente com propriedades validadas.
  public class Cliente
  {
    // Identificador único para o cliente
    public int Id { get; set; }

    // Nome do cliente, obrigatório e com tamanho mínimo de 2 e máximo de 50 caracteres
    [Required(ErrorMessage = "O nome é obrigatório")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "O nome deve ter entre 2 e 50 caracteres")]
    public string Nome { get; set; }

    // Email do cliente, obrigatório e deve seguir um formato válido de email
    [Required(ErrorMessage = "O email é obrigatório")]
    [EmailAddress(ErrorMessage = "O email deve ser válido")]
    public string Email { get; set; }

    // CPF do cliente, obrigatório e deve seguir o formato 000.000.000-00
    [Required(ErrorMessage = "O CPF é obrigatório")]
    [RegularExpression(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$", ErrorMessage = "O CPF deve estar no formato 000.000.000-00")]
    public string CPF { get; set; }
  }
}
