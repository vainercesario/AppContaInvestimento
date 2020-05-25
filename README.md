# Projeto ExemploDDD
> Uso de padrão DDD com aspnet core 3.1, usando ORM Entity Framework Core e banco de dados MySql.

O projeto foi criado para dar visibilidade de meus conhecimentos técnicos, como uso do padrão DDD, ORM e Testes. 
Juntamente de uma organização, escrita de código, sua distribuição e formas de cobertura de testes. 

## Detalhe do Projeto

O projeto simula um sistema de controle de conta corrente bancária que processa solicitações de depósitos, resgates e pagamentos. 
Ainda, foi criado uma rentabilização do dinheiro parado na conta de um dia para o outro, como uma conta corrente remunerada.

## Contextualização
Ao acessar o sistema será solicitado informações básicas de uma Conta Corrente, o número da Agência, Conta, Nome do Cliente e seu CPF.
Após cadastrar estas informações será liberada a tela principal do App, onde será possível realizar as operações de Depósito, Pagamentos e Resgates.
Ainda, diariamente, o valor do saldo da conta corrente será corrigido com 100% CDI.

As operações de Depósito, Pagamento e Resgate possuem regras específicas:

### Regras para Depósito:
1. Não permitir depósitos zerados;
2. Não permitir depósitos negativos.

### Regras para Pagamento:
1. Não permitir pagamentos zerados;
2. Não permitir pagamentos negativo;
3. Garantir que a conta tenha saldo suficiente para a transação;
4. Não permitir pagamentos acima de R$ 10.000,00.

### Regras para Resgate:
1. Não permitir resgates zerados;
2. Não permitir resgates negativo;
3. Garantir que a conta tenha saldo suficiente para a transação;
4. Não permitir resgates acima de R$ 30.000,00.

## Detalhamento Técnico
O App foi desenvolvido sob a plataforma .Net 3.1

Na camada de Interface foi utilizado o template nativo da Microsoft:
1. [Razor](https://docs.microsoft.com/pt-br/aspnet/core/razor-pages/?view=aspnetcore-3.1&tabs=visual-studio)
2. [Bootstrap](https://getbootstrap.com/docs/4.5/getting-started/introduction)

Nos demais projetos da solução foram utilizados os seguintes componentes
1. Entity Framework Core - Pacote: Microsoft.EntityFrameworkCore.Tools (v3.1.4)
2. Banco de Dados MySql - Pacote: MySql.Data.EntityFrameworkCore (v8.0.20)
3. AutoMapper - Pacote: AutoMapper.Extensions.Microsoft.DependencyInjection (v7.0.0)

Para os testes foram utilizados os componentes:
1. Moq - Pacote: Moq (v4.14.1)
2. xUnit - Pacote: xunit (v2.4.0)
3. FluentAssertions - Pacote: FluentAssertions (v5.10.3)


