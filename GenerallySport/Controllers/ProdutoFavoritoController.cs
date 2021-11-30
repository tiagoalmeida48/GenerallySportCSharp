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
    public class ProdutoFavoritoController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public List<ProdutoFavorito> Get()
        {
            ProdutoFavoritoDAO produtoFavoritoDAO = new ProdutoFavoritoDAO();
            return produtoFavoritoDAO.RetornarListaProdutosFavoritos();
        }

        [HttpGet("{id}")]
        [Authorize]
        public ProdutoFavorito GetById([FromRoute] int id)
        {
            ProdutoFavoritoDAO produtoFavoritoDAO = new ProdutoFavoritoDAO();

           var lstProdFav = produtoFavoritoDAO.RetornarListaProdutoFavoritoPorId(id);

            ProdutoFavorito produtoFavorito = lstProdFav.FirstOrDefault();

                return produtoFavorito;           
            
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult<IEnumerable<string>> Post([FromBody] ProdutoFavorito produtoFavorito)
        {
            int retorno = 0;
            ProdutoFavoritoDAO produtoFavoritoDAO = new ProdutoFavoritoDAO();

            if (produtoFavorito.Id < 1)
            {
                retorno = produtoFavoritoDAO.CadastrarProdutosFavoritos(produtoFavorito);


            }
            else
                return new string[] { "Produto Favorito já cadastrado!" };

            if (retorno == 1)
            {

                return new string[] { "Produto Favorito inserido com sucesso!" };
            }

            return new string[] { "Produto Favorito não inserido!" };
        }

        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult<IEnumerable<string>> Delete(int id)
        {
            int retorno = 0;

            if (id == 0)
                return new string[] { "Produto favorito invalido!" };
            else
            {
                ProdutoFavoritoDAO produtoFavoritoDAO = new ProdutoFavoritoDAO();
                ProdutoFavorito produtoFavorito = new ProdutoFavorito();
                produtoFavorito.Id = id;

                retorno = produtoFavoritoDAO.DeletarProdutoFavorito(produtoFavorito);
                if (retorno == 1)
                    return new string[] { "Produto favorito excluido com sucesso!" };
            }
            return new string[] { string.Empty };
        }

    }
}
