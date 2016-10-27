using SelfHostWebApp.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace SelfHostWebApp.Controller
{
    public class UploadController : ApiController
    {
        public void Post(FileData file)
        {
            File.WriteAllBytes(file.Name, file.Data);
        }
    }
}
