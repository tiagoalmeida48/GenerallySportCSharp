using GenerallySport.DAO;
using GenerallySport.Models;
using GenerallySport.Token;
using GenerallySport.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace GenerallySport.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ITokenGenerator _tokenGenerator;

        public LoginController(IConfiguration configuration, ITokenGenerator tokenGenerator)
        {
            _configuration = configuration;
            _tokenGenerator = tokenGenerator;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            try
            {
                var token = _tokenGenerator.GenerateToken(loginViewModel.Email, loginViewModel.Senha);

                if(token != null)
                { 
                    return Ok(new ResultViewModel
                    {
                        Message = "Cliente autenticado com sucesso",
                        Success = true,
                        Data = new
                        {
                            Token = token,
                            TokenExpires = DateTime.UtcNow.AddHours(int.Parse(_configuration["Jwt:HoursToExpire"])),
                        }
                    });
                }
                else
                {
                    return StatusCode(401, "O e-mail e senha estão incorretos");
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocorreu algum erro interno na aplicação, por favor tente novamente");
            }
        }
    }
}
