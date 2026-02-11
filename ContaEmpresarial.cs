using System;
using System.Collections.Generic;
using System.Text;

namespace OUTBANK
{

    internal class ContaEmpresarial : Conta
    {
        public override double LimiteEmprestimo { get; } = 1000;

        public ContaEmpresarial(string titular) : base(titular) {}
    }
}
