using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaliacaoDotnet.Entidades
{
    public class Documento
    {
        public string Modelo { get; set; }
        public string Numero { get; set; }
        public decimal Valor { get; set; }
        public List<ItemDocumento> Itens { get; set; } = new List<ItemDocumento>();

        public bool ValorEhValido()
        {

            if (Itens == null) return false;

            decimal somaItens = Itens.Sum(i => i.Valor);
            return somaItens == Valor;
        }
    }
}