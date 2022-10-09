using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using OnlineJokes.Models;

namespace OnlineJokes.Context
{
    public class JokesContext:DbContext
    {
        public DbSet<JokesCategory> JokesCategorys { get; set; }
        public DbSet<JokesInfo> JokesInfos { get; set; }
        public DbSet<Admin> Admins { get; set; }  
        public DbSet<ClientComment> ClientComments { get; set; }  
    }
} 