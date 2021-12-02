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
            Random randNum = new Random();
            var numeroValido = randNum.Next(10000, 100000).ToString();
            var existeNumero = RetornarListaPedidoVoucherPorCodigo(numeroValido);
            //  var existeNumero1 = existeNumero.Select(c => c.Validado == numeroValido).ToList();

            while (existeNumero != null) 

            {

             numeroValido = randNum.Next(10000, 100000).ToString();

            } 

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
            cmd.Parameters.AddWithValue("@Validado", numeroValido);

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

        public List<PedidoVendaVoucher> RetornarListaPedidoVoucher()
        {
            List<PedidoVendaVoucher> lstPedidoVoucher = new List<PedidoVendaVoucher>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.AppendLine("SELECT * FROM PEDIDOVENDA_VOUCHER");

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
                            PedidoVendaVoucher pedidoVoucher = new PedidoVendaVoucher();

                            int iConvert = 0;
                            decimal decConvert = 0;
                            DateTime dtConvert = DateTime.MinValue;

                            if (sdReader["ID"] != null) pedidoVoucher.Id = int.TryParse(sdReader["ID"].ToString(), out iConvert) ? iConvert : 0;

                            if (sdReader["ID_VOUCHER"] != null)
                                pedidoVoucher.IdVoucher = int.TryParse(sdReader["ID_VOUCHER"].ToString(), out iConvert) ? iConvert : 0;

                            if (sdReader["ID_CLIENTE"] != null) pedidoVoucher.IdCliente = int.TryParse(sdReader["ID_CLIENTE"].ToString(), out iConvert) ? iConvert : 0;

                            if (sdReader["DATA_PEDIDOVENDA"] != null) pedidoVoucher.DataPedidovenda = DateTime.TryParse(sdReader["DATA_PEDIDOVENDA"].ToString(), out dtConvert) ? dtConvert : DateTime.MinValue;

                            if (sdReader["SITUACAO_PEDIDOVENDA"] != null)
                                pedidoVoucher.SituacaoPedidovenda = sdReader["SITUACAO_PEDIDOVENDA"].ToString();

                            if (sdReader["CONDICAO_PAGAMENTO"] != null)
                                pedidoVoucher.Condicaopagamento = sdReader["CONDICAO_PAGAMENTO"].ToString();

                            if (sdReader["VALOR_FINAL"] != null) pedidoVoucher.ValorFinal = decimal.TryParse(sdReader["VALOR_FINAL"].ToString(), out decConvert) ? decConvert : 0;

                            if (sdReader["VALIDADO"] != null) pedidoVoucher.Validado = sdReader["VALIDADO"].ToString();

                            lstPedidoVoucher.Add(pedidoVoucher);
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
            return lstPedidoVoucher;
        }

        public PedidoVendaVoucher RetornarListaPedidoVoucherPorCodigo(string validado)
        {
            List<PedidoVendaVoucher> lstPedidoVoucher = RetornarListaPedidoVoucher();

            PedidoVendaVoucher voucherPedido = lstPedidoVoucher.Where(c => c.Validado == validado).LastOrDefault();
            return voucherPedido;
        }

        public string VoucherValidado(string codigo)
        {
           // PedidoVendaVoucher voucherPedido = new PedidoVendaVoucher();
          //  var voucherPedidoCod = RetornarListaPedidoVoucherPorCodigo(codigo).LastOrDefault();
           // var voucherPedido = voucherPedidoCod.Id.ToString();
            var validado = "VALIDADO";

            SqlConnection connection = new SqlConnection(this.connectionString);

            int retorno = 0;

            string query = "UPDATE PEDIDOVENDA_VOUCHER SET " +
                "VALIDADO = @Validado WHERE VALIDADO = @Codigo AND CONDICAO_PAGAMENTO <> 'boleto'";
            SqlCommand cmd = new SqlCommand(query.ToString(), connection);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@Codigo", codigo);
            cmd.Parameters.AddWithValue("@Validado", validado);

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
            return retorno.ToString();
        }
    }
}
