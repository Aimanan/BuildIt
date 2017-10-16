using Buildit.Data.Contracts;
using Buildit.Data.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buildit.Services.TestsMsTest
{

    [TestClass]
    public class PublicationTypeServiceShould
    {
        [TestMethod]
        public void ThrowArgumentNullExceptionWhenDataIsNull()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new PublicationTypeService(null));
        }

        [TestMethod]
        public void NotThrowExceptionIfTheParametersAreNotNullt()
        {
            var mockedData = new Mock<IBuilditData>();
            var PublicationTypeService = new PublicationTypeService(mockedData.Object);

            Assert.IsNotNull(PublicationTypeService);
        }

        [TestMethod]
        public void ReturnCorrectListWhenGetPublicationTypesIsInvoked()
        {
            var mockedData = new Mock<IBuilditData>();
            var publ = new List<PublicationType>()
            {
                new Mock<PublicationType>().Object,
                new Mock<PublicationType>().Object,
            }.AsQueryable();

            mockedData.Setup(x => x.PublicationTypes.All).Returns(publ);

            var service = new PublicationTypeService(mockedData.Object);

            var result = service.GetPublicationTypes();

            Assert.AreEqual(publ.ToList().Count(), result.Count());
        }
    }
}
