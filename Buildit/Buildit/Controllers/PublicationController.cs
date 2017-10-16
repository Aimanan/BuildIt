using System;
using System.Web.Mvc;
using Buildit.Services.Contracts;
using Bytes2you.Validation;
using Buildit.Web.Models.AdditionalInfo;
using Buildit.Common.Providers.Contracts;
using Buildit.Attributes;
using Buildit.Common.Providers;
using Buildit.Web.Models;

namespace Buildit.Controllers
{
    public class PublicationController : Controller
    {
        private readonly IPublicationService publicationService;
        private readonly IRatingsService ratingsService;
        private readonly IMapperAdapter mapper;
        private readonly IUserProvider userProvider;

        public PublicationController(IPublicationService publicationService, IRatingsService ratingsService, IMapperAdapter mapper, IUserProvider userProvider)
        {
            Guard.WhenArgument(publicationService, "PublicationService").IsNull().Throw();
            Guard.WhenArgument(ratingsService, "RatingsService").IsNull().Throw();
            Guard.WhenArgument(userProvider, "UserProvider").IsNull().Throw();
            Guard.WhenArgument(mapper, "Mapper").IsNull().Throw();

            this.publicationService = publicationService;
            this.ratingsService = ratingsService;
            this.mapper = mapper;
            this.userProvider = userProvider;
        }

        //public ActionResult Index(int id)
        //{
        //    var publication = this.publicationService.GetById(id);

        //    if (publication == null)
        //    {
        //        return this.View("publicationError");
        //    }

        //    var publicationViewModel = this.mapper.Map<PublicationDetailsViewModel>(publication);

        //    return this.View(publicationViewModel);
        //}

        public ActionResult Details(int id)
        {
            var model = this.publicationService.GetById(id);

            var viewModel = this.mapper.Map<PublicationViewModel>(model);

            return this.View(viewModel);
        }
    }
}