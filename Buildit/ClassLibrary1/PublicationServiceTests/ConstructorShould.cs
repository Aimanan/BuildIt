using Buildit.Data.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buildit.Services.TestsMsTest.PublicationServiceTests
{
    [TestClass]
    public class ConstructorShould
    {
        [TestMethod]
        public void ThrowArgumentNullExceptionWhenNullData()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new PublicationService(null));
        }

        [TestMethod]
        public void NotThrowWhenDataIsNotNull()
        {
            var mockedData = new Mock<IBuilditData>();
            var publService = new PublicationService(mockedData.Object);

            Assert.IsNotNull(publService);
        }
    }
}
