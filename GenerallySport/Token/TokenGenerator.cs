﻿using GenerallySport.DAO;
using GenerallySport.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace GenerallySport.Token
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly IConfiguration _configuration;

        public TokenGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(string email, string senha)
        {
            EncriptografarSenhas encripSenha = new EncriptografarSenhas(SHA512.Create());
            ClienteDAO clienteDAO = new ClienteDAO();
            var clientes = clienteDAO.RetornarListaCliente();

            var clienteAutenticado = clientes.Where(c => c.Email == email).FirstOrDefault();

            var senhaValidada = false;

            if (clienteAutenticado != null)
                senhaValidada = encripSenha.ConfirmarSenha(senha, clienteAutenticado.Senha);

            if (clienteAutenticado == null || !senhaValidada) return null;

            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, clienteAutenticado.Id.ToString()),
                    new Claim(ClaimTypes.Name, clienteAutenticado.Nome.ToString()),
                }),
                Expires = DateTime.UtcNow.AddHours(int.Parse(_configuration["Jwt:HoursToExpire"])),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
