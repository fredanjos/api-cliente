using ClienteApi.Models;

namespace ClienteApi.Repositories
{
  // Interface que define o contrato para operações de CRUD (Create, Read, Update, Delete) 
  // relacionadas aos clientes, além de verificar a existência de um CPF.
  public interface IClienteRepository
  {
    // Retorna uma coleção de todos os clientes
    IEnumerable<Cliente> GetAll();

    // Retorna um cliente específico com base no ID fornecido
    Cliente GetById(int id);

    // Adiciona um novo cliente ao repositório
    void Create(Cliente cliente);

    // Atualiza os dados de um cliente existente
    void Update(Cliente cliente);

    // Remove um cliente do repositório com base no ID
    void Delete(int id);

    // Verifica se um CPF já está cadastrado no repositório
    bool CPFExists(string cpf);
  }
}
