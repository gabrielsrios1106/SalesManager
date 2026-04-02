# 📦 Controle de estoque e organização financeira

## Sobre o projeto

Solução desenvolvida para uma microempresa que permite uma organização da rotina por meio do cadastro de clientes, produtos, controle de entradas e saídas de estoque e acompanhamento financeiro básico.

### Arquitetura da solução

A solução conta com uma arquitetura simples devido a necessidade da empresa. Tudo foi feito utilizando a versão 8 do .NET.

- **SalesManager.API**: API REST com ASP.NET em C#
- **SalesManager.Web**: FrontEnd Web com Blazor
- **Models**: Classes e enumeradores
- **DataTransferObjects**: Projeto compartilhado com as DTO's utilizadas na API e no FrontEnd Web.
- **Banco de dados SQLite**: Por limitação de infraestrutura foi utilizado o SQLite. O arquivo `.db` fica dentro de uma pasta da API.

### Funcionalidades

| **Funcionalidade** | **Descrição** |
| :--- | :--- |
| **Cadastro de usuários** | Permite que um usuário faça um cadastro simples para acesso ao sistema informando nome, e-mail e senha. Ao finalizar o cadastro é feito login automático |
| **Login** | Permite que um usuário faça login informando e-mail cadastrado e senha |
| **Clientes** | Manutenção do cadastro de clientes. Ao acessar a página é exibida uma tabela com os clientes cadastrados e um campo para pesquisa. |
| **Departamentos** | Manutenção do cadastro de departamentos. Ao acessar a página é exibida uma tabela com os departamentos cadastrados e um campo para pesquisa. |
| **Produtos** | Manutenção do cadastro de produtos. Ao acessar a página é exibida uma tabela com os produtos cadastrados e um campo para pesquisa. Para cadastrar um produto é necessário que exista ao menos um departamento cadastrado. Ao cadastrar o produto deve-se informar o estoque inicial. Com isso o sistema faz um lançamento de entrada no estoque com a quantidade informada. |
| **Movimento do estoque** | Registros de movimentação de estoque. Ao acessar a página é exibida uma tabela com os registros cadastrados e um campo para pesquisa. Existe a opção de registrar vendas e reposições de estoque |
| **Financeiro** | Gráfico e registros de movimentação financeira por produto. Sempre que alguma movimentação de estoque é realizada o sistema registra o valor da transação. Nessa página é possível visualizar um gráfico com o resultado consolidado e uma tabela com a movimentação de cada produto com opção de filtrar o produto a ser visualizado |

<br/>

🔗 [Acesse o sistema para testes](https://jolly-desert-0b552050f.2.azurestaticapps.net/)
> *Usuário: teste@gmail.com* <br>
> *Senha: 12345*