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

namespace Buildit.Web.TestsMsTets.HomeControllerTests
{
    [TestClass]
    public class ConstructorShould
    {
        [TestMethod]
        public void ThrowArgumentNullExceptionWithCorrectMessageWhenPublicationsServiceIsNull()
        {
            var mockedCacheProvider = new Mock<ICacheProvider>();
            var mockedMapper = new Mock<IMapperAdapter>();
            //var homeController = new HomeController(null, mockedCacheProvider.Object, mockedMapper.Object);
            Assert.ThrowsException<ArgumentNullException>(() => new HomeController(null, mockedCacheProvider.Object, mockedMapper.Object));
        }

        [TestMethod]
        public void ThrowArgumentNullExceptionWithCorrectMessage_WhenCacheProviderIsNull()
        {
            var mockedPublService = new Mock<IPublicationService>();
            var mockedMapper = new Mock<IMapperAdapter>();

            Assert.ThrowsException<ArgumentNullException>(() => new HomeController(mockedPublService.Object,null, mockedMapper.Object));
        }

        [TestMethod]
        public void ThrowArgumentNullExceptionWithCorrectMessageWhenMapperIsNull()
        {
            var mockedPublService = new Mock<IPublicationService>();
            var mockedCacheProvider = new Mock<ICacheProvider>();

            Assert.ThrowsException<ArgumentNullException>(() => new HomeController(mockedPublService.Object, mockedCacheProvider.Object, null));
        }

        [TestMethod]
        public void NotThrowWhenArgumentsAreNotNull()
        {
            var mockedPublicationsService = new Mock<IPublicationService>();
            var mockedCacheProvider = new Mock<ICacheProvider>();
            var mockedMapper = new Mock<IMapperAdapter>();
            var homeController = new HomeController(mockedPublicationsService.Object, mockedCacheProvider.Object, mockedMapper.Object);
            Assert.IsNotNull(homeController);
        }
    }
}
