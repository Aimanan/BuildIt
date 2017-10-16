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
    public class SearchPublicationsShould
    {
        [DataRow("1")]
        [DataRow("aaa")]
        public void FilterBySearchWordRegardingTitleAndAuthor(string searchWord)
        {
            var mockedData = new Mock<IBuilditData>();
            var PublicationsData = new List<Publication>
            {
                new Publication() { Title = "sdsggdssdgg1111", Author = "" },
                new Publication() { Title = "", Author = "aaaaaaaaaaaa" },
                new Publication() { Title = "fggfs", Author = "gssdsgds" },
            }.AsQueryable();

            mockedData.Setup(x => x.Publications.All).Returns(PublicationsData);

            var service = new PublicationService(mockedData.Object);

            var publ = service.SearchPublications(searchWord, null, null, 1, 10);

            var expected = PublicationsData.Where(x => x.Title.Contains(searchWord) || x.Author.Contains(searchWord)).ToList();
            Assert.Equals(expected, publ);
        }

        [DataRow(1)]
        [DataRow(2, 4)]
        public void FilterByTypes(params int[] PublicationTypeIds)
        {
            var mockedData = new Mock<IBuilditData>();
            var PublicationsData = new List<Publication>
            {
                new Publication() { PublicationTypeId = 1 },
                new Publication() { PublicationTypeId = 2 },
                new Publication() { PublicationTypeId = 4 },
                new Publication() { PublicationTypeId = 55 }
            }.AsQueryable();

            mockedData.Setup(x => x.Publications.All).Returns(PublicationsData);

            var service = new PublicationService(mockedData.Object);

            var publ = service.SearchPublications(null, PublicationTypeIds, null, 1, 10);

            var expected = PublicationsData.Where(x => PublicationTypeIds.Contains(x.PublicationTypeId)).ToList();
            //Assert.Equals(expected, publ);
            Assert.IsTrue(expected.SequenceEqual(publ));
        }

        [TestMethod]
        public void FilterBySearchWordAndType()
        {
            var searchWord = "abc";
            var PublicationTypeIds = new int[] { 1, 3 };
            var mockedData = new Mock<IBuilditData>();

            var PublicationsData = new List<Publication>
            {
                new Publication() { Title = "", Author = "abc-2552", PublicationTypeId = 1 },
                new Publication() { Title = "abc", Author = "jhjhhh", PublicationTypeId = 3 },
                new Publication() { Title = "", Author = "jhjhjhjh", PublicationTypeId = 3 },
                new Publication() { Title = "abc", Author = "1111111111111", PublicationTypeId = 4 }
            }.AsQueryable();

            mockedData.Setup(x => x.Publications.All).Returns(PublicationsData);

            var service = new PublicationService(mockedData.Object);
            var publ = service.SearchPublications(searchWord, PublicationTypeIds, null, 1, 10);
            var expected = PublicationsData
                .Where(x => x.Title.Contains(searchWord) || x.Author.Contains(searchWord))
                .Where(x => PublicationTypeIds.Contains(x.PublicationTypeId)).ToList();

            Assert.IsTrue(expected.SequenceEqual(publ));
        }

        [TestMethod]
        public void ReturnWholeCollectionВhenFiltersAreNull()
        {
            var mockedData = new Mock<IBuilditData>();
            var PublicationsData = new List<Publication>
            {
                new Mock<Publication>().Object,
                new Mock<Publication>().Object,
                new Mock<Publication>().Object,
                new Mock<Publication>().Object,
            }.AsQueryable();

            mockedData.Setup(x => x.Publications.All).Returns(PublicationsData);

            var service = new PublicationService(mockedData.Object);
            var publ = service.SearchPublications(null, null, null, 1, 10);
            Assert.IsTrue(PublicationsData.ToList().SequenceEqual(publ));
        }

        [TestMethod]
        public void OrderPublicationsInDescendingWhenYearIsPassed()
        {
            var mockedData = new Mock<IBuilditData>();
            var PublicationsData = new List<Publication>
            {
                new Publication() { PublishedOn = new DateTime(2009,10,11) },
                new Publication() { PublishedOn = new DateTime(2013,10,11) },
                new Publication() { PublishedOn = new DateTime(2018,10,11) },
                new Publication() { PublishedOn = new DateTime(2022,10,11) }
            }.AsQueryable();

            mockedData.Setup(x => x.Publications.All).Returns(PublicationsData);

            var service = new PublicationService(mockedData.Object);
            var publ = service.SearchPublications(null, null, "year", 1, 10);
            var expected = PublicationsData.OrderByDescending(x => x.PublishedOn.Year).ToList();
            Assert.IsFalse(expected.SequenceEqual(publ));
        }

        [TestMethod]
        public void ReturnOrderedCollection()
        {
            var mockedData = new Mock<IBuilditData>();
            var PublicationsData = new List<Publication>
            {
                new Publication() { Author = "111" },
                new Publication() { Author = "222" },
                new Publication() { Author = "333" },
                new Publication() { Author = "555" }
            }.AsQueryable();

            mockedData.Setup(x => x.Publications.All).Returns(PublicationsData);

            var service = new PublicationService(mockedData.Object);
            var publ = service.SearchPublications(null, null, "author", 1, 10);
            var expected = PublicationsData.OrderBy(x => x.Author).ToList();
            Assert.IsTrue(expected.SequenceEqual(publ));
        }

        [TestMethod]
        public void OrderByTitleWhenArgumentIsIncorrect()
        {
            var mockedData = new Mock<IBuilditData>();
            var PublicationsData = new List<Publication>
            {
                new Publication() { Author = "111" },
                new Publication() { Author = "222" },
                new Publication() { Author = "333" },
                new Publication() { Author = "555" }
            }.AsQueryable();

            mockedData.Setup(x => x.Publications.All).Returns(PublicationsData);

            var service = new PublicationService(mockedData.Object);
            var publ = service.SearchPublications(null, null, "non-existing prop", 1, 10);
            var expected = PublicationsData.OrderBy(x => x.Title).ToList();
            //Assert.AreEqual(expected, publ);
            Assert.IsTrue(expected.SequenceEqual(publ));
        }

        [TestMethod]
        public void GetCorrectNumberOfElements()
        {
            int page = 3;
            int perPage = 2;
            var mockedData = new Mock<IBuilditData>();
            var mockedLastButOnePublication = new Mock<Publication>();
            var mockedLastPublication = new Mock<Publication>();
            var PublicationsData = new List<Publication>
            {
                new Mock<Publication>().Object,
                new Mock<Publication>().Object,
                new Mock<Publication>().Object,
                new Mock<Publication>().Object,
                new Mock<Publication>().Object
            }.AsQueryable();

            mockedData.Setup(x => x.Publications.All).Returns(PublicationsData);

            var service = new PublicationService(mockedData.Object);
            var publ = service.SearchPublications(null, null, null, page, perPage);
            var expected = PublicationsData.OrderBy(x => x.Title).Skip(4).Take(perPage);
            Assert.IsTrue(expected.SequenceEqual(publ));
        }

        [TestMethod]
        public void OrderAndFilterCorrectly()
        {
            var searchWord = "b";
            var PublicationTypeIds = new int[] { 2, 3 };
            var mockedData = new Mock<IBuilditData>();
            var PublicationsData = new List<Publication>
            {
                new Publication() { Title = "bbb", PublicationTypeId = 2, Author = "" },
                new Publication() { Title = "bee", PublicationTypeId = 3, Author = "" },
                new Publication() { Title = "eee", PublicationTypeId = 1, Author = "" },
                new Publication() { Title = "ggg", PublicationTypeId = 1, Author = "" }
            }.AsQueryable();

            mockedData.Setup(x => x.Publications.All).Returns(PublicationsData);

            var service = new PublicationService(mockedData.Object);
            var publ = service.SearchPublications("b", PublicationTypeIds, "title", 1, 10);
            var expected = PublicationsData
                .Where(x => x.Title.Contains(searchWord) || x.Author.Contains(searchWord))
                .Where(x => PublicationTypeIds.Contains(x.PublicationTypeId))
                .OrderBy(x => x.Title)
                .ToList();
            Assert.IsTrue(expected.SequenceEqual(publ));
        }

        [TestMethod]
        public void SkipNumberOfElements()
        {
            int page = 2;
            int perPage = 3;
            var mockedData = new Mock<IBuilditData>();
            var mockedLastButOnePublication = new Mock<Publication>();
            var mockedLastPublication = new Mock<Publication>();
            var PublicationsData = new List<Publication>
            {
                new Mock<Publication>().Object,
                new Mock<Publication>().Object,
                new Mock<Publication>().Object,
                new Mock<Publication>().Object,
                new Mock<Publication>().Object
            }.AsQueryable();

            mockedData.Setup(x => x.Publications.All).Returns(PublicationsData);

            var service = new PublicationService(mockedData.Object);
            var publ = service.SearchPublications(null, null, null, page, perPage);
            var expected = PublicationsData.OrderBy(x => x.Title).Skip(3).Take(perPage);
            Assert.IsTrue(expected.SequenceEqual(publ));
        }

    }
}
