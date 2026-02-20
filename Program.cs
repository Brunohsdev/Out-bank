

using OUTBANK;

void Menu()
{
    Console.WriteLine(@"  
░█████╗░██╗░░░██╗████████╗  ██████╗░░█████╗░███╗░░██╗██╗░░██╗
██╔══██╗██║░░░██║╚══██╔══╝  ██╔══██╗██╔══██╗████╗░██║██║░██╔╝
██║░░██║██║░░░██║░░░██║░░░  ██████╦╝███████║██╔██╗██║█████═╝░
██║░░██║██║░░░██║░░░██║░░░  ██╔══██╗██╔══██║██║╚████║██╔═██╗░
╚█████╔╝╚██████╔╝░░░██║░░░  ██████╦╝██║░░██║██║░╚███║██║░╚██╗
░╚════╝░░╚═════╝░░░░╚═╝░░░  ╚═════╝░╚═╝░░╚═╝╚═╝░░╚══╝╚═╝░░╚═╝");
    Console.WriteLine("1. Acessar Conta");
    Console.WriteLine("2. Criar Conta");
    Console.WriteLine("3. Sair");

    int opcao = int.Parse(Console.ReadLine());

    switch(opcao)
    {
        case 1:
            AcessarConta();
            break;
        case 2:
            CriarConta();
            break;
        case 3:
            Console.WriteLine("Saindo...");
            break;
        default:
            Console.WriteLine("Opção inválida! Tente novamente.");
            Menu();
            break;
    }
}


void AcessarConta()
{
    Console.WriteLine("Acessar Conta");
    Console.WriteLine("Digite seu CPF:");
    string cpf = Console.ReadLine();

    if (cpf.Length >=1)
    {
        Console.WriteLine("em andamento");
    }
    else
    {
        Console.Write("Cpf não encontrado! \n Deseja criar conta ? responda com s ou n ");
        if(Console.ReadLine().ToLower() == "s")
        {
            CriarConta();
        }
        else
        {
            Console.WriteLine("Voltando ao menu...");
            Menu();
        }
    }
}


void CriarConta()
{
    Console.WriteLine("Criar Conta"); 
    Console.WriteLine("Digite seu Nome:");
    string titular = Console.ReadLine();

    Console.WriteLine($"Seja bem-vindo {titular}");
    Console.WriteLine("Temos 3 opções de conta para a criação:");
    Console.WriteLine("1 - Conta Corrente: Ideal para movimentações frequentes. Possui uma pequena taxa a cada saque realizado.");
    Console.WriteLine("2 - Conta Poupança: Não possui taxa de saque e ainda pode gerar rendimento mensal sobre o saldo.");
    Console.WriteLine("3 - Conta Empresarial: Indicada para empresas. Oferece limite de empréstimo extra para auxiliar no fluxo de caixa.");
    Console.Write("Que tipo de conta você gostaria de criar no OUT BANK ?");
    int resposta = int.Parse(Console.ReadLine());

    if (resposta < 1 || resposta > 3)
    {
        Console.WriteLine("Opção inválida! Voltando ao menu...");
        Menu();
        return;
    }
    else if (resposta == 1)
    {
        ContaCorrente contaCorrente = new ContaCorrente(titular);
    }
    else if (resposta == 2)
    {
        ContaPoupanca contaPoupanca = new ContaPoupanca(titular);
    }
    else if(resposta == 3)
    {
        ContaEmpresarial contaEmpresarial = new ContaEmpresarial(titular);
    }
    //adicionar um validador que preste atenção se o cpf é valido ou não, e se já existe uma conta com esse cpf

    Console.WriteLine("Seja bem vindo !");
    

}


Menu();