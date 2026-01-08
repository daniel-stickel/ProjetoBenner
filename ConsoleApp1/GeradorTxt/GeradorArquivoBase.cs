using AvaliacaoDotnet.Entidades;
using AvaliacaoDotnet.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace AvaliacaoDotnet
{
    /// <summary>
    /// Implementa a geração do Leiaute 1.
    /// IMPORTANTE: métodos NÃO marcados como virtual de propósito.
    /// O candidato deve decidir onde permitir override para suportar versões futuras.
    /// </summary>
    public class GeradorArquivoBase : IGeradorArquivo // mudança
    {
        // ADIÇÃO Essa lista implementa o rodapé 
        protected List<string> _TiposDeLinha = new List<string>(); //A principio erra um arra, mas array é limitadon lista não

        public virtual void Processar(List<Empresa> empresas, string caminhoSaida)
        {
        
            _TiposDeLinha.Clear();
            var sb = new StringBuilder();
            foreach (var emp in empresas)
            {
                EscreverTipo00(sb, emp);
                foreach (var doc in emp.Documentos)
                {
                    EscreverTipo01(sb, doc);
                    foreach (var item in doc.Itens)
                    {
                        EscreverTipo02(sb, item);
                    }
                }
            }
            GerarRodape(sb);

            var pasta = Path.GetDirectoryName(caminhoSaida); // vai pegar o caminho da pasta

            if (!Directory.Exists(pasta))
            {
                Directory.CreateDirectory(pasta);
            }
            File.WriteAllText(caminhoSaida, sb.ToString(), Encoding.UTF8);
        }


        protected string ToMoney(decimal val)
        {
            // Força ponto como separador decimal, conforme muitos leiautes.
            return val.ToString("0.00", CultureInfo.InvariantCulture);
        }

        protected void EscreverTipo00(StringBuilder sb, Empresa emp)
        {
            // 00|CNPJEMPRESA|NOMEEMPRESA|TELEFONE
            sb.Append("00").Append("|")
              .Append(emp.CNPJ).Append("|")
              .Append(emp.Nome).Append("|")
              .Append(emp.Telefone).AppendLine();
            _TiposDeLinha.Add("00");
        }

        protected void EscreverTipo01(StringBuilder sb, Documento doc)
        {
            // 01|MODELODOCUMENTO|NUMERODOCUMENTO|VALORDOCUMENTO
            sb.Append("01").Append("|")
              .Append(doc.Modelo).Append("|")
              .Append(doc.Numero).Append("|")
              .Append(ToMoney(doc.Valor)).AppendLine();
            _TiposDeLinha.Add("01");
        }

        protected virtual void EscreverTipo02(StringBuilder sb, ItemDocumento item) //Esse método alterei para virtual para poder alterar no GeradorLayout2
        {
            // 02|DESCRICAOITEM|VALORITEM
            sb.Append("02").Append("|")
              .Append(item.Descricao).Append("|")
              .Append(ToMoney(item.Valor)).AppendLine();
            _TiposDeLinha.Add("02");
        }

        //ADICIONADO
        protected void RegistraLinha(string tipo) //auxiliar para os métodos filhos Layout 2
        {
            _TiposDeLinha.Add(tipo);
        }

        protected void GerarRodape(StringBuilder sb)
        {
            var grupos = _TiposDeLinha.GroupBy(tipo => tipo).OrderBy(g => g.Key);

            foreach ( var grupo in grupos)
            {
                sb.AppendLine($"09| {grupo.Key} | {grupo.Count()}");
            }

            int totalGeral = _TiposDeLinha.Count + grupos.Count() + 1;

            sb.AppendLine($"99| {totalGeral}");
        }

    }
}