using System;

namespace OUTBANK
{
    // Herança + Polimorfismo (override de Sacar)
    internal class ContaCorrente : Conta
    {
        public double TaxaSaque { get; } = 0.05; // 5% por saque

        public ContaCorrente(string titular, string cpf, DateTime dataNascimento, string senha, int numeroConta = 0)
            : base(titular, cpf, dataNascimento, senha, numeroConta) { }

        public ContaCorrente(Titular titular, int numeroConta)
            : base(titular, numeroConta) { }

        // Polimorfismo — sobrescreve o comportamento de saque
        public override void Sacar(double valor)
        {
            if (valor <= 0)
                throw new ArgumentException("O valor do saque deve ser maior que zero.");

            double taxa = valor * TaxaSaque;
            double totalDescontado = valor + taxa;

            if (totalDescontado > SaldoConta)
                throw new InvalidOperationException(
                    $"Saldo insuficiente. O saque de R$ {valor:F2} + taxa de R$ {taxa:F2} = R$ {totalDescontado:F2}");

            SaldoConta -= totalDescontado;
            Console.WriteLine($"  Saque de R$ {valor:F2} realizado.");
            Console.WriteLine($"  Taxa cobrada  : R$ {taxa:F2} ({TaxaSaque * 100:F0}%)");
            Console.WriteLine($"  Total debitado: R$ {totalDescontado:F2}");
            Console.WriteLine($"  Novo saldo    : R$ {SaldoConta:F2}");
        }

        public override void ExibirInformacoes()
        {
            base.ExibirInformacoes();
            Console.WriteLine($"  Taxa de saque : {TaxaSaque * 100:F0}% por operação");
        }
    }
}