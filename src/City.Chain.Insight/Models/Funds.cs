using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace City.Chain.Insight.Models
{
    public class Funds
    {
        public Funds()
        {
            Data = new List<Fund>();
        }

        public List<Fund> Data { get; set; }
    }
}
