using GenerallySport.DAO;
using GenerallySport.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace GenerallySport.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        [HttpGet]
        public List<Cliente> Get()
        {
            ClienteDAO clienteDAO = new ClienteDAO();
            return clienteDAO.RetornarListaCliente();
        }

        [HttpGet("{id}")]
        public Cliente GetById(int id)
        {
            ClienteDAO clienteDAO = new ClienteDAO();
            List<Cliente> lstCliente = clienteDAO.RetornarListaCliente();

            List<Cliente> lstClienteWhere = lstCliente.Where(c => c.Id == id).Take(1).ToList();

            Cliente cliente = lstClienteWhere.FirstOrDefault();
            return cliente;
        }

        [HttpPost]
        public ActionResult<IEnumerable<string>> Post([FromBody] Cliente cliente)
        {
            int retorno = 0;
            ClienteDAO clienteDAO = new ClienteDAO();
            if (cliente.Id < 1)
                retorno = clienteDAO.CadastrarCliente(cliente);
            else
                return new string[] { "Cliente já cadastrado!" };

            if (retorno == 1)
                return new string[] { "Cliente inserido com sucesso!" };
            
            return new string[] { "Cliente não inserido!" };
        }

        // PUT api/<ClienteController>/5
        [HttpPut("{id}")]
        public ActionResult<IEnumerable<string>> Put([FromBody] Cliente cliente)
        {
            int retorno = 0;
            ClienteDAO clienteDAO = new ClienteDAO();
            if (cliente.Id > 0)
                retorno = clienteDAO.AtualizarCliente(cliente);
            else
                return new string[] { "Cliente não existe!" };

            if (retorno == 1)
                return new string[] { "Cliente atualizado com sucesso!" };

            return new string[] { "Cliente não atualizado!" };
        }

        [HttpDelete("{id}")]
        public ActionResult<IEnumerable<string>> Delete(int id)
        {
            int retorno = 0;

            if (id == 0)
                return new string[] { "Cliente invalido!" };
            else
            {
                ClienteDAO clienteDAO = new ClienteDAO();
                Cliente cliente = new Cliente();
                cliente.Id = id;

                retorno = clienteDAO.DeletarCliente(cliente);
                if (retorno == 1)
                    return new string[] { "Cliente excluido com sucesso!" };
            }
            return new string[] { string.Empty };
        }
    }
}
