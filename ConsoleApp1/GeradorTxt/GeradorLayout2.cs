using AvaliacaoDotnet.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaliacaoDotnet.GeradorTxt
{
    public class GeradorLayout2 : GeradorArquivoBase
    {

        protected override void EscreverTipo02(StringBuilder sb, ItemDocumento item)
        {
            //Layout 02|NumeroITEM|DESCRICAO|VALORITEM
            sb.Append("02|")
                .Append(item.NumeroItem).Append("|") //REQUISITO número do item
                .Append(item.Descricao).Append("|")
                .Append(ToMoney(item.Valor)).AppendLine();

            RegistraLinha("02");

            foreach (var cat in item.Categorias)
            {
                sb.Append("03|")
                .Append(cat.Codigo).Append("|")
                .Append(cat.Descricao).AppendLine();

                RegistraLinha("03");
            }
        }
    }
}
