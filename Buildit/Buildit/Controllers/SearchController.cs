using Buildit.Attributes;
using Buildit.Common.Providers;
using Buildit.Services;
using Buildit.Services.Contracts;
using Buildit.Web.Models;
using Buildit.Web.Models.Search;
using Bytes2you.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Buildit.Controllers
{
    public class SearchController : Controller
    {
        private readonly IPublicationService publicationService;
        private readonly IPublicatioTypeService publTypeService;
        private readonly IMapperAdapter mapper;

        public SearchController(IPublicationService publicationService,
            IPublicatioTypeService publTypeService,
            IMapperAdapter mapper)
        {
            Guard.WhenArgument(publicationService, "PublicationService").IsNull().Throw();
            Guard.WhenArgument(publTypeService, "PublicationTypeService").IsNull().Throw();
            Guard.WhenArgument(mapper, "Mapper").IsNull().Throw();

            this.publicationService = publicationService;
            this.publTypeService = publTypeService;
            this.mapper = mapper;
        }

        public ActionResult Index()
        {

            var model = new SearchViewModel();
            var publType = this.publTypeService.GetPublicationTypes();
            model.PublicationTypes = this.mapper.Map<IEnumerable<PublicationTypeViewModel>>(publType);

            return this.View(model);
        }

        [ChildActionOnly]
        public PartialViewResult SearchInitial()
        {
            int page = 1;

            return this.ExecuteSearch(new SearchViewResultModel(), page);
        }

        [HttpPost]
        [AjaxOnly]
        public PartialViewResult SearchBooks(SearchViewResultModel searchModel, int? page)
        {
            int actualPage = page ?? 1;

            return this.ExecuteSearch(searchModel, actualPage);
        }

        private PartialViewResult ExecuteSearch(SearchViewResultModel searchModel, int page)
        {
            var result = this.publicationService.SearchPublications(searchModel.SearchWord, 
                searchModel.ChosenPublicationTypesIds, 
                searchModel.SortBy,
                page, 
                Constants.PublicationsPerPage);
            var count = this.publicationService.GetPublicationsCount(searchModel.SearchWord, 
                searchModel.ChosenPublicationTypesIds);

            var resultViewModel = new SearchResultsViewModel();
            resultViewModel.PublicationsCount = count;
            resultViewModel.SearchModel = searchModel;
            resultViewModel.Pages = (int)Math.Ceiling((double)count / Constants.PublicationsPerPage);
            resultViewModel.Page = page;
            resultViewModel.Publications = this.mapper.Map<IEnumerable<PublicationViewModel>>(result);

            return this.PartialView("_ResultsPartial", resultViewModel);
        }
    }
}