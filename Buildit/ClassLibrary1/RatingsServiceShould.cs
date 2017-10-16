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
    public class RatingsServiceShould
    {
        [TestMethod]
        public void ThrowArgumentNullExceptionWhenDataIsNull()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new RatingsService(null));
        }


        [TestMethod]
        public void NotThrowWhenDataIsNotNull()
        {
            var mockedData = new Mock<IBuilditData>();
            var ratingS= new RatingsService(mockedData.Object);

            Assert.IsNotNull(ratingS);
        }

        [DataRow(4, "aaa", 10)]
        [DataRow(60, "user", 2)]
        public void GetRartingReturnCorrectRatingWhenFound(int PublicationId, string userId, int expectedValue)
        {
            var mockedData = new Mock<IBuilditData>();

            var rating1Match = new Rating() { PublicationId = 4, UserId = "aaa", Value = 10 };
            var rating2Match = new Rating() { PublicationId = 60, UserId = "user", Value = 2 };
            var ratingNotMatch = new Rating() { PublicationId = 1, UserId = "aaa" };
            var ratings = new List<Rating>()
            {
                rating1Match,
                rating2Match,
                ratingNotMatch
            }.AsQueryable();

            mockedData.Setup(x => x.Ratings.All).Returns(ratings);
            mockedData.Setup(x => x.SaveChanges()).Verifiable();

            var service = new RatingsService(mockedData.Object);
            var rating = service.GetRating(PublicationId, userId);
            Assert.AreEqual(expectedValue, rating);
        }

        [TestMethod]
        public void GetRartingReturnZeroWhenRatingNotFound()
        {
            var mockedData = new Mock<IBuilditData>();

            var ratings = new List<Rating>()
            {
                new Mock<Rating>().Object,
                new Mock<Rating>().Object,
                new Mock<Rating>().Object
            }.AsQueryable();

            mockedData.Setup(x => x.Ratings.All).Returns(ratings);

            var service = new RatingsService(mockedData.Object);
            var rating = service.GetRating(1, "str");

            Assert.AreEqual(0, rating);
        }

        [TestMethod]
        public void ChangeRatingValueWhenRatingFound()
        {
            var userId = "user-id-12424";
            var PublicationId = 24147;
            int rateValue = 5;
            var mockedData = new Mock<IBuilditData>();
            var rating1 = new Rating() { UserId = userId, PublicationId = PublicationId };
            var rating2 = new Rating() { UserId = userId, PublicationId = 2345 };
            var ratings = new List<Rating>()
            {
                rating1,
                rating2
            }.AsQueryable();

            mockedData.Setup(x => x.Ratings.All).Returns(ratings);

            var service = new RatingsService(mockedData.Object);
            service.RatePublication(PublicationId, userId, rateValue);
            Assert.AreEqual(rateValue, rating1.Value);
        }

        [TestMethod]
        public void RatePublicationCreateNewRatingWithCorrectPropertiesWhenRatingNotFound()
        {
            var mockedData = new Mock<IBuilditData>();
            var rating1 = new Rating() { UserId = "id1", PublicationId = 1234 };
            var rating2 = new Rating() { UserId = "id2", PublicationId = 2222 };
            var ratings = new List<Rating>()
            {
                rating1,
                rating2
            }.AsQueryable();

            Rating addedRating = null;
            mockedData.Setup(x => x.Ratings.All).Returns(ratings);
            mockedData.Setup(x => x.Ratings.Add(It.IsAny<Rating>()))
                .Callback((Rating r) => addedRating = r);

            var service = new RatingsService(mockedData.Object);

            service.RatePublication(1, "id", 5);

            mockedData.Verify(x => x.Ratings.Add(It.IsAny<Rating>()), Times.Once);
            Assert.AreEqual(1, addedRating.PublicationId);
            Assert.AreEqual("id", addedRating.UserId);
            Assert.AreEqual(5, addedRating.Value);
        }

        [TestMethod]
        public void RatePublicationCallDataSaveChangesWhenRatingNotFound()
        {
            var mockedData = new Mock<IBuilditData>();
            mockedData.Setup(x => x.Ratings.All).Returns(new List<Rating>().AsQueryable());
            mockedData.Setup(x => x.SaveChanges()).Verifiable();

            var service = new RatingsService(mockedData.Object);
            service.RatePublication(1, "id", 5);
            mockedData.Verify(x => x.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void RatePublicationCallDataSaveChangesWhenRatingFound()
        {
            var userId = "id32143532";
            var PublicationId = 1111;
            var mockedData = new Mock<IBuilditData>();

            var rating1 = new Rating() { UserId = userId, PublicationId = PublicationId };
            var ratings = new List<Rating>()
            {
                rating1
            }.AsQueryable();

            mockedData.Setup(x => x.Ratings.All).Returns(ratings);
            mockedData.Setup(x => x.SaveChanges()).Verifiable();

            var service = new RatingsService(mockedData.Object);

            service.RatePublication(PublicationId, userId, 5);
            mockedData.Verify(x => x.SaveChanges(), Times.Once);
        }
    }
}

