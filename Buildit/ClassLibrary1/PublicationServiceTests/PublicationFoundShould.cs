using Buildit.Data.Contracts;
using Buildit.Data.Models;
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
    public class PublicationFoundShould
    {
        [TestMethod]
        public void ReturnTrueWhenPublicationIsFound()
        {

            var title = "Title";
            var mockedData = new Mock<IBuilditData>();
            var foundPubl = new Publication() { Title = title };
            var Publications = new List<Publication>
            {
                 new Mock<Publication>().Object,
                 foundPubl
            }.AsQueryable();

            mockedData.Setup(x => x.Publications.All).Returns(Publications);

            var service = new PublicationService(mockedData.Object);

            var exists = service.PublicationFound(title);
            Assert.IsTrue(exists);
        }

        [TestMethod]
        public void ReturnFalseWhenPublicationIsNotFound()
        {
            var mockedData = new Mock<IBuilditData>();
            var Publications = new List<Publication>
            {
                 new Mock<Publication>().Object,
                 new Mock<Publication>().Object,
                 new Mock<Publication>().Object
            }.AsQueryable();

            mockedData.Setup(x => x.Publications.All).Returns(Publications);

            var service = new PublicationService(mockedData.Object);

            var exists = service.PublicationFound("Title");
            Assert.IsFalse(exists);
        }
    }
}
