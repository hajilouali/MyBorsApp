using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Models.Site.Models.contracts;

namespace DataLayer
{
   public class MyContext:DbContext
    {
        public DbSet<Users> Userses { get; set; }
        public DbSet<Roles> Roleses { get; set; }
        public DbSet<UserContracts> UserContractses { get; set; }
        public DbSet<UsersOrderLogs> UsersOrderLogses { get; set; }
        public DbSet<ContractCellslog> ContractCellslogs { get; set; }
        public System.Data.Entity.DbSet<Models.Api.Response.ContractsViewModel> ContractsViewModels { get; set; }
    }
}
