using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace GeradorTxt
{
    /// <summary>
    /// Implementa a geração do Leiaute 1.
    /// IMPORTANTE: métodos NÃO marcados como virtual de propósito.
    /// O candidato deve decidir onde permitir override para suportar versões futuras.
    /// </summary>
    public class GeradorArquivoBase
    {
        protected int contador00;
        protected int contador01;
        protected int contador02;
        protected int contador03;
        public void Gerar(List<Empresa> empresas, string outputPath)
        {
            contador00 = 0;
            contador01 = 0;
            contador02 = 0;
            contador03 = 0;

            var sb = new StringBuilder();
            foreach (var emp in empresas)
            {
                EscreverTipo00(sb, emp);

                foreach (var doc in emp.Documentos)
                {
                    if (!doc.ValidarValor())
                    {
                        Console.WriteLine($"O documento {doc.Numero} da empresa {emp.Nome} está com o valor divergente do valor total");
                        throw new Exception();
                    }
                    EscreverTipo01(sb, doc);
                    foreach (var item in doc.Itens)
                    {
                        EscreverTipo02(sb, item);
                    }
                }
            }

            EscreverContadores(sb);
            File.WriteAllText(outputPath, sb.ToString(), Encoding.UTF8);
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

            contador00++;
        }

        protected void EscreverTipo01(StringBuilder sb, Documento doc)
        {
            // 01|MODELODOCUMENTO|NUMERODOCUMENTO|VALORDOCUMENTO
            sb.Append("01").Append("|")
              .Append(doc.Modelo).Append("|")
              .Append(doc.Numero).Append("|")
              .Append(ToMoney(doc.Valor)).AppendLine();

            contador01++;
        }

        protected virtual void EscreverTipo02(StringBuilder sb, ItemDocumento item)
        {
            // 02|DESCRICAOITEM|VALORITEM
            sb.Append("02").Append("|")
              .Append(item.Descricao).Append("|")
              .Append(ToMoney(item.Valor)).AppendLine();

            contador02++;
        }

        protected void EscreverContadores(StringBuilder sb)
        {
            int linhasResumo09 = 0;
            const int QTD_LINHA_99 = 1;

            if (contador00 > 0)
            {
                sb.Append($"09|00|{contador00}").AppendLine();
                linhasResumo09++;
            }

            if (contador01 > 0)
            {
                sb.Append($"09|01|{contador01}").AppendLine();
                linhasResumo09++;
            }

            if (contador02 > 0)
            {
                sb.Append($"09|02|{contador02}").AppendLine();
                linhasResumo09++;
            }

            if (contador03 > 0)
            {
                sb.Append($"09|03|{contador03}").AppendLine();
                linhasResumo09++;
            }

            int totalLinhas = contador00 + contador01 + contador02 + contador03 + linhasResumo09 + QTD_LINHA_99;

            sb.Append($"99|{totalLinhas}").AppendLine();
        }
    }
}
