using GenerallySport.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerallySport.DAO
{
    public class ProdutoFavoritoDAO : DAO
    {
        public List<ProdutoFavorito> RetornarListaProdutosFavoritos()
        {
            List<ProdutoFavorito> lstProdutoFavorito = new List<ProdutoFavorito>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.AppendLine("SELECT F.*, P.* FROM FAVORITO F INNER JOIN PRODUTO P ON F.ID_PRODUTO = P.ID");

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
                            ProdutoFavorito produtoFavorito = new ProdutoFavorito();
                            produtoFavorito.Produto = new Produto();

                            int iConvert = 0;
                            decimal decConvert = 0;
                            DateTime dtConvert = DateTime.MinValue;


                            if (sdReader["ID"] != null) produtoFavorito.Id = int.TryParse(sdReader["ID"].ToString(), out iConvert) ? iConvert : 0;

                            if (sdReader["ID_PRODUTO"] != null)
                            {
                                produtoFavorito.IdProduto = int.TryParse(sdReader["ID_PRODUTO"].ToString(), out iConvert) ? iConvert : 0;
                            }

                            if (sdReader["ID_PRODUTO"] != null)
                            {
                                produtoFavorito.Produto.Id = int.TryParse(sdReader["ID_PRODUTO"].ToString(), out iConvert) ? iConvert : 0;
                            }

                            if (sdReader["ID_FORNECEDOR"] != null) produtoFavorito.Produto.IdFornecedor = int.TryParse(sdReader["ID_FORNECEDOR"].ToString(), out iConvert) ? iConvert : 0;
                            if (sdReader["NOME"] != null) produtoFavorito.Produto.Nome = sdReader["NOME"].ToString();
                            if (sdReader["DESCRICAO"] != null) produtoFavorito.Produto.Descricao = sdReader["DESCRICAO"].ToString();
                            if (sdReader["CATEGORIA"] != null) produtoFavorito.Produto.Categoria = sdReader["CATEGORIA"].ToString();
                            if (sdReader["DATA_VALIDADE"] != null) produtoFavorito.Produto.DataValidade = DateTime.TryParse(sdReader["DATA_VALIDADE"].ToString(), out dtConvert) ? dtConvert : DateTime.MinValue;
                            if (sdReader["QTDE_ESTOQUE"] != null) produtoFavorito.Produto.QtdeEstoque = int.TryParse(sdReader["QTDE_ESTOQUE"].ToString(), out iConvert) ? iConvert : 0;
                            if (sdReader["QTDE_ESTOQUEATUAL"] != null) produtoFavorito.Produto.QtdeEstoqueatual = int.TryParse(sdReader["QTDE_ESTOQUEATUAL"].ToString(), out iConvert) ? iConvert : 0;
                            if (sdReader["CAMINHO_FOTO"] != null) produtoFavorito.Produto.CaminhoFoto = sdReader["CAMINHO_FOTO"].ToString();
                            if (sdReader["PRECO_UNITARIO"] != null) produtoFavorito.Produto.PrecoUnitario = decimal.TryParse(sdReader["PRECO_UNITARIO"].ToString(), out decConvert) ? decConvert : 0;
                            if (sdReader["PRECO_VENDA"] != null) produtoFavorito.Produto.PrecoVenda = decimal.TryParse(sdReader["PRECO_VENDA"].ToString(), out decConvert) ? decConvert : 0;
                            if (sdReader["INATIVO"] != null) produtoFavorito.Produto.Inativo = sdReader["INATIVO"].ToString();
                            

                            lstProdutoFavorito.Add(produtoFavorito);
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
            return lstProdutoFavorito;
        }

        public List<ProdutoFavorito> RetornarListaProdutoFavoritoPorId(int id)
        {
            List<ProdutoFavorito> lstProdutoFavorito = RetornarListaProdutosFavoritos();

            List<ProdutoFavorito> produtoFav = lstProdutoFavorito.Where(c => c.Id == id).ToList();
            return produtoFav;
        }

        public int CadastrarProdutosFavoritos(ProdutoFavorito produtoFavorito)
        {
            SqlConnection connection = new SqlConnection(this.connectionString);

            int retorno = 0;

            string query = "INSERT INTO FAVORITO " +
                "(ID_PRODUTO) VALUES (@IdProduto);";
                
            SqlCommand cmd = new SqlCommand(query.ToString(), connection);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@IdProduto", produtoFavorito.IdProduto);

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

        public int DeletarProdutoFavorito(ProdutoFavorito produtoFavorito)
        {
            SqlConnection connection = new SqlConnection(this.connectionString);

            int retorno = 0;

            string query = "DELETE FROM FAVORITO WHERE ID = @Id";
            SqlCommand cmd = new SqlCommand(query.ToString(), connection);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@Id", produtoFavorito.Id);

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
