using OUTBANK;
using System;
using System.Collections.Generic;
using System.Text;

namespace OUTBANK
{
    internal class Conta
    {
        string Titular { get; }
        public double SaldoConta { get; private set; } = 0;
        private double SaldoTotal = 0;
        public double SaldoEmprestimo { get; private set;}
        public int NumeroConta { get; } = new Random().Next(1000, 9999);
        public virtual double LimiteEmprestimo { get; } = 400;

        public Conta(string titular)
        {
            Titular = titular;
        }

        public void ExibirSaldoTotal()
        {
            Console.WriteLine($"Saldo total: R${SaldoTotal}");
        }

        public void AtualizarSaldoTotal()
        {
            SaldoTotal = SaldoConta + SaldoEmprestimo;
        }

        public virtual void Depositar(double valor)
        {
            if (valor <= 0)
            {
                Console.WriteLine("O valor tem que ser maior que zero!");
                return;
            }

            SaldoConta += valor;
            AtualizarSaldoTotal();
        }

        public virtual void Sacar(double valor) {
            if (valor <= 0)
            {
                Console.WriteLine("O valor tem que ser maior que zero!");
                return;
            }

            SaldoConta -= valor;
            AtualizarSaldoTotal();
        }

        public void PegarEmprestimo(double valor) 
        {
           if(valor > LimiteEmprestimo)
           {
               Console.WriteLine("Valor do empréstimo excede o limite.");
                return;
           }
           
           SaldoEmprestimo += valor;
            AtualizarSaldoTotal();
        }
        public void PagarEmprestimo(double valor)
        {
            if(valor <= 0)
            {
                Console.WriteLine("O valor tem que ser maior que zero!");
                return;
            }
            
            if(valor > SaldoEmprestimo)
            {
                valor = SaldoEmprestimo;
            }

            SaldoEmprestimo -= valor;
            AtualizarSaldoTotal();
        }
    }
}
