using GenerallySport.DAO;
using GenerallySport.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace GenerallySport.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnderecoController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public List<Endereco> Get()
        {
            EnderecoDAO enderecoDAO = new EnderecoDAO();
            return enderecoDAO.RetornarListaEndereco();
        }

        [HttpGet("{id}")]
        [Authorize]
        public Endereco GetById(int id)
        {
            EnderecoDAO enderecoDAO = new EnderecoDAO();
            return enderecoDAO.RetornarEnderecoPorId(id);
        }

        [HttpPost]
        [Authorize]
        public ActionResult<IEnumerable<string>> Post([FromBody] Endereco endereco)
        {
            int retorno = 0;
            EnderecoDAO enderecoDAO = new EnderecoDAO();
            if (endereco.Id < 1)
                retorno = enderecoDAO.CadastrarEndereco(endereco);
            else
                return new string[] { "Endereco já cadastrado!" };

            if (retorno == 1)
                return new string[] { "Endereco inserido com sucesso!" };

            return new string[] { "Endereco não inserido!" };
        }

        [HttpPut("{id}")]
        [Authorize]
        public ActionResult<IEnumerable<string>> Put([FromBody] Endereco endereco)
        {
            int retorno = 0;
            EnderecoDAO enderecoDAO = new EnderecoDAO();
            if (endereco.Id > 0)
                retorno = enderecoDAO.AtualizarEndereco(endereco);
            else
                return new string[] { "Endereco não existe!" };

            if (retorno == 1)
                return new string[] { "Endereco atualizado com sucesso!" };

            return new string[] { "Endereco não atualizado!" };
        }

        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult<IEnumerable<string>> Delete(int id)
        {
            int retorno = 0;

            if (id == 0)
                return new string[] { "Endereco invalido!" };
            else
            {
                EnderecoDAO enderecoDAO = new EnderecoDAO();
                Endereco endereco = new Endereco();
                endereco.Id = id;

                retorno = enderecoDAO.DeletarEndereco(endereco);
                if (retorno == 1)
                    return new string[] { "Endereco excluido com sucesso!" };
            }
            return new string[] { string.Empty };
        }
    }
}
