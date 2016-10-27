using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Dependencies;

namespace SelfHostWebApp.Core
{
    class DependencyResolver : IDependencyResolver
    {
        private CompositionContainer _container;

        public DependencyResolver(CompositionContainer container)
        {
            _container = container;
        }

        public IDependencyScope BeginScope()
        {
            return this;
        }

        public void Dispose()
        { }

        public object GetService(Type serviceType)
        {
            try
            {
                Debug.WriteLine($"resolve {serviceType.FullName}");
                var export = _container.GetExports(serviceType, null, "").FirstOrDefault();
                if (export != null)
                    return export.Value;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _container.GetExports(serviceType, null, "").Select(_ => _.Value);
        }
    }
}
