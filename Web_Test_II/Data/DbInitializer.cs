using Web_Test_II_DAL.Context;
using Web_Test_II_DAL.Entityes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Web_Test_II.Data
{
    public class DbInitializer
    {
        private readonly WebTestDB db;
        private readonly ILogger<DbInitializer> logger;

        public DbInitializer(WebTestDB Db, ILogger<DbInitializer> logger) 
        {
            this.db = Db;
            this.logger = logger;
            db.Database.Migrate();
        }



    }
}
