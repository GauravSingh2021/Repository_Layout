﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Models
{
    public class RepositoryDbContext:DbContext
    {
        public RepositoryDbContext(DbContextOptions<RepositoryDbContext> options) : base(options) { }

        public DbSet<Users> Repositorytbl { get; set; }
    }
}
