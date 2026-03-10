using System;

namespace OUTBANK
{
    internal class Titular
    {
        public string Nome { get; }
        public string CPF { get; }
        public DateTime DataNascimento { get; }
        private string Senha { get; }

        public Titular(string nome, string cpf, DateTime dataNascimento, string senha)
        {
            Nome = nome;
            CPF = cpf;
            DataNascimento = dataNascimento;
            Senha = senha;
        }

        public bool ValidarSenha(string senha) => Senha == senha;

        public string SenhaParaSalvar() => Senha;

        public void ExibirDados()
        {
            Console.WriteLine($"  Nome           : {Nome}");
            Console.WriteLine($"  CPF            : {FormatarCPF(CPF)}");
            Console.WriteLine($"  Data Nascimento: {DataNascimento:dd/MM/yyyy}");
        }

        public static string FormatarCPF(string cpf)
        {
            if (cpf.Length == 11)
                return $"{cpf[..3]}.{cpf[3..6]}.{cpf[6..9]}-{cpf[9..]}";
            return cpf;
        }

        public static bool ValidarFormatoCPF(string cpf)
        {
            string apenasDigitos = System.Text.RegularExpressions.Regex.Replace(cpf, @"\D", "");
            return apenasDigitos.Length == 11;
        }

        public override string ToString() => Nome;
    }
}