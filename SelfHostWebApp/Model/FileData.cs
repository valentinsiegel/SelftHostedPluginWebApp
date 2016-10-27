using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfHostWebApp.Model
{
    public class FileData
    {
        public string Name { get; set; }
        public byte[] Data { get; set; }

        public FileData(string name, byte[] data)
        {
            Name = name;
            Data = data;
        }

    }
}
