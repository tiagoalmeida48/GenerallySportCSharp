using GenerallySport.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace GenerallySport.DAO
{
    public class ClienteDAO : DAO
    {
        public List<Cliente> RetornarListaCliente()
        {
            List<Cliente> lstCliente = new List<Cliente>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.AppendLine("SELECT * FROM CLIENTE CLI JOIN ENDERECO EN ON CLI.ID_ENDERECO = EN.ID");

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
                            Cliente cliente = new Cliente();
                            cliente.Endereco = new Endereco();

                            int iConvert = 0;
                            DateTime dtConvert = DateTime.MinValue;


                            if (sdReader["ID"] != null) 
                                cliente.Id = int.TryParse(sdReader["ID"].ToString(), out iConvert) ? iConvert : 0;

                            if (sdReader["NOME"] != null)
                                cliente.Nome = sdReader["NOME"].ToString();

                            if (sdReader["CPF"] != null) cliente.Cpf = sdReader["CPF"].ToString();

                            if (sdReader["DATA_NASCIMENTO"] != null) cliente.DataNascimento = DateTime.TryParse(sdReader["DATA_NASCIMENTO"].ToString(), out dtConvert) ? dtConvert : DateTime.MinValue;

                            if (sdReader["SEXO"] != null) cliente.Sexo = sdReader["SEXO"].ToString();

                            if (sdReader["CELULAR"] != null) cliente.Celular = sdReader["CELULAR"].ToString();

                            if (sdReader["TELEFONE"] != null) cliente.Telefone = sdReader["TELEFONE"].ToString();

                            //ENDEREÇO CLIENTE
                            if (sdReader["ID_ENDERECO"] != null)
                                cliente.IdEndereco = int.TryParse(sdReader["ID_ENDERECO"].ToString(), out iConvert) ? iConvert : 0;

                            if (sdReader["ID_ENDERECO"] != null) 
                                cliente.Endereco.Id = int.TryParse(sdReader["ID_ENDERECO"].ToString(), out iConvert) ? iConvert : 0;

                            if (sdReader["CEP"] != null) cliente.Endereco.Cep = sdReader["CEP"].ToString();

                            if (sdReader["LOGRADOURO"] != null) cliente.Endereco.Logradouro = sdReader["LOGRADOURO"].ToString();

                            if (sdReader["NUMERO"] != null) cliente.Endereco.Numero = sdReader["NUMERO"].ToString();

                            if (sdReader["COMPLEMENTO"] != null) cliente.Endereco.Complemento = sdReader["COMPLEMENTO"].ToString();

                            if (sdReader["BAIRRO"] != null) cliente.Endereco.Bairro = sdReader["BAIRRO"].ToString();

                            if (sdReader["CIDADE"] != null) cliente.Endereco.Cidade = sdReader["CIDADE"].ToString();

                            if (sdReader["UF"] != null) cliente.Endereco.Uf = sdReader["UF"].ToString();

                            lstCliente.Add(cliente);
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
            return lstCliente;
        }

        public Cliente RetornarClientePorId(int id)
        {
            List<Cliente> lstCliente = RetornarListaCliente();
            Cliente cliente = lstCliente.Where(c => c.Id == id).FirstOrDefault();
            return cliente;
        }

        public int CadastrarCliente(Cliente cliente)
        {
            SqlConnection connection = new SqlConnection(this.connectionString);

            int retorno = 0;

            string query = "INSERT INTO CLIENTE " +
                "(NOME, CPF, DATA_NASCIMENTO, SEXO, CELULAR, TELEFONE, ID_ENDERECO) VALUES" +
                "(@Nome, @Cpf, @DataNascimento, @Sexo, @Celular, @Telefone, @IdEndereco)";
            SqlCommand cmd = new SqlCommand(query.ToString(), connection);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@Nome", cliente.Nome);
            cmd.Parameters.AddWithValue("@Cpf", cliente.Cpf);
            cmd.Parameters.AddWithValue("@DataNascimento", cliente.DataNascimento);
            cmd.Parameters.AddWithValue("@Sexo", cliente.Sexo);
            cmd.Parameters.AddWithValue("@Celular", cliente.Celular);
            cmd.Parameters.AddWithValue("@Telefone", cliente.Telefone);
            cmd.Parameters.AddWithValue("@IdEndereco", (int)cliente.IdEndereco);

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

        public int AtualizarCliente(Cliente cliente)
        {
            SqlConnection connection = new SqlConnection(this.connectionString);

            int retorno = 0;

            string query = "UPDATE CLIENTE SET " +
                "NOME = @Nome, CPF = @Cpf, DATA_NASCIMENTO = @DataNascimento, SEXO = @Sexo, CELULAR = @Celular, TELEFONE = @Telefone, ID_ENDERECO = @IdEndereco WHERE ID = @Id";
            SqlCommand cmd = new SqlCommand(query.ToString(), connection);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@Id", cliente.Id);
            cmd.Parameters.AddWithValue("@Nome", cliente.Nome);
            cmd.Parameters.AddWithValue("@Cpf", cliente.Cpf);
            cmd.Parameters.AddWithValue("@DataNascimento", cliente.DataNascimento);
            cmd.Parameters.AddWithValue("@Sexo", cliente.Sexo);
            cmd.Parameters.AddWithValue("@Celular", cliente.Celular);
            cmd.Parameters.AddWithValue("@Telefone", cliente.Telefone);
            cmd.Parameters.AddWithValue("@IdEndereco", (int)cliente.IdEndereco);

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

        public int DeletarCliente(Cliente cliente)
        {
            SqlConnection connection = new SqlConnection(this.connectionString);

            int retorno = 0;

            string query = "DELETE FROM CLIENTE WHERE ID = @Id";
            SqlCommand cmd = new SqlCommand(query.ToString(), connection);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@Id", cliente.Id);

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
