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

## Domínios
Foram criados 3 modelos de domínios no sistema:
1. __Pessoas__;
2. __Contas Correntes__;
3. __Operações__.

## Detalhamento da Codificação
Os modelos de __Pessoas__ e __ContasCorrentes__ possuem apenas uma operação inicial de cadastramento de conta, após seu cadastramento sempre será utilizado essa __ContaCorrente__ para as __Operações__. 
Como se trata apenas de um código para dar uma visibilidade de conduta e escrita de código, foi criado apenas a possibilidade de criação da __ContaCorrente__, porém nessa criação simula uma execução "transacional" pois na mesma execução é realizado a inserção de __Pessoas__ e de __ContasCorrentes__.

A tela principal do sistema é a tela da __ContaCorrente__ onde mostra a __Pessoa__, dona da __ContaCorrente__, o saldo total e a data da Última Movimentação. A baixa é exibido o histórico de __Operações__ da __ContaCorrente__. 
Para que a verificação do rendimento da __ContaCorrente__ seja realizada, sempre que a listagem é acessa é executado uma verificação de existência de __Operação__ no dia corrente, caso não tenha tido, é verificado a quantidade de dias sem movimentação e se aplica a correção monetária usando o índice CDI como rendimento diário. A contabilização do índice e taxa diária está estática no código. Como se trata de um índice volátil, contabilizei-o pegando o índice acumalado dos últimos 12 meses e dividi pela totalidade de dias úteis no ano. Desse resultado apliquei a formula de juros compostos sobre este índice, a variante de tempo peguei a diferença de dias úteis entre a data da última movimentação com o dia atual. Dessa verificação lanço uma __Operação__ de rendimento e atualizo o saldo na __ContaCorrente__.

## Detalhamento Técnico
O App foi desenvolvido sob a plataforma .Net 3.1

Na camada de Interface foi utilizado o template nativo da Microsoft:
1. [Razor](https://docs.microsoft.com/pt-br/aspnet/core/razor-pages/?view=aspnetcore-3.1&tabs=visual-studio)
2. [Bootstrap](https://getbootstrap.com/docs/4.5/getting-started/introduction)

Nos demais projetos da solução foram utilizados os seguintes componentes
1. Entity Framework Core - Pacote: Microsoft.EntityFrameworkCore.Tools (v3.1.4)
2. Banco de Dados MySql - Pacote: MySql.Data.EntityFrameworkCore (v8.0.20)
3. AutoMapper - Pacote: AutoMapper.Extensions.Microsoft.DependencyInjection (v7.0.0)

### Cobertura de Testes
Os testes desenvolvidos foram criados para dar sustentação nas principais operações do sistema, como a abertura de conta, depósito, pagamentos e resgate.

Para os casos foram utilizados os componentes:
1. Moq - Pacote: Moq (v4.14.1)
2. xUnit - Pacote: xunit (v2.4.0)
3. FluentAssertions - Pacote: FluentAssertions (v5.10.3)
