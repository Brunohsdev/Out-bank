

using OUTBANK;


void ExibirTituloDaOpcao(string titulo)
{
    int quantidadeDeLetras = titulo.Length;
    string asteriscos = string.Empty.PadLeft(quantidadeDeLetras, '*');
    Console.WriteLine(asteriscos);
    Console.WriteLine(titulo);
    Console.WriteLine(asteriscos + "\n");
}

void LimparTela(int segundos)
{
    Thread.Sleep(segundos * 1000); 
    Console.Clear();
}

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
    ExibirTituloDaOpcao("Acessar Conta");
    Console.WriteLine("Digite seu Nome:");
    LimparTela(0);
    string nome = Console.ReadLine();

    if (nome.Length >=1)
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
    ExibirTituloDaOpcao("Criar Conta");
    Console.WriteLine("Digite seu Nome:");
    string titular = Console.ReadLine();
    LimparTela(0);
    Console.WriteLine($"Seja bem-vindo {titular}");
    LimparTela(2);

    Console.WriteLine("Temos 3 opções de conta para a criação:");
    LimparTela(2);
    Console.WriteLine("1 - Conta Corrente: Ideal para movimentações frequentes. Possui uma pequena taxa a cada saque realizado.");
    LimparTela(4);
    Console.WriteLine("2 - Conta Poupança: Não possui taxa de saque e ainda pode gerar rendimento mensal sobre o saldo.");
    LimparTela(4);
    Console.WriteLine("3 - Conta Empresarial: Indicada para empresas. Oferece limite de empréstimo extra para auxiliar no fluxo de caixa.");
    LimparTela(4);

    
    Console.Write("Que tipo de conta você gostaria de criar no OUT BANK ? ");
    int resposta = int.Parse(Console.ReadLine());

    Conta conta;
    if (resposta < 1 || resposta > 3)
    {
        Console.WriteLine("Opção inválida! Voltando ao menu...");
        Menu();
        return;
    }
    else if (resposta == 1)
    {
        conta = new ContaCorrente(titular);
        conta.exibirInformacoes();
    }
    else if (resposta == 2)
    {
        conta = new ContaPoupanca(titular);
        conta.exibirInformacoes();
    }
    else if(resposta == 3)
    {
        conta = new ContaEmpresarial(titular);
        conta.exibirInformacoes();
    }
    
    DentroDoBanco(conta);
    LimparTela(5);

}




void DentroDoBanco(Conta conta)
{
    System.Console.WriteLine($"Olá {conta.exibirTitular}!");
    System.Console.WriteLine("O que deseja fazer hoje: ");
    System.Console.WriteLine("Ver Saldo");
}

Menu();
