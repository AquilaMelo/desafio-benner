using GeradorTxt;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.GeradorTxt
{
    public class SwitchMenu
    {
        private string _jsonPath;
        private string _outputDir;
        public SwitchMenu(string _jsonPath, string _outputDir)
        {
            this._jsonPath = _jsonPath;
            this._outputDir = _outputDir;
        }
        public void Opcao1()
        {
            Console.Write("Informe o caminho completo do arquivo .json: ");
            var jp = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(jp) && File.Exists(jp))
            {
                _jsonPath = jp;
                Console.WriteLine("OK! JSON configurado: " + _jsonPath);
            }
            else
            {
                Console.WriteLine("Caminho inválido ou arquivo não encontrado.");
            }
        }

        public void Opcao2()
        {
            Console.Write("Informe o diretório de saída para o .txt: ");
            var od = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(od))
            {
                _outputDir = od;
                Directory.CreateDirectory(_outputDir);
                Console.WriteLine("OK! Diretório de saída configurado: " + _outputDir);
            }
            else
            {
                Console.WriteLine("Diretório inválido.");
            }
        }

        public void Opcao3()
        {
            Console.Write("Informe o tipo de Layout: (1 ou 2)");
            string opcaoLayout = Console.ReadLine();

            if (opcaoLayout == "1")
            {
                GerarArquivo(new GeradorArquivoBase(), "01");
            }
            else if (opcaoLayout == "2")
            {
                GerarArquivo(new GeradorArquivoLayout02(), "02");
            }
            else { Console.WriteLine("Opção Inválida"); }
        }

        private void GerarArquivo(GeradorArquivoBase gerador, string versao)
        {
            try
            {

                var dados = JsonRepository.LoadEmpresas(_jsonPath);

                var fileName = $"saida_leiaute_versão {versao}_{DateTime.Now:yyyyMMdd_HHmmss}.txt";

                var fullPath = Path.Combine(_outputDir, fileName);

                gerador.Gerar(dados, fullPath);

                Console.WriteLine("Arquivo gerado em: " + fullPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao gerar arquivo: " + ex.Message);
            }
        }

    }
}
