using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AtNelson.Models
{
    public class Pessoa
    {
        public int Id { get; set; }
        public String Nome { get; set; }
        public String Sobrenome { get; set; }
        public DateTime DataNascimento { get; set; }
    }
}
