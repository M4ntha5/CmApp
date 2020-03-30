﻿using CmApp.Domains;
using CmApp.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace CmApp.Contracts
{
    public interface IAuthService
    {
        Task<string> Register(User user);
        Task<JwtSecurityToken> Login(User userData);
        JwtSecurityToken GenerateDefaultToken(UserEntity user);
        JwtSecurityToken GenerateAdminToken(UserEntity user);
        Task<UserEntity> Me(string userId);
    }
}