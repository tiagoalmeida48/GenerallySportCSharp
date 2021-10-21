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
        public List<Carrinho> RetornarListaCarrinho()
        {
            List<Carrinho> lstCarrinho = new List<Carrinho>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.AppendLine("SELECT * FROM CARRINHO");

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

                            int iConvert = 0;
                            decimal decConvert = 0;


                            if (sdReader["ID"] != null)
                                carrinho.Id = int.TryParse(sdReader["ID"].ToString(), out iConvert) ? iConvert : 0;

                            if (sdReader["ID_CLIENTE"] != null)
                                carrinho.IdCliente = int.TryParse(sdReader["ID_CLIENTE"].ToString(), out iConvert) ? iConvert : 0;

                            if (sdReader["ID_PRODUTO"] != null)
                                carrinho.IdProduto = int.TryParse(sdReader["ID_PRODUTO"].ToString(), out iConvert) ? iConvert : 0;

                            if (sdReader["QTDE"] != null)
                                carrinho.Qtde = int.TryParse(sdReader["QTDE"].ToString(), out iConvert) ? iConvert : 1;

                            if (sdReader["PRECO"] != null)
                                carrinho.Preco = decimal.TryParse(sdReader["PRECO"].ToString(), out decConvert) ? iConvert : 0;

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
            cmd.Parameters.AddWithValue("@Qtde", carrinho.Qtde != 0 ? carrinho.Qtde : 1);
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
            cmd.Parameters.AddWithValue("@Qtde", carrinho.Qtde != 0 ? carrinho.Qtde : 1);
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
