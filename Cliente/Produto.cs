using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Produto
{
    public class Produto
    {
        public int codigo { get; private set; }
        public string descricao { get; private set; }
        public DateTime dataValidade { get; private set; }
        public double preco { get; private set; }
        public double TaxaLucro { get; private set; }

        public void setCodigo(int c)
        {
            if (c < 0)
                throw new Exception("Código inválido");
            else
                this.codigo = c;
        }
        //
        public void setCodigo(string c)
        {
            this.setCodigo(Convert.ToInt32(c));

        }
        public void setDescricao(string descricao)
        {
            this.descricao = descricao;
        }
        public void setDataValidade(DateTime dataValidade)
        {
            this.dataValidade = dataValidade;
        }

        public void setDataValidade(String dataValidade)
        {
            this.setDataValidade(Convert.ToDateTime(dataValidade));
        }
        public void setPreco(double i)
        {
            if (i < 0)
                throw new Exception("Preço inválido.");
            else
                this.preco = i;
        }
        public void setPreco(string i)
        {
            this.setPreco(Convert.ToDouble(i));
        }
        //
        public void setTaxaLucro(double l)
        {
            if (l < 0)
                throw new Exception("Lucro inválido.");
            else
                this.TaxaLucro = l;
        }
        public void setTaxaLucro(string l)
        {
            this.setTaxaLucro(Convert.ToDouble(l));
        }

        internal void setDescricao(object text)
        {
            throw new NotImplementedException();
        }
    }
}
