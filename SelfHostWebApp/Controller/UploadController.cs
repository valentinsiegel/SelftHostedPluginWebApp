using Common;
using SelfHostWebApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace SelfHostWebApp.Controller
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class UploadController : ApiController
    {
        [ImportMany]
        public IEnumerable<ILogger> Loggers { get; set; }

        [HttpGet]
        public string[] List()
        {
            return Directory.GetFiles(Environment.CurrentDirectory + @"\plugins").Select(_ => Path.GetFileName(_)).ToArray();
        }

        [Authorize] 
        [HttpPost]
        public void Post(FileData file)
        {
            foreach (var logger in Loggers)
                logger.Log("ADD PLUGIN!");

            File.WriteAllBytes(@"uploads\\" + file.Name, file.Data);
        }

        [Authorize]
        [HttpDelete]
        public void Delete(string dllName)
        {
            foreach (string fullPath in Directory.GetFiles(Environment.CurrentDirectory + @"\uploads"))
            {
                if (dllName == Path.GetFileName(dllName))
                {
                    File.Delete(fullPath);
                }
            }
            //Directory.GetFiles(Environment.CurrentDirectory + @"\uploads").ToList().ElementAt(index);
        }
    }
}
