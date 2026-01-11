using ConsoleApp1.GeradorTxt;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;

namespace GeradorTxt
{
    public class Empresa
    {
        public string CNPJ { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public List<Documento> Documentos { get; set; }
    }

    public class Documento
    {
        public string Modelo { get; set; }
        public string Numero { get; set; }
        public decimal Valor { get; set; }
        public List<ItemDocumento> Itens { get; set; }

        public bool ValidarValor()
        {
            if (Itens == null || Itens.Count == 0)
            {
                return true;
            }
            decimal valorTotal = 0;
            foreach (var item in Itens)
            {
                valorTotal += item.Valor;
            }
            return valorTotal == Valor;
        }
    }

    public class ItemDocumento
    {
        public int NumeroItem { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public List<Categoria> Categorias { get; set; }
    }
}
