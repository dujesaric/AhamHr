﻿using AhamHr.Data.Entities;
using AhamHr.Data.Entities.Models;
using AhamHr.Domain.Abstractions;
using AhamHr.Domain.Helpers;
using AhamHr.Domain.Models.ViewModels.Account;
using AhamHr.Domain.Repositories.Interfaces;
using AhamHr.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AhamHr.Domain.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        public UserRepository(AhamHrContext dbContext, IClaimProvider claimProvider)
        {
            _dbContext = dbContext;
            _claimProvider = claimProvider;
        }
        private readonly AhamHrContext _dbContext;
        private readonly IClaimProvider _claimProvider;


        public User GetUser(int userId)
        {
            return _dbContext.Users.Find(userId);
        }

        public ResponseResult<User> GetUserIfValidCredentials(CredentialsModel credentialsModel)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Email == credentialsModel.Email.ToLower().Trim());
            if (user is null)
                return ResponseResult<User>.Error("Invalid credentials");

            var isValidPassword = EncryptionHelper.ValidatePassword(credentialsModel.Password, user.EncryptedPassword);
            return isValidPassword
                ? new ResponseResult<User>(user)
                : ResponseResult<User>.Error("Invalid credentials");
        }
    }
}
