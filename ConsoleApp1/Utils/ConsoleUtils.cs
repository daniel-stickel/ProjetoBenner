using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaliacaoDotnet.Utils
{
    internal class ConsoleUtils
    {
        public static void ExibirSucesso(string mensagem)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(mensagem);
            Console.ResetColor();
        }

        public static void ExibirErro(string mensagem)
        {

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(mensagem);
            Console.ResetColor();
        }

        public static void ExibirTitulo(string mensagem)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(mensagem);
            Console.ResetColor();
        }


    }
}
