using System;
using System.Collections.Generic;
using System.Text;

namespace OUTBANK
{
    internal class ContaPoupanca : Conta
    {
        public double TaxaRendimentoMes { get; private set; } = 0.05;
        public ContaPoupanca(string titular) : base(titular) {}

        public void AnalisarRendimento(int meses)
        {
            double valorAtual = Saldo;
            double rendimento = 0;

            for (int i = 0; i< meses; i++)
            {
                rendimento = valorAtual * TaxaRendimentoMes;
                valorAtual += rendimento;
            }

            Console.WriteLine($"O valor do rendimento em {meses} é de R$ {valorAtual:F2}.");
        }

    }
}
