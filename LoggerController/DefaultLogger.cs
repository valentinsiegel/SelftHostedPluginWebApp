using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggerController
{
    [Export(typeof(ILogger))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class DefaultLogger : ILogger
    {
        public void Log(string msg)
        {
            Console.WriteLine(msg);
        }
    }
}
