using Commonn;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{   
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ExportPluginAttribute : ExportAttribute, IPluginMetadata
    {
        public string Name { get; }

        public string Version { get; }

        public ExportPluginAttribute(string name, string version) : base(typeof(IPlugin))
        {
            Name = name;
            Version = version;
        }
    }
}
