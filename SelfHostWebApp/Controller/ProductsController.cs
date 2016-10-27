using SelfHostWebApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace SelfHostWebApp.Controller
{
    [Export]
    public class ProductsController : ApiController
    {
        [Import]
        public IMyService MyService { get; set; }

        public string getProduct(int id)
        {
            MyService.Helloworld();
            return "hello";
        }
    }
}
