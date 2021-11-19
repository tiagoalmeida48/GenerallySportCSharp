using GenerallySport.DAO;
using GenerallySport.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenerallySport.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoVendaController : ControllerBase
    {
        [HttpGet]
        public List<PedidoVenda> Get()
        {
            PedidoVendaDAO pedidoVendaDAO = new PedidoVendaDAO();
            return pedidoVendaDAO.RetornarListaPedidosVendidos();
        }

        [HttpGet]
        [Route("{Id}")]
        [AllowAnonymous]
        public List<PedidoVenda> Get([FromRoute] int Id)
        {
            PedidoVendaDAO pedidoVendaDAO = new PedidoVendaDAO();

            return pedidoVendaDAO.RetornarListaPedidosVendaPorId(Id);

        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult<IEnumerable<string>> Post([FromBody] PedidoVenda pedidoVenda)
        {
            int retorno = 0;
            PedidoVendaDAO pedidoVendaDAO = new PedidoVendaDAO();
           // ItensPedidoVenda itensPedidoVenda = new ItensPedidoVenda();

            if (pedidoVenda.Id < 1)
            {
              retorno = pedidoVendaDAO.CadastrarPedidoVenda(pedidoVenda);
                

            }
            else
                return new string[] { "Pedido já cadastrado!" };

            if (retorno == 1)
            {

                return new string[] { "Pedido inserido com sucesso!" };
            }

            return new string[] { "Pedido de venda não inserido!" };
        }

        [HttpPost("itens")]
        [AllowAnonymous]
        public ActionResult<IEnumerable<string>> Post( [FromBody] ItensPedidoVenda itensPedidoVenda)
        {
            int retorno = 0;
            PedidoVendaDAO pedidoVendaDAO = new PedidoVendaDAO();
            // ItensPedidoVenda itensPedidoVenda = new ItensPedidoVenda();

            if (itensPedidoVenda.Id < 1)
            {
                var ultimoPedido = pedidoVendaDAO.RetornarListaPedidosVendidos().LastOrDefault();
               retorno = pedidoVendaDAO.CadastrarItensPedidoVenda(ultimoPedido.Id, itensPedidoVenda);

            }
            else
                return new string[] { "Itens do Pedido já cadastrado!" };

            if (retorno == 1)
            {

                return new string[] { "Itens do Pedido inserido com sucesso!" };
            }

            return new string[] { "Itens do Pedido de venda não inserido!" };
        }

        //// PUT api/<ClienteController>/5
        //[HttpPut("{id}")]
        //[Authorize]
        //public ActionResult<IEnumerable<string>> Put([FromBody] Cliente cliente)
        //{
        //    int retorno = 0;
        //    bool encriptografarSenha = cliente.Senha != null ? true : false;
        //    ClienteDAO clienteDAO = new ClienteDAO();
        //    if (cliente.Id > 0)
        //        retorno = clienteDAO.AtualizarCliente(cliente, encriptografarSenha);
        //    else
        //        return new string[] { "Cliente não existe!" };

        //    if (retorno == 1)
        //        return new string[] { "Cliente atualizado com sucesso!" };

        //    return new string[] { "Cliente não atualizado!" };
        //}

    //    [HttpPut()]
    //    [Authorize]
    //    public ActionResult<IEnumerable<string>> BaixaEstoque()
    //    {
    //        int retorno = 0;
    //        PedidoVendaDAO pedidoVendaDAO = new PedidoVendaDAO();

    //            retorno = pedidoVendaDAO.BaixaEstoque();

    //        if (retorno == 1)
    //        {

    //            return new string[] { "Itens do Pedido inserido com sucesso!" };
    //        }

    //        return new string[] { "Itens do Pedido de venda não inserido!" };
    //    }
    //}
    }
}
