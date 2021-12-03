﻿using GenerallySport.DAO;
using GenerallySport.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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

        [HttpGet("pedidovendavoucher")]
        public List<PedidoVendaVoucher> GetPedidoVendaVoucher()
        {
            VoucherDAO voucherDAO = new VoucherDAO();
            int idCliente = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            return voucherDAO.RetornarListaPedidoVoucher(idCliente);
        }

        [HttpGet]
        [Route("pedidovendavoucher/{Id}")]
        [AllowAnonymous]
        public PedidoVendaVoucher GetPedidoVendaVoucherPorId([FromRoute] int Id)
        {
            VoucherDAO voucherDAO = new VoucherDAO();
            int idCliente = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            return voucherDAO.RetornarListaPedidoVoucherPorId(Id, idCliente);
        }

        [HttpGet]
        [Route("pedidovendavoucher/cliente/{idCliente}")]
        [AllowAnonymous]
        public List<PedidoVendaVoucher> GetPedidoVendaVoucherPorIdCliente([FromRoute] int idCliente)
        {
            VoucherDAO voucherDAO = new VoucherDAO();
            ClienteDAO clienteDAO = new ClienteDAO();

          //  var infoCliente = clienteDAO.RetornarClientePorId(idCliente);
            return voucherDAO.RetornarListaPedidoVoucherPorIdCliente(idCliente);
            
        }

        [HttpPost("pedidovenda")]
        [AllowAnonymous]
        public ActionResult<IEnumerable<string>> Post([FromBody] PedidoVendaVoucher pedidoVendaVoucher)
        {
            int retorno = 0;
            VoucherDAO voucherDAO = new VoucherDAO();
            int idCliente = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (pedidoVendaVoucher.Id < 1)
            {
            //    var ultimoVoucher = voucherDAO.RetornarListaVoucher().LastOrDefault();
                retorno = voucherDAO.CadastrarVoucherPedidoVenda(pedidoVendaVoucher, idCliente);

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
        [AllowAnonymous]
        public ActionResult<IEnumerable<string>> Put([FromRoute] string codigo)
        {
            string retorno = "0";
          //  Cliente cliente = new Cliente();
            ClienteDAO clienteDAO = new ClienteDAO();
            
            VoucherDAO voucherDAO = new VoucherDAO();

            var voucherPedidoCod = voucherDAO.RetornarListaPedidoVoucherPorCodigo(codigo);

            if (codigo != null)
            {
                retorno = voucherDAO.VoucherValidado(codigo);
                

            }
            else
                return new string[] { "Voucher não existe!" };

            if (retorno == "1") {
                var infoCliente = clienteDAO.RetornarClientePorId(voucherPedidoCod.IdCliente);
                return new string[] { $"Voucher validado com sucesso! <br>Cliente: {infoCliente.Nome}, <br>CPF: {infoCliente.Cpf},<br> Email: {infoCliente.Email},<br> Celular: {infoCliente.Celular}"  };
            }
            return new string[] { "Voucher não atualizado!" };
        }
    }
}
