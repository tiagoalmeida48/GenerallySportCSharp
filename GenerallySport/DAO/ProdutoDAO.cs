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
    public class ProdutoDAO : DAO
    {
        public List<Produto> RetornarListaProduto()
        {
            List<Produto> lstProduto = new List<Produto>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.AppendLine("SELECT P.*, F.* FROM PRODUTO P INNER JOIN FORNECEDOR F ON P.ID_FORNECEDOR = F.ID");

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
                            Produto produto = new Produto();
                            produto.Fornecedor = new Fornecedor();

                            int iConvert = 0;
                            decimal decConvert = 0;
                            DateTime dtConvert = DateTime.MinValue;


                            if (sdReader["ID"] != null) produto.Id = int.TryParse(sdReader["ID"].ToString(), out iConvert) ? iConvert : 0;

                            if (sdReader["NOME"] != null) 
                                produto.Nome = sdReader["NOME"].ToString(); 

                            if (sdReader["DESCRICAO"] != null) produto.Descricao = sdReader["DESCRICAO"].ToString();

                            if (sdReader["CATEGORIA"] != null) produto.Categoria = sdReader["CATEGORIA"].ToString();
                            
                            if (sdReader["DATA_VALIDADE"] != null) produto.DataValidade = DateTime.TryParse(sdReader["DATA_VALIDADE"].ToString(), out dtConvert) ? dtConvert : DateTime.MinValue;
                            
                            if (sdReader["QTDE_ESTOQUE"] != null) produto.QtdeEstoque = int.TryParse(sdReader["QTDE_ESTOQUE"].ToString(), out iConvert) ? iConvert : 0;
                             
                            if (sdReader["QTDE_ESTOQUEATUAL"] != null) produto.QtdeEstoqueatual = int.TryParse(sdReader["QTDE_ESTOQUEATUAL"].ToString(), out iConvert) ? iConvert : 0;
                             
                            if (sdReader["CAMINHO_FOTO"] != null) produto.CaminhoFoto = sdReader["CAMINHO_FOTO"].ToString();
                            
                            if (sdReader["PRECO_UNITARIO"] != null) produto.PrecoUnitario = decimal.TryParse(sdReader["PRECO_UNITARIO"].ToString(), out decConvert) ? decConvert : 0;
                             
                            if (sdReader["PRECO_VENDA"] != null) produto.PrecoVenda = decimal.TryParse(sdReader["PRECO_VENDA"].ToString(), out decConvert) ? decConvert : 0;
                             
                            if (sdReader["INATIVO"] != null) produto.Inativo = sdReader["INATIVO"].ToString();


                            if (sdReader["ID_FORNECEDOR"] != null)
                            {
                                produto.IdFornecedor = int.TryParse(sdReader["ID_FORNECEDOR"].ToString(), out iConvert) ? iConvert : 0;
                            }

                            if (sdReader["ID_FORNECEDOR"] != null)
                            {
                                produto.Fornecedor.Id = int.TryParse(sdReader["ID_FORNECEDOR"].ToString(), out iConvert) ? iConvert : 0;
                            }

                            if (sdReader["FANTASIA"] != null) produto.Fornecedor.Fantasia = sdReader["FANTASIA"].ToString();
                            if (sdReader["CNPJ"] != null) produto.Fornecedor.Cnpj = sdReader["CNPJ"].ToString();
                            if (sdReader["CELULAR"] != null) produto.Fornecedor.Celular = sdReader["CELULAR"].ToString();
                            if (sdReader["TELEFONE"] != null) produto.Fornecedor.Telefone = sdReader["TELEFONE"].ToString();

                            lstProduto.Add(produto);
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
            return lstProduto;
        }

        public List<Produto> RetornarListaProdutoPorId(int Id)
        {
            List<Produto> lstProduto = RetornarListaProduto();

            List<Produto> produto = lstProduto.Where(c => c.Id == Id).ToList();
            return produto;
        }

        public List<Produto> RetornarListaProdutoPorNome(string nome)
        {
            List<Produto> lstProduto = RetornarListaProduto();

            List<Produto> produto = lstProduto.Where(c => c.Nome == nome).ToList();
            return produto;
        }
    }
}
