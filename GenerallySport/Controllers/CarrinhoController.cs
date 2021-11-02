using GenerallySport.DAO;
using GenerallySport.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace GenerallySport.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarrinhoController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public List<Carrinho> Get()
        {
            CarrinhoDAO carrinhoDAO = new CarrinhoDAO();
            return carrinhoDAO.RetornarListaCarrinho();
        }

        [HttpPost]
        [Authorize]
        public ActionResult<IEnumerable<string>> Post([FromBody] Carrinho carrinho)
        {
            int retorno = 0;
            CarrinhoDAO carrinhoDAO = new CarrinhoDAO();
            if (carrinho.Id < 1)
                retorno = carrinhoDAO.CadastrarCarrinho(carrinho);
            else
                return new string[] { "Carrinho já cadastrado!" };

            if (retorno == 1)
                return new string[] { "Carrinho inserido com sucesso!" };

            return new string[] { "Carrinho não inserido!" };
        }

        [HttpPut("{id}")]
        [Authorize]
        public ActionResult<IEnumerable<string>> Put([FromBody] Carrinho carrinho)
        {
            int retorno = 0;
            CarrinhoDAO carrinhoDAO = new CarrinhoDAO();
            if (carrinho.Id > 0)
                retorno = carrinhoDAO.AtualizarCarrinho(carrinho);
            else
                return new string[] { "Carrinho não existe!" };

            if (retorno == 1)
                return new string[] { "Carrinho atualizado com sucesso!" };

            return new string[] { "Carrinho não atualizado!" };
        }

        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult<IEnumerable<string>> Delete(int id)
        {
            int retorno = 0;

            if (id == 0)
                return new string[] { "Carrinho invalido!" };
            else
            {
                CarrinhoDAO carrinhoDAO = new CarrinhoDAO();
                Carrinho carrinho = new Carrinho();
                carrinho.Id = id;

                retorno = carrinhoDAO.DeletarCarrinho(carrinho);
                if (retorno == 1)
                    return new string[] { "Carrinho excluido com sucesso!" };
            }
            return new string[] { string.Empty };
        }

        [HttpGet("/ObterIdUsuarioLogado")]
        [Authorize]
        public ActionResult<string> ObterIdUsuarioLogado()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }

        [HttpGet("/ObterNomeUsuarioLogado")]
        [Authorize]
        public ActionResult<string> ObterNomeUsuarioLogado()
        {
            return User.FindFirst(ClaimTypes.Name).Value;
        }
    }
}
