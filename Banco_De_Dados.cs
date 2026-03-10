using System;
using System.Collections.Generic;
using System.IO;

namespace OUTBANK
{
    
    internal static class BancoDados
    {
        private static readonly string Pasta = "dados";
        private static readonly string Arquivo = Path.Combine(Pasta, "contas.txt");
        private const char Separador = '|';

        // Garante que a pasta e o arquivo existem
        static BancoDados()
        {
            if (!Directory.Exists(Pasta))
                Directory.CreateDirectory(Pasta);

            if (!File.Exists(Arquivo))
                File.WriteAllText(Arquivo, string.Empty);
        }

        // Salva ou atualiza uma conta no arquivo
        public static void SalvarConta(Conta conta)
        {
            List<string> linhas = new List<string>(File.ReadAllLines(Arquivo));
            string novaLinha = conta.VoltaproNormal();
            bool encontrado = false;

            for (int i = 0; i < linhas.Count; i++)
            {
                string[] partes = linhas[i].Split(Separador);
                if (partes.Length >= 3 && partes[2] == conta.Titular.Nome)
                {
                    linhas[i] = novaLinha;
                    encontrado = true;
                    break;
                }
            }

            if (!encontrado)
                linhas.Add(novaLinha);

            File.WriteAllLines(Arquivo, linhas);
        }

        // Carrega todas as contas do arquivo
        public static Dictionary<string, Conta> CarregarContas()
        {
            var contas = new Dictionary<string, Conta>(StringComparer.OrdinalIgnoreCase);

            if (!File.Exists(Arquivo)) return contas;

            string[] linhas = File.ReadAllLines(Arquivo);

            foreach (string linha in linhas)
            {
                if (string.IsNullOrWhiteSpace(linha)) continue;

                try
                {
                    Conta conta = VoltaProNormal(linha);
                    if (conta != null)
                        contas[conta.Titular.Nome] = conta;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"  [Aviso] Erro ao ler linha do banco: {ex.Message}");
                }
            }

            return contas;
        }

        // Exclui uma conta pelo nome
        public static void ExcluirConta(string nome)
        {
            if (!File.Exists(Arquivo)) return;

            List<string> linhas = new List<string>(File.ReadAllLines(Arquivo));
            linhas.RemoveAll(l =>
            {
                string[] p = l.Split(Separador);
                return p.Length >= 3 && p[2].Equals(nome, StringComparison.OrdinalIgnoreCase);
            });
            File.WriteAllLines(Arquivo, linhas);
        }

        // Coloca uma linha do .txt de volta para objeto Conta
        
        private static Conta VoltaProNormal(string linha)
        {
            string[] p = linha.Split(Separador);
            if (p.Length < 8)
                throw new FormatException("Linha do banco com formato inválido.");

            string tipo = p[0];
            int numero = int.Parse(p[1]);
            string nome = p[2];
            string cpf = p[3];
            DateTime dataNasc = DateTime.Parse(p[4]);
            string senha = p[5];
            double saldoConta = double.Parse(p[6]);
            double saldoEmp = double.Parse(p[7]);

            var titular = new Titular(nome, cpf, dataNasc, senha);

            Conta conta = tipo switch
            {
                "ContaCorrente" => new ContaCorrente(titular, numero),
                "ContaPoupanca" => new ContaPoupanca(titular, numero),
                "ContaEmpresarial" => new ContaEmpresarial(titular, numero),
                _ => throw new InvalidOperationException($"Tipo de conta desconhecido: {tipo}")
            };

            conta.RestaurarSaldos(saldoConta, saldoEmp);
            return conta;
        }

        public static void ListarContas()
        {
            if (!File.Exists(Arquivo))
            {
                Console.WriteLine("  Nenhuma conta cadastrada.");
                return;
            }

            string[] linhas = File.ReadAllLines(Arquivo);
            int count = 0;

            Console.WriteLine("\n  ╔══ CONTAS CADASTRADAS ══════════════╗");
            foreach (string linha in linhas)
            {
                if (string.IsNullOrWhiteSpace(linha)) continue;
                string[] p = linha.Split(Separador);
                if (p.Length >= 5)
                {
                    count++;
                    Console.WriteLine($"  [{count}] {p[0],-18} | {p[2],-20} | Conta: {p[1]}");
                }
            }

            if (count == 0)
                Console.WriteLine("  Nenhuma conta cadastrada.");

            Console.WriteLine("  ╚════════════════════════════════════╝");
        }
    }
}