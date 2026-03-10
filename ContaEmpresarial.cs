using System;

namespace OUTBANK
{
    // maior limite de empréstimo
    internal class ContaEmpresarial : Conta
    {
        public override double LimiteEmprestimo { get; } = 10000;
        public string CNPJ { get; }

        public ContaEmpresarial(string titular, string cpf, DateTime dataNascimento, string senha, int numeroConta = 0)
            : base(titular, cpf, dataNascimento, senha, numeroConta) { }

        public ContaEmpresarial(Titular titular, int numeroConta)
            : base(titular, numeroConta) { }

        public override void ExibirInformacoes()
        {
            base.ExibirInformacoes();
            Console.WriteLine($"  Limite Emp.   : R$ {LimiteEmprestimo:F2} (conta empresarial)");
        }
    }
}