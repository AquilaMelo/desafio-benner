using ConsoleApp1.GeradorTxt;
using NUnit.Framework;
using System.Reflection;
using System.Text;

namespace GeradorTxt.Teste
{
    [TestFixture]
    public class UnitTest1
    {
        [Test]
        public void Linha99_DeveSerGerada()
        {
            var sb = new StringBuilder();
            var gerador = new GeradorArquivoBase();

            typeof(GeradorArquivoBase).GetField("contador00", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.SetValue(gerador, 1);

            typeof(GeradorArquivoBase).GetMethod("EscreverContadores", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.Invoke(gerador, new object[] { sb });

            Assert.That(sb.ToString(), Does.Contain("99|"));
        }

        [Test]
        public void EscreverTipo02_DeveGerarLinha02()
        {
            var sb = new StringBuilder();
            var gerador = new GeradorArquivoLayout02();

            var item = new ItemDocumento
            {
                NumeroItem = 1,
                Descricao = "Produto",
                Valor = 10m
            };

            typeof(GeradorArquivoLayout02).GetMethod("EscreverTipo02", BindingFlags.NonPublic | BindingFlags.Instance)?.Invoke(gerador, new object[] { sb, item });

            Assert.That(sb.ToString(), Does.Contain("02|1|Produto|10.00"));
        }
    }
}
