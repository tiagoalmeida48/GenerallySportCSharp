using GenerallySport.DAO;
using GenerallySport.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;

namespace GenerallySport.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadImagesController : ControllerBase
    {
        public static IWebHostEnvironment _environment;

        public UploadImagesController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        [HttpPost]
        [AllowAnonymous]
        public string Post ([FromForm] FileUpload file)
        {
            if (file.Files.Length > 0)
            {
                var DIRETORIO = @"C:\Desenvolvimento\C#\Generally Sports"; 
                var PASTA_UPLOAD = @"\uploadsFotos\";
                try
                {
                    if (!Directory.Exists(DIRETORIO + PASTA_UPLOAD))
                        Directory.CreateDirectory(DIRETORIO + PASTA_UPLOAD);

                    using (FileStream fileStream = System.IO.File.Create(DIRETORIO + PASTA_UPLOAD + file.Files.FileName))
                    {
                        file.Files.CopyTo(fileStream);
                        fileStream.Flush();

                        // PESQUISAR O USUARIO LOGADO E GRAVAR A FOTO DO USUARIO LOGADO
                        ClienteDAO clienteDAO = new ClienteDAO();
                        var idUsuarioLogado = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

                        Cliente cliente = clienteDAO.RetornarClientePorId(idUsuarioLogado);
                        cliente.CaminhoFoto = file.Files.FileName;
                        clienteDAO.AtualizarCliente(cliente, false);

                        return PASTA_UPLOAD + file.Files.FileName;
                    }

                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
            }
            return "Unsuccessfull";
        }
    }

}
