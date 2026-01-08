using System.Collections.Generic;

namespace AvaliacaoDotnet.Entidades
{
    public class ItemDocumento
    {
        public string Descricao { get; set; }
        public decimal Valor { get; set; }

        public int NumeroItem { get; set; }
        public List<Categoria> Categorias { get; set; } = new List<Categoria>();
    }
}