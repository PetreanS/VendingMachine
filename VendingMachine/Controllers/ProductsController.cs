using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendingMachine.Data;
using VendingMachine.Models;

namespace VendingMachine.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ProductsController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Product> productsFromDb = _db.Products.Include(x=>x.Categorie).ToList();

            List<ProductViewModel> productViewModel = new List<ProductViewModel>(); 
            foreach (var product in productsFromDb)
            {
                var viewModel = new ProductViewModel()
                {
                    ID = product.ID,
                    Nume = product.Nume,
                    Pret = product.Pret,
                    CategoryName = product.Categorie.Nume
                };
                productViewModel.Add(viewModel);
            }
            return View(productViewModel); 


        }
        public IActionResult Search(string searchValue)
        {
            IEnumerable<Product> productsWithCategories = _db.Products.Include(x => x.Categorie).ToList();

            var produseCautate = productsWithCategories.Where(produs => produs.Categorie.Nume == searchValue).Select(product => new ProductViewModel() {
                ID = product.ID,
                Nume = product.Nume,
                Pret = product.Pret, 
                CategoryName = product.Categorie.Nume
            });


            return View("Index", produseCautate);
        }

        //Sa fac search ul cu un where

        //GET Action
        public IActionResult Create()
        {
            return View();
        }


        //POST Action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AdaugareProdus obj)
        {
            var produsDeAdaugat = new Product()
            {
                Nume = obj.Nume,
                Cantitate =obj.Cantitate,               
                Pret=obj.Pret
            };

            var categorieProdus = _db.Categories.FirstOrDefault(x => x.Nume == obj.CategoriName);
            {
                if (categorieProdus == null)
                {
                    produsDeAdaugat.Categorie = new Categorie()
                    {
                        Nume = obj.CategoriName
                    };
                }
                else
                {
                    produsDeAdaugat.Categorie = categorieProdus;
                }
            }

            _db.Products.Add(produsDeAdaugat);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        //GET Action
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var productFromDb = _db.Products.Find(id);

            if (productFromDb == null)
            {
                return NotFound();
            }

            return View(productFromDb);
        }


        //POST Action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product obj) 
        {
            if (ModelState.IsValid)                    
            {
                _db.Products.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Product edited successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }



        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var productFromDb = _db.Products.Find(id);
            if (productFromDb == null)
            {
                return NotFound();
            }

            return View(productFromDb);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _db.Products.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _db.Products.Remove(obj);
                _db.SaveChanges();
                TempData["success"] = "Product deleted successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult VizualizareDetalii(int? id)
        {
            var productFromDb = _db.Products.Include(x => x.Categorie).FirstOrDefault(x=>x.ID==id);
                 
           

            var productDetails = new ProductViewModel()
            {
                ID = productFromDb.ID,
                Nume = productFromDb.Nume,
                Pret = productFromDb.Pret,
                CategoryName = productFromDb.Categorie.Nume
            };

            return View(productDetails);
        }
       

    }
}

//un input care cauta dupa categorie  . in lista de produse