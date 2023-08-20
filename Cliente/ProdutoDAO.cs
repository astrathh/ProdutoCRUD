using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Produto
{
    //DAO - Data Access Object
    public class ProdutoDAO
    {
        public int gravar(Produto obj)
        {
            Banco banco;
            int qtde = 0;
            try
            {
                banco = new Banco();
                banco.comando.CommandText = "Insert into produto(descricao, preco, TaxaLucro, dataValidade) values(@n, @i, @l, @a);";
                banco.comando.Parameters.Add("@n", NpgsqlTypes.NpgsqlDbType.Varchar).Value = obj.descricao;
                banco.comando.Parameters.Add("@i", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.preco;
                banco.comando.Parameters.Add("@l", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.TaxaLucro;
                banco.comando.Parameters.Add("@a", NpgsqlTypes.NpgsqlDbType.Date).Value = obj.dataValidade;
                banco.comando.Prepare();
                qtde = banco.comando.ExecuteNonQuery();
                Banco.conexao.Close();
                return qtde;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao gravar produto." + ex.Message);
            }
        }

        public void gravarGetCodigo(Produto obj)
        {
            Banco banco;
            int codigo;
            try
            {
                banco = new Banco();
                banco.comando.CommandText = "Insert into produto(descricao, preco, TaxaLucro, dataValidade) values(@n, @i, @l, @a) returning codigo;";
                banco.comando.Parameters.Add("@n", NpgsqlTypes.NpgsqlDbType.Varchar).Value = obj.descricao;
                banco.comando.Parameters.Add("@i", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.preco;
                banco.comando.Parameters.Add("@l", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.TaxaLucro;
                banco.comando.Parameters.Add("@a", NpgsqlTypes.NpgsqlDbType.Date).Value = obj.dataValidade;
                banco.comando.Prepare();
                codigo = (int)banco.comando.ExecuteScalar();
                obj.setCodigo(codigo);

                Banco.conexao.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao gravar produto com codigo." + ex.Message);
            }
        }

        public DataTable listar()
        {
            Banco banco;
            try
            {
                banco = new Banco();
                banco.comando.CommandText = "Select codigo, descricao, preco, TaxaLucro, dataValidade from produto order by 1;";
                banco.reader = banco.comando.ExecuteReader(); //Retorna uma tabela postgress

                banco.tabela = new DataTable();
                banco.tabela.Load(banco.reader);

                Banco.conexao.Close();
                return banco.tabela;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao listar produto" + e.Message);
            }
        }
        public Produto preencher(int codigo)
        {
            Banco banco;
            Produto produto = null;
            try
            {
                banco = new Banco();
                banco.comando.CommandText = "Select codigo, descricao, preco, TaxaLucro, dataValidade from produto where codigo = @c;";
                banco.comando.Parameters.Add("@c", NpgsqlTypes.NpgsqlDbType.Integer).Value = codigo;
                banco.reader = banco.comando.ExecuteReader();
                if (banco.reader.Read())
                {
                    produto = new Produto();
                    produto.setCodigo((int)banco.reader[0]);
                    produto.setDescricao((string)banco.reader[1]);
                    produto.setPreco((int)banco.reader[2]);
                    produto.setTaxaLucro((int)banco.reader[3]);
                }
                Banco.conexao.Close();
                return produto;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao preencher produto: " + e.Message);
            }
        }

        public void deletar(int codigo)
        {
            Banco banco;
            try
            {
                banco = new Banco();
                banco.comando.CommandText = "DELETE FROM produto WHERE codigo = @codigo;";
                banco.comando.Parameters.Add("@codigo", NpgsqlTypes.NpgsqlDbType.Integer).Value = codigo;
                banco.comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw new Exception("Erro ao deletar produto: " + ex.Message);
            }
        }
        public void deletar(string codigo)
        {
            Banco banco;
            try
            {
                banco = new Banco();
                banco.comando.CommandText = "DELETE FROM produto WHERE codigo = @codigo;";
                banco.comando.Parameters.Add("@codigo", NpgsqlTypes.NpgsqlDbType.Integer).Value = Convert.ToInt32(codigo);
                banco.comando.Prepare();
                banco.comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw new Exception("Erro ao deletar produto: " + ex.Message);
            }
        }
        public int alterar(Produto obj)
        {
            try
            {
                Banco banco = new Banco();

                banco.comando.CommandText = "UPDATE produto SET descricao = @descricao, preco = @preco, TaxaLucro = @TaxaLucro, datavalidade = @datavalidade WHERE codigo = @codigo;";

                banco.comando.Parameters.AddWithValue("codigo", obj.codigo); // Supondo que obj.Codigo é o código do produto que está sendo atualizado
                banco.comando.Parameters.AddWithValue("descricao", obj.descricao);
                banco.comando.Parameters.AddWithValue("preco", obj.preco);
                banco.comando.Parameters.AddWithValue("TaxaLucro", obj.TaxaLucro);
                banco.comando.Parameters.AddWithValue("datavalidade", obj.dataValidade);
                banco.comando.Prepare();
                int rowsAffected = banco.comando.ExecuteNonQuery();

                return rowsAffected; // Retorna o número de linhas afetadas
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao alterar produto: " + ex.Message);
            }
        }
        public DataTable consultar(string descricao)
        {
            Banco banco;
            try
            {
                banco = new Banco();
                banco.comando.CommandText = "Select codigo,descricao,datavalidade,preco,TaxaLucro from produto WHERE descricao LIKE @d order by 1;";
                banco.comando.Parameters.Add("@d", NpgsqlTypes.NpgsqlDbType.Varchar).Value = "%" + descricao + "%";
                banco.reader = banco.comando.ExecuteReader();// Retorna uma tabela postgres
                banco.tabela = new DataTable();
                banco.tabela.Load(banco.reader);
                Banco.conexao.Close();
                return (banco.tabela);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao listar produto: " + ex.Message);
            }
        }

    }
}
