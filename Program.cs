using OUTBANK;
Dictionary<string, Conta> contas = new Dictionary<string, Conta>();


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

    if (contas.ContainsKey(nome))
    {
        var conta = contas[nome];
        Console.WriteLine($"Seja bem-vindo de volta {conta.exibirTitular}");
        DentroDoBanco(conta);
    }
    else
    {
        Console.Write("Nome do titular não encontrado! \n Deseja criar conta ? responda com s ou n ");
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

    TipoDeConta();

    if (!int.TryParse(Console.ReadLine(), out int resposta))
    {
        Console.WriteLine("Entrada inválida! Voltando ao menu...");
        Menu();
        return;
    }

    Conta conta = null;

    if (resposta < 1 || resposta > 3)
    {
        Console.WriteLine("Opção inválida! Voltando ao menu...");
        Menu();
        return;
    }
    else if (resposta == 1)
    {
        conta = new ContaCorrente(titular);
    }
    else if (resposta == 2)
    {
        conta = new ContaPoupanca(titular);
    }
    else // resposta == 3
    {
        conta = new ContaEmpresarial(titular);
    }

    if (conta != null)
    {
        conta.exibirInformacoes();
        LimparTela(5);

        if (!string.IsNullOrWhiteSpace(titular))
        {
            contas.Add(titular, conta);
            Console.WriteLine($"Bem-vindo ao OUT-BANK {titular}");
            DentroDoBanco(conta);
        }
        else
        {
            Console.WriteLine("Titular inválido. Conta não foi adicionada ao sistema.");
        }
    }

    LimparTela(5);

}

void TipoDeConta()
{
    Console.WriteLine("Temos 3 opções de conta para a criação:");
    LimparTela(2);
    Console.WriteLine("1 - Conta Corrente: Ideal para movimentações frequentes. Possui uma pequena taxa a cada saque realizado.");
    LimparTela(4);
    Console.WriteLine("2 - Conta Poupança: Não possui taxa de saque e ainda pode gerar rendimento mensal sobre o saldo.");
    LimparTela(4);
    Console.WriteLine("3 - Conta Empresarial: Indicada para empresas. Oferece limite de empréstimo extra para auxiliar no fluxo de caixa.");
    LimparTela(4);


    Console.Write("Que tipo de conta você gostaria de criar no OUT BANK ? ");
    

}


void DentroDoBanco(Conta conta)
{
    LimparTela(0);
    if (conta == null)
    {
        System.Console.WriteLine("Conta inválida. Voltando ao menu...");
        Menu();
        return;
    }

    // Exibe saudação e o titular usando o método existente na classe Conta
    
    System.Console.WriteLine($"O que deseja fazer hoje {conta} ? ");
    MenuPrincipalDentroDaConta(conta);

}

void MenuPrincipalDentroDaConta(Conta conta)
{
    ExibirTituloDaOpcao("Menu Principal");
    System.Console.WriteLine("1 - Ver Saldo");
    System.Console.WriteLine("2 - Depositar");
    System.Console.WriteLine("3 - Sacar");
    System.Console.WriteLine("4 - Pegar empréstimo");
    System.Console.WriteLine("4 - Pagar empréstimo");
    System.Console.WriteLine("5 - Investir");

    int opcao = int.Parse(Console.ReadLine());
    switch (opcao)
    {
        case 1:
            //Console.WriteLine($"Saldo: R$ {conta.ExibirSaldoTotal()}");
            Console.WriteLine("só de teste");
            DentroDoBanco(conta);
            break;
        case 2:
            Console.Write("Quanto você deseja depositar? ");
            double valorDepositar = double.Parse(Console.ReadLine());
            conta.Depositar(valorDepositar);

            DentroDoBanco(conta);
            break;
        case 3:
            Console.WriteLine("Quanto você deseja sacar ?");
                double valorSacar = double.Parse(Console.ReadLine());
                conta.Sacar(valorSacar);
                DentroDoBanco(conta);
            break;
        case 4:
            Console.WriteLine("Quanto você deseja pegar de empréstimo ?");
                double valorEmprestimo = double.Parse(Console.ReadLine());
                conta.PegarEmprestimo(valorEmprestimo);
                DentroDoBanco(conta);
            break;
        case 5:
            Console.WriteLine("Investir");
            DentroDoBanco(conta);
            break;
        default:
            Console.WriteLine("Opção inválida! Tente novamente.");
            MenuPrincipalDentroDaConta(conta);
            break;
    }
}
Menu();
