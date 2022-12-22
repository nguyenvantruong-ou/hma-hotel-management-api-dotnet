namespace Hotel.API.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using NET.Domain;
using System.Net;
using System.Text;
using System.Security.Claims;
using Hotel.API.DTOs.RequestDTOs;
using Hotel.API.DTOs.Constant;
using Hotel.Domain.Accounts.Entity;

public class JwtUtil
    {
        public static string GetToken(IConfiguration Configuration, Account Req, int Code)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                Configuration["Jwt:Issuer"],
                Configuration["Jwt:Audience"],
                new[]
                {
                        new Claim(ClaimTypesJwt.Code, Code.ToString()),
                        new Claim(ClaimTypes.NameIdentifier, Req.Email),
                        new Claim(ClaimTypesJwt.Password, Req.Password),
                        new Claim(ClaimTypesJwt.LastName, Req.LastName),
                        new Claim(ClaimTypesJwt.FirstName, Req.FirstName),
                        new Claim(ClaimTypesJwt.Gender, Req.Gender),
                        new Claim(ClaimTypesJwt.CardId, Req.CardId),
                        new Claim(ClaimTypesJwt.PhoneNumber, Req.PhoneNumber),
                        new Claim(ClaimTypesJwt.Avatar, " set avatar"),
                        new Claim(ClaimTypesJwt.Address, Req.Address),
                        new Claim(ClaimTypes.Role, DTOs.Constant.Role.User)
                },
                expires: DateTime.Now.AddDays(15),
                signingCredentials: credentials
             );

        return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static string GetTokenRegister(IConfiguration Configuration, int Code)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                Configuration["Jwt:Issuer"],
                Configuration["Jwt:Audience"],
                new[]
                {
                            new Claim(ClaimTypesJwt.Code, Code.ToString())
                },
                expires: DateTime.Now.AddDays(15),
                signingCredentials: credentials
             );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
}
