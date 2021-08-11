using SportsBetsServer.Models.Account;
using SportsBetsServer.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace SportsBetsServer.Contracts.Services 
{
    public interface IAccountService 
    {
        string CreatePasswordHash(string password);
        bool VerifyPassword(string password, string hashedPassword);      
        Account CreateNewAccount(AccountRequestDTO userCredentials);
        Account Authenticate(AccountRequestDTO userCredentials);     
        Task<Account> GetAccountById(Guid id);
        Task DeleteUserById(Guid id);
        Task<Account> RegisterNewAccount(AccountRequestDTO userCredentials);
        Account GetAccountByUsername(string username);
        AccountResponseDTO MapAccountToUser(Account account);
        List<AccountResponseDTO> GetAllUsers();
    }
}