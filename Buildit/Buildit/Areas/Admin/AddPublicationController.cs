using Buildit.Common.Providers;
using Buildit.Common.Providers.Contracts;
using Buildit.Services;
using Buildit.Services.Contracts;
using Buildit.Web.Models;
using Buildit.Web.Models.Admin;
using Bytes2you.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Buildit.Areas.Admin
{
    [Authorize(Roles = Constants.AdminRole)]
    public class AddPublicationController : Controller
    {
        private readonly IUserProvider userProvider;
        private readonly IServerProvider serverProvider;
        private readonly ICacheProvider cacheProvider;
        private readonly IPublicationService publicationService;
        private readonly IPublicatioTypeService publicationTypeService;
        private readonly IMapperAdapter mapper;

        public AddPublicationController(
            IUserProvider userProvider,
            IServerProvider serverProvider,
            ICacheProvider cacheProvider,
            IPublicationService publicationService,
            IPublicatioTypeService publicationTypeService,
            IMapperAdapter mapper)
        {
            Guard.WhenArgument(userProvider, "UserProvider").IsNull().Throw();
            Guard.WhenArgument(serverProvider, "ServerProvider").IsNull().Throw();
            Guard.WhenArgument(cacheProvider, "CacheProvider").IsNull().Throw();
            Guard.WhenArgument(publicationService, "PublicationService").IsNull().Throw();
            Guard.WhenArgument(publicationTypeService, "PublicationTypeService").IsNull().Throw();
            Guard.WhenArgument(mapper, "Mapper").IsNull().Throw();

            this.userProvider = userProvider;
            this.serverProvider = serverProvider;
            this.cacheProvider = cacheProvider;
            this.publicationService = publicationService;
            this.publicationTypeService = publicationTypeService;
            this.mapper = mapper;
        }

        public ActionResult Index()
        {
            var model = new AddPublicationViewModel();
            model.PublicationTypes = this.GetPublicationTypes();
            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Index([Bind(Exclude = "Genres")]AddPublicationViewModel publModel)
        {
            if (!ModelState.IsValid)
            {
                publModel.PublicationTypes = this.GetPublicationTypes();
                return this.View(publModel);
            }

            if (!this.IsImageFile(publModel.Picture))
            {
                this.ModelState.AddModelError("CoverFile", Constants.PictureErrorMessage);
                publModel.PublicationTypes = this.GetPublicationTypes();
                return this.View(publModel);
            }

            if (this.publicationService.PublicationFound(publModel.Title))
            {
                this.ModelState.AddModelError("Title", Constants.TitleExistsErrorMessage);
                publModel.PublicationTypes = this.GetPublicationTypes();
                return this.View(publModel);
            }

            var filename = publModel.Picture.FileName;
            var path = this.serverProvider.MapPath(Constants.ImagesRelativePath + filename);
            publModel.Picture.SaveAs(path);

            var bookModel = this.mapper.Map<PublicationViewModelAdmin>(publModel);
            var bookId = this.publicationService.AddPublication(bookModel, filename);

            this.TempData.Add(Constants.AddPublicationSuccessKey, Constants.AddPublicationSuccessMessage);
            return this.Redirect($"/book/{bookId}");
        }

        private bool IsImageFile(HttpPostedFileBase file)
        {
            if (file == null)
            {
                return false;
            }

            var contentType = file.ContentType.ToLower();
            var result = contentType == "image/jpg"
                || contentType == "image/jpeg"
                || contentType == "image/png";

            return result;
        }

        private IEnumerable<SelectListItem> GetPublicationTypes()
        {
            IEnumerable<SelectListItem> genres;
            var cachedGenres = this.cacheProvider.GetValue(Constants.PublicationTypeCache);
            if (cachedGenres != null)
            {
                genres = (IEnumerable<SelectListItem>)cachedGenres;
            }
            else
            {
                genres = this.publicationTypeService.GetPublicationTypes().Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name }).ToList();

                // maxvalue because api
                this.cacheProvider.InsertWithSlidingExpiration(Constants.PublicationTypeCache, genres, Constants.PublicationTypeExpirationInMinutes);
            }

            return genres;
        }
    }
}