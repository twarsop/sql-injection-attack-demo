using System.Collections.Generic;
using DataLayer.Models;

namespace DataLayer.Interfaces
{
    public interface ITitleRepository
    {
        List<Title> GetAll();
    }    
}