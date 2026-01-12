using GeradorTxt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.GeradorTxt
{
    public class GeradorArquivoLayout02 : GeradorArquivoBase
    {
        protected override void EscreverTipo02(StringBuilder sb, ItemDocumento item)
        {
            // 02|NUMEROITEM|DESCRICAOITEM|VALORITEM
            sb.Append("02").Append("|")
              .Append(item.NumeroItem).Append("|")
              .Append(item.Descricao).Append("|")
              .Append(ToMoney(item.Valor)).AppendLine();

            contador02++;

            // 03|NUMEROCATEGORIA|DESCRICAOCATEGORIA
            if (item.Categorias != null && item.Categorias.Count > 0)
            {
                foreach (var categoria in item.Categorias)
                {
                    sb.Append("03").Append("|")
                      .Append(categoria.NumeroCategoria).Append("|")
                      .Append(categoria.DescricaoCategoria).AppendLine();

                    contador03++;
                }
            }
        }
    }
}
