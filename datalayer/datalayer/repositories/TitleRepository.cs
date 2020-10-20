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

            using (var command = new NpgsqlCommand("SELECT Id, Name FROM public.Titles", conn))
            {
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var id = System.Int32.Parse(reader["Id"].ToString());
                    var name = reader["Name"].ToString();
                    titles.Add(new Title { Id = id, Name = name });
                }
            }

            conn.Close();

            return titles;
        }
    }
}