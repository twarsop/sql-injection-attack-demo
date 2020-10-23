using System.Collections.Generic;
using System.IO;
using datalayer.interfaces;
using datalayer.models;
using Npgsql;

namespace datalayer.repositories
{
    public class TitleRepository : ITitleRepository
    {
        private readonly string _connstr;
        private readonly string _tablestr;

        public TitleRepository()
        {
            _connstr = "Host=localhost;Username=postgres;Password=postgres;Database=sqlinjectionattackdemo";
            _tablestr = "public.titles";
        }

        public List<Title> GetAll()
        {
            List<Title> titles = new List<Title>();

            var conn = new NpgsqlConnection(_connstr);
            conn.Open();

            using (var command = new NpgsqlCommand("SELECT id, name FROM " + _tablestr, conn))
            {
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    titles.Add(new Title { 
                        Id = System.Int32.Parse(reader["id"].ToString()), 
                        Name = reader["name"].ToString() 
                    });
                }
            }

            conn.Close();

            return titles;
        }
    }
}