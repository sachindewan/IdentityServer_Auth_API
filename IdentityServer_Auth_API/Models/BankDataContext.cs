using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankOfDotNet.Models
{
    public class BankDataContext:DbContext
    {
        public BankDataContext(DbContextOptions<BankDataContext> contextOptions):base(contextOptions)
        {

        } 
        //
        public DbSet<Customer> Customers { get; set; }
    }
}
