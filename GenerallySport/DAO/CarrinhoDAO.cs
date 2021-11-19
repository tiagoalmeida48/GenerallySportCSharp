using GenerallySport.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace GenerallySport.DAO
{
    public class CarrinhoDAO : DAO
    {
        public List<Carrinho> RetornarListaCarrinho(int idCliente)
        {
            List<Carrinho> lstCarrinho = new List<Carrinho>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.AppendLine("SELECT CARRINHO.*, PRODUTO.* FROM CARRINHO JOIN PRODUTO ON PRODUTO.ID = CARRINHO.ID_PRODUTO WHERE ID_CLIENTE = " + idCliente);

                SqlCommand objCmd = new SqlCommand(sbQuery.ToString(), conn);
                objCmd.CommandType = CommandType.Text;

                try
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();

                        SqlDataReader sdReader = objCmd.ExecuteReader();

                        while (sdReader.Read())
                        {
                            Carrinho carrinho = new Carrinho();
                            carrinho.Produto = new Produto();

                            int iConvert = 0;
                            decimal decConvert = 0;
                            DateTime dtConvert = DateTime.MinValue;

                            if (sdReader["ID"] != null)
                                carrinho.Id = int.TryParse(sdReader["ID"].ToString(), out iConvert) ? iConvert : 0;

                            if (sdReader["ID_CLIENTE"] != null)
                                carrinho.IdCliente = int.TryParse(sdReader["ID_CLIENTE"].ToString(), out iConvert) ? iConvert : 0;
                            if (sdReader["QTDE"] != null)
                                carrinho.Qtde = int.TryParse(sdReader["QTDE"].ToString(), out iConvert) ? iConvert : 1;

                            if (sdReader["PRECO"] != null)
                                carrinho.Preco = decimal.TryParse(sdReader["PRECO"].ToString(), out decConvert) ? iConvert : 0;

                            if (sdReader["ID_PRODUTO"] != null)
                                carrinho.IdProduto = int.TryParse(sdReader["ID_PRODUTO"].ToString(), out iConvert) ? iConvert : 0;

                            if (sdReader["ID_PRODUTO"] != null)
                                carrinho.Produto.Id = int.TryParse(sdReader["ID_PRODUTO"].ToString(), out iConvert) ? iConvert : 0;

                            if (sdReader["ID_FORNECEDOR"] != null)
                                carrinho.Produto.IdFornecedor = int.TryParse(sdReader["ID_FORNECEDOR"].ToString(), out iConvert) ? iConvert : 0;

                            if (sdReader["NOME"] != null) carrinho.Produto.Nome = sdReader["NOME"].ToString();

                            if (sdReader["DESCRICAO"] != null) carrinho.Produto.Descricao = sdReader["DESCRICAO"].ToString();

                            if (sdReader["CATEGORIA"] != null) carrinho.Produto.Categoria = sdReader["CATEGORIA"].ToString();

                            if (sdReader["DATA_VALIDADE"] != null) carrinho.Produto.DataValidade = DateTime.TryParse(sdReader["DATA_VALIDADE"].ToString(), out dtConvert) ? dtConvert : DateTime.MinValue;

                            if (sdReader["QTDE_ESTOQUE"] != null) carrinho.Produto.QtdeEstoque = int.TryParse(sdReader["QTDE_ESTOQUE"].ToString(), out iConvert) ? iConvert : 0;

                            if (sdReader["QTDE_ESTOQUEATUAL"] != null) carrinho.Produto.QtdeEstoqueatual = int.TryParse(sdReader["QTDE_ESTOQUEATUAL"].ToString(), out iConvert) ? iConvert : 0;

                            if (sdReader["CAMINHO_FOTO"] != null) carrinho.Produto.CaminhoFoto = sdReader["CAMINHO_FOTO"].ToString();

                            if (sdReader["PRECO_UNITARIO"] != null) carrinho.Produto.PrecoUnitario = decimal.TryParse(sdReader["PRECO_UNITARIO"].ToString(), out decConvert) ? decConvert : 0;

                            if (sdReader["PRECO_VENDA"] != null) carrinho.Produto.PrecoVenda = decimal.TryParse(sdReader["PRECO_VENDA"].ToString(), out decConvert) ? decConvert : 0;

                            if (sdReader["INATIVO"] != null) carrinho.Produto.Inativo = sdReader["INATIVO"].ToString();

                            lstCarrinho.Add(carrinho);
                        }
                    }
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                catch (Exception ex1)
                {
                    throw ex1;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
            }
            return lstCarrinho;
        }

        public Carrinho RetornarCarrinhoProdutoPorId(int idProduto, int idCliente)
        {
            List<Carrinho> lstCarrinho = RetornarListaCarrinho(idCliente);
            Carrinho carrinho = lstCarrinho.Where(c => c.IdProduto == idProduto).FirstOrDefault();
            return carrinho;
        }

        public int CadastrarCarrinho(Carrinho carrinho)
        {
            SqlConnection connection = new SqlConnection(this.connectionString);

            int retorno = 0;

            string query = "INSERT INTO CARRINHO " +
                "(ID_CLIENTE, ID_PRODUTO, QTDE, PRECO) VALUES" +
                "(@IdCliente, @IdProduto, @Qtde, @Preco)";
            SqlCommand cmd = new SqlCommand(query.ToString(), connection);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@IdCliente", carrinho.IdCliente);
            cmd.Parameters.AddWithValue("@IdProduto", carrinho.IdProduto);
            cmd.Parameters.AddWithValue("@Qtde", carrinho.Qtde);
            cmd.Parameters.AddWithValue("@Preco", carrinho.Preco);

            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                    retorno = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (connection.State == ConnectionState.Open) connection.Close();
            }
            return retorno;
        }

        public int AtualizarCarrinho(Carrinho carrinho)
        {
            SqlConnection connection = new SqlConnection(this.connectionString);

            int retorno = 0;

            string query = "UPDATE CARRINHO SET " +
                "ID_CLIENTE = @IdCliente, ID_PRODUTO = @IdProduto, QTDE = @Qtde, PRECO = @Preco " +
                "WHERE ID = @Id";
            SqlCommand cmd = new SqlCommand(query.ToString(), connection);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@Id", carrinho.Id);
            cmd.Parameters.AddWithValue("@IdCliente", carrinho.IdCliente);
            cmd.Parameters.AddWithValue("@IdProduto", carrinho.IdProduto);
            cmd.Parameters.AddWithValue("@Qtde", carrinho.Qtde);
            cmd.Parameters.AddWithValue("@Preco", carrinho.Preco);

            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                    retorno = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (connection.State == ConnectionState.Open) connection.Close();
            }
            return retorno;
        }

        public int DeletarCarrinho(Carrinho carrinho)
        {
            SqlConnection connection = new SqlConnection(this.connectionString);

            int retorno = 0;

            string query = "DELETE FROM CARRINHO WHERE ID = @Id";
            SqlCommand cmd = new SqlCommand(query.ToString(), connection);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@Id", carrinho.Id);

            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                    retorno = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (connection.State == ConnectionState.Open) connection.Close();
            }
            return retorno;
        }
    }
}
