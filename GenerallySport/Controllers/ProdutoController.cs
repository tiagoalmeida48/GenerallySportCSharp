using GenerallySport.DAO;
using GenerallySport.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenerallySports.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        [HttpGet]
        public List<Produto> Get()
        {
            ProdutoDAO objApiProduto = new ProdutoDAO();

            try
            {
                return objApiProduto.RetornarListaProduto();
            }
            catch (Exception ex)
            {
                var retorno = new List<Produto>();
                var produto = new Produto();
                produto.Nome = ex.Message;
                retorno.Add(produto);
                return retorno;
            }

        }

        [HttpGet]
        [Route("{Id}")]
        public List<Produto> Get([FromRoute] int Id)
        {
            ProdutoDAO objApiProduto = new ProdutoDAO();

            return objApiProduto.RetornarListaProdutoPorId(Id);

        }

        [HttpGet]
        [Route("nome/{nome}")]
        public List<Produto> Get([FromRoute] string nome)
        {
            ProdutoDAO objApiProduto = new ProdutoDAO();

            return objApiProduto.RetornarListaProdutoPorNome(nome);

        }
    }
}
