using Buildit.Common.Providers;
using Buildit.Common.Providers.Contracts;
using Buildit.Controllers;
using Buildit.Data.Models;
using Buildit.Services.Contracts;
using Buildit.Web.Models.AdditionalInfo;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.FluentMVCTesting;

namespace Buildit.Web.TestMethodsMsTets.PublicationControllerTestMethods
{
    [TestClass]
    public class PublicationIndexShould
    {
        [TestMethod]
        public void CallPublicationsServiceGetById()
        {
            var mockedPublsService = new Mock<IPublicationService>();
            var mockedRatingsService = new Mock<IRatingsService>();
            var mockedMapper = new Mock<IMapperAdapter>();
            var mockedUserProvider = new Mock<IUserProvider>();
            mockedPublsService.Setup(x => x.GetById(It.IsAny<int>())).Verifiable();

            var controller = new PublicationController(mockedPublsService.Object, mockedRatingsService.Object, mockedMapper.Object, mockedUserProvider.Object);

            controller.Details(5);

            mockedPublsService.Verify(x => x.GetById(It.IsAny<int>()), Times.Once);
        }

        [DataRow(3)]
        [DataRow(2233)]
        public void CallPublicationsServiceGetByIdWithCorrectId(int id)
        {
            var mockedPublsService = new Mock<IPublicationService>();
            var mockedRatingsService = new Mock<IRatingsService>();
            var mockedMapper = new Mock<IMapperAdapter>();
            var mockedUserProvider = new Mock<IUserProvider>();
            mockedPublsService.Setup(x => x.GetById(It.IsAny<int>())).Verifiable();

            var controller = new PublicationController(mockedPublsService.Object, mockedRatingsService.Object, mockedMapper.Object, mockedUserProvider.Object);

            controller.Details(id);

            mockedPublsService.Verify(x => x.GetById(id), Times.Once);
        }

        [TestMethod]
        public void ReturnCorrectView_WhenPublicationIsNotNull()
        {
            var mockedPublsService = new Mock<IPublicationService>();
            var mockedRatingsService = new Mock<IRatingsService>();
            var mockedMapper = new Mock<IMapperAdapter>();
            var mockedUserProvider = new Mock<IUserProvider>();
            mockedPublsService.Setup(x => x.GetById(It.IsAny<int>())).Returns(new Mock<Publication>().Object);

            var controller = new PublicationController(mockedPublsService.Object, mockedRatingsService.Object, mockedMapper.Object, mockedUserProvider.Object);
            controller.WithCallTo(c => c.Details(5)).ShouldRenderDefaultView();
        }
    }
}
