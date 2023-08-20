using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Produto
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void btnGravar_Click(object sender, EventArgs e)
        {

            Produto produto;
            ProdutoDAO produtoDAO;
            try
            {
                produto = new Produto();
                produto.setDescricao(txtDescricao.Text);
                produto.setDataValidade(dateTimePicker.Value);
                produto.setPreco(txtPreco.Text);
                produto.setTaxaLucro(txtTaxaLucro.Text);

                produtoDAO = new ProdutoDAO();
                if (produtoDAO.gravar(produto) > 0)
                {
                    MessageBox.Show("Salvo com Sucesso");
                    dgvDados.DataSource = produtoDAO.listar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            ProdutoDAO produtoDAO;
            try
            {
                produtoDAO = new ProdutoDAO();
                dgvDados.DataSource = produtoDAO.listar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDeletar_Click(object sender, EventArgs e)
        {
            try
            {
                ProdutoDAO produtoDAO = new ProdutoDAO();
                produtoDAO.deletar(txtCodigo.Text);
                dgvDados.DataSource = produtoDAO.listar();

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            Produto produto;
            ProdutoDAO produtoDAO;

            try
            {
                produto = new Produto();
                produto.setCodigo(txtCodigo.Text);
                produto.setDescricao(txtDescricao.Text);
                produto.setPreco(txtPreco.Text);
                produto.setTaxaLucro(txtTaxaLucro.Text);
                produto.setDataValidade(dateTimePicker.Value); 

                produtoDAO = new ProdutoDAO();
                int rowsAffected = produtoDAO.alterar(produto);

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Produto atualizado com sucesso");
                    dgvDados.DataSource = produtoDAO.listar();
                }
                else
                {
                    MessageBox.Show("Nenhum produto foi atualizado");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao alterar produto: " + ex.Message);
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }

        private void txtBoxConsultar_KeyUp(object sender, KeyEventArgs e)
        {
            ProdutoDAO produtoDAO;
            produtoDAO = new ProdutoDAO();

            dgvDados.DataSource = produtoDAO.consultar(txtBoxConsultar.Text);
        }

        private void btnGrafico_Click(object sender, EventArgs e)
        {
            FGrafico f;
            f = new FGrafico();
            f.ShowDialog();
        }

        /*private void dgvDados_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0) // Certifica-se de que a célula editada está dentro dos limites da DataGridView
            {
                DataGridViewRow row = dgvDados.Rows[e.RowIndex];

                int codigo = Convert.ToInt32(row.Cells["Codigo"].Value); // Supondo que a coluna que contém o código se chama "Codigo"
                string descricao = Convert.ToString(row.Cells["Descricao"].Value); // Supondo que a coluna que contém a descrição se chama "Descricao"
                double preco = (double)row.Cells["Preco"].Value; // Supondo que a coluna que contém o preço se chama "Preco"
                double TaxaLucro = (double)row.Cells["TaxaLucro"].Value; // Supondo que a coluna que contém o TaxaLucro se chama "TaxaLucro"
                DateTime dataValidade = Convert.ToDateTime(row.Cells["DataValidade"].Value); // Supondo que a coluna que contém a data de validade se chama "DataValidade"

                Produto produto = new Produto
                {
                    codigo = codigo,
                    descricao = descricao,
                    preco = preco,
                    TaxaLucro = TaxaLucro,
                    dataValidade = dataValidade
                };

                ProdutoDAO produtoDAO = new ProdutoDAO();
                int rowsAffected = produtoDAO.alterar(produto);

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Produto atualizado com sucesso");
                    // Pode ou não atualizar a DataGridView aqui, dependendo se a operação foi bem-sucedida
                }
                else
                {
                    MessageBox.Show("Nenhum produto foi atualizado");
                }
            }
        }*/
    }
}