using Common;
using SelfHostWebApp.Model;
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
    public class UploadController : ApiController
    {
        [ImportMany]
        public IEnumerable<ILogger> Loggers { get; set; }

        public void Post(FileData file)
        {
            foreach (var logger in Loggers)
                logger.Log("ADD PLUGIN!");

            File.WriteAllBytes(@"plugins\\" + file.Name, file.Data);    
        }
    }
}
