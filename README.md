# ⚙️ EquilibraMais - Projeto Devops (Api de Clima Organizacional)

### 👥 Integrantes do Projeto

- Gustavo de Aguiar Lima Silva - RM: 557707  
- Julio Cesar Conceição Rodrigues - RM: 557298  
- Matheus de Freitas Silva - RM: 552602  

---

### 💡 Descrição da Solução

Este projeto em .NET 9 utiliza Minimal APIs e Entity Framework Core para fornecer uma API de gestão e relatório de clima organizacional. Possui funcionalidades para cadastro e consulta de funcionários e seus indicadores de humor, energia, carga e sono, e relatórios agregados por setor e empresa.

---

### 🛠️ Configuração e Execução Local

#### Pré-requisitos

- [.NET SDK 9.0+](https://dotnet.microsoft.com/en-us/download)
- Conta Azure com permissão para criar recursos
- IDE (Rider, Visual Studio 2022+, VS Code com extensão C#)

#### Execução

1. Clone o repositório e navegue até a pasta do projeto:
    ```
    git clone https://github.com/EquilibraMais/.Net.git
    cd .Net/EquilibraMais
    ```

2. Configure `appsettings.json` com sua connection string para Azure SQL.

3. Rode a API localmente:
    ```
    dotnet run --project EquilibraMais.sln
    ```

---

### 🌐 Como criar um Azure SQL Database e Web App

#### 1. Criando o Azure SQL Database

- Acesse o [portal.azure.com](https://portal.azure.com)
- Procure por **SQL Database** e clique em "Criar"
- Escolha o grupo de recursos (ou crie um novo)
- Defina o nome do banco e crie/associe um servidor lógico SQL:
  - Defina admin/login e senha (lembre da senha para configurar o projeto)
- Escolha a camada (para testes, use a camada gratuita ou básica)
- Confirme e crie.

**Após a criação:**
- Na tela do banco, pegue a connection string em "Configurações -> Cadeias de conexão"
- Libere o acesso ao IP do DevOps (opcional: libere para seu IP local para testes)
- Importe as tabelas do projeto executando os scripts SQL pelo Query Editor ou Azure Data Studio.

#### 2. Criando o Azure Web App

- No portal Azure, procure por **App Service** e clique em "Criar"
- Escolha o grupo de recursos e o nome do Web App (ex: equilibramais-api)
- Selecione stack `.NET` e região desejada
- Confirme e crie

**Após a criação:**
- Acesse a configuração do App Service e configure a string de conexão para acessar o Azure SQL banco
- O connection string tem que coincidir com o nome definido em `appsettings.json`

---

### 🛠️ Pipeline DevOps (Azure DevOps) - Etapas dos Jobs

O arquivo `azure-pipelines.yml` utilizado para automação da pipeline pode ser encontrado no próprio repositório como referência.

Estes são os nomes dos jobs executados na pipeline de build e deploy:

- Initialize job
- Checkout EquilibraMais
- NuGetToolInstaller
- NuGetCommand
- Run tests
- DotNetCoreCLI
- Listar arquivos publicados
- PublishBuildArtifacts
- AzureWebApp
- Post-job: Checkout
- Finalize Job

#### Explicação das etapas (jobs/tasks):

| Job/Task                     | O que faz?                                                                                      |
|------------------------------|-------------------------------------------------------------------------------------------------|
| Initialize job               | Inicializa e prepara o ambiente de execução, validando agentes e variáveis para os próximos jobs |
| Checkout EquilibraMais       | Realiza o checkout do projeto, baixando todo o código fonte do repositório Git                  |
| NuGetToolInstaller           | Instala o NuGet CLI, ferramenta responsável pelo gerenciamento de pacotes .NET                   |
| NuGetCommand                 | Restaura os pacotes NuGet usados pelo projeto, resolvendo todas as dependências necessárias      |
| Run tests                    | Executa os testes automatizados definidos na solução, validando se o código está correto         |
| DotNetCoreCLI                | Compila e constrói o código do projeto .NET, gerando os artefatos para publicação                |
| Listar arquivos publicados   | Lista os artefatos gerados durante o build, permitindo checagem de arquivos publicados           |
| PublishBuildArtifacts        | Publica os artefatos de build para utilização nas próximas etapas, como o deploy                 |
| AzureWebApp                  | Realiza o deploy dos artefatos para o Azure App Service configurado para o projeto               |
| Post-job: Checkout           | Executa tarefas de controle e limpeza após o deploy, garantindo que tudo terminou corretamente   |
| Finalize Job                 | Finaliza a execução da pipeline, consolidando logs e marcando o status do processo               |

---

O arquivo YAML completo com as etapas da pipeline está disponível no repositório e pode ser utilizado como exemplo para sua própria automação de CI/CD.

3. **Configurar Service Connection:**  
   - No Azure DevOps, acesse Projeto > Project Settings > Service Connections
   - Adicione uma conexão do tipo Azure Resource Manager (recomendada)
   - Autentique com permissões no grupo de recursos do App Service e SQL Database

4. **Pipeline de Release e Deploy:**  
   - No pipeline YAML, adicione task para `AzureWebApp`:
      - Use o nome do App Service criado
      - Defina variáveis de ambiente para connection string
      - Configure deploy automático quando o build for concluído

5. **Monitoramento e Logs:**  
   - Status de build e deploy aparecem nos Pipelines e Releases
   - Configure notificações e alertas para falhas

6. **Validação:**  
   - Após o deploy, acesse a URL do Web App (ex: https://equilibramais-api.azurewebsites.net) e adicione ao final /scalar
   - Teste endpoints via navegação e ferramenta (Scalar)

---

### 📦 Tecnologias Utilizadas

- .NET 9, Entity Framework Core + Azure SQL  
- Azure DevOps Pipelines  
- Azure App Service  
- Minimal APIs  
- Scalar.AspNetCore  
- OpenAPI  
- Helpcheck para monitoramento  
- C#

---

### 📬 Uso da API Localmente

- Interaja via Scalar UI, Postman, curl, ou navegador  
- Documentação Swagger disponível em `/swagger`

---

### 📋 Endpoints da API

| Entidade         | Método HTTP | Rota                                     | Descrição                                      |
|------------------|-------------|------------------------------------------|------------------------------------------------|
| Empresa          | GET         | /api/v1/empresas                        | Retorna todas as empresas                       |
| Empresa          | GET         | /api/v1/empresas/{id}                   | Retorna uma empresa por ID                       |
| Empresa          | POST        | /api/v1/empresas/inserir                | Insere uma nova empresa                          |
| Empresa          | PUT         | /api/v1/empresas/atualizar/{id}         | Atualiza uma empresa                             |
| Empresa          | DELETE      | /api/v1/empresas/deletar/{id}           | Remove uma empresa pelo ID                       |
| Setor            | GET         | /api/v1/setores                         | Retorna todos os setores                         |
| Setor            | GET         | /api/v1/setores/{id}                    | Retorna um setor por ID                          |
| Setor            | POST        | /api/v1/setores/inserir                 | Insere um novo setor                             |
| Setor            | PUT         | /api/v1/setores/atualizar/{id}          | Atualiza um setor                                |
| Setor            | DELETE      | /api/v1/setores/deletar/{id}            | Remove um setor pelo ID                          |
| Usuario          | GET         | /api/v1/usuarios                        | Retorna todos os usuários                        |
| Usuario          | GET         | /api/v1/usuarios/{id}                   | Retorna um usuário por ID                        |
| Usuario          | POST        | /api/v1/usuarios/inserir                | Insere um novo usuário                           |
| Usuario          | PUT         | /api/v1/usuarios/atualizar/{id}         | Atualiza um usuário                              |
| Usuario          | DELETE      | /api/v1/usuarios/deletar/{id}           | Remove um usuário pelo ID                        |
| Funcionario_Info | GET         | /api/v1/funcionario_infos               | Retorna todos os registros de funcionário        |
| Funcionario_Info | GET         | /api/v1/funcionario_infos/{id}          | Retorna um registro de funcionário por ID        |
| Funcionario_Info | GET         | /api/v1/funcionario_infos/user_id/{id}  | Retorna registro por ID de Usuário               |
| Funcionario_Info | GET         | /api/v1/funcionario_infos/user_id/{id}/date/{date} | Retorna registro por ID de Usuário e Data       |
| Funcionario_Info | POST        | /api/v1/funcionario_infos/inserir       | Insere um registro de funcionário                |
| Funcionario_Info | PUT         | /api/v1/funcionario_infos/atualizar/{id}| Atualiza um registro de funcionário              |
| Funcionario_Info | DELETE      | /api/v1/funcionario_infos/deletar/{id}  | Remove um registro pelo ID                        |
| Relatorios       | GET         | /api/v2/relatorios/humor                 | Relatório agregando humor por setor e empresa    |
| Relatorios       | GET         | /api/v2/relatorios/humor-medio-por-setor| Relatório de humor médio por setor               |

---

### 🧪 Testes Automatizados

1. Certifique que as dependências estão configuradas.  
2. No terminal, execute:  
    ```
    dotnet test
    ```
3. Verifique os resultados no console e no pipeline DevOps.
