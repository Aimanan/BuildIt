using Buildit.Data.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Buildit.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buildit.Services.TestsMsTest.PublicationServiceTests
{
    [TestClass]
    public class GetByIdShould
    {
        [DataRow(0)]
        [DataRow(500)]
        public void ReturnCorrectPublication(int id)
        {
            var mockedData = new Mock<IBuilditData>();
            var expectedPublication = new Publication();
            expectedPublication.Id = id;
            var Publications = new List<Publication>
            {
                 new Mock<Publication>().Object,
                 new Mock<Publication>().Object,
                 expectedPublication,
            }.AsQueryable();

            mockedData.Setup(x => x.Publications.All).Returns(Publications);

            var service = new PublicationService(mockedData.Object);
            var Publication = service.GetById(id);

            Assert.AreEqual(expectedPublication, Publication);
        }

        [DataRow(0)]
        [DataRow(500)]
        public void ReturnNullWhenPublicationIsNotFound(int id)
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
            var Publication = service.GetById(id);

            Assert.IsNull(Publication);
        }
    }
}
