using System.Collections.Generic;
using System.IO;
using datalayer.interfaces;
using datalayer.models;

namespace datalayer.repositories
{
    public class TitleRepository : ITitleRepository
    {
        public List<Title> GetAll()
        {
            List<Title> titles = new List<Title>();
            using (StreamReader sr = new StreamReader(@"C:\Users\t_war\Documents\projects\code\sql-injection-attack-demo\datalayer\datalayer\data\titles.csv"))
            {
                // read the header
                sr.ReadLine();

                while (sr.Peek() != -1)
                {
                    string[] splitLine = sr.ReadLine().Split(new[] { ","}, System.StringSplitOptions.RemoveEmptyEntries);
                    int id = System.Convert.ToInt32(splitLine[0]);
                    titles.Add(new Title { Id = id, Name = splitLine[1] });
                }
            }

            return titles;
        }
    }
}