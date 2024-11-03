#ClienteApi
ClienteApi é uma API RESTful desenvolvida com .NET Core para gerenciar o cadastro de clientes, validando campos essenciais como CPF, email e nome. Esta aplicação inclui testes automatizados para garantir a integridade das operações de CRUD e validação dos dados.

Funcionalidades
Cadastro de clientes com os campos:
Nome (obrigatório, entre 2 e 50 caracteres)
Email (obrigatório, formato válido)
CPF (obrigatório, no formato 000.000.000-00)
Atualização de dados de clientes existentes
Consulta de todos os clientes cadastrados ou por ID
Exclusão de clientes
Validação de CPF, email e nome
Tecnologias Utilizadas
.NET Core
XUnit (testes unitários)
FluentAssertions (para asserções em testes)
In-Memory Database (banco de dados em memória para testes e desenvolvimento)
Estrutura do Projeto
.
├── ClienteApi/                 # Código principal da API
│   ├── Controllers/            # Controladores da API
│   ├── Models/                 # Modelos da aplicação (Cliente)
│   ├── Repositories/           # Repositórios (lógica de persistência)
│   └── Program.cs              # Ponto de entrada da aplicação
└── ClienteApi.Tests/            # Testes unitários
    └── ClienteRepositoryTests.cs  # Testes para o repositório de clientes
Pré-requisitos
.NET Core SDK 6.0+
Visual Studio ou VS Code
Git (para controle de versão)
Configuração do Ambiente
Clone o repositório:
git clone https://github.com/seu-usuario/cliente-api.git
cd cliente-api
Instale as dependências:

Execute o comando abaixo no terminal para restaurar os pacotes NuGet necessários:
dotnet restore
Compile o projeto:

Compile a aplicação com:
dotnet build
Execute os testes:

Para rodar os testes unitários, utilize o comando:
dotnet test
Execute a API:

Para iniciar o servidor local, utilize o comando:
dotnet run
A API estará disponível em: http://localhost:5000

Endpoints da API
A API oferece os seguintes endpoints:
|Método	  |Rota	                |Descrição
|GET	    |/api/clientes	      |Retorna todos os clientes
|GET	    |/api/clientes/{id}	  |Retorna um cliente por ID
|POST	    |/api/clientes	      |Cria um novo cliente
|PUT	    |/api/clientes/{id}	  |Atualiza um cliente existente
|DELETE	  |/api/clientes/{id}	  |Remove um cliente

Exemplo de Requisição POST
POST /api/clientes
Content-Type: application/json

{
  "nome": "João Silva",
  "email": "joao.silva@example.com",
  "cpf": "123.456.789-10"
}

Exemplo de Requisição PUT
PUT /api/clientes/1
Content-Type: application/json

{
  "id": 1,
  "nome": "João Silva Atualizado",
  "email": "joao.silva.atualizado@example.com",
  "cpf": "123.456.789-10"
}

Testes Unitários
O projeto possui uma cobertura de testes unitários usando XUnit e FluentAssertions. Os testes estão localizados no diretório ClienteApi.Tests e cobrem:

Criação de clientes
Atualização de clientes
Exclusão de clientes
Validação de CPF, email e nome
Como rodar os testes
Execute o seguinte comando para rodar os testes:
dotnet test

Validações Implementadas
CPF
Deve estar no formato 000.000.000-00.
CPF inválido ou com caracteres errados será rejeitado.
Email
Deve ser um endereço de e-mail válido (ex: usuario@dominio.com).
Nome
O nome deve ter entre 2 e 50 caracteres.
Nomes contendo números ou caracteres especiais são considerados inválidos.
Contribuição
Se quiser contribuir com o projeto:

Licença
Este projeto está licenciado sob a licença MIT.
