using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VendingMachine.Models
{
    public class ProductViewModel
    {
        public int ID { get; set; }
        public string Nume { get; set; }
        public double Pret { get; set; }
        public int Cantitate { get; set; }
        public string CategoryName { get; set; }
    }
}
