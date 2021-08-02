using System.Collections.Generic;

namespace Domain.Entidades
{
    public class RetornoExcecao
    {
        public int Codigo { get; set; }
        public string Mensagem { get; set; }
        public List<CodigoMensagemErro> Erros { get; set; }
    }

    public class CodigoMensagemErro
    {
        public int CdErro { get; set; }
        public string DsErro { get; set; }
        public string Info { get; set; }
    }
}
