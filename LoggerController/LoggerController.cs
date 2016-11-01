using Common;
using Commonn;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace LoggerController
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class LoggerController : ApiController
    {
        //private IEnumerable<Lazy<ILogger, ExportPluginAttribute>> _loggers;

        //[ImportingConstructor]
        //public LoggerController(IEnumerable<Lazy<ILogger, ExportPluginAttribute>> Loggers)
        //{

        //    _loggers = Loggers;

        //    foreach (Lazy<ILogger, ExportPluginAttribute> logger in _loggers)
        //    {
        //        Console.WriteLine("Plugin name : {0}, Plugin Version {1}", logger.Metadata.Name, logger.Metadata.Version);
        //        logger.Value.Start();
        //    }
        //}
        [ImportMany(AllowRecomposition = true)]
        public IEnumerable<Lazy<ILogger>> Loggers { get; set; }

        [HttpGet]
        public void Get()
        {
            foreach (var logger in Loggers)
            {
                logger.Value.Log("hello world");
            }
        }
    }
}
