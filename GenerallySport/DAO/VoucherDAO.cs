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
    public class VoucherDAO : DAO
    {
        public List<Voucher> RetornarListaVoucher()
        {
            List<Voucher> lstVoucher = new List<Voucher>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.AppendLine("SELECT * FROM VOUCHER");

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
                            Voucher voucher = new Voucher();

                            int iConvert = 0;
                            decimal decConvert = 0;

                            if (sdReader["ID"] != null) voucher.Id = int.TryParse(sdReader["ID"].ToString(), out iConvert) ? iConvert : 0;

                            if (sdReader["TITULO"] != null) 
                                voucher.Titulo = sdReader["TITULO"].ToString(); 

                            if (sdReader["CAMINHO_FOTO"] != null) voucher.CaminhoFoto = sdReader["CAMINHO_FOTO"].ToString();
                           
                            if (sdReader["PRECO"] != null) voucher.Preco = decimal.TryParse(sdReader["PRECO"].ToString(), out decConvert) ? decConvert : 0;
                             
                            if (sdReader["INATIVO"] != null) voucher.Inativo = sdReader["INATIVO"].ToString();

                            lstVoucher.Add(voucher);
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
            return lstVoucher;
        }

        public List<Voucher> RetornarListaVoucherPorId(int Id)
        {
            List<Voucher> lstVoucher = RetornarListaVoucher();

            List<Voucher> voucher = lstVoucher.Where(c => c.Id == Id).ToList();
            return voucher;
        }

        public List<Voucher> RetornarListaVoucherPorTitulo(string titulo)
        {
            List<Voucher> lstVoucher = RetornarListaVoucher();

            List<Voucher> voucher = lstVoucher.Where(c => c.Titulo == titulo).ToList();
            return voucher;
        }

        public int CadastrarVoucher(Voucher voucher)
        {
            SqlConnection connection = new SqlConnection(this.connectionString);

            int retorno = 0;

            string query = "INSERT INTO VOUCHER " +
                "(TITULO, PRECO, CAMINHO_FOTO, INATIVO) VALUES" +
                "(@Titulo, @Preco, @CaminhoFoto, @Inativo);";
            SqlCommand cmd = new SqlCommand(query.ToString(), connection);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@Titulo", voucher.Titulo);
            cmd.Parameters.AddWithValue("@Preco", voucher.Preco);
            cmd.Parameters.AddWithValue("@CaminhoFoto", voucher.CaminhoFoto);
            cmd.Parameters.AddWithValue("@Inativo", voucher.Inativo);


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


        public int CadastrarVoucherPedidoVenda(PedidoVendaVoucher pedidoVendaVoucher)
        {
           // pedidoVendaVoucher.IdVoucher = id;
            SqlConnection connection = new SqlConnection(this.connectionString);

            int retorno = 0;

            string query = "INSERT INTO PEDIDOVENDA_VOUCHER " +
                "(ID_VOUCHER, ID_CLIENTE, DATA_PEDIDOVENDA, SITUACAO_PEDIDOVENDA, CONDICAO_PAGAMENTO, VALOR_FINAL, VALIDADO) VALUES" +
                "(@IdVoucher, @IdCliente, @DataPedidovenda, @SituacaoPedidovenda, @CondicaoPagamento, @ValorFinal, @Validado);";
            SqlCommand cmd = new SqlCommand(query.ToString(), connection);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@IdVoucher", pedidoVendaVoucher.IdVoucher);
            cmd.Parameters.AddWithValue("@IdCliente", pedidoVendaVoucher.IdCliente);
            cmd.Parameters.AddWithValue("@DataPedidovenda", pedidoVendaVoucher.DataPedidovenda);
            cmd.Parameters.AddWithValue("@SituacaoPedidovenda", pedidoVendaVoucher.SituacaoPedidovenda);
            cmd.Parameters.AddWithValue("@CondicaoPagamento", pedidoVendaVoucher.Condicaopagamento);
            cmd.Parameters.AddWithValue("@ValorFinal", pedidoVendaVoucher.ValorFinal);
            cmd.Parameters.AddWithValue("@Validado", pedidoVendaVoucher.ValorFinal);

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
