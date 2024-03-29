﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace RazorHotelDB21.Services
{
    /// <summary>
    /// Klasse der bruger til at håndtere forbindelse til databasen
    /// </summary>
    public abstract class Connection
    {
        protected String connectionString;
        public IConfiguration Configuration { get; }

        public Connection(IConfiguration configuration)
        {
            Configuration = configuration;
            connectionString = Configuration["ConnectionStrings:DefaultConnection"];
        }

    }
}
