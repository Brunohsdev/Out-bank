using System;
using System.Collections.Generic;
using OUTBANK;

// pega tudo do txt antes de começar 
Dictionary<string, Conta> contas = BancoDados.CarregarContas();

// utils

void ExibirTituloDaOpcao(string titulo)
{
    string borda = new string('═', titulo.Length + 4);
    Console.WriteLine($"\n  ╔{borda}╗");
    Console.WriteLine($"  ║  {titulo}  ║");
    Console.WriteLine($"  ╚{borda}╝\n");
}

void Pausar(int segundos = 2)
{
    System.Threading.Thread.Sleep(segundos * 1000);
}

void LimparEContinuar(string mensagem = "")
{
    if (!string.IsNullOrEmpty(mensagem))
        Console.WriteLine($"\n  {mensagem}");
    Console.Write("\n  Pressione ENTER para continuar...");
    Console.ReadLine();
    Console.Clear();
}

double LerDouble(string prompt)
{
    while (true)
    {
        Console.Write($"  {prompt}");
        try
        {
            string entrada = Console.ReadLine()?.Replace(",", ".").Trim();
            double valor = double.Parse(entrada, System.Globalization.CultureInfo.InvariantCulture);
            return valor;
        }
        catch (FormatException)
        {
            Console.WriteLine("  Valor inválido! Digite um número (ex: 150.50).");
        }
    }
}

int LerInt(string prompt)
{
    while (true)
    {
        Console.Write($"  {prompt}");
        try
        {
            return int.Parse(Console.ReadLine()?.Trim() ?? "0");
        }
        catch (FormatException)
        {
            Console.WriteLine("  Digite apenas números inteiros.");
        }
    }
}

string LerCPF()
{
    while (true)
    {
        Console.Write("  CPF (apenas números): ");
        string cpf = Console.ReadLine()?.Trim() ?? "";
        string apenasDigitos = System.Text.RegularExpressions.Regex.Replace(cpf, @"\D", "");

        if (apenasDigitos.Length == 11)
            return apenasDigitos;

        Console.WriteLine("  CPF inválido. Informe exatamente 11 dígitos.");
    }
}

DateTime LerData()
{
    while (true)
    {
        Console.Write("  Data de nascimento (dd/mm/aaaa): ");
        string entrada = Console.ReadLine()?.Trim() ?? "";
        if (DateTime.TryParseExact(entrada, "dd/MM/yyyy",
            System.Globalization.CultureInfo.InvariantCulture,
            System.Globalization.DateTimeStyles.None, out DateTime data))
        {
            if (data > DateTime.Today)
            {
                Console.WriteLine("  Data de nascimento não pode ser no futuro.");
                continue;
            }
            return data;
        }
        Console.WriteLine("  Data inválida. Use o formato dd/mm/aaaa.");
    }
}

// menu principal

void Menu()
{
    Console.Clear();
    Console.WriteLine(@"
  ░█████╗░██╗░░░██╗████████╗  ██████╗░░█████╗░███╗░░██╗██╗░░██╗
  ██╔══██╗██║░░░██║╚══██╔══╝  ██╔══██╗██╔══██╗████╗░██║██║░██╔╝
  ██║░░██║██║░░░██║░░░██║░░░  ██████╦╝███████║██╔██╗██║█████═╝░
  ██║░░██║██║░░░██║░░░██║░░░  ██╔══██╗██╔══██║██║╚████║██╔═██╗░
  ╚█████╔╝╚██████╔╝░░░██║░░░  ██████╦╝██║░░██║██║░╚███║██║░╚██╗
  ░╚════╝░░╚═════╝░░░░╚═╝░░░  ╚═════╝░╚═╝░░╚═╝╚═╝░░╚══╝╚═╝░░╚═╝
    ");
    Console.WriteLine("  ─────────────────────────────────────────");
    Console.WriteLine("  [1] Acessar Conta");
    Console.WriteLine("  [2] Criar Conta");
    Console.WriteLine("  [3] Listar Contas");
    Console.WriteLine("  [4] Sair");
    Console.WriteLine("  ─────────────────────────────────────────");

    int opcao = LerInt("Escolha uma opção: ");

    switch (opcao)
    {
        case 1: AcessarConta(); break;
        case 2: CriarConta(); break;
        case 3:
            BancoDados.ListarContas();
            LimparEContinuar();
            Menu();
            break;
        case 4:
            Console.WriteLine("\n  Obrigado por usar o OUT BANK. Até logo!");
            break;
        default:
            Console.WriteLine("  Opção inválida.");
            Pausar(1);
            Menu();
            break;
    }
}

// acessar conta

void AcessarConta()
{
    ExibirTituloDaOpcao("Acessar Conta");

    Console.Write("  Nome do titular: ");
    string nome = Console.ReadLine()?.Trim() ?? "";

    if (!contas.ContainsKey(nome))
    {
        Console.WriteLine($"\n  Titular '{nome}' não encontrado.");
        Console.Write("  Deseja criar uma nova conta? (s/n): ");
        string resp = Console.ReadLine()?.Trim().ToLower() ?? "n";

        if (resp == "s")
            CriarConta();
        else
        {
            LimparEContinuar("Voltando ao menu...");
            Menu();
        }
        return;
    }

    // Validação de senha
    Conta conta = contas[nome];
    int tentativas = 3;

    while (tentativas > 0)
    {
        Console.Write("  Senha: ");
        string senha = LerSenhaOculta();

        if (conta.ValidarSenha(senha))
        {
            Console.Clear();
            Console.WriteLine($"\n Bem-vindo de volta, {conta.Titular.Nome}!");
            Pausar(1);
            DentroDoBanco(conta);
            return;
        }

        tentativas--;
        Console.WriteLine($"  Senha incorreta. Tentativas restantes: {tentativas}");
    }

    LimparEContinuar("Acesso bloqueado. Muitas tentativas incorretas.\nSaia daqui");
    Menu();
}

string LerSenhaOculta()
{
    string senha = "";
    ConsoleKeyInfo tecla;
    while (true)
    {
        tecla = Console.ReadKey(intercept: true);
        if (tecla.Key == ConsoleKey.Enter) break;
        if (tecla.Key == ConsoleKey.Backspace && senha.Length > 0)
        {
            senha = senha[..^1];
            Console.Write("\b \b");
        }
        else if (tecla.Key != ConsoleKey.Backspace)
        {
            senha += tecla.KeyChar;
            Console.Write("*");
        }
    }
    Console.WriteLine();
    return senha;
}

// criar conta

void CriarConta()
{
    ExibirTituloDaOpcao("Criar Conta");

    Console.Write("  Nome completo: ");
    string nome = Console.ReadLine()?.Trim() ?? "";

    if (string.IsNullOrWhiteSpace(nome))
    {
        LimparEContinuar("Nome inválido. Operação cancelada.");
        Menu();
        return;
    }

    if (contas.ContainsKey(nome))
    {
        LimparEContinuar($"Já existe uma conta para '{nome}'. Use 'Acessar Conta'.");
        Menu();
        return;
    }

    string cpf = LerCPF();
    DateTime dataNasc = LerData();

    Console.Write("  Crie uma senha: ");
    string senha = LerSenhaOculta();
    Console.Write("  Confirme a senha: ");
    string confirmacao = LerSenhaOculta();

    if (senha != confirmacao)
    {
        LimparEContinuar("As senhas não coincidem. Operação cancelada.");
        Menu();
        return;
    }

    ExibirTiposDeConta();
    int resposta = LerInt("Escolha o tipo de conta: ");

    Conta conta;

    try
    {
        conta = resposta switch
        {
            1 => new ContaCorrente(nome, cpf, dataNasc, senha),
            2 => new ContaPoupanca(nome, cpf, dataNasc, senha),
            3 => new ContaEmpresarial(nome, cpf, dataNasc, senha),
            _ => throw new ArgumentOutOfRangeException("Opção inválida.")
        };
    }
    catch (ArgumentOutOfRangeException e)
    {
        LimparEContinuar($" {e.Message}");
        Menu();
        return;
    }

    conta.ExibirInformacoes();
    contas[nome] = conta;
    BancoDados.SalvarConta(conta);

    Console.WriteLine($"\n Conta criada com sucesso! Bem-vindo ao OUT BANK, {nome}!");
    Pausar(2);
    DentroDoBanco(conta);
}

void ExibirTiposDeConta()
{
    Console.WriteLine("\n  ╔══ TIPOS DE CONTA ══════════════════════════════════════════╗");
    Console.WriteLine("  [1] Conta Corrente   — Taxa de 5% por saque. Limite R$ 400");
    Console.WriteLine("  [2] Conta Poupança   — Sem taxa. Rendimento 0.5%/mês");
    Console.WriteLine("  [3] Conta Empresarial — Sem taxa. Limite empréstimo R$ 10.000");
    Console.WriteLine("  ╚════════════════════════════════════════════════════════════╝");
}

//dentro do banco 

void DentroDoBanco(Conta conta)
{
    Console.Clear();
    Console.WriteLine($"  Olá, {conta.Titular.Nome}! O que deseja fazer hoje?");
    MenuPrincipal(conta);
}

void MenuPrincipal(Conta conta)
{
    ExibirTituloDaOpcao("Menu da Conta");
    Console.WriteLine("  [1] Ver Saldo");
    Console.WriteLine("  [2] Depositar");
    Console.WriteLine("  [3] Sacar");
    Console.WriteLine("  [4] Pegar Empréstimo");
    Console.WriteLine("  [5] Pagar Empréstimo");
    Console.WriteLine("  [6] Simular Rendimento (Poupança)");
    Console.WriteLine("  [7] Informações da Conta");
    Console.WriteLine("  [8] Sair da Conta");
    Console.WriteLine("  ─────────────────────────────────────");

    int opcao = LerInt("Escolha uma opção: ");
    Console.WriteLine();

    switch (opcao)
    {
        case 1:
            ExibirTituloDaOpcao("Saldo");
            conta.ExibirSaldoTotal();
            LimparEContinuar();
            DentroDoBanco(conta);
            break;

        case 2:
            ExibirTituloDaOpcao("Depositar");
            try
            {
                double valor = LerDouble("Valor a depositar: R$ ");
                conta.Depositar(valor);
                BancoDados.SalvarConta(conta);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine($"  {e.Message}");
            }
            LimparEContinuar();
            DentroDoBanco(conta);
            break;

        case 3:
            ExibirTituloDaOpcao("Sacar");
            try
            {
                double valor = LerDouble("Valor a sacar: R$ ");
                conta.Sacar(valor);
                BancoDados.SalvarConta(conta);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine($"  {e.Message}");
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine($"  {e.Message}");
            }
            LimparEContinuar();
            DentroDoBanco(conta);
            break;

        case 4:
            ExibirTituloDaOpcao("Pegar Empréstimo");
            Console.WriteLine($"Limite disponível: R$ {(conta.LimiteEmprestimo - conta.SaldoEmprestimo):F2}");
            try
            {
                double valor = LerDouble("Valor do empréstimo: R$ ");
                conta.PegarEmprestimo(valor);
                BancoDados.SalvarConta(conta);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine($"  {e.Message}");
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine($"  {e.Message}");
            }
            LimparEContinuar();
            DentroDoBanco(conta);
            break;

        case 5:
            ExibirTituloDaOpcao("Pagar Empréstimo");
            Console.WriteLine($"Dívida atual: R$ {conta.SaldoEmprestimo:F2}");
            try
            {
                double valor = LerDouble("Valor a pagar: R$ ");
                conta.PagarEmprestimo(valor);
                BancoDados.SalvarConta(conta);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine($"  {e.Message}");
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine($"  {e.Message}");
            }
            LimparEContinuar();
            DentroDoBanco(conta);
            break;

        case 6:
            ExibirTituloDaOpcao("Simular Rendimento");
            if (conta is ContaPoupanca poupanca)
            {
                try
                {
                    int meses = LerInt("Quantos meses deseja simular? ");
                    poupanca.SimularRendimento(meses);
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine($" {e.Message}");
                }
                catch (InvalidOperationException e)
                {
                    Console.WriteLine($" {e.Message}");
                }
            }
            else
            {
                Console.WriteLine(" Esta funcionalidade é exclusiva da Conta Poupança.");
            }
            LimparEContinuar();
            DentroDoBanco(conta);
            break;

        case 7:
            conta.ExibirInformacoes();
            LimparEContinuar();
            DentroDoBanco(conta);
            break;

        case 8:
            BancoDados.SalvarConta(conta); 
            LimparEContinuar("Saindo da conta... Até logo!");
            Menu();
            break;

        default:
            Console.WriteLine(" Opção inválida!");
            Pausar(1);
            DentroDoBanco(conta);
            break;
    }
}

//começando o menu
Menu();