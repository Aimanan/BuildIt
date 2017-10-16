using Buildit.Attributes;
using Buildit.Common.Providers;
using Buildit.Controllers;
using Buildit.Data.Models;
using Buildit.Services;
using Buildit.Services.Contracts;
using Buildit.Web.Models;
using Buildit.Web.Models.Search;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using TestStack.FluentMVCTesting;

namespace Buildit.Web.TestMethodsMsTets.SearchControllerTestMethods
{

    [TestClass]
    public class SearchPublicationShould
    {
        [TestMethod]
        public void CallPublicationsServiceSearchPublicationsWithCorrectParams()
        {
            var searchWord = "word";
            var sortBy = "sorting";
            var ChosenTypesId = new List<int> { 2, 6 };
            var page = 15;

            var mockedPublicationsService = new Mock<IPublicationService>();
            var mockedTypesService = new Mock<IPublicationTypeService>();
            var mockedMapper = new Mock<IMapperAdapter>();
            var searchResModel = new SearchViewResultModel()
            {
                SearchWord = searchWord,
                SortBy = sortBy,
                ChosenPublicationTypesIds = ChosenTypesId
            };

            mockedPublicationsService.Setup(x => x.SearchPublications(searchWord, ChosenTypesId, sortBy, page, Constants.PublicationsPerPage)).Verifiable();

            var controller = new SearchController(mockedPublicationsService.Object, mockedTypesService.Object, mockedMapper.Object);
            controller.SearchPublications(searchResModel, page);

            mockedPublicationsService.Verify(x => x.SearchPublications(searchWord, ChosenTypesId, sortBy, page, Constants.PublicationsPerPage), Times.Once);
        }

        [TestMethod]
        public void ReturnCorrectPartialView()
        {
            var mockedPublicationsService = new Mock<IPublicationService>();
            var mockedTypesService = new Mock<IPublicationTypeService>();
            var mockedMapper = new Mock<IMapperAdapter>();
            var controller = new SearchController(mockedPublicationsService.Object, mockedTypesService.Object, mockedMapper.Object);

            controller.WithCallTo(c => c.SearchPublications(new SearchViewResultModel(), null))
                .ShouldRenderPartialView("_ResultsPartial");
        }


        [TestMethod]
        public void ReturnViewModelWithCorrectsearchResModel()
        {
            var mockedPublicationsService = new Mock<IPublicationService>();
            var mockedTypesService = new Mock<IPublicationTypeService>();
            var mockedMapper = new Mock<IMapperAdapter>();
            var searchResModel = new SearchViewResultModel();

            var controller = new SearchController(mockedPublicationsService.Object, mockedTypesService.Object, mockedMapper.Object);

            controller.WithCallTo(c => c.SearchPublications(searchResModel, null))
                .ShouldRenderPartialView("_ResultsPartial")
                .WithModel<SearchResultsViewModel>(x => x.SearchModel == searchResModel);
        }

        [TestMethod]
        public void ReturnViewModelWithCorrectPublications()
        {
            var mockedPublicationsService = new Mock<IPublicationService>();
            var mockedTypesService = new Mock<IPublicationTypeService>();
            var mockedMapper = new Mock<IMapperAdapter>();
            var mappedPublications = new List<PublicationViewModel>();
            mockedMapper.Setup(x => x.Map<IEnumerable<PublicationViewModel>>(It.IsAny<IEnumerable<Publication>>()))
                .Returns(mappedPublications);

            var controller = new SearchController(mockedPublicationsService.Object, mockedTypesService.Object, mockedMapper.Object);

            controller.WithCallTo(c => c.SearchPublications(new SearchViewResultModel(), null))
                .ShouldRenderPartialView("_ResultsPartial")
                .WithModel<SearchResultsViewModel>(x => x.Publications == mappedPublications);
        }
    }
}

