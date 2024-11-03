# ClienteApi

**ClienteApi** é uma API RESTful desenvolvida com **.NET Core** para gerenciar o cadastro de clientes, validando campos essenciais como CPF, email e nome. Esta aplicação inclui testes automatizados para garantir a integridade das operações de CRUD e a validação dos dados.

## Funcionalidades

- Cadastro de clientes com os seguintes campos:
  - **Nome**: obrigatório, entre 2 e 50 caracteres
  - **Email**: obrigatório, formato válido
  - **CPF**: obrigatório, no formato `000.000.000-00`
- Atualização de dados de clientes existentes
- Consulta de todos os clientes cadastrados ou por ID
- Exclusão de clientes
- Validação de CPF, email e nome

## Tecnologias Utilizadas

- **.NET Core**
- **XUnit** (testes unitários)
- **FluentAssertions** (para asserções em testes)
- **In-Memory Database** (banco de dados em memória para testes e desenvolvimento)

## Estrutura do Projeto

```bash
.
├── ClienteApi/                 
│   ├── Controllers/            # Controladores da API
│   ├── Models/                 # Modelos da aplicação (Cliente)
│   ├── Repositories/           # Repositórios (lógica de persistência)
│   └── Program.cs              # Ponto de entrada da aplicação
└── ClienteApi.Tests/            
    └── ClienteRepositoryTests.cs  # Testes para o repositório de clientes
