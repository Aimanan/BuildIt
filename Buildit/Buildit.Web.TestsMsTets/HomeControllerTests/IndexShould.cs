using Buildit.Common.Providers;
using Buildit.Common.Providers.Contracts;
using Buildit.Controllers;
using Buildit.Data.Models;
using Buildit.Services.Contracts;
using Buildit.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.FluentMVCTesting;

namespace Buildit.Web.TestsMsTets.HomeControllerTests
{
    [TestClass]
    public class IndexShould
    {
        [TestMethod]
        public void CallCacheGetValueWithCorrectKey()
        {
            var mockedPublService = new Mock<IPublicationService>();
            var mockedCacheProvider = new Mock<ICacheProvider>();
            var mockedMapper = new Mock<IMapperAdapter>();
            mockedCacheProvider.Setup(x => x.GetValue(It.IsAny<string>())).Verifiable();

            var controller = new HomeController(mockedPublService.Object, mockedCacheProvider.Object, mockedMapper.Object);

            controller.Index();

            mockedCacheProvider.Verify(x => x.GetValue(Constants.TopPublicationsCache), Times.Once);
        }

        [TestMethod]
        public void NotCallPublicationsServiceWhenCacheIsNotNull()
        {
            var mockedPublService = new Mock<IPublicationService>();
            var mockedCacheProvider = new Mock<ICacheProvider>();
            var mockedMapper = new Mock<IMapperAdapter>();
            mockedPublService.Setup(x => x.GetTopPublications(It.IsAny<int>())).Verifiable();
            mockedCacheProvider.Setup(x => x.GetValue(It.IsAny<string>())).Returns(new List<PublicationViewModel>());

            var controller = new HomeController(mockedPublService.Object, mockedCacheProvider.Object, mockedMapper.Object);
            controller.Index();

            mockedPublService.Verify(x => x.GetTopPublications(It.IsAny<int>()), Times.Never);
        }

        [TestMethod]
        public void CallPublicationsServiceHighestRatedPublicationsWhenCacheIsNull()
        {
            var mockedPublService = new Mock<IPublicationService>();
            var mockedCacheProvider = new Mock<ICacheProvider>();
            var mockedMapper = new Mock<IMapperAdapter>();
            mockedCacheProvider.Setup(x => x.GetValue(It.IsAny<string>())).Returns(null);
            mockedPublService.Setup(x => x.GetTopPublications(It.IsAny<int>())).Returns(new List<Publication>());

            var controller = new HomeController(mockedPublService.Object, mockedCacheProvider.Object, mockedMapper.Object); 
            controller.Index();

            mockedPublService.Verify(x => x.GetTopPublications(It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        public void CallPublicationsServiceHighestRatedPublicationsWithCorrectCountWhenCacheIsNull()
        {
            var mockedPublService = new Mock<IPublicationService>();
            var mockedCacheProvider = new Mock<ICacheProvider>();
            var mockedMapper = new Mock<IMapperAdapter>();
            mockedCacheProvider.Setup(x => x.GetValue(It.IsAny<string>())).Returns(null);
            mockedPublService.Setup(x => x.GetTopPublications(It.IsAny<int>())).Returns(new List<Publication>());

            var controller = new HomeController(mockedPublService.Object, mockedCacheProvider.Object, mockedMapper.Object);
            controller.Index();

            mockedPublService.Verify(x => x.GetTopPublications(Constants.TopPublicationsCount), Times.Once);
        }

        [TestMethod]
        public void CallMapperWithCorrectCollectionWhenCacheIsNull()
        {
            var mockedPublService = new Mock<IPublicationService>();
            var mockedCacheProvider = new Mock<ICacheProvider>();
            var mockedMapper = new Mock<IMapperAdapter>();

            mockedCacheProvider.Setup(x => x.GetValue(It.IsAny<string>())).Returns(null);

            var Publications = new List<Publication>();
            mockedPublService.Setup(x => x.GetTopPublications(It.IsAny<int>())).Returns(Publications);
            mockedMapper.Setup(x => x.Map<IEnumerable<PublicationViewModel>>(It.IsAny<IEnumerable<Publication>>())).Verifiable();
            var controller = new HomeController(mockedPublService.Object, mockedCacheProvider.Object, mockedMapper.Object);
            controller.Index();

            mockedMapper.Verify(x => x.Map<IEnumerable<PublicationViewModel>>(Publications), Times.Once);
        }

        [TestMethod]
        public void CallCacheInsertWithCorrectPublicationsWhenCacheIsNull()
        {
            var mockedPublService = new Mock<IPublicationService>();
            var mockedCacheProvider = new Mock<ICacheProvider>();
            var mockedMapper = new Mock<IMapperAdapter>();

            var mappedPublications = new List<PublicationViewModel>();
            mockedMapper.Setup(x => x.Map<IEnumerable<PublicationViewModel>>(It.IsAny<IEnumerable<Publication>>())).Returns(mappedPublications);

            IEnumerable<PublicationViewModel> cachedPublications = null;
            mockedCacheProvider.Setup(x => x.GetValue(It.IsAny<string>())).Returns(null);
            mockedCacheProvider.Setup(x => x.InsertWithAbsoluteExpiration(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<DateTime>()))
                .Callback((string key, object value, DateTime expiration) => cachedPublications = (IEnumerable<PublicationViewModel>)value);

            var controller = new HomeController(mockedPublService.Object, mockedCacheProvider.Object, mockedMapper.Object);
            controller.Index();

            Assert.AreEqual(mappedPublications, cachedPublications);
        }

        [TestMethod]
        public void CallCacheInsertWithCorrectKeyWhenCacheIsNull()
        {
            var mockedPublService = new Mock<IPublicationService>();
            var mockedCacheProvider = new Mock<ICacheProvider>();
            var mockedMapper = new Mock<IMapperAdapter>();

            mockedCacheProvider.Setup(x => x.GetValue(It.IsAny<string>())).Returns(null);
            mockedCacheProvider.Setup(x => x.InsertWithAbsoluteExpiration(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<DateTime>())).Verifiable();
            var controller = new HomeController(mockedPublService.Object, mockedCacheProvider.Object, mockedMapper.Object);
            controller.Index();

            mockedCacheProvider.Verify(x => x.InsertWithAbsoluteExpiration(Constants.TopPublicationsCache, It.IsAny<object>(), It.IsAny<DateTime>()), Times.Once);
        }

        [TestMethod]
        public void ReturnDefaultView()
        {
            var mockedPublService = new Mock<IPublicationService>();
            var mockedCacheProvider = new Mock<ICacheProvider>();
            var mockedMapper = new Mock<IMapperAdapter>();
            var controller = new HomeController(mockedPublService.Object, mockedCacheProvider.Object, mockedMapper.Object);

            controller.WithCallTo(c => c.Index()).ShouldRenderDefaultView();
        }

        [TestMethod]
        public void ReturnViewWithCorrectModelWhenCacheIsNotNull()
        {
            var mockedPublService = new Mock<IPublicationService>();
            var mockedCacheProvider = new Mock<ICacheProvider>();
            var mockedMapper = new Mock<IMapperAdapter>();

            var mappedPublications = new List<PublicationViewModel>();
            mockedCacheProvider.Setup(x => x.GetValue(It.IsAny<string>())).Returns(mappedPublications);

            var controller = new HomeController(mockedPublService.Object, mockedCacheProvider.Object, mockedMapper.Object);

            controller.WithCallTo(c => c.Index()).ShouldRenderDefaultView()
                .WithModel(mappedPublications);
        }
    }
}
