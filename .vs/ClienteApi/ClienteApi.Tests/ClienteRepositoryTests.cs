using FluentAssertions;
using Xunit;
using ClienteApi.Models;
using ClienteApi.Repositories;
using System.ComponentModel.DataAnnotations;

namespace ClienteApi.Tests
{
  // Testes para as operações de CRUD no repositório de clientes e validações de dados
  public class ClienteRepositoryTests
  {
    private readonly IClienteRepository _repository;

    // Configura um repositório de cliente antes de cada teste
    public ClienteRepositoryTests()
    {
      _repository = new ClienteRepository();
    }

    // Testa se um cliente válido é corretamente adicionado ao repositório
    [Fact]
    public void Create_ValidCliente_ShouldAddCliente()
    {
      // Arrange: Cria um cliente válido
      var cliente = new Cliente { Nome = "Teste", Email = "teste@teste.com", CPF = "121.574.540-04" };

      // Act: Adiciona o cliente ao repositório
      _repository.Create(cliente);

      // Assert: Verifica se o cliente foi adicionado corretamente
      var result = _repository.GetAll().ToList();
      result.Should().ContainSingle();
      result[0].Nome.Should().Be("Teste");
      result[0].CPF.Should().Be("121.574.540-04");
      result[0].Email.Should().Be("teste@teste.com");
    }

    // Testa se um cliente existente é atualizado corretamente no repositório
    [Fact]
    public void Update_ValidCliente_ShouldUpdateCliente()
    {
      // Arrange: Cria e adiciona um cliente, depois modifica seus dados
      var cliente = new Cliente { Nome = "Teste", Email = "teste@teste.com", CPF = "121.574.540-04" };
      _repository.Create(cliente);
      cliente.Nome = "Teste Atualizado";
      cliente.Email = "testeatualizado@teste.com";

      // Act: Atualiza o cliente no repositório
      _repository.Update(cliente);

      // Assert: Verifica se os dados foram atualizados corretamente
      var updatedCliente = _repository.GetById(cliente.Id);
      updatedCliente.Should().NotBeNull();
      updatedCliente.Nome.Should().Be("Teste Atualizado");
      updatedCliente.Email.Should().Be("testeatualizado@teste.com");
    }

    // Testa se um cliente é removido corretamente com um ID válido
    [Fact]
    public void Delete_ValidId_ShouldRemoveCliente()
    {
      // Arrange: Cria e adiciona um cliente ao repositório
      var cliente = new Cliente { Nome = "Teste", Email = "teste@teste.com", CPF = "12345678901" };
      _repository.Create(cliente);
      var idToDelete = cliente.Id;

      // Act: Remove o cliente pelo ID
      _repository.Delete(idToDelete);

      // Assert: Verifica se o cliente foi removido
      var deletedCliente = _repository.GetById(idToDelete);
      deletedCliente.Should().BeNull();
    }

    // Testes para validar os diferentes formatos de CPF
    [Theory]
    [InlineData("121.574.540-04", true)]  // CPF válido
    [InlineData("005.413.050-69", true)]  // Outro CPF válido
    [InlineData("000.000.000-00", false)] // Todos os dígitos iguais (inválido)
    [InlineData("12345678900", false)]    // CPF sem formatação (inválido)
    [InlineData("12345678901", false)]    // CPF inválido
    [InlineData("123.456.789-0a", false)] // CPF com letra (inválido)
    [InlineData("123.456.789", false)]    // CPF com menos de 11 dígitos (inválido)
    [InlineData("123.456.789-098", false)] // CPF com mais de 11 dígitos (inválido)
    [InlineData("  ", false)]              // Apenas espaços
    [InlineData(null, false)]              // Nulo
    public void IsValidoCPF_Test(string cpf, bool expected)
    {
      // Act: Chama o método de validação de CPF
      var result = ClienteValidator.IsValidoCPF(cpf);

      // Assert: Verifica o resultado esperado
      Assert.Equal(expected, result);
    }

    // Testes para validar diferentes formatos de email
    [Theory]
    [InlineData("teste@example.com", true)]  // E-mail válido
    [InlineData("usuario@gmail.com", true)]  // Outro e-mail válido
    [InlineData("usuario@", false)]          // E-mail sem domínio (inválido)
    [InlineData("usuario@.com", true)]       // E-mail faltando parte do domínio (válido)
    [InlineData("usuario@example.", false)]  // E-mail sem extensão (inválido)
    [InlineData("@example.com", false)]      // E-mail sem parte local (inválido)
    [InlineData("usuario@@example.com", false)] // Dois '@' (inválido)
    [InlineData("usuario@example.com.br", true)] // E-mail válido com domínio brasileiro
    [InlineData("usuario.example.com", false)]   // E-mail sem '@' (inválido)
    [InlineData("", false)]                     // E-mail vazio (inválido)
    [InlineData(null, false)]                   // E-mail nulo (inválido)
    public void isValidaEmail_Test(string email, bool expected)
    {
      // Act: Chama o método de validação de email
      var result = ClienteValidator.isValidaEmail(email);

      // Assert: Verifica o resultado esperado
      Assert.Equal(expected, result);
    }

    // Testes para validar diferentes entradas de nome
    [Theory]
    [InlineData("", false)]                 // Nome vazio (inválido)
    [InlineData(" ", false)]                // Nome com apenas espaços (inválido)
    [InlineData(null, false)]               // Nome nulo (inválido)
    [InlineData("A", false)]                // Nome muito curto (inválido)
    [InlineData("AB", true)]                // Nome no limite inferior (válido)
    [InlineData("João", true)]              // Nome com acento (válido)
    [InlineData("John Doe", true)]          // Nome com espaço (válido)
    [InlineData("João da Silva", true)]     // Nome composto (válido)
    [InlineData("João123", false)]          // Nome com número (inválido)
    [InlineData("João da Silva!", false)]   // Nome com caractere especial (inválido)
    [InlineData("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA", false)] // Nome muito longo (inválido)
    [InlineData("A B", true)]               // Nome com mínimo de 2 letras e espaço (válido)
    public void IsNomeValido_Test(string nome, bool expected)
    {
      // Act: Chama o método de validação de nome
      var result = ClienteValidator.IsNomeValido(nome);

      // Assert: Verifica o resultado esperado
      Assert.Equal(expected, result);
    }
  }
}