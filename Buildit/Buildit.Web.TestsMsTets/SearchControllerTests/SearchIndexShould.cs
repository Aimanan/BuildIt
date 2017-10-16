using Buildit.Common.Providers;
using Buildit.Controllers;
using Buildit.Data.Models;
using Buildit.Services;
using Buildit.Services.Contracts;
using Buildit.Web.Models.Search;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.FluentMVCTesting;

namespace Buildit.Web.TestsMsTets.SearchControllerTests
{
    [TestClass]
    public class SearchIndexShould
    {
        [TestMethod]
        public void CallPublicationTypesServiceOnces()
        {
            var mockedPublService = new Mock<IPublicationService>();
            var mockedPublicationTypesService = new Mock<IPublicationTypeService>();
            var mockedMapper = new Mock<IMapperAdapter>();
            mockedPublicationTypesService.Setup(x => x.GetPublicationTypes()).Returns(new List<PublicationType>());

            var controller = new SearchController(mockedPublService.Object, mockedPublicationTypesService.Object, mockedMapper.Object);
            controller.Index();

            mockedPublicationTypesService.Verify(x => x.GetPublicationTypes(), Times.Once);
        }

        [TestMethod]
        public void CallMapper()
        {
            var mockedPublService = new Mock<IPublicationService>();
            var mockedPublicationTypesService = new Mock<IPublicationTypeService>();
            var mockedMapper = new Mock<IMapperAdapter>();
            var PublicationTypes = new List<PublicationType>();
            mockedPublicationTypesService.Setup(x => x.GetPublicationTypes()).Returns(PublicationTypes);
            mockedMapper.Setup(x => x.Map<IEnumerable<PublicationTypeViewModel>>(PublicationTypes)).Verifiable();
            var controller = new SearchController(mockedPublService.Object, mockedPublicationTypesService.Object, mockedMapper.Object);
            controller.Index();

            mockedMapper.Verify(x => x.Map<IEnumerable<PublicationTypeViewModel>>(PublicationTypes), Times.Once);
        }

        [TestMethod]
        public void ReturnDefaultView()
        {
            var mockedPublService = new Mock<IPublicationService>();
            var mockedPublicationTypesService = new Mock<IPublicationTypeService>();
            var mockedMapper = new Mock<IMapperAdapter>();
            var controller = new SearchController(mockedPublService.Object, mockedPublicationTypesService.Object, mockedMapper.Object);

            controller.WithCallTo(c => c.Index()).ShouldRenderDefaultView();
        }

        [TestMethod]
        public void GiveViewAModelWithPublicationTypes()
        {
            var mockedPublService = new Mock<IPublicationService>();
            var mockedPublicationTypesService = new Mock<IPublicationTypeService>();
            var mockedMapper = new Mock<IMapperAdapter>();
            var PublicationTypes = new List<PublicationType>();
            var mappedPublicationTypes = new List<PublicationTypeViewModel>();
            mockedPublicationTypesService.Setup(x => x.GetPublicationTypes()).Returns(PublicationTypes);
            mockedMapper.Setup(x => x.Map<IEnumerable<PublicationTypeViewModel>>(PublicationTypes)).Returns(mappedPublicationTypes);

            var controller = new SearchController(mockedPublService.Object, mockedPublicationTypesService.Object, mockedMapper.Object);

            controller.WithCallTo(c => c.Index()).ShouldRenderDefaultView()
                .WithModel<SearchViewModel>(m => m.PublicationTypes == mappedPublicationTypes);
        }
    }
}
