using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ClienteApi.Models
{
  public static class ClienteValidator
  {
    public static bool IsValidoCPF(string cpf)
    {
      // Verificar se o cpf é nulo
      if (string.IsNullOrEmpty(cpf))
        return false;

      // Remover pontos e traços do CPF e garantir que sejam apenas números
      cpf = cpf.Trim().Replace(".", "").Replace("-", "");

      // Verificar se o CPF tem 11 dígitos e se são todos numéricos
      if (cpf.Length != 11 || !cpf.All(char.IsDigit))
        return false;

      // Verificar se todos os dígitos são iguais, o que invalida o CPF
      if (cpf.Distinct().Count() == 1)
        return false;


      // Arrays de multiplicadores para o cálculo dos dígitos verificadores
      int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
      int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

      // Calcular o primeiro dígito verificador
      string tempCpf = cpf.Substring(0, 9);
      int soma = 0;

      for (int i = 0; i < 9; i++)
        soma += (tempCpf[i] - '0') * multiplicador1[i];

      int resto = soma % 11;
      int digito1 = resto < 2 ? 0 : 11 - resto;

      // Calcular o segundo dígito verificador
      tempCpf = tempCpf + digito1;
      soma = 0;

      for (int i = 0; i < 10; i++)
        soma += (tempCpf[i] - '0') * multiplicador2[i];

      resto = soma % 11;
      int digito2 = resto < 2 ? 0 : 11 - resto;

      // Verificar se os dois dígitos calculados são iguais aos do CPF
      return cpf.EndsWith(digito1.ToString() + digito2.ToString());
    }

    public static bool isValidaEmail(string email)
    {
      if (string.IsNullOrWhiteSpace(email))
        return false;

      var emailValidator = new EmailAddressAttribute();
      // Primeiro, validar o formato básico
      if (!emailValidator.IsValid(email))
        return false;

      // Verificação extra: garantir que há uma extensão no domínio após o ponto
      var indexOfAt = email.LastIndexOf("@");
      if (indexOfAt == -1 || indexOfAt == email.Length - 1)
        return false;

      var domain = email.Substring(indexOfAt + 1);
      // Verifica se o domínio tem pelo menos uma parte após o ponto e se não termina com um ponto
      if (!domain.Contains(".") || domain.EndsWith("."))
        return false;

      return emailValidator.IsValid(email);
    }

    public static bool IsNomeValido(string nome)
    {
      // Verifica se o nome está nulo ou vazio
      if (string.IsNullOrWhiteSpace(nome))
      {
        return false;
      }

      // Remove espaços em branco extras
      nome = nome.Trim();

      // Verifica o comprimento do nome
      if (nome.Length < 2 || nome.Length > 50)
      {
        return false;
      }

      // Verifica se o nome contém apenas letras e espaços
      string pattern = @"^[\p{L} ]+$"; // \p{L} corresponde a qualquer letra Unicode
      return Regex.IsMatch(nome, pattern);
    }
  }
}
