using OUTBANK;
using System;
using System.Collections.Generic;
using System.Text;

namespace OUTBANK
{
    internal class Conta
    {
        string Titular { get; }
        public double SaldoConta { get; protected set; } = 0;
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
        public void exibirTitular()
        {
            Console.WriteLine($"Titular: {Titular}");
        }

        public virtual void exibirInformacoes()
        {
            Console.WriteLine($"Titular: {Titular}");
            Console.WriteLine($"Número da Conta: {NumeroConta}");
            Console.WriteLine($"Saldo da Conta: R${SaldoConta}");
            Console.WriteLine($"Saldo do Empréstimo: R${SaldoEmprestimo}");
            Console.WriteLine($"Limite de Empréstimo: R${LimiteEmprestimo}");
        }
    }

}
