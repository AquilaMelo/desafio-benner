using ConsoleApp1.GeradorTxt;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace GeradorTxt.Teste
{
    [TestFixture]
    public class UnitTest1
    {
        [Test]
        public void Gerar_DeveCriarLinha99_NoLayout02()
        {
            var empresas = new List<Empresa>
    {
        new Empresa
        {
            CNPJ = "123",
            Nome = "Empresa Teste",
            Telefone = "9999",
            Documentos = new List<Documento>
            {
                new Documento
                {
                    Modelo = "55",
                    Numero = "1",
                    Valor = 10,
                    Itens = new List<ItemDocumento>
                    {
                        new ItemDocumento
                        {
                            NumeroItem = 1,
                            Descricao = "Produto",
                            Valor = 10
                        }
                    }
                }
            }
        }
    };

            var path = Path.GetTempFileName();
            var gerador = new GeradorArquivoLayout02();

            gerador.Gerar(empresas, path);

            var conteudo = File.ReadAllText(path);
            Assert.That(conteudo, Does.Contain("99|"));
        }
    }
}
