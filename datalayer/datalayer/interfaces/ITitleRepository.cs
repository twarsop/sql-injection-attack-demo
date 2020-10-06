using System.Collections.Generic;
using datalayer.models;

namespace datalayer.interfaces
{
    public interface ITitleRepository
    {
        List<Title> GetAll();
    }    
}