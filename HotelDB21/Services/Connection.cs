using System;
using System.Collections.Generic;
using System.Text;

namespace HotelDBConsole21.Services
{
    public abstract class Connection
    {
        //indsæt din egen connectionstring
        protected String connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=HotelDbtest2;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        
    }
}
