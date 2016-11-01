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
                var export = _container.GetExports(serviceType, null, null).FirstOrDefault();
                if (export != null)
                {
                    Console.WriteLine($"Resolved {serviceType.FullName}");
                    return export.Value;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to resolve {0}. {1}", serviceType.FullName, e.Message);
            }
            return null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                Debug.WriteLine($"resolve {serviceType.FullName}");
                var exports = _container.GetExports(serviceType, null, null);
                if (exports != null)
                {
                    Console.WriteLine("Resolved {0} objects of type {1}", exports.Count(), serviceType.FullName);
                    return exports.Select(_ => _.Value);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to resolve {0}. {1}", serviceType.FullName, e.Message);
            }
            return null;
        }
    }
}
