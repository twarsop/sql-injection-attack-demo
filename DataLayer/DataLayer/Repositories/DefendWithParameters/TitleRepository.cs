using System.Collections.Generic;
using System.IO;
using DataLayer.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using DataLayer.Models;
using Npgsql;

namespace DataLayer.Repositories.DefendWithParameters
{
    public class TitleRepository : DataLayer.Repositories.Attack.TitleRepository, ITitleRepository
    {
        
    }
}