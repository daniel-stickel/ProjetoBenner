using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvaliacaoDotnet.Entidades;

namespace AvaliacaoDotnet.Interfaces
{
    public interface IGeradorArquivo
    {
        void Processar(List<Empresa> empresas, string caminhoSaida);
    }
}