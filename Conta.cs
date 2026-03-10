using System;

namespace OUTBANK
{
    
    internal abstract class Conta
    {
        public Titular Titular { get; }
        public double SaldoConta { get; protected set; } = 0;
        public double SaldoEmprestimo { get; protected set; } = 0;
        public int NumeroConta { get; }
        public virtual double LimiteEmprestimo { get; } = 400;

        // Construtor principal
        protected Conta(string titular, string cpf, DateTime dataNascimento, string senha, int numeroConta = 0)
        {
            Titular = new Titular(titular, cpf, dataNascimento, senha);
            NumeroConta = numeroConta == 0 ? new Random().Next(1000, 9999) : numeroConta;
        }

        // Construtor por Titular (usado ao carregar do banco)
        protected Conta(Titular titular, int numeroConta)
        {
            Titular = titular;
            NumeroConta = numeroConta;
        }

        public double SaldoTotal => SaldoConta - SaldoEmprestimo;

        public void ExibirSaldoTotal()
        {
            Console.WriteLine($"  Saldo disponível : R$ {SaldoConta:F2}");
            Console.WriteLine($"  Dívida empréstimo: R$ {SaldoEmprestimo:F2}");
            Console.WriteLine($"  Saldo líquido    : R$ {SaldoTotal:F2}");
        }

        public virtual void Depositar(double valor)
        {
            if (valor <= 0)
                throw new ArgumentException("O valor do depósito deve ser maior que zero.");

            SaldoConta += valor;
            Console.WriteLine($"  Depósito de R$ {valor:F2} realizado com sucesso.");
            Console.WriteLine($"  Novo saldo: R$ {SaldoConta:F2}");
        }

        public virtual void Sacar(double valor)
        {
            if (valor <= 0)
                throw new ArgumentException("O valor do saque deve ser maior que zero.");

            if (valor > SaldoConta)
                throw new InvalidOperationException("Saldo insuficiente para o saque.");

            SaldoConta -= valor;
            Console.WriteLine($"  Saque de R$ {valor:F2} realizado com sucesso.");
            Console.WriteLine($"  Novo saldo: R$ {SaldoConta:F2}");
        }

        public void PegarEmprestimo(double valor)
        {
            if (valor <= 0)
                throw new ArgumentException("O valor do empréstimo deve ser maior que zero.");

            if (valor > LimiteEmprestimo - SaldoEmprestimo)
                throw new InvalidOperationException(
                    $"Valor excede o limite disponível de empréstimo. " +
                    $"Disponível: R$ {(LimiteEmprestimo - SaldoEmprestimo):F2}");

            SaldoEmprestimo += valor;
            SaldoConta += valor;
            Console.WriteLine($"  Empréstimo de R$ {valor:F2} creditado na sua conta.");
            Console.WriteLine($"  Dívida atual: R$ {SaldoEmprestimo:F2}");
        }

        public void PagarEmprestimo(double valor)
        {
            if (valor <= 0)
                throw new ArgumentException("O valor deve ser maior que zero.");

            if (SaldoEmprestimo == 0)
                throw new InvalidOperationException("Você não possui empréstimos ativos.");

            if (valor > SaldoConta)
                throw new InvalidOperationException("Saldo insuficiente para pagar o empréstimo.");

            double pago = Math.Min(valor, SaldoEmprestimo);
            SaldoEmprestimo -= pago;
            SaldoConta -= pago;
            Console.WriteLine($"  Pagamento de R$ {pago:F2} realizado.");
            Console.WriteLine($"  Dívida restante: R$ {SaldoEmprestimo:F2}");
        }

        public virtual void ExibirInformacoes()
        {
            Console.WriteLine($"\n  ╔══ DADOS DA CONTA ══════════════════╗");
            Console.WriteLine($"  Tipo          : {GetType().Name}");
            Console.WriteLine($"  N° da Conta   : {NumeroConta}");
            Titular.ExibirDados();
            Console.WriteLine($"  Saldo         : R$ {SaldoConta:F2}");
            Console.WriteLine($"  Empréstimo    : R$ {SaldoEmprestimo:F2} / R$ {LimiteEmprestimo:F2}");
            Console.WriteLine($"  ╚════════════════════════════════════╝");
        }

        public bool ValidarSenha(string senha) => Titular.ValidarSenha(senha);

        public virtual string VoltaproNormal()
        {
            return $"{GetType().Name}|{NumeroConta}|{Titular.Nome}|{Titular.CPF}|{Titular.DataNascimento:yyyy-MM-dd}|{Titular.SenhaParaSalvar()}|{SaldoConta:F2}|{SaldoEmprestimo:F2}";
        }


        public void RestaurarSaldos(double saldoConta, double saldoEmprestimo)
        {
            SaldoConta = saldoConta;
            SaldoEmprestimo = saldoEmprestimo;
        }

        public override string ToString() => Titular.Nome;
    }
}