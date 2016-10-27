using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfHostWebApp.Services
{
    public interface IMyService
    {
        void Helloworld();
    }

    [Export(typeof(IMyService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MyService : IMyService
    {
        public void Helloworld()
        {
            Debug.WriteLine("helloword");
        }
    }
}
