using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CentricConsulting; // need this to access the models
using System.Data.Entity;   // need this to access the DbContext object


namespace CentricConsulting.Context
{
    public class CentricContext
    {
        public CentricContext(): base("name=AspnetMembership")
            {

        }

    }
}