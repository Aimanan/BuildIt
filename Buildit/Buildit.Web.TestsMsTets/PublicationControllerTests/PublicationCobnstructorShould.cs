using Buildit.Common.Providers;
using Buildit.Common.Providers.Contracts;
using Buildit.Controllers;
using Buildit.Services.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buildit.Web.TestsMsTets.PublicationControllerTests
{
    [TestClass]
    public class PublicationCobnstructorShould
    {
        [TestMethod]
        public void ThrowArgumentNullExceptionWithhenIsNull()
        {
            var mockedRatingsService = new Mock<IRatingsService>();
            var mockedMapper = new Mock<IMapperAdapter>();
            var mockedUserProvider = new Mock<IUserProvider>();

            Assert.ThrowsException<ArgumentNullException>(() => new PublicationController(null, mockedRatingsService.Object, mockedMapper.Object, mockedUserProvider.Object));
        }
    

        [TestMethod]
        public void ThrowArgumentNullExceptionWithhenIsNull2()
        {
            var mockedPublService = new Mock<IPublicationService>();
            var mockedMapper = new Mock<IMapperAdapter>();
            var mockedUserProvider = new Mock<IUserProvider>();

            Assert.ThrowsException<ArgumentNullException>(() => new PublicationController(mockedPublService.Object, null,
                mockedMapper.Object, mockedUserProvider.Object));
        }

        [TestMethod]
        public void ThrowArgumentNullExceptionWithhenIsNull3()
        {
            var mockedPublService = new Mock<IPublicationService>();
            var mockedRatingsService = new Mock<IRatingsService>();
            var mockedUserProvider = new Mock<IUserProvider>();

            Assert.ThrowsException<ArgumentNullException>(() => new PublicationController(mockedPublService.Object, mockedRatingsService.Object,
                 null, mockedUserProvider.Object));
        }

        [TestMethod]
        public void ThrowArgumentNullExceptionWithhenIsNull4()
        {
            var mockedPublService = new Mock<IPublicationService>();
            var mockedRatingsService = new Mock<IRatingsService>();
            var mockedMapper = new Mock<IMapperAdapter>();

            Assert.ThrowsException<ArgumentNullException>(() => new PublicationController(mockedPublService.Object, mockedRatingsService.Object,
                 mockedMapper.Object, null));
        }

        [TestMethod]
        public void NotThrow_WhenArgumentsAreNotNull()
        {
            var mockedPublService = new Mock<IPublicationService>();
            var mockedRatingsService = new Mock<IRatingsService>();
            var mockedMapper = new Mock<IMapperAdapter>();
            var mockedUserProvider = new Mock<IUserProvider>();

            var homeController = new PublicationController(mockedPublService.Object, mockedRatingsService.Object,
                 mockedMapper.Object, mockedUserProvider.Object);
            Assert.IsNotNull(homeController);

        }
    }
}
