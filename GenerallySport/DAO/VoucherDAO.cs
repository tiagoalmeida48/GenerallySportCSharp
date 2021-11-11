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
    }
}
