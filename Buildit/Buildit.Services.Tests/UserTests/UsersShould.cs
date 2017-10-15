using Buildit.Data.Models;
using NUnit.Framework;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buildit.Services.Tests.UserTests
{
    [TestFixture]
    public class UsersShould
    {
        [Test]
        public void InheritIdentityUser()
        {
            Assert.IsTrue(typeof(User).IsSubclassOf(typeof(IdentityUser)));
        }
    }
}
