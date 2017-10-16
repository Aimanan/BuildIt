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
    public class GetPublicationsCountShould
    {
        [DataRow(1, "1")]
        [DataRow(2, "x")]
        public void ReturnCorrectcountWhenFilterBySearchWord(int expectedCount, string searchWord)
        {
            var mockedData = new Mock<IBuilditData>();
            var PublicationsData = new List<Publication>
            {
                new Publication() { Title = "some-digits-1232", Author = "" },
                new Publication() { Title = "", Author = "xxxx" },
                new Publication() { Title = "none", Author = "pesho" },
            }.AsQueryable();

            mockedData.Setup(x => x.Publications.All).Returns(PublicationsData);

            var service = new PublicationService(mockedData.Object);

            var count = service.GetPublicationsCount(searchWord, null);

            Assert.AreEqual(expectedCount, count);
        }

        [DataRow(1, 1)]
        [DataRow(2, 2, 4)]
        public void ReturnCorrectCountWhenFilterByTypes(int expectedCount, params int[] PublicationTypeIds)
        {
            var mockedData = new Mock<IBuilditData>();
            var PublicationsData = new List<Publication>
            {
                new Publication() { PublicationTypeId = 1 },
                new Publication() { PublicationTypeId = 2 },
                new Publication() { PublicationTypeId = 8 },
                new Publication() { PublicationTypeId = 6 }
            }.AsQueryable();

            mockedData.Setup(x => x.Publications.All).Returns(PublicationsData);
            var service = new PublicationService(mockedData.Object);

            var count = service.GetPublicationsCount(null, PublicationTypeIds);

            Assert.AreEqual(expectedCount, count);
        }

        [TestMethod]
        public void ReturnCorrectCountWhenFilterBySearchWordAndTypes()
        {
            var searchWord = "xxx";
            var PublicationTypeIds = new int[] { 1, 3 };
            var mockedData = new Mock<IBuilditData>();
            var PublicationsData = new List<Publication>
            {
                new Publication() { Title = "", Author = "xxx-fsafjsbjbfksfvbk", PublicationTypeId = 1 },
                new Publication() { Title = "xxx", Author = "", PublicationTypeId = 3 },
                new Publication() { Title = "", Author = "", PublicationTypeId = 3 },
                new Publication() { Title = "xxx", Author = "", PublicationTypeId = 4 }
            }.AsQueryable();

            mockedData.Setup(x => x.Publications.All).Returns(PublicationsData);

            var service = new PublicationService(mockedData.Object);

            var count = service.GetPublicationsCount(searchWord, PublicationTypeIds);

            Assert.AreEqual(2, count);
        }

        [TestMethod]
        public void ReturnCountOfAllElementsWhenFiltersAreNull()
        {
            var mockedData = new Mock<IBuilditData>();

            var PublicationsData = new List<Publication>
            {
                new Mock<Publication>().Object,
                new Mock<Publication>().Object,
                new Mock<Publication>().Object,
            }.AsQueryable();

            mockedData.Setup(x => x.Publications.All).Returns(PublicationsData);

            var service = new PublicationService(mockedData.Object);

            var count = service.GetPublicationsCount(null, null);

            Assert.AreEqual(PublicationsData.Count(), count);
        }
    }
}
