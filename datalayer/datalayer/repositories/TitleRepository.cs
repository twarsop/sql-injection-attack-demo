using System.Collections.Generic;
using System.IO;
using datalayer.interfaces;
using datalayer.models;
using Npgsql;

namespace datalayer.repositories
{
    public class TitleRepository : ITitleRepository
    {
        public List<Title> GetAll()
        {
            List<Title> titles = new List<Title>();

            var conn = new NpgsqlConnection("Host=localhost;Username=postgres;Password=postgres;Database=sqlinjectionattackdemo");
            conn.Open();

            using (var command = new NpgsqlCommand("SELECT id, name FROM public.titles", conn))
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