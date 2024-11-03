using FluentAssertions;
using Xunit;
using ClienteApi.Models;
using ClienteApi.Repositories;
using System.ComponentModel.DataAnnotations;

namespace ClienteApi.Tests
{
  // Testes para as opera��es de CRUD no reposit�rio de clientes e valida��es de dados
  public class ClienteRepositoryTests
  {
    private readonly IClienteRepository _repository;

    // Configura um reposit�rio de cliente antes de cada teste
    public ClienteRepositoryTests()
    {
      _repository = new ClienteRepository();
    }

    // Testa se um cliente v�lido � corretamente adicionado ao reposit�rio
    [Fact]
    public void Create_ValidCliente_ShouldAddCliente()
    {
      // Arrange: Cria um cliente v�lido
      var cliente = new Cliente { Nome = "Teste", Email = "teste@teste.com", CPF = "121.574.540-04" };

      // Act: Adiciona o cliente ao reposit�rio
      _repository.Create(cliente);

      // Assert: Verifica se o cliente foi adicionado corretamente
      var result = _repository.GetAll().ToList();
      result.Should().ContainSingle();
      result[0].Nome.Should().Be("Teste");
      result[0].CPF.Should().Be("121.574.540-04");
      result[0].Email.Should().Be("teste@teste.com");
    }

    // Testa se um cliente existente � atualizado corretamente no reposit�rio
    [Fact]
    public void Update_ValidCliente_ShouldUpdateCliente()
    {
      // Arrange: Cria e adiciona um cliente, depois modifica seus dados
      var cliente = new Cliente { Nome = "Teste", Email = "teste@teste.com", CPF = "121.574.540-04" };
      _repository.Create(cliente);
      cliente.Nome = "Teste Atualizado";
      cliente.Email = "testeatualizado@teste.com";

      // Act: Atualiza o cliente no reposit�rio
      _repository.Update(cliente);

      // Assert: Verifica se os dados foram atualizados corretamente
      var updatedCliente = _repository.GetById(cliente.Id);
      updatedCliente.Should().NotBeNull();
      updatedCliente.Nome.Should().Be("Teste Atualizado");
      updatedCliente.Email.Should().Be("testeatualizado@teste.com");
    }

    // Testa se um cliente � removido corretamente com um ID v�lido
    [Fact]
    public void Delete_ValidId_ShouldRemoveCliente()
    {
      // Arrange: Cria e adiciona um cliente ao reposit�rio
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
    [InlineData("121.574.540-04", true)]  // CPF v�lido
    [InlineData("005.413.050-69", true)]  // Outro CPF v�lido
    [InlineData("000.000.000-00", false)] // Todos os d�gitos iguais (inv�lido)
    [InlineData("12345678900", false)]    // CPF sem formata��o (inv�lido)
    [InlineData("12345678901", false)]    // CPF inv�lido
    [InlineData("123.456.789-0a", false)] // CPF com letra (inv�lido)
    [InlineData("123.456.789", false)]    // CPF com menos de 11 d�gitos (inv�lido)
    [InlineData("123.456.789-098", false)] // CPF com mais de 11 d�gitos (inv�lido)
    [InlineData("  ", false)]              // Apenas espa�os
    [InlineData(null, false)]              // Nulo
    public void IsValidoCPF_Test(string cpf, bool expected)
    {
      // Act: Chama o m�todo de valida��o de CPF
      var result = ClienteValidator.IsValidoCPF(cpf);

      // Assert: Verifica o resultado esperado
      Assert.Equal(expected, result);
    }

    // Testes para validar diferentes formatos de email
    [Theory]
    [InlineData("teste@example.com", true)]  // E-mail v�lido
    [InlineData("usuario@gmail.com", true)]  // Outro e-mail v�lido
    [InlineData("usuario@", false)]          // E-mail sem dom�nio (inv�lido)
    [InlineData("usuario@.com", true)]       // E-mail faltando parte do dom�nio (v�lido)
    [InlineData("usuario@example.", false)]  // E-mail sem extens�o (inv�lido)
    [InlineData("@example.com", false)]      // E-mail sem parte local (inv�lido)
    [InlineData("usuario@@example.com", false)] // Dois '@' (inv�lido)
    [InlineData("usuario@example.com.br", true)] // E-mail v�lido com dom�nio brasileiro
    [InlineData("usuario.example.com", false)]   // E-mail sem '@' (inv�lido)
    [InlineData("", false)]                     // E-mail vazio (inv�lido)
    [InlineData(null, false)]                   // E-mail nulo (inv�lido)
    public void isValidaEmail_Test(string email, bool expected)
    {
      // Act: Chama o m�todo de valida��o de email
      var result = ClienteValidator.isValidaEmail(email);

      // Assert: Verifica o resultado esperado
      Assert.Equal(expected, result);
    }

    // Testes para validar diferentes entradas de nome
    [Theory]
    [InlineData("", false)]                 // Nome vazio (inv�lido)
    [InlineData(" ", false)]                // Nome com apenas espa�os (inv�lido)
    [InlineData(null, false)]               // Nome nulo (inv�lido)
    [InlineData("A", false)]                // Nome muito curto (inv�lido)
    [InlineData("AB", true)]                // Nome no limite inferior (v�lido)
    [InlineData("Jo�o", true)]              // Nome com acento (v�lido)
    [InlineData("John Doe", true)]          // Nome com espa�o (v�lido)
    [InlineData("Jo�o da Silva", true)]     // Nome composto (v�lido)
    [InlineData("Jo�o123", false)]          // Nome com n�mero (inv�lido)
    [InlineData("Jo�o da Silva!", false)]   // Nome com caractere especial (inv�lido)
    [InlineData("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA", false)] // Nome muito longo (inv�lido)
    [InlineData("A B", true)]               // Nome com m�nimo de 2 letras e espa�o (v�lido)
    public void IsNomeValido_Test(string nome, bool expected)
    {
      // Act: Chama o m�todo de valida��o de nome
      var result = ClienteValidator.IsNomeValido(nome);

      // Assert: Verifica o resultado esperado
      Assert.Equal(expected, result);
    }
  }
}