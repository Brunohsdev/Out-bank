using System;

namespace OUTBANK
{
    
    internal class ContaPoupanca : Conta
    {
        public double TaxaRendimentoMes { get; } = 0.005; // 0.5% ao mês

        public ContaPoupanca(string titular, string cpf, DateTime dataNascimento, string senha, int numeroConta = 0)
            : base(titular, cpf, dataNascimento, senha, numeroConta) { }

        public ContaPoupanca(Titular titular, int numeroConta)
            : base(titular, numeroConta) { }

        // Método exclusivo da poupança
        public void SimularRendimento(int meses)
        {
            if (meses <= 0)
                throw new ArgumentException("O número de meses deve ser maior que zero.");

            if (SaldoConta <= 0)
                throw new InvalidOperationException("Seu saldo precisa ser maior que zero para simular rendimento.");

            double valorAtual = SaldoConta;
            Console.WriteLine($"\n  Simulação de rendimento ({TaxaRendimentoMes * 100:F2}% ao mês):");
            Console.WriteLine($"  'Mês', | 'Rendimento', | 'Saldo Acumulado',");
            Console.WriteLine($"  {new string('-', 40)}");

            for (int i = 1; i <= meses; i++)
            {
                double rendimento = valorAtual * TaxaRendimentoMes;
                valorAtual += rendimento;
                Console.WriteLine($"  {i,-5} | R$ {rendimento,-12:F2} | R$ {valorAtual:F2}");
            }

            Console.WriteLine($"\n  Saldo final estimado em {meses} meses: R$ {valorAtual:F2}");
        }

        public override void ExibirInformacoes()
        {
            base.ExibirInformacoes();
            Console.WriteLine($"  Rendimento    : {TaxaRendimentoMes * 100:F2}% ao mês (sem taxa de saque)");
        }
    }
}