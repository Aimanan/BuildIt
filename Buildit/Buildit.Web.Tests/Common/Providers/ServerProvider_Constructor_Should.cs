using Buildit.Common.Providers;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Buildit.Web.Tests.Common.Providers
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenParameterIsNull()
        {
            Assert.That(() => new ServerProvider(null),
                Throws.ArgumentNullException.With.Message.Contains("HttpContext"));
        }

        [Test]
        public void NotThrow_WhenArgumentsAreNotNull()
        {
            var mockedContext = new Mock<HttpContextBase>();

            Assert.DoesNotThrow(() => new ServerProvider(mockedContext.Object));
        }
    }
}
