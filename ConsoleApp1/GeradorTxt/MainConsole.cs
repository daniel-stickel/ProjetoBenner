using AvaliacaoDotnet;
using AvaliacaoDotnet.GeradorTxt;
using System;
using System.IO;
using AvaliacaoDotnet.Utils;

namespace GeradorTxt
{
    /// <summary>
    /// Responsável por interagir com o usuário via console.
    /// </summary>
    public static class MainConsole
    {
        private static string _jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data", "base-dados.json");
        private static string _outputDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "out");

        public static void Run()
        {
            Directory.CreateDirectory(_outputDir);
            while (true)
            {
                Console.WriteLine();
                ConsoleUtils.ExibirTitulo("Menu");
                Console.WriteLine("1. Configurar arquivo .json (base de dados)");
                Console.WriteLine("2. Configurar diretório de output");
                Console.WriteLine("3. Gerar arquivo");
                Console.WriteLine("0. Sair");
                Console.Write("Opção: ");

                var opt = Console.ReadLine();
                Console.WriteLine();

                switch (opt)
                {
                    case "1":
                        Console.Write("Informe o caminho completo do arquivo .json: ");
                        var jp = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(jp) && File.Exists(jp))
                        {
                            _jsonPath = jp;
                            ConsoleUtils.ExibirSucesso("OK! JSON configurado: " + _jsonPath);
                        }
                        else
                        {
                            ConsoleUtils.ExibirErro("Caminho inválido ou arquivo não encontrado.");
                        }
                        break;

                    case "2":
                        Console.Write("Informe o diretório de saída para o .txt: ");
                        var od = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(od))
                        {
                            try
                            {

                                Directory.CreateDirectory(od);

                                _outputDir = od;
                                ConsoleUtils.ExibirSucesso("OK! Diretório de saída configurado: " + _outputDir);
                            }
                            catch (Exception ex)
                            {
                                ConsoleUtils.ExibirErro("Caminho inválido, Digite o caminho correto\n" +
                                                  " Erro: " + ex.Message);
                            }
                        }
                        else
                        {
                            ConsoleUtils.ExibirErro("Diretório inválido ou vazio.");
                        }
                        break;

                    case "3": //Mudança

                        ConsoleUtils.ExibirTitulo("Gerar arquivo\n" +
                                      "Digite 1 para o layout padrão \n" +
                                      "Digite 2 para o novo layout com a informação Categoria");
                        var versao = Console.ReadLine();

                        if (versao != "1" && versao != "2")
                        {
                            ConsoleUtils.ExibirErro("Opção inválida");
                            break;
                        }

                        GeradorArquivoBase gerador;
                        string sufixo;

                        if (versao == "1")
                        {
                            gerador = new GeradorArquivoBase();
                            sufixo = "v01";
                        }
                        else
                        {
                            gerador = new GeradorLayout2();
                            sufixo = "v02";
                        }

                        try
                        {

                            var dados = JsonRepository.LoadEmpresas(_jsonPath);

                            var fileName = $"saida_leiaute_versão 01_{DateTime.Now:yyyyMMdd_HHmmss}.txt";

                            var fullPath = Path.Combine(_outputDir, fileName);

                            gerador.Processar(dados, fullPath);
                            ConsoleUtils.ExibirSucesso("Arquivo gerado em: " + fullPath);
                        }
                        catch (Exception ex)
                        {
                            ConsoleUtils.ExibirErro("Erro ao gerar arquivo: " + ex.Message);
                        }
                        break;

                    case "0":
                        return;

                    default:
                        ConsoleUtils.ExibirErro("Opção inválida.");
                        break;
                }
            }
        }
    }
}
