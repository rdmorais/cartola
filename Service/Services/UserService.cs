using System;
using System.Collections.Generic;
using System.Linq;
using ApiFantasy.Domain.Models;
using ApiFantasy.Infra.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ApiFantasy.Service.Services
{
    public class UserService
    {
        private AppDbContext _context = new AppDbContext();

        /// <summary>
        /// Recupera o usuário com base no Id informado
        /// </summary>
        public List<User> GetAll (){
            var users =  _context.Users
                        .Select(u => new User {
                            Id = u.Id,
                            Email = u.Email,
                            Username = u.Username
                        }).ToList();
            
            return users;
        }

        /// <summary>
        /// Recupera o usuário com base no Id informado
        /// </summary>
        public User GetById (int id){
            var user =  _context.Users
                        .Where(u => u.Id == id)
                        .Select(u => new User {
                            Id = u.Id,
                            Email = u.Email,
                            Username = u.Username
                        })
                        .FirstOrDefault();
            
            return user;
        }
        
    }
}