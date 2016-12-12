using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace SelfHostWebApp.Controller
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class DeleteController : ApiController
    {
        [HttpDelete]
        public void Delete(int index)
        { 
            //foreach (string fullPath in Directory.GetFiles(Environment.CurrentDirectory + @"\uploads"))
            //{
            //    if (dllName == Path.GetFileName(dllName))
            //    {
            //        File.Delete(fullPath);
            //    }
            //}
            Directory.GetFiles(Environment.CurrentDirectory + @"\uploads").ToList().ElementAt(index);
        }
    }
}
