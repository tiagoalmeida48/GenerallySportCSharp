using GenerallySport.DAO;
using GenerallySport.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenerallySports.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoucherController : ControllerBase
    {
        [HttpGet]
        public List<Voucher> Get()
        {
            VoucherDAO voucherDAO = new VoucherDAO();
            return voucherDAO.RetornarListaVoucher();
        }

        [HttpGet]
        [Route("{Id}")]
        [AllowAnonymous]
        public List<Voucher> Get([FromRoute] int Id)
        {
            VoucherDAO voucherDAO = new VoucherDAO();

            return voucherDAO.RetornarListaVoucherPorId(Id);
        }

        [HttpGet]
        [Route("titulo/{titulo}")]
        [AllowAnonymous]
        public List<Voucher> Get([FromRoute] string titulo)
        {
            VoucherDAO voucherDAO = new VoucherDAO();

            return voucherDAO.RetornarListaVoucherPorTitulo(titulo);

        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult<IEnumerable<string>> Post([FromBody] Voucher voucher)
        {
            int retorno = 0;
            VoucherDAO voucherDAO = new VoucherDAO();

            if (voucher.Id < 1)
            {
                retorno = voucherDAO.CadastrarVoucher(voucher);


            }
            else
                return new string[] { "Voucher já cadastrado!" };

            if (retorno == 1)
            {

                return new string[] { "Voucher inserido com sucesso!" };
            }

            return new string[] { "Voucher não inserido!" };
        }

        [HttpPost("pedidovenda")]
        [AllowAnonymous]
        public ActionResult<IEnumerable<string>> Post([FromBody] PedidoVendaVoucher pedidoVendaVoucher)
        {
            int retorno = 0;
            VoucherDAO voucherDAO = new VoucherDAO();

            if (pedidoVendaVoucher.Id < 1)
            {
            //    var ultimoVoucher = voucherDAO.RetornarListaVoucher().LastOrDefault();
                retorno = voucherDAO.CadastrarVoucherPedidoVenda(pedidoVendaVoucher);

            }
            else
                return new string[] { "Voucher pedido já cadastrado!" };

            if (retorno == 1)
            {

                return new string[] { "Voucher pedido inserido com sucesso!" };
            }

            return new string[] { "Voucher pedido de venda não inserido!" };
        }

        [HttpPut("validar/{codigo}")]
        [Authorize]
        public ActionResult<IEnumerable<string>> Put([FromRoute] string codigo)
        {
            string retorno = "0"; 

            VoucherDAO voucherDAO = new VoucherDAO();

            if (codigo != null)
                retorno = voucherDAO.VoucherValidado(codigo);
            else
                return new string[] { "Voucher não existe!" };

            if (retorno == "1")
                return new string[] { "Voucher validado com sucesso!" };

            return new string[] { "Voucher não atualizado!" };
        }
    }
}
