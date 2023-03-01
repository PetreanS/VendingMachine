using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VendingMachine.Models
{
    public class Product
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Nume { get; set; }
        [DisplayName("Cantitate")]
        [Range(1, 50, ErrorMessage = "Display Quantity must be between 1 and 50")]
        public int Cantitate { get; set; }

        public double Pret { get; set; }

        public int CategorieID { get; set; }
        public Categorie Categorie { get; set; }


    }
}
