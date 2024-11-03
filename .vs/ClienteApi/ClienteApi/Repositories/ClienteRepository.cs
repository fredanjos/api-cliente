using ClienteApi.Models;

namespace ClienteApi.Repositories
{
  // Classe responsável pela gestão dos dados de clientes
  // Implementa a interface IClienteRepository
  public class ClienteRepository : IClienteRepository
  {
    // Lista interna que armazena os clientes em memória
    private readonly List<Cliente> _clienteList = new List<Cliente>();

    // Variável para gerar o próximo ID dos clientes
    private int _nextId = 1;

    // Retorna todos os clientes da lista
    public IEnumerable<Cliente> GetAll() => _clienteList;

    // Busca um cliente específico pelo ID
    public Cliente GetById(int id) => _clienteList.FirstOrDefault(c => c.Id == id);

    // Adiciona um novo cliente na lista
    public void Create(Cliente cliente)
    {
      // Define o ID do cliente. Se já houver clientes, o próximo ID será o maior ID atual +1
      cliente.Id = _clienteList.Any() ? _clienteList.Max(c => c.Id) + 1 : 1;

      // Adiciona o cliente à lista
      _clienteList.Add(cliente);
    }

    // Atualiza as informações de um cliente existente
    public void Update(Cliente cliente)
    {
      // Busca o cliente pelo ID
      var existingClient = GetById(cliente.Id);

      // Se o cliente existir, atualiza os dados
      if (existingClient != null)
      {
        existingClient.Nome = cliente.Nome;
        existingClient.Email = cliente.Email;
        existingClient.CPF = cliente.CPF;
      }
    }

    // Remove um cliente da lista pelo ID
    public void Delete(int id)
    {
      // Busca o cliente pelo ID
      var cliente = GetById(id);

      // Se o cliente existir, remove-o da lista
      if (cliente != null)
      {
        _clienteList.Remove(cliente);
      }
    }

    // Verifica se um CPF já existe na lista de clientes
    public bool CPFExists(string cpf)
    {
      // Retorna verdadeiro se algum cliente na lista tiver o CPF especificado
      return _clienteList.Any(c => c.CPF == cpf);
    }
  }
}
