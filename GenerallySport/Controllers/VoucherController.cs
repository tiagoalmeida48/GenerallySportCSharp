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
    }
}
