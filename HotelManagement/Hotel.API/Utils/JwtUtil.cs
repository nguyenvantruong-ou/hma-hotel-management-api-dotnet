namespace Hotel.API.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using Hotel.Domain;
using System.Net;
using System.Text;
using System.Security.Claims;
using Hotel.API.DTOs.RequestDTOs;
using Hotel.API.DTOs.Constant;
using Hotel.Domain.Accounts.Entities;

public class JwtUtil
    {
        public static string GetToken(IConfiguration configuration, Account req, int expirationDate)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, req.Email),
                    new Claim(ClaimTypesJwt.LastName, req.LastName),
                    new Claim(ClaimTypesJwt.FirstName, req.FirstName),
                    new Claim(ClaimTypes.Role, req.Role.RoleName)
                },
                expires: DateTime.Now.AddHours(expirationDate),
                //expires : DateTime.Now.AddSeconds(expirationDate),
                signingCredentials: credentials
             );

        return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static string GetTokenRegister(IConfiguration configuration, int code, string email)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                new[]
                {
                    new Claim(ClaimTypesJwt.Code, code.ToString()),
                    new Claim(ClaimTypes.Email, email)
                },
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
             );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
}
