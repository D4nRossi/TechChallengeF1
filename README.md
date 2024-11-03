
# Tech Challenge - Cadastro de Contatos Regionais

## Descrição do Projeto

Este projeto foi desenvolvido como parte do Tech Challenge de 2024 e implementa um sistema de cadastro de contatos regionais. Utilizando **.NET 8**, o sistema permite CRUD completo de contatos, consulta de DDD via API externa (ViaCEP), e validações para garantir a consistência dos dados. 

## Funcionalidades

- **CRUD de Contatos**: Permite o cadastro, atualização, leitura e exclusão de contatos.
- **Validação de Dados**: Inclui validações para o formato do e-mail, telefone e CEP.
- **Consulta de DDD via API**: Usa a API do ViaCEP para definir o DDD automaticamente com base no CEP.
- **Documentação com Swagger**: Interface interativa para testar as funcionalidades da API.
- **Testes Unitários**: Validações e funcionalidades testadas usando xUnit.

## Estrutura do Projeto

O projeto segue uma estrutura modular com separação de camadas:

- `Controllers`: Contém os controladores da API, como o `ContatoController`.
- `Models`: Define as entidades e DTOs utilizados.
- `Repository`: Camada de acesso a dados e manipulação de banco com o Entity Framework Core.
- `Tests`: Inclui testes unitários usando xUnit para validar a lógica do projeto.

## Tecnologias Utilizadas

- .NET 8
- Entity Framework Core
- Dapper
- xUnit para testes unitários
- Moq para mocks em testes
- Swagger para documentação da API
- ViaCEP API para consulta de DDD

## Pré-requisitos

- **.NET SDK** 8.0 ou superior
- **SQL Server** para a persistência dos dados (ou substituição pelo banco de dados em memória nos testes)
- **Postman** ou **Swagger** para testes de API (opcional)

## Configuração do Projeto

1. **Clonar o repositório**:
   ```bash
   git clone <url-do-repositorio>
   cd <nome-do-repositorio>
   ```

2. **Configurar o Banco de Dados**:
   - No arquivo `appsettings.json`, defina a connection string do SQL Server no campo `DB_CONNECTION`.

3. **Instalar as Dependências**:
   - Certifique-se de que todas as dependências NuGet estão instaladas:
     ```bash
     dotnet restore
     ```

4. **Configurar a API ViaCEP**:
   - No arquivo `appsettings.json`, configure a URL base da API ViaCEP no campo `ApiSettings:ViaCepUrl`.

## Executando a Aplicação

1. Compile e execute a aplicação:
   ```bash
   dotnet run
   ```

2. Acesse a documentação do Swagger para interagir com a API:
   - Acesse `http://localhost:<porta>/swagger` no navegador.

## Executando os Testes

O projeto inclui testes unitários com xUnit para validações e funcionalidades principais. Para rodar os testes:

```bash
dotnet test
```

### Estrutura dos Testes

- **ContatoValidatorTests**: Testa as validações de CEP, telefone e e-mail.
- **ContatoRepositoryTests**: Verifica o comportamento de persistência de dados e a integração com a API ViaCEP.

## Endpoints Principais

- **POST /Contato**: Cadastra um novo contato.
- **GET /Contato/GetByDdd**: Consulta contatos filtrando por DDD.
- **PUT /Contato/{id}**: Atualiza um contato pelo ID.
- **DELETE /Contato/{id}**: Exclui um contato pelo ID.

## Considerações Finais

Este projeto foi desenvolvido com foco em boas práticas de código, incluindo separação de camadas, testes unitários, e validações rigorosas para garantir dados consistentes. O uso de uma API externa para definir o DDD automaticamente adiciona valor ao sistema, proporcionando uma experiência automatizada para o usuário final.

Agradecemos por conferir o projeto!
