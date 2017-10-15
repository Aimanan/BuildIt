using Buildit.Common.Providers;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Buildit.Web.Tests2.Common.Providers
{
    [TestFixture]
    public class ServerProviderConstructorShould
    {
        [Test]
        public void ThrowArgumentNullExceptionWithCorrectMessage_WhenHttpContextBaseIsNull()
        {
            Assert.That(() => new ServerProvider(null), Throws.ArgumentNullException.With.Message.Contains("httpContext"));
        }

        [Test]
        public void NotThrow_WhenArgumentsAreNotNull()
        {
            var mockedContext = new Mock<HttpContextBase>();
            Assert.DoesNotThrow(() => new ServerProvider(mockedContext.Object));
        }
    }
}
