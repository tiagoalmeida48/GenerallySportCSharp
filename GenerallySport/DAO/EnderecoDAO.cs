using GenerallySport.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace GenerallySport.DAO
{
    public class EnderecoDAO : DAO
    {
        public List<Endereco> RetornarListaEndereco()
        {
            List<Endereco> lstEndereco = new List<Endereco>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.AppendLine("SELECT * FROM ENDERECO");

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
                            Endereco endereco = new Endereco();

                            int iConvert = 0;

                            if (sdReader["ID"] != null) 
                                endereco.Id = int.TryParse(sdReader["ID"].ToString(), out iConvert) ? iConvert : 0;

                            if (sdReader["CEP"] != null)
                                endereco.Cep = sdReader["CEP"].ToString();

                            if (sdReader["LOGRADOURO"] != null)
                                endereco.Logradouro = sdReader["LOGRADOURO"].ToString();

                            if (sdReader["NUMERO"] != null) endereco.Numero = sdReader["NUMERO"].ToString();

                            if (sdReader["COMPLEMENTO"] != null) endereco.Complemento = sdReader["COMPLEMENTO"].ToString();

                            if (sdReader["BAIRRO"] != null) endereco.Bairro = sdReader["BAIRRO"].ToString();

                            if (sdReader["CIDADE"] != null) endereco.Cidade = sdReader["CIDADE"].ToString();
                            
                            if (sdReader["UF"] != null) endereco.Uf = sdReader["UF"].ToString();

                            lstEndereco.Add(endereco);
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
            return lstEndereco;
        }

        public Endereco RetornarEnderecoPorId(int id)
        {
            List<Endereco> lstEndereco = RetornarListaEndereco();
            Endereco endereco = lstEndereco.Where(c => c.Id == id).FirstOrDefault();
            return endereco;
        }

        public int CadastrarEndereco(Endereco endereco)
        {
            SqlConnection connection = new SqlConnection(this.connectionString);

            int retorno = 0;

            string query = "INSERT INTO ENDERECO " +
                "(CEP, LOGRADOURO, NUMERO, COMPLEMENTO, BAIRRO, CIDADE, UF) VALUES" +
                "(@Cep, @Logradouro, @Numero, @Complemento, @Bairro, @Cidade, @Uf); SELECT SCOPE_IDENTITY()";
            SqlCommand cmd = new SqlCommand(query.ToString(), connection);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@Cep", endereco.Cep);
            cmd.Parameters.AddWithValue("@Logradouro", endereco.Logradouro);
            cmd.Parameters.AddWithValue("@Numero", endereco.Numero);
            cmd.Parameters.AddWithValue("@Complemento", endereco.Complemento);
            cmd.Parameters.AddWithValue("@Bairro", endereco.Bairro);
            cmd.Parameters.AddWithValue("@Cidade", endereco.Cidade);
            cmd.Parameters.AddWithValue("@Uf", endereco.Uf);

            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                    retorno = Convert.ToInt32(cmd.ExecuteScalar());

                    //SqlCommand cmd = new SqlCommand(query.ToString(), connection);
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

        public int AtualizarEndereco(Endereco endereco)
        {
            SqlConnection connection = new SqlConnection(this.connectionString);

            int retorno = 0;

            string query = "UPDATE ENDERECO SET " +
                "CEP = @Cep, LOGRADOURO = @Logradouro, NUMERO = @Numero, COMPLEMENTO = @Complemento, BAIRRO = @Bairro, CIDADE = @Cidade, UF = @Uf WHERE ID = @Id";
            SqlCommand cmd = new SqlCommand(query.ToString(), connection);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@Id", endereco.Id);
            cmd.Parameters.AddWithValue("@Cep", endereco.Cep);
            cmd.Parameters.AddWithValue("@Logradouro", endereco.Logradouro);
            cmd.Parameters.AddWithValue("@Numero", endereco.Numero);
            cmd.Parameters.AddWithValue("@Complemento", endereco.Complemento);
            cmd.Parameters.AddWithValue("@Bairro", endereco.Bairro);
            cmd.Parameters.AddWithValue("@Cidade", endereco.Cidade);
            cmd.Parameters.AddWithValue("@Uf", endereco.Uf);

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

        public int DeletarEndereco(Endereco endereco)
        {
            SqlConnection connection = new SqlConnection(this.connectionString);

            int retorno = 0;

            string query = "DELETE FROM endereco WHERE ID = @Id";
            SqlCommand cmd = new SqlCommand(query.ToString(), connection);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@Id", endereco.Id);

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
