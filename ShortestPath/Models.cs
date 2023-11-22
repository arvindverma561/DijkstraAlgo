using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Nodes
    {
        public string Name { get; set; }
        public Dictionary<Nodes, int> Neighbors { get; set; }

        public Nodes(string name)
        {
            Name = name;
            Neighbors = new Dictionary<Nodes, int>();
        }
    }

    public class ShortestPathData
    {
        public List<string> NodeNames { get; set; }
        public int Distance { get; set; }
    }
}
