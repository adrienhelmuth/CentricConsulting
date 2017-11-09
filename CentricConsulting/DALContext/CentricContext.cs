using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CentricConsulting.Models; //need this to access the models
using System.Data.Entity; //need this to access the DbContext object
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.Entity.ModelConfiguration.Conventions;


namespace CentricConsulting.DALContext 
{
    public class CentricContext : DbContext
    {

        public CentricContext() : base("name=DefaultConnection")
        {


        }

        public DbSet<userDetails> userDetails { get; set; }
        public DbSet<Recognition> Recognition { get; set; }

    }
}