using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VendingMachine.Models
{
    public class Categorie
    {
        public int ID { get; set; }

        public string Nume { get; set; }

        public List<Product> Produse { get; set; }

          
    }
}

