using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Produto
{
    public partial class FGrafico : Form
    {
        public FGrafico()
        {
            InitializeComponent();
            mostrarGrafico();
        }

        public void mostrarGrafico()
        {
            DataTable tabela;
            ProdutoDAO produtoDAO;
            int i;
            double valor = 0;
            try
            {
                produtoDAO = new ProdutoDAO();
                tabela = produtoDAO.listar();
                chart1.ChartAreas[0].AxisX.Title = "Descrição";
                chart1.ChartAreas[0].AxisY.Title = "Lucro em R$ e Dias";
                chart1.Titles.Add("Lucro e Validade");

                chart1.Series[0].Name = "Lucro";
                chart1.Series[0].IsVisibleInLegend = true;

                chart1.Series.Add(new Series());
                chart1.Series[1].Name = "Validade";
                chart1.Series[1].ChartType = SeriesChartType.Line;
                chart1.Series[1].IsVisibleInLegend = true;

                chart1.Series[0].Points.Clear();
                chart1.Series[1].Points.Clear();

                for (i = 0; i < tabela.Rows.Count; i++)
                {
                    if (! ((tabela.Rows[i][4] != DBNull.Value) && (Double.TryParse(tabela.Rows[i][4].ToString(), out valor))))
                    {
                        valor = 0;
                    }
                    //MessageBox.Show("teste");
                    chart1.Series[0].Points.AddXY((string)tabela.Rows[i][1],
                        Convert.ToDouble(tabela.Rows[i][3]));
                    chart1.Series[1].Points.Add(((DateTime)tabela.Rows[i][4] - DateTime.Today).Days);
                    //MessageBox.Show("teste2");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }
    }
}
