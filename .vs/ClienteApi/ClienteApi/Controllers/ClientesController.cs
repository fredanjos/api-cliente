using ClienteApi.Models;
using ClienteApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ClienteApi.Controllers
{
  [ApiController]
  // Define o roteamento da API, utilizando o nome do controlador como parte da URL
  [Route("api/[controller]")]
  public class ClientesController : ControllerBase
  {
    private readonly IClienteRepository _clienteRepository;

    // Construtor que injeta uma instância do repositório de clientes (IClienteRepository)
    public ClientesController(IClienteRepository clienteRepository)
    {
      // Verifica se o repositório injetado não é nulo
      _clienteRepository = clienteRepository ?? throw new ArgumentNullException(nameof(clienteRepository));
    }

    // Método GET para buscar todos os clientes
    [HttpGet]
    public IActionResult GetAll()
    {
      // Retorna uma resposta OK (status 200) com a lista de clientes
      return Ok(_clienteRepository.GetAll());
    }

    // Método GET para buscar um cliente específico pelo ID
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
      // Tenta buscar o cliente pelo ID
      var cliente = _clienteRepository.GetById(id);

      // Se o cliente não for encontrado, retorna NotFound (status 404)
      if (cliente == null)
      {
        return NotFound();
      }

      // Se encontrado, retorna o cliente com status 200
      return Ok(cliente);
    }

    // Método POST para criar um novo cliente
    [HttpPost]
    public IActionResult Create([FromBody] Cliente cliente)
    {
      // Validação do CPF
      if (!ClienteValidator.IsValidoCPF(cliente.CPF))
      {
        return BadRequest("CPF inválido");
      }

      // Validação do email
      if (!ClienteValidator.isValidaEmail(cliente.Email))
      {
        return BadRequest("Email inválido");
      }

      // Verifica se o CPF já está cadastrado
      if (_clienteRepository.CPFExists(cliente.CPF))
      {
        return BadRequest("CPF já cadastrado!");
      }

      // Validação do nome
      if (!ClienteValidator.IsNomeValido(cliente.Nome))
      {
        return BadRequest("Nome inválido");
      }

      // Verifica se o objeto cliente é nulo (caso não tenha sido enviado corretamente)
      if (cliente == null)
      {
        return BadRequest("Dados faltantes");
      }

      // Cria o novo cliente no repositório
      _clienteRepository.Create(cliente);

      // Retorna CreatedAtAction (status 201) com o cliente criado e a URL para acessá-lo
      return CreatedAtAction(nameof(GetById), new { id = cliente.Id }, cliente);
    }

    // Método PUT para atualizar um cliente existente
    [HttpPut("{id}")]
    public IActionResult Upadete(int id, [FromBody] Cliente cliente)
    {
      // Busca o cliente existente pelo ID
      var existingCliente = _clienteRepository.GetById(id);

      // Se o cliente não for encontrado, retorna NotFound (status 404)
      if (existingCliente == null)
      {
        return NotFound();
      }

      // Validação do CPF
      if (!ClienteValidator.IsValidoCPF(cliente.CPF))
      {
        return BadRequest("CPF inválido");
      }

      // Validação do email
      if (!ClienteValidator.isValidaEmail(cliente.Email))
      {
        return BadRequest("Email inváldio");
      }

      // Define o ID do cliente para garantir que ele seja atualizado corretamente
      cliente.Id = id;

      // Atualiza o cliente no repositório
      _clienteRepository.Update(cliente);

      // Retorna NoContent (status 204) para indicar que a operação foi bem-sucedida
      return NoContent();
    }

    // Método DELETE para remover um cliente pelo ID
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
      // Busca o cliente pelo ID
      var cliente = _clienteRepository.GetById(id);

      // Se o cliente não for encontrado, retorna NotFound (status 404)
      if (cliente == null)
      {
        return NotFound();
      }

      // Remove o cliente do repositório
      _clienteRepository.Delete(id);

      // Retorna NoContent (status 204) para indicar que a remoção foi bem-sucedida
      return NoContent();
    }
  }
}
