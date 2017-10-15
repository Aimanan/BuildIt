using Buildit.Common.Providers;
using Buildit.Common.Providers.Contracts;
using Buildit.Services.Contracts;
using Buildit.Web.Models;
using Bytes2you.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Buildit.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPublicationService publicationService;
        private readonly ICacheProvider cacheProvider;
        private readonly IMapperAdapter mapper;

        public HomeController(IPublicationService publicationService,
            ICacheProvider cacheProvider,
            IMapperAdapter mapper)
        {
            Guard.WhenArgument(publicationService, "PublicationService").IsNull().Throw();
            Guard.WhenArgument(cacheProvider, "CacheProvider").IsNull().Throw();
            Guard.WhenArgument(mapper, "Mapper").IsNull().Throw();

            this.publicationService = publicationService;
            this.cacheProvider = cacheProvider;
            this.mapper = mapper;
        }

        public ActionResult Index()
        {
            var topPublications = (IEnumerable<PublicationViewModel>)this.cacheProvider.GetValue(Constants.TopPublicationsCache);

            if (topPublications == null)
            {
                var publications = this.publicationService.GetTopPublications(Constants.TopPublicationsCount).ToList();
                topPublications = this.mapper.Map<IEnumerable<PublicationViewModel>>(publications);

                this.cacheProvider.InsertWithAbsoluteExpiration(Constants.TopPublicationsCache, 
                    topPublications,
                    DateTime.UtcNow.AddMinutes(Constants.TopPublicationsExpirationInMinutes));
            }

            return this.View(topPublications);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact me :P";

            return View();
        }
    }
}