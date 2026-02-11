using OUTBANK;
using System;
using System.Collections.Generic;
using System.Text;

namespace OUTBANK
{
    internal class ContaCorrente : Conta
    {
        double Taxa { get; } = 0.05;
        public ContaCorrente(string titular) : base(titular) {}

        public override void Sacar(double valor)
        {
            if (valor <= 0)
            {
                Console.WriteLine("O valor tem que ser maior que zero!");
                return;
            }

            double valorTaxa = Taxa * valor;
            Saldo = Saldo - (valor + valorTaxa);

        }
    }
}

