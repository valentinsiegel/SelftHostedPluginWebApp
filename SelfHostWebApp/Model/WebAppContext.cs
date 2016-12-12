using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfHostWebApp.Model
{
    public class WebAppContext : IdentityDbContext<MyUser>
    {
    }
}
