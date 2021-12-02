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
    public class PedidoVendaDAO : DAO
    {
        public List<PedidoVenda> RetornarListaPedidosVendidos(long idCliente)
        {
            List<PedidoVenda> lstPredidosVendidos = new List<PedidoVenda>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.AppendLine("SELECT P.*, C.* FROM PEDIDOVENDA P INNER JOIN CLIENTE C ON P.ID_CLIENTE = C.ID WHERE P.ID_CLIENTE = " + idCliente);

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
                            PedidoVenda pedidoVenda = new PedidoVenda();
                            pedidoVenda.Cliente = new Cliente();

                            int iConvert = 0;
                            decimal decConvert = 0;
                            DateTime dtConvert = DateTime.MinValue;


                            if (sdReader["ID"] != null) pedidoVenda.Id = int.TryParse(sdReader["ID"].ToString(), out iConvert) ? iConvert : 0;

                            if (sdReader["DATA_PEDIDOVENDA"] != null) pedidoVenda.DataPedidovenda = DateTime.TryParse(sdReader["DATA_PEDIDOVENDA"].ToString(), out dtConvert) ? dtConvert : DateTime.MinValue;

                            if (sdReader["DATA_ENTREGAVENDA"] != null) pedidoVenda.DataEntregavenda = DateTime.TryParse(sdReader["DATA_ENTREGAVENDA"].ToString(), out dtConvert) ? dtConvert : DateTime.MinValue;

                            if (sdReader["SITUACAO_PEDIDOVENDA"] != null)
                                pedidoVenda.SituacaoPedidovenda = sdReader["SITUACAO_PEDIDOVENDA"].ToString();

                            if (sdReader["CONDICAO_PAGAMENTO"] != null) pedidoVenda.CondicaoPagamento = sdReader["CONDICAO_PAGAMENTO"].ToString();

                            if (sdReader["VALOR_FINAL"] != null) pedidoVenda.ValorFinal = decimal.TryParse(sdReader["VALOR_FINAL"].ToString(), out decConvert) ? decConvert : 0;


                            if (sdReader["ID_CLIENTE"] != null)
                            {
                                pedidoVenda.IdCliente = int.TryParse(sdReader["ID_CLIENTE"].ToString(), out iConvert) ? iConvert : 0;
                            }

                            if (sdReader["ID_CLIENTE"] != null)
                            {
                                pedidoVenda.Cliente.Id = int.TryParse(sdReader["ID_CLIENTE"].ToString(), out iConvert) ? iConvert : 0;
                            }

                            if (sdReader["NOME"] != null) pedidoVenda.Cliente.Nome = sdReader["NOME"].ToString();
                            if (sdReader["CPF"] != null) pedidoVenda.Cliente.Cpf = sdReader["CPF"].ToString();
                            if (sdReader["DATA_NASCIMENTO"] != null) pedidoVenda.Cliente.DataNascimento = DateTime.TryParse(sdReader["DATA_NASCIMENTO"].ToString(), out dtConvert) ? dtConvert : DateTime.MinValue;
                            if (sdReader["SEXO"] != null) pedidoVenda.Cliente.Sexo = sdReader["SEXO"].ToString();
                            if (sdReader["CELULAR"] != null) pedidoVenda.Cliente.Celular = sdReader["CELULAR"].ToString();
                            if (sdReader["TELEFONE"] != null) pedidoVenda.Cliente.Telefone = sdReader["TELEFONE"].ToString();
                            if (sdReader["SENHA"] != null) pedidoVenda.Cliente.Senha = sdReader["SENHA"].ToString();
                            if (sdReader["EMAIL"] != null) pedidoVenda.Cliente.Email = sdReader["EMAIL"].ToString();
                            if (sdReader["CAMINHO_FOTO"] != null) pedidoVenda.Cliente.CaminhoFoto = sdReader["CAMINHO_FOTO"].ToString();
                            if (sdReader["ID_ENDERECO"] != null) pedidoVenda.Cliente.IdEndereco = int.TryParse(sdReader["ID_ENDERECO"].ToString(), out iConvert) ? iConvert : 0;

                            lstPredidosVendidos.Add(pedidoVenda);
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
            return lstPredidosVendidos;
        }

        public List<PedidoVenda> RetornarListaPedidosVendaPorId(int Id, long idCliente)
        {
            List<PedidoVenda> lstPredidosVendidos = RetornarListaPedidosVendidos(idCliente);

            List<PedidoVenda> pedidoVenda = lstPredidosVendidos.Where(c => c.Id == Id).ToList();
            return pedidoVenda;
        }


        public int CadastrarItensPedidoVenda(int id, ItensPedidoVenda itensPedidoVenda)
        {
            itensPedidoVenda.IdPedidovenda = id; 
            SqlConnection connection = new SqlConnection(this.connectionString);

            int retorno = 0;

            string query = "INSERT INTO ITENS_PEDIDOVENDA " +
                "(ID_PEDIDOVENDA, ID_PRODUTO, QTDE, PRECO) VALUES" +
                "(@IdPedidovenda, @IdProduto, @Qtde, @Preco);";
            SqlCommand cmd = new SqlCommand(query.ToString(), connection);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@IdPedidovenda", id);
            cmd.Parameters.AddWithValue("@IdProduto", itensPedidoVenda.IdProduto);
            cmd.Parameters.AddWithValue("@Qtde", itensPedidoVenda.Qtde);
            cmd.Parameters.AddWithValue("@Preco", itensPedidoVenda.Preco);


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

            BaixaEstoque(itensPedidoVenda.IdProduto, itensPedidoVenda.Qtde);
            return retorno;
        }

        public int CadastrarPedidoVenda(PedidoVenda pedidoVenda)
        {
            SqlConnection connection = new SqlConnection(this.connectionString);

            int retorno = 0;

            string query = "INSERT INTO PEDIDOVENDA " +
                "(ID_CLIENTE, DATA_PEDIDOVENDA, DATA_ENTREGAVENDA, SITUACAO_PEDIDOVENDA, CONDICAO_PAGAMENTO, VALOR_FINAL) VALUES" +
                "(@IdCliente, @DataPedidovenda, @DataEntregavenda, @SituacaoPedidovenda, @CondicaoPagamento, @ValorFinal);"; 
            SqlCommand cmd = new SqlCommand(query.ToString(), connection);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@IdCliente", pedidoVenda.IdCliente);
            cmd.Parameters.AddWithValue("@DataPedidovenda", pedidoVenda.DataPedidovenda);
            cmd.Parameters.AddWithValue("@DataEntregavenda", pedidoVenda.DataEntregavenda);
            cmd.Parameters.AddWithValue("@SituacaoPedidovenda", pedidoVenda.SituacaoPedidovenda);
            cmd.Parameters.AddWithValue("@CondicaoPagamento", pedidoVenda.CondicaoPagamento);
            cmd.Parameters.AddWithValue("@ValorFinal", pedidoVenda.ValorFinal);


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

        public int BaixaEstoque(int id, int? qtde)
        {
            Produto produto = new Produto();
            produto.Id = id;
            var qtdeVendida = qtde;
          //  var precoVenda = preco;
            //produto.QtdeEstoqueatual -=  qtde;
            //produto.PrecoVenda -= preco;

            SqlConnection connection = new SqlConnection(this.connectionString);

            int retorno = 0;

            string query = "UPDATE PRODUTO SET " +
                "QTDE_ESTOQUEATUAL = QTDE_ESTOQUEATUAL - @QtdeEstoqueatual WHERE ID = @Id"; 
            SqlCommand cmd = new SqlCommand(query.ToString(), connection);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@Id", produto.Id);
            cmd.Parameters.AddWithValue("@QtdeEstoqueatual", qtdeVendida);
           // cmd.Parameters.AddWithValue("@PrecoVenda", precoVenda);

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

        public List<ItensPedidoVenda> RetornarListaItensPedidoVenda(long idCliente)
        {
            List<ItensPedidoVenda> lstItensPedidoVenda = new List<ItensPedidoVenda>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.AppendLine("SELECT I.*, V.*, P.* FROM ITENS_PEDIDOVENDA I INNER JOIN PEDIDOVENDA V ON I.ID_PEDIDOVENDA = V.ID INNER JOIN PRODUTO P ON I.ID_PRODUTO = P.ID WHERE ID_CLIENTE = " + idCliente);

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
                            ItensPedidoVenda itensPedidoVenda = new ItensPedidoVenda();
                            itensPedidoVenda.PedidoVenda = new PedidoVenda();
                            itensPedidoVenda.Produto = new Produto();

                            int iConvert = 0;
                            decimal decConvert = 0;
                            DateTime dtConvert = DateTime.MinValue;


                            if (sdReader["ID"] != null) itensPedidoVenda.Id = int.TryParse(sdReader["ID"].ToString(), out iConvert) ? iConvert : 0;

                            if (sdReader["QTDE"] != null) itensPedidoVenda.Qtde = int.TryParse(sdReader["QTDE"].ToString(), out iConvert) ? iConvert : 0;

                            if (sdReader["PRECO"] != null) itensPedidoVenda.Preco = decimal.TryParse(sdReader["PRECO"].ToString(), out decConvert) ? decConvert : 0;


                            if (sdReader["ID_PEDIDOVENDA"] != null)
                            {
                                itensPedidoVenda.IdPedidovenda = int.TryParse(sdReader["ID_PEDIDOVENDA"].ToString(), out iConvert) ? iConvert : 0;
                            }

                            if (sdReader["ID_PEDIDOVENDA"] != null)
                            {
                                itensPedidoVenda.PedidoVenda.Id = int.TryParse(sdReader["ID_PEDIDOVENDA"].ToString(), out iConvert) ? iConvert : 0;
                            }

                            if (sdReader["ID_PRODUTO"] != null)
                            {
                                itensPedidoVenda.IdProduto = int.TryParse(sdReader["ID_PRODUTO"].ToString(), out iConvert) ? iConvert : 0;
                            }

                            if (sdReader["ID_PRODUTO"] != null)
                            {
                                itensPedidoVenda.Produto.Id = int.TryParse(sdReader["ID_PRODUTO"].ToString(), out iConvert) ? iConvert : 0;
                            }

                            if (sdReader["ID_CLIENTE"] != null) itensPedidoVenda.PedidoVenda.IdCliente = int.TryParse(sdReader["ID_CLIENTE"].ToString(), out iConvert) ? iConvert : 0;
                            if (sdReader["DATA_PEDIDOVENDA"] != null) itensPedidoVenda.PedidoVenda.DataPedidovenda = DateTime.TryParse(sdReader["DATA_PEDIDOVENDA"].ToString(), out dtConvert) ? dtConvert : DateTime.MinValue;
                            if (sdReader["DATA_ENTREGAVENDA"] != null) itensPedidoVenda.PedidoVenda.DataEntregavenda = DateTime.TryParse(sdReader["DATA_ENTREGAVENDA"].ToString(), out dtConvert) ? dtConvert : DateTime.MinValue;
                            if (sdReader["SITUACAO_PEDIDOVENDA"] != null) itensPedidoVenda.PedidoVenda.SituacaoPedidovenda = sdReader["SITUACAO_PEDIDOVENDA"].ToString();
                            if (sdReader["CONDICAO_PAGAMENTO"] != null) itensPedidoVenda.PedidoVenda.CondicaoPagamento = sdReader["CONDICAO_PAGAMENTO"].ToString();
                            if (sdReader["VALOR_FINAL"] != null) itensPedidoVenda.PedidoVenda.ValorFinal = decimal.TryParse(sdReader["VALOR_FINAL"].ToString(), out decConvert) ? decConvert : 0;
                            if (sdReader["ID_FORNECEDOR"] != null) itensPedidoVenda.Produto.IdFornecedor = int.TryParse(sdReader["ID_FORNECEDOR"].ToString(), out iConvert) ? iConvert : 0;
                            if (sdReader["NOME"] != null) itensPedidoVenda.Produto.Nome = sdReader["NOME"].ToString();
                            if (sdReader["DESCRICAO"] != null) itensPedidoVenda.Produto.Descricao = sdReader["DESCRICAO"].ToString();
                            if (sdReader["CATEGORIA"] != null) itensPedidoVenda.Produto.Categoria = sdReader["CATEGORIA"].ToString();
                            if (sdReader["DATA_VALIDADE"] != null) itensPedidoVenda.Produto.DataValidade = DateTime.TryParse(sdReader["DATA_VALIDADE"].ToString(), out dtConvert) ? dtConvert : DateTime.MinValue;
                            if (sdReader["QTDE_ESTOQUE"] != null) itensPedidoVenda.Produto.QtdeEstoque = int.TryParse(sdReader["QTDE_ESTOQUE"].ToString(), out iConvert) ? iConvert : 0;
                            if (sdReader["QTDE_ESTOQUEATUAL"] != null) itensPedidoVenda.Produto.QtdeEstoqueatual = int.TryParse(sdReader["QTDE_ESTOQUEATUAL"].ToString(), out iConvert) ? iConvert : 0;
                            if (sdReader["CAMINHO_FOTO"] != null) itensPedidoVenda.Produto.CaminhoFoto = sdReader["CAMINHO_FOTO"].ToString();
                            if (sdReader["PRECO_UNITARIO"] != null) itensPedidoVenda.Produto.PrecoUnitario = decimal.TryParse(sdReader["PRECO_UNITARIO"].ToString(), out decConvert) ? decConvert : 0;
                            if (sdReader["PRECO_VENDA"] != null) itensPedidoVenda.Produto.PrecoVenda = decimal.TryParse(sdReader["PRECO_VENDA"].ToString(), out decConvert) ? decConvert : 0;
                            if (sdReader["INATIVO"] != null) itensPedidoVenda.Produto.Inativo = sdReader["INATIVO"].ToString();

                            lstItensPedidoVenda.Add(itensPedidoVenda);
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
            return lstItensPedidoVenda;
        }

        public List<ItensPedidoVenda> RetornarListaItensPedidosVendaPorId(int Id, long idCliente)
        {
            List<ItensPedidoVenda> lstItensPedidoVenda = RetornarListaItensPedidoVenda(idCliente);

            List<ItensPedidoVenda> itensPedidoVenda = lstItensPedidoVenda.Where(c => c.Id == Id).ToList();
            return itensPedidoVenda;
        }

    }
}
