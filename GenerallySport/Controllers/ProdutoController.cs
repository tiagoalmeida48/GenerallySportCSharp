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
    public class ProdutoController : ControllerBase
    {
        [HttpGet]
        public List<Produto> Get()
        {
            ProdutoDAO produtoDAO = new ProdutoDAO();
            return produtoDAO.RetornarListaProduto();
        }

        [HttpGet]
        [Route("{Id}")]
        [AllowAnonymous]
        public List<Produto> Get([FromRoute] int Id)
        {
            ProdutoDAO produtoDAO = new ProdutoDAO();

            return produtoDAO.RetornarListaProdutoPorId(Id);

        }

        [HttpGet]
        [Route("nome/{nome}")]
        [AllowAnonymous]
        public List<Produto> Get([FromRoute] string nome)
        {
            ProdutoDAO produtoDAO = new ProdutoDAO();

            return produtoDAO.RetornarListaProdutoPorNome(nome);

        }
    }
}
