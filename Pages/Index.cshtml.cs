using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Intwenty.DataClient;
using Intwenty.DataClient.Reflection;
using Microsoft.AspNetCore.Mvc;

namespace MiniCore.Pages
{

    public class IndexModel : PageModel
    {



        public IndexModel()
        {
           
        }

        public void OnGet()
        {
           var dbclient = new Connection(DBMS.SQLite, "Data Source=wwwroot/sqlite/MiniCore.db");
           if (!dbclient.TableExists("Product"))
           {
               dbclient.CreateTable<Product>();
               dbclient.CreateTable<ProductDependency>();

               var id = dbclient.InsertEntity(new Product{ Name="Mini Core", Dependencies=new List<ProductDependency>() });
               dbclient.InsertEntity(new ProductDependency(){ Name="Net8", Version="3.1", Purpose="The server side platform, razor, routing etc", ProductId=id });
               dbclient.InsertEntity(new ProductDependency(){ Name="Vue.Js", Version="3", Purpose="Data binding, reactivity etc... client side", ProductId=id });
               dbclient.InsertEntity(new ProductDependency(){ Name="Bootstrap", Version="4", Purpose="A fast way to a nice looking UI", ProductId=id });
               dbclient.InsertEntity(new ProductDependency(){ Name="Intwenty Data Client", Version="2.0.0", Purpose="Quick database access to common db engines", ProductId=id });

           }

           dbclient.Close();

        }

        public IActionResult OnGetList()
        {
             var dbclient = new Connection(DBMS.SQLite, "Data Source=wwwroot/sqlite/MiniCore.db");
             var p = dbclient.GetEntities<Product>().FirstOrDefault();
             if (p.Id>0)
             {
                 var deps = dbclient.GetEntities<ProductDependency>(string.Format("select * from ProductDependency where Productid={0}",p.Id));
                 p.Dependencies=deps;
             }
             dbclient.Close();
            return new JsonResult(p);
        }

      

       
    }

    [DbTablePrimaryKey("Id")]
    [DbTableName("Product")]
    public class Product
    {
        [AutoIncrement]
        public int Id {get; set;}

        public string Name {get; set;}

        [Ignore]
        public List<ProductDependency> Dependencies {get; set;}

    }

    [DbTablePrimaryKey("Id")]
    [DbTableName("ProductDependency")]
    public class ProductDependency
    {
        [AutoIncrement]
        public int Id {get; set;}

        public int ProductId {get; set;}

        public string Name {get; set;}
        public string Version {get; set;}
        public string Purpose {get; set;}


    }
}