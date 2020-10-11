using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercicio_XML.Model
{
    [Serializable()]
    public class Produto
    {
        public int cod { get; set; }
        public string nome { get; set; }
        public float valor { get; set; }
        public bool disponivel { get; set; }
    }
}
