using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggerController
{
    [ExportPlugin("p1", "1.0.0")]
    public class Plugin : IPlugin
    {
        public void Start()
        {
        }
    }
}
