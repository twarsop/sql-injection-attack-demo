using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using DataLayer.Interfaces;
using DataLayer.Models;
using Npgsql;

namespace DataLayer.Repositories.DefendWithParameters
{
    public class CustomerRepository : DataLayer.Repositories.Attack.CustomerRepository, ICustomerRepository
    {
        
    }
}