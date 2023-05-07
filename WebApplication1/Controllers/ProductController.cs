using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Context;

namespace WebApplication1.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        curdappEntities dbObj = new curdappEntities();
        public ActionResult Product()
        {
                return View();
        }

        [HttpPost]
        public ActionResult AddProduct(TableProduct model)
        {
            if (ModelState.IsValid)
            {
                
                TableProduct obj = new TableProduct();
                obj.ProductId = model.ProductId;
                obj.ProductName = model.ProductName;
                obj.CategoryId = model.CategoryId;
                obj.CategoryName = model.CategoryName;

                if (model.ProductId == 0)
                {
                    dbObj.TableProducts.AddOrUpdate(obj);
                    dbObj.SaveChanges();
                }
                else
                {
                    dbObj.Entry(obj).State=EntityState.Modified;
                    dbObj.SaveChanges();
                }
            }
            ModelState.Clear();
            return View("Product");
        }

        public ActionResult ProductList() 
        { 
            var res=dbObj.TableProducts.ToList();
            
            return View(res);
        
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var del= dbObj.TableProducts.Where(x=>x.ProductId==id).First();
            dbObj.TableProducts.Remove(del);
            dbObj.SaveChanges();
            var res = dbObj.TableProducts.ToList();
            return View("ProductList", res);
        }

        public ActionResult Edit(int id)
        {
            var edit = dbObj.TableProducts.Where(x => x.ProductId == id).First();
            if (edit != null)
            {
                return View("Product",edit);
            }
            else
                return View("Product");
        }
        

    }
}